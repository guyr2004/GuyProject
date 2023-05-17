using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GuyProject.App_Code;
using System.Data;
using System.Data.OleDb;
using System.Globalization;

namespace GuyProject
{
    public partial class SetLesson : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Session["studentID"] == null)
            {
                Session["page"] = "SetLesson.aspx";
                Response.Redirect("Login.aspx");
            }
            Session["teacherID"] = (string)Session["teacherID"];
            //Session["teacherID"] = "214777286";
            if (!Page.IsPostBack)
            {
                ShowTeacher();
                this.LabelSubjectLevel.Visible = false;
                this.LabelDate.Visible = false;
                this.LabelShowHourLesson.Visible = false;
                this.LabelPricePerHour.Visible = false;
                this.LabelMessageSetLesson.Visible = false;
            }
        }

        public string GetDayByNum(string num)
        {
            if (num == "1")
                return "Sunday";
            if (num == "2")
                return "Monday";
            if (num == "3")
                return "Tuesday";
            if (num == "4")
                return "wednesday";
            if (num == "5")
                return "Thursday";
            if (num == "6")
                return "Friday";
            if (num == "7")
                return "Saturday";
            return "יום לא חוקי";
        }
        protected void ShowTeacher()
        {
            TeacherDetails teacherDetails = new TeacherDetails();
            TeacherService teacherService = new TeacherService();
            UserService userService = new UserService();
            UserDetails userDetails = new UserDetails();
            string teacherID = (string)Session["teacherID"];
            teacherDetails = teacherService.GetTeacherByTeacherID(teacherID);
            userDetails = userService.GetUserByUserID(teacherID);
            this.LabelFullName.Text = " " + userDetails.FirstName + " " + userDetails.LastName;
            this.LabelPhoneNumber.Text = userDetails.Phone;
            this.LabelLearnPlace.Text = teacherDetails.LearnPlace;
            this.LabelDescription.Text = teacherDetails.Description;
            this.ImageTeacher.ImageUrl = teacherDetails.ImageTeacher;
            this.LabelShowTeacherPhoneNumber.Text = userDetails.Phone;
            if (this.LabelLearnPlace.Text == "בתיאום עם התלמיד" || this.LabelLearnPlace.Text == "מבית המורה" || this.LabelLearnPlace.Text == "מרחוק" || this.LabelLearnPlace.Text == "גמיש")
            {
                this.TextBoxShowLessonPlace.Visible = false;
                this.LabelShowLessonPlace.Text = this.LabelLearnPlace.Text;
            }
            if (this.LabelLearnPlace.Text == "מבית התלמיד")
            {
                this.LabelShowLessonPlace.Visible = false;
                this.LabelLearnPlace.Text = "הכנס את הכתובת שלך שבה תבצעו את השיעור";
            }
            this.GridViewSubjectsAndLevels.DataSource = teacherService.GetTeacherWithSubjects(teacherID);
            this.GridViewSubjectsAndLevels.DataBind();
        }
        public static DateTime ParseDateString(string dateString)
        {
            string[] formatStrings = new string[]
            {
        "dd/MM/yyyy HH:mm:ss",
        "dd/MM/yyyy H:mm:ss",
        "dd/MM/yyyy HH:mm",
        "dd/MM/yyyy H:mm",
        "dd/MM/yyyy"
            };

            foreach (string formatString in formatStrings)
            {
                if (DateTime.TryParseExact(dateString, formatString, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
                {
                    return dateTime;
                }
            }

            throw new ArgumentException("Invalid date string format.");
        }
        public DateTime ConvertStringToDateTime(string timeString)
        {
            DateTime result;
            if (DateTime.TryParseExact(timeString, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
            {
                return result;
            }
            else
            {
                throw new FormatException("Invalid time string format.");
            }
        }
        protected void Populate_DropDownListHours(string teacherID, DateTime dateTime)
        {
            WorkingService workingService = new WorkingService();
            AbsenceTeacherService absenceTeacherService = new AbsenceTeacherService();
            string day = dateTime.DayOfWeek.ToString();
            DataSet datasetworkinghours = workingService.GetAllHoursWorkingByDay(teacherID, day);
            DataTable dataTableWorkingHours = datasetworkinghours.Tables["HoursWorkingInDayTbl"];
            TimeSpan starthour = new TimeSpan();
            TimeSpan endhour = new TimeSpan();
            if (datasetworkinghours != null && dataTableWorkingHours != null)
            {
                foreach (DataRow row in dataTableWorkingHours.Rows)
                {
                    endhour = ParseDateString(row["Endhour"].ToString()).TimeOfDay;
                    starthour = ParseDateString(row["Starthour"].ToString()).TimeOfDay;
                    for (TimeSpan hour = starthour; hour < endhour; hour += TimeSpan.FromHours(1))
                    {
                        string timeRange = hour.ToString(@"hh\:mm") + "-" + hour.Add(TimeSpan.FromHours(1)).ToString(@"hh\:mm");
                        DropDownListHours.Items.Add(timeRange);
                        DropDownListHours.SelectedValue = timeRange;
                    }
                }
                DataSet dataSetAbsenceTeacher = absenceTeacherService.GetAllAbsenceTeacherByTeacherIDAndDate(teacherID, dateTime);
                DataTable dataTableAbsence = dataSetAbsenceTeacher.Tables["AbsenceTeacherTbl"];
                if (dataSetAbsenceTeacher != null && dataTableAbsence != null)
                {
                    foreach (DataRow row in dataTableAbsence.Rows)
                    {
                        starthour = ParseDateString(row["Starthour"].ToString()).TimeOfDay;
                        endhour = ParseDateString(row["Endhour"].ToString()).TimeOfDay;
                        for (TimeSpan hour = starthour; hour < endhour; hour += TimeSpan.FromHours(1))
                        {
                            string timeRange = hour.ToString(@"hh\:mm") + "-" + hour.Add(TimeSpan.FromHours(1)).ToString(@"hh\:mm");
                            DropDownListHours.Items.Remove(timeRange);
                        }
                    }
                }
                LessonService lessonService = new LessonService();
                LessonsDetails lessonsDetails = new LessonsDetails();
                lessonsDetails.TeacherID = teacherID;
                lessonsDetails.LessonDate = dateTime;
                DataTable dataTableLessonTeacher = lessonService.GetAllLessonByTeacherIDAndDate(lessonsDetails).Tables["LessonsTeacher"];
                if (dataTableLessonTeacher != null)
                {
                    foreach (DataRow row in dataTableLessonTeacher.Rows)
                    {
                        starthour = ParseDateString(row["Starthour"].ToString()).TimeOfDay;
                        if (starthour.ToString() == DateTime.Today.TimeOfDay.TotalHours.ToString())
                        {
                            endhour = ConvertStringToDateTime("01:00").TimeOfDay;
                        }
                        else
                        {
                            endhour = starthour.Add(TimeSpan.FromHours(1));
                        }
                        string timeRange = starthour.ToString(@"hh\:mm") + "-" + endhour.ToString(@"hh\:mm");
                        DropDownListHours.Items.Remove(timeRange);
                    }
                }
            }
            else
            {
                this.LabelTeacherHours.Text = "המורה לא עובד ביום הזה";
            }
        }
        protected void CalendarLessons_SelectionChanged(object sender, EventArgs e)
        {
            string teacherID = (string)Session["teacherID"];
            Populate_DropDownListHours(teacherID, this.CalendarLessons.SelectedDate);
            this.LabelDate.Visible = true;
            this.LabelDate.Text = this.CalendarLessons.SelectedDate.Date.ToString();
        }
        protected void GridViewSubjectsAndLevels_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ShowSubjectLevel")
            {
                object row = e.CommandArgument;
                int rowNumber = int.Parse(row.ToString());
                this.LabelSubjectLevel.Visible = true;
                Session["subjectName"] = ((GridView)sender).Rows[rowNumber].Cells[1].Text;
                Session["levelID"] = ((GridView)sender).Rows[rowNumber].Cells[2].Text;
                Session["priceperhour"] = ((GridView)sender).Rows[rowNumber].Cells[3].Text;
                this.LabelSubjectLevel.Text = ((GridView)sender).Rows[rowNumber].Cells[1].Text + " "
                    + ((GridView)sender).Rows[rowNumber].Cells[2].Text;
                this.LabelPricePerHour.Visible = true;
                this.LabelPricePerHour.Text = ((GridView)sender).Rows[rowNumber].Cells[3].Text + " שקלים";
            }
        }
        protected void DropDownListHours_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.LabelShowHourLesson.Visible = true;
            this.LabelShowHourLesson.Text = this.DropDownListHours.Text;
        }
        protected void ButtonSetLesson_Click(object sender, EventArgs e)
        {
            try
            {
                LessonsDetails lessonsDetails = new LessonsDetails();
                LessonService lessonService = new LessonService();
                SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
                lessonsDetails.TeacherID = (string)Session["teacherID"];
                lessonsDetails.StudentID = (string)Session["studentID"];
                lessonsDetails.LessonDate = DateTime.ParseExact(this.LabelDate.Text, "dd/MM/yyyy H:mm:ss", CultureInfo.InvariantCulture).Date;
                string hour = this.LabelShowHourLesson.Text;
                string[] hours = hour.Split('-');
                string startHour = hours[0];
                lessonsDetails.StartHour = TimeSpan.ParseExact(startHour, @"hh\:mm", CultureInfo.InvariantCulture);
                lessonsDetails.SubjectID = subjectsLevelsService.GetSubjectIDBySubjectName((string)Session["subjectName"]);
                lessonsDetails.LevelID = subjectsLevelsService.GetLevelIDByLevelName((string)Session["levelID"]);
                lessonsDetails.PaymentStatus = "לא שולם";
                if (this.LabelShowLessonPlace.Visible == true)
                    lessonsDetails.Address = this.LabelShowLessonPlace.Text;
                else
                    lessonsDetails.Address = this.TextBoxShowLessonPlace.Text;
                lessonsDetails.Status = "נקבע";
                lessonsDetails.PricePerHour = int.Parse((string)Session["priceperhour"]);
                if (lessonsDetails.LessonDate < DateTime.Now)
                {
                    this.LabelMessageSetLesson.Visible = true;
                    this.LabelMessageSetLesson.Text = "לא ניתם לקבוע שיעור לזמן שעבר";
                }
                else
                {
                    if (lessonsDetails != null)
                    {
                        if (lessonService.GetLesson(lessonsDetails.LessonDate, lessonsDetails.StartHour, lessonsDetails.TeacherID, lessonsDetails.StudentID) != null)
                        {
                            lessonService.InsertNewLesson(lessonsDetails);
                        }
                        this.DropDownListHours.Items.Remove(this.DropDownListHours.SelectedItem);
                        this.LabelMessageSetLesson.Visible = true;
                        this.LabelMessageSetLesson.Text = "קביעת השיעור עברה בהצלחה";
                    }
                    else
                    {
                        this.LabelMessageSetLesson.Visible = true;
                        this.LabelMessageSetLesson.Text = "קיימת בעיה - השיעור לא נקבע";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}