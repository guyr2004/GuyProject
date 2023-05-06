using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using GuyProject.App_Code;
using System.Globalization;

namespace GuyProject
{
    public partial class UserLessons : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["studentID"] != null)
                Session["userID"] = Session["studentID"];
            if (Session["teacherID"] != null)
                Session["userID"] = Session["teacherID"];
            if (Session["userID"] == null)
            {
                Session["page"] = "UserLessons.aspx";
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                Populate_GridViewShowLessons();
                this.LabelDeleteMessage.Visible = false;
            }
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
        protected DataTable GetData()
        {
            UserService userService = new UserService();
            SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
            LessonService lessonService = new LessonService();
            UserDetails studentDetails = new UserDetails();
            UserDetails TeacherDetails = new UserDetails();
            DataSet dataSetUserLessons = lessonService.GetAllLessonsByUserID((string)Session["userID"]);
            DataTable dataTableUserLessons = dataSetUserLessons.Tables["UserLessons"];
            dataTableUserLessons.Columns.Add("TeacherName");
            dataTableUserLessons.Columns.Add("StudentName");
            dataTableUserLessons.Columns.Add("TeacherPhone");
            dataTableUserLessons.Columns.Add("StudentPhone");
            dataTableUserLessons.Columns.Add("SubjectName");
            dataTableUserLessons.Columns.Add("Price");
            foreach (DataRow row in dataTableUserLessons.Rows)
            {
                studentDetails = userService.GetUserByUserID(row["StudentID"].ToString());
                row["StudentName"] = studentDetails.FirstName + " " + studentDetails.LastName;
                row["StudentPhone"] = studentDetails.Phone;
                TeacherDetails = userService.GetUserByUserID(row["TeacherID"].ToString());
                row["TeacherName"] = TeacherDetails.FirstName + " " + TeacherDetails.LastName;
                row["TeacherPhone"] = TeacherDetails.Phone;
                string subjectName = subjectsLevelsService.GetSubjectNameBySubjectID(int.Parse(row["SubjectID"].ToString()));
                string levelName = subjectsLevelsService.GetLevelNameByLevelID(int.Parse(row["LevelID"].ToString()));
                row["SubjectName"] = subjectName + " " + levelName;
                row["Price"] = " שקלים " + row["PricePerHour"].ToString();
            }
            return dataTableUserLessons;
        }
        protected void Populate_GridViewShowLessons()
        {
            this.GridViewShowLessons.DataSource = GetData();
            this.GridViewShowLessons.DataBind();
        }
        protected void GridViewShowLessons_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dataTable = GetData();
            try
            {
                LessonsDetails lessonsDetails = new LessonsDetails();
                LessonService lessonService = new LessonService();
                int rowselected = e.RowIndex;
                DateTime dateNow = DateTime.Now;
                string dateoflesson = dataTable.Rows[rowselected]["LessonDate"].ToString();
                DateTime dateLesson = DateTime.ParseExact(dateoflesson, "dd/MM/yyyy H:mm:ss", CultureInfo.InvariantCulture).Date;
                int difference = dateLesson.Subtract(dateNow).Days;
                if (difference > 3)//בודק אם תאריך השיעור הוא לא בתווך 3 הימים הקרובים
                {
                    lessonsDetails.TeacherID = dataTable.Rows[rowselected]["TeacherID"].ToString();
                    lessonsDetails.StudentID = dataTable.Rows[rowselected]["StudentID"].ToString();
                    lessonsDetails.LessonDate = dateLesson;
                    lessonsDetails.StartHour = DateTime.ParseExact(dataTable.Rows[rowselected]["StartHour"].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay;
                    lessonService.DeleteLesson(lessonsDetails);
                    this.LabelDeleteMessage.Visible = true;
                    this.LabelDeleteMessage.Text = "מחיקת השיעור עברה בהצלחה";
                    Populate_GridViewShowLessons();
                }
                else
                {
                    this.LabelDeleteMessage.Visible = true;
                    this.LabelDeleteMessage.Text = "לא ניתן לבטל דרך האתר שיעור 3 ימים לפני רק בתיאום עם המורה בטלפון";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ButtonDeleteLastLessons_Click(object sender, EventArgs e)
        {
            DataTable dataTable = GetData();
            try
            {
                LessonsDetails lessonsDetails = new LessonsDetails();
                LessonService lessonService = new LessonService();
                foreach (DataRow row in dataTable.Rows)
                {
                    lessonsDetails.LessonDate = DateTime.ParseExact(row["LessonDate"].ToString(), "dd/MM/yyyy H:mm:ss", CultureInfo.InvariantCulture).Date;
                    DateTime date = DateTime.Today;
                    if (lessonsDetails.LessonDate < date)
                    {
                        lessonsDetails.TeacherID = row["TeacherID"].ToString();
                        lessonsDetails.StudentID = row["StudentID"].ToString();
                        lessonsDetails.StartHour = DateTime.ParseExact(row["StartHour"].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay;
                        lessonService.DeleteLesson(lessonsDetails);
                    }
                }
                Populate_GridViewShowLessons();
                this.LabelDeleteMessage.Visible = true;
                this.LabelDeleteMessage.Text = "מחיקת השיעורים הקודמים עברה בהצלחה";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}