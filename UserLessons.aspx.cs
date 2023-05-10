using System;
using System.Collections;
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
        LessonsList lessonsList = new LessonsList();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Session["studentID"] != null && !Page.IsPostBack)
            {
                Session["userID"] = Session["studentID"];
                this.DropDownListTeachers.Visible = true;
                Populate_DropDownListTeachers();
                this.DropDownListStudents.Visible = false;
            }
            if (Session["teacherID"] != null && !Page.IsPostBack)
            {
                Session["userID"] = Session["teacherID"];
                this.DropDownListStudents.Visible = true;
                Populate_DropDownListStudents();
                this.DropDownListTeachers.Visible = false;
            }
            if (Session["teacherID"] != null && Session["studentID"] != null && !Page.IsPostBack)
            {
                this.DropDownListTeachers.Visible = true;
                this.DropDownListStudents.Visible = true;
                Populate_DropDownListStudents();
                Populate_DropDownListTeachers();
            }
            if (Session["userID"] == null)
            {
                Session["page"] = "UserLessons.aspx";
                Response.Redirect("Login.aspx");
            }
            LessonService lessonService = new LessonService();
            DataSet dataSetUserLessons = lessonService.GetAllLessonsByUserID((string)Session["userID"]);
            Session["dataSetLessons"] = dataSetUserLessons;
            if (!Page.IsPostBack)
            {
                Populate_GridViewShowLessons();
                this.LabelDeleteMessage.Visible = false;
            }
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
        private void Populate_DropDownListTeachers()
        {
            LessonService lessonService = new LessonService();
            DataSet dataSetTeachersName = new DataSet();
            dataSetTeachersName = lessonService.GetAllTeachersNameByStudentID((string)Session["studentID"]);
            this.DropDownListTeachers.DataSource = dataSetTeachersName.Tables["TeachersName"];
            this.DropDownListTeachers.DataTextField = "Name";
            this.DropDownListTeachers.DataValueField = "Name";
            this.DropDownListTeachers.DataBind();
            this.DropDownListTeachers.Items.Insert(0, new ListItem("בחר מורה", "בחר מורה"));
        }
        private void Populate_DropDownListStudents()
        {
            LessonService lessonService = new LessonService();
            DataSet dataSetStudentsName = new DataSet();
            dataSetStudentsName = lessonService.GetAllStudentsNameByTeacherID((string)Session["teacherID"]);
            this.DropDownListStudents.DataSource = dataSetStudentsName.Tables["StudentsName"];
            this.DropDownListStudents.DataTextField = "Name";
            this.DropDownListStudents.DataValueField = "Name";
            this.DropDownListStudents.DataBind();
            this.DropDownListStudents.Items.Insert(0, new ListItem("בחר תלמיד", "בחר תלמיד"));
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
        protected DataTable GetData(DataSet dataSetUserLessons)
        {
            UserService userService = new UserService();
            SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
            LessonService lessonService = new LessonService();
            UserDetails studentDetails = new UserDetails();
            UserDetails TeacherDetails = new UserDetails();
            DataTable dataTableUserLessons = dataSetUserLessons.Tables["UserLessons"];
            if (dataTableUserLessons == null)
            {
                dataTableUserLessons = dataSetUserLessons.Tables["Table1"];
            }
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
            this.GridViewShowLessons.DataSource = GetData((DataSet)Session["dataSetLessons"]);
            this.GridViewShowLessons.DataBind();
        }
        protected void Populate_GridViewLessonsToPay(DataSet dataSet)
        {
            this.GridViewShowLessons.DataSource = GetData(dataSet);
            this.GridViewShowLessons.DataBind();
        }
        protected void GridViewShowLessons_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable dataTable = GetData((DataSet)Session["dataSetLessons"]);
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
            DataTable dataTable = GetData((DataSet)Session["dataSetLessons"]);
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
        protected void DropDownListTeachers_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTableLessons = GetData((DataSet)Session["dataSetLessons"]);
            DataView view = dataTableLessons.DefaultView;
            string selectedvalue = this.DropDownListTeachers.SelectedValue;
            view.RowFilter = "TeacherName = '" + selectedvalue + "'";
            this.GridViewShowLessons.DataSource = view;
            this.GridViewShowLessons.DataBind();
            if (selectedvalue == "בחר מורה")
            {
                Populate_GridViewShowLessons();
            }
        }
        protected void DropDownListStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTableLessons = GetData((DataSet)Session["dataSetLessons"]);
            DataView view = dataTableLessons.DefaultView;
            string selectedvalue = this.DropDownListStudents.SelectedValue;
            view.RowFilter = "StudentName = '" + selectedvalue + "'";
            this.GridViewShowLessons.DataSource = view;
            this.GridViewShowLessons.DataBind();
            if (selectedvalue == "בחר תלמיד")
            {
                Populate_GridViewShowLessons();
            }
        }
        public DataSet ConvertArrayListToDataSet(ArrayList arrayList)
        {
            // Create a new DataTable
            DataTable dataTable = new DataTable();

            // Add columns to the DataTable
            dataTable.Columns.Add("LessonID", typeof(string));
            dataTable.Columns.Add("LessonDate", typeof(DateTime));
            dataTable.Columns.Add("StartHour", typeof(TimeSpan));
            dataTable.Columns.Add("TeacherID", typeof(string));
            dataTable.Columns.Add("StudentID", typeof(string));
            dataTable.Columns.Add("SubjectID", typeof(int));
            dataTable.Columns.Add("LevelID", typeof(int));
            dataTable.Columns.Add("Address", typeof(string));
            dataTable.Columns.Add("Status", typeof(string));
            dataTable.Columns.Add("PricePerHour", typeof(int));
            dataTable.Columns.Add("PaymentStatus", typeof(string));

            // Add rows to the DataTable
            foreach (LessonsDetails lesson in arrayList)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["LessonID"] = lesson.LessonID;
                dataRow["LessonDate"] = lesson.LessonDate;
                dataRow["StartHour"] = lesson.StartHour;
                dataRow["TeacherID"] = lesson.TeacherID;
                dataRow["StudentID"] = lesson.StudentID;
                dataRow["SubjectID"] = lesson.SubjectID;
                dataRow["LevelID"] = lesson.LevelID;
                dataRow["Address"] = lesson.Address;
                dataRow["Status"] = lesson.Status;
                dataRow["PricePerHour"] = lesson.PricePerHour;
                dataRow["PaymentStatus"] = lesson.PaymentStatus;
                dataTable.Rows.Add(dataRow);
            }

            // Create a new DataSet and add the DataTable to it
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dataTable);

            // Return the DataSet
            return dataSet;
        }

        protected void GridViewShowLessons_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Pay")
            {
                UserService userService = new UserService();
                LessonsDetails lessonsDetails = new LessonsDetails();
                LessonService lessonService = new LessonService();
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = this.GridViewShowLessons.Rows[index];
                lessonsDetails.LessonDate = ParseDateString(row.Cells[9].Text);
                lessonsDetails.StartHour = DateTime.ParseExact(row.Cells[8].Text, "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                lessonsDetails.TeacherID = userService.GetUserIDByPhoneNumber(row.Cells[6].Text);
                lessonsDetails.StudentID = userService.GetUserIDByPhoneNumber(row.Cells[4].Text);
                lessonsDetails = lessonService.GetLesson(lessonsDetails.LessonDate, lessonsDetails.StartHour, lessonsDetails.TeacherID, lessonsDetails.StudentID);
                lessonsList.AddLesson(lessonsDetails);
                Session["myLessons"] = lessonsList;
                DataSet dataSet = ConvertArrayListToDataSet(lessonsList.lessonsList);
                Populate_GridViewLessonsToPay(dataSet);
            }
        }
    }
}