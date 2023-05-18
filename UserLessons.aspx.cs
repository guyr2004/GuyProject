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
        DataSet dataSetLessonsNew = new DataSet();
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            bool userIdExist = false;
            if (Session["teacherID"] != null && Session["studentID"] != null)
            {
                if (Session["teacherID"] != Session["studentID"])
                {
                    Session["teacherID"] = null;
                }
            }
            if (Session["teacherID"] != null && Session["studentID"] != null && !Page.IsPostBack && !userIdExist)
            {
                Session["userID"] = (string)Session["teacherID"];
                this.DropDownListTeachers.Visible = true;
                this.DropDownListStudents.Visible = true;
                Populate_DropDownListStudents();
                Populate_DropDownListTeachers();
                userIdExist = true;
            }
            if (Session["studentID"] != null && !Page.IsPostBack && !userIdExist)
            {
                Session["userID"] = (string)Session["studentID"];
                Session["userID"] = Session["studentID"];
                this.DropDownListTeachers.Visible = true;
                Populate_DropDownListTeachers();
                this.DropDownListStudents.Visible = false;
                userIdExist = true;
            }
            if (Session["teacherID"] != null && !Page.IsPostBack && !userIdExist)
            {
                Session["userID"] = (string)Session["teacherID"];
                Session["userID"] = Session["teacherID"];
                this.DropDownListStudents.Visible = true;
                Populate_DropDownListStudents();
                this.DropDownListTeachers.Visible = false;
            }
            if (Session["teacherID"] == null && Session["studentID"] == null)
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
                this.LabelAddLessons.Visible = false;
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
        protected DataSet GetData(DataSet dataSetUserLessons)
        {
            UserService userService = new UserService();
            SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
            LessonService lessonService = new LessonService();
            UserDetails studentDetails = new UserDetails();
            UserDetails TeacherDetails = new UserDetails();
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
                if (row["PricePerHour"] != null && int.TryParse(row["PricePerHour"].ToString(), out int pricePerHour))
                {
                    row["Price"] = " שקלים " + pricePerHour.ToString();
                }
                else
                {
                    row["Price"] = "N/A";
                }
            }
            Session["dataSetLessons"] = dataSetUserLessons;
            count++;
            dataSetUserLessons.AcceptChanges();
            return dataSetUserLessons;
        }
        protected void Populate_GridViewShowLessons()
        {
            if (count != 1)
            {
                this.GridViewShowLessons.DataSource = GetData((DataSet)Session["dataSetLessons"]);
            }
            else
            {
                this.GridViewShowLessons.DataSource = (DataSet)Session["dataSetLessons"];
            }
            this.GridViewShowLessons.DataBind();

        }
        protected void Populate_GridViewLessonsToPay(DataSet dataSet)
        {
            this.GridViewLessonstoPay.DataSource = dataSet;
            this.GridViewLessonstoPay.DataBind();
        }
        protected void GridViewShowLessons_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataSet dataTable = (DataSet)Session["dataSetLessons"];
            try
            {
                LessonsDetails lessonsDetails = new LessonsDetails();
                LessonService lessonService = new LessonService();
                int rowselected = e.RowIndex;
                DataRow dataRow = dataTable.Tables["UserLessons"].Rows[rowselected];
                DateTime dateNow = DateTime.Now;
                string dateoflesson = dataTable.Tables["UserLessons"].Rows[rowselected]["LessonDate"].ToString();
                DateTime dateLesson = DateTime.ParseExact(dateoflesson, "dd/MM/yyyy H:mm:ss", CultureInfo.InvariantCulture).Date;
                int difference = dateLesson.Subtract(dateNow).Days;
                if (difference > 3 || difference < 0)//בודק אם תאריך השיעור הוא לא בתווך 3 הימים הקר
                {
                    lessonsDetails.TeacherID = dataTable.Tables["UserLessons"].Rows[rowselected]["TeacherID"].ToString();
                    lessonsDetails.StudentID = dataTable.Tables["UserLessons"].Rows[rowselected]["StudentID"].ToString();
                    lessonsDetails.LessonDate = dateLesson;
                    lessonsDetails.StartHour = DateTime.ParseExact(dataTable.Tables["UserLessons"].Rows[rowselected]["StartHour"].ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay;
                    lessonService.DeleteLesson(lessonsDetails);
                    dataTable.Tables["UserLessons"].Rows.Remove(dataRow);
                    Session["dataSetLessons"] = dataTable;
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
            DataSet dataTable = GetData((DataSet)Session["dataSetLessons"]);
            try
            {
                LessonsDetails lessonsDetails = new LessonsDetails();
                LessonService lessonService = new LessonService();
                foreach (DataRow row in dataTable.Tables["UserLessons"].Rows)
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
            DataSet dataTableLessons = GetData((DataSet)Session["dataSetLessons"]);
            DataView view = dataTableLessons.Tables["UserLessons"].DefaultView;
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
            DataSet dataTableLessons = GetData((DataSet)Session["dataSetLessons"]);
            DataView view = dataTableLessons.Tables["UserLessons"].DefaultView;
            string selectedvalue = this.DropDownListStudents.SelectedValue;
            view.RowFilter = "StudentName = '" + selectedvalue + "'";
            this.GridViewShowLessons.DataSource = view;
            this.GridViewShowLessons.DataBind();
            if (selectedvalue == "בחר תלמיד")
            {
                Populate_GridViewShowLessons();
            }
        }
        protected void GridViewShowLessons_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            UserService userService = new UserService();
            DataSet AllUserLessons = new DataSet();
            AllUserLessons = GetData((DataSet)Session["dataSetLessons"]);
            string userID = (string)Session["userID"];
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = GridViewShowLessons.Rows[index];
            string studentID = userService.GetUserIDByPhoneNumber(row.Cells[4].Text);
            if (studentID == userID)
            {
                if (e.CommandName == "Pay")
                {
                    dataSetLessonsNew = (DataSet)Session["dataSetLessonsNew"];
                    if (dataSetLessonsNew == null)
                    {
                        dataSetLessonsNew = AllUserLessons.Clone();
                    }
                    else
                    {
                        dataSetLessonsNew = (DataSet)Session["dataSetLessonsNew"];
                    }
                    index = Convert.ToInt32(e.CommandArgument);
                    DataRow dataRow = AllUserLessons.Tables["UserLessons"].Rows[index];
                    string lessonID = dataRow["LessonID"].ToString();
                    bool lessonExists = false;
                    if (dataSetLessonsNew != null)
                    {
                        lessonExists = dataSetLessonsNew.Tables["UserLessons"].Select($"LessonID='{lessonID}'").Length > 0;
                    }

                    if (!string.IsNullOrEmpty(dataRow["TeacherName"].ToString()))
                    {
                        string teacherName = dataRow["TeacherName"].ToString();
                        bool teacherExists = false;
                        if (dataSetLessonsNew != null)
                        {
                            teacherExists = dataSetLessonsNew.Tables["UserLessons"].Select($"TeacherName='{teacherName}'").Length > 0;
                        }
                        if (teacherExists || dataSetLessonsNew.Tables["UserLessons"].Rows.Count == 0)
                        {
                            if (!lessonExists)
                            {
                                if (dataRow["PaymentStatus"].ToString() == "שולם")
                                {
                                    LabelAddLessons.Visible = true;
                                    LabelAddLessons.Text = "לא ניתן להוסיף שורה זו מכיוון שהתשלום כבר בוצע";
                                    return;
                                }
                                DataRow data = dataSetLessonsNew.Tables["UserLessons"].NewRow();
                                foreach (DataColumn column in dataSetLessonsNew.Tables["UserLessons"].Columns)
                                {
                                    data[column.ColumnName] = dataRow[column.ColumnName];
                                }
                                dataSetLessonsNew.Tables["UserLessons"].Rows.Add(data);
                                Session["dataSetLessonsNew"] = dataSetLessonsNew;
                                Populate_GridViewLessonsToPay(dataSetLessonsNew);
                            }
                            else
                            {
                                this.LabelAddLessons.Visible = true;
                                LabelAddLessons.Text = "השיעור שניסית להוסיף כבר קיים ברשימה לתשלום";
                            }
                        }
                        else
                        {
                            this.LabelAddLessons.Visible = true;
                            LabelAddLessons.Text = "נבחר שיעור של מורה שלא ברשימה עם המורה שנבחר ברשימה";
                        }
                    }
                    else
                    {
                        if (!lessonExists)
                        {
                            if (dataRow["PaymentStatus"].ToString() == "שולם")
                            {
                                LabelAddLessons.Visible = true;
                                LabelAddLessons.Text = "לא ניתן להוסיף שורה זו מכיוון שהתשלום כבר בוצע";
                                return;
                            }
                            DataRow data = dataSetLessonsNew.Tables["UserLessons"].NewRow();
                            foreach (DataColumn column in dataSetLessonsNew.Tables["UserLessons"].Columns)
                            {
                                data[column.ColumnName] = dataRow[column.ColumnName];
                            }
                            dataSetLessonsNew.Tables["UserLessons"].Rows.Add(data);
                            Session["dataSetLessonsNew"] = dataSetLessonsNew;
                            Populate_GridViewLessonsToPay(dataSetLessonsNew);
                        }
                        else
                        {
                            this.LabelAddLessons.Visible = true;
                            LabelAddLessons.Text = "השיעור הזה כבר נוסף או שנבחר שיעור שאינו של המורה שכבר נבחר";
                        }
                    }
                }
            }
            else
            {
                this.LabelAddLessons.Visible = true;
                this.LabelAddLessons.Text = "לא ניתן לשלם על שיעור שאתה לא התלמיד";
            }
        }
        protected void GridViewLessonstoPay_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "DeleteLessonToPay")
            {
                dataSetLessonsNew = (DataSet)Session["dataSetLessonsNew"];
                int index = Convert.ToInt32(e.CommandArgument);
                DataRow dataRow = dataSetLessonsNew.Tables["UserLessons"].Rows[index];
                dataSetLessonsNew.Tables["UserLessons"].Rows.Remove(dataRow);
                Session["dataSetLessonsNew"] = dataSetLessonsNew;
                Populate_GridViewLessonsToPay(dataSetLessonsNew);
            }
        }
        protected void ButtonSubmitToPay_Click(object sender, EventArgs e)
        {
            int sumToPay = 0;
            dataSetLessonsNew = (DataSet)Session["dataSetLessonsNew"];
            string teacherPhone = "";
            string teacherName = "";
            foreach (DataRow row in dataSetLessonsNew.Tables["UserLessons"].Rows)
            {
                string moneyPerLesson = row["PricePerHour"].ToString();
                sumToPay += int.Parse(moneyPerLesson);
                teacherPhone = row["TeacherPhone"].ToString();
                teacherName = row["TeacherName"].ToString();
            }
            Session["dataSetLessonsNew"] = dataSetLessonsNew;
            Session["sumToPay"] = sumToPay;
            Session["teacherPhone"] = teacherPhone;
            Session["teacherName"] = teacherName;
            Response.Redirect("Bills.aspx");
        }
        protected void GridViewShowLessons_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewShowLessons.EditIndex = -1;
            Populate_GridViewShowLessons();
            GridViewShowLessons.DataBind();
        }
        protected void GridViewShowLessons_RowEditing(object sender, GridViewEditEventArgs e)
        {
            DataSet dataSet = (DataSet)Session["dataSetLessons"];
            UserService userService = new UserService();
            int index = Convert.ToInt32(e.NewEditIndex);
            GridViewRow row = this.GridViewShowLessons.Rows[index];
            if ((string)Session["teacherID"] != userService.GetUserIDByPhoneNumber(row.Cells[6].Text))
            {
                this.LabelDeleteMessage.Visible = true;
                this.LabelDeleteMessage.Text = "ניתן לשלם למורה במזומן אבל רק הוא יכול לעדכן את הסטטוס";
            }
            else
            {
                this.GridViewShowLessons.EditIndex = e.NewEditIndex;
                Populate_GridViewShowLessons();
            }
        }
        protected void GridViewShowLessons_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                UserService userService = new UserService();
                DataSet dataSetUserLessons = (DataSet)Session["dataSetLessons"];
                LessonsDetails lessonsDetails = new LessonsDetails();
                LessonService lessonServiceupdateStatusPayment = new LessonService();
                int index = Convert.ToInt32(e.RowIndex);
                GridViewRow row = this.GridViewShowLessons.Rows[index];
                lessonsDetails.LessonDate = ParseDateString(row.Cells[9].Text);
                lessonsDetails.StartHour = DateTime.ParseExact(row.Cells[8].Text, "hh:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                lessonsDetails.TeacherID = userService.GetUserIDByPhoneNumber(row.Cells[6].Text);
                lessonsDetails.StudentID = userService.GetUserIDByPhoneNumber(row.Cells[4].Text);
                string paymentStatus = ((DropDownList)(row.Cells[1].FindControl("DropDownListPaymentStatus"))).SelectedItem.Text;
                lessonServiceupdateStatusPayment.UpdateLessonPaymentStatus(lessonsDetails, paymentStatus);
                GridViewLessonstoPay.EditIndex = -1;
                Populate_GridViewShowLessons();
                GridViewLessonstoPay.DataBind();
            }
            catch(Exception Ex)
            {
                throw Ex;
            }
        }
    }
}