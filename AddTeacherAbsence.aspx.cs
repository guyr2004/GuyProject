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
    public partial class AddTeacherAbsence : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (Session["teacherID"] == null)
            {
                Session["page"] = "AddTeacherAbsence.aspx";
                Response.Redirect("Login.aspx");
            }
            //Session["teacherID"] = "214777286";
            if (!Page.IsPostBack)
            {
                this.LabelMessage.Visible = false;
                PopulateDropDownListStartHourAndEndHours();
                Populate_GridViewAbsenceTeacher();
            }
        }

        protected void CustomValidatorStartHour_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (this.DropDownListStartHour.Text == "בחר שעה")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
        protected void CustomValidatorEndHour_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (this.DropDownListStartHour.Text == "בחר שעה")
            {
                args.IsValid = false;
            }
            else
            {
                args.IsValid = true;
            }
        }
        protected void PopulateDropDownListStartHourAndEndHours()
        {
            this.DropDownListStartHour.Items.Add("בחר שעה");
            this.DropDownListEndHour.Items.Add("בחר שעה");
            string hour = "";
            for (int i = 8; i < 24; i++)
            {
                if (i < 10)
                {
                    hour = "0" + i.ToString();
                    this.DropDownListStartHour.Items.Add(hour + ":00");
                    this.DropDownListEndHour.Items.Add(hour + ":00");
                }
                else
                {
                    this.DropDownListStartHour.Items.Add(i.ToString() + ":00");
                    this.DropDownListEndHour.Items.Add(i.ToString() + ":00");
                }
            }
            for (int i = 0; i < 8; i++)
            {
                hour = "0" + i.ToString();
                this.DropDownListStartHour.Items.Add(hour + ":00");
                this.DropDownListEndHour.Items.Add(hour + ":00");
            }
        }
        protected DataTable GetData()
        {
            AbsenceTeacherService absenceTeacherService = new AbsenceTeacherService();
            DataSet ds = absenceTeacherService.GetAllAbsenceTeacherByTeacherID((string)Session["teacherID"]);
            DataTable dataTable = ds.Tables["AbsenceTeacherTbl"];
            DataView dataView = dataTable.DefaultView;
            dataView.Sort = "AbsenceDate ASC";
            return dataTable;
        }
        protected void Populate_GridViewAbsenceTeacher()
        {
            this.GridViewAbsenceTeacher.DataSource = GetData();
            this.GridViewAbsenceTeacher.DataBind();
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
        protected void GridViewAbsenceTeacher_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.GridViewAbsenceTeacher.EditIndex = -1;
            Populate_GridViewAbsenceTeacher();
        }
        protected void GridViewAbsenceTeacher_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GridViewAbsenceTeacher.EditIndex = e.NewEditIndex;
            Populate_GridViewAbsenceTeacher();
        }
        protected void GridViewAbsenceTeacher_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                AbsenceTeacherDetails absenceTeacherDetails = new AbsenceTeacherDetails();
                AbsenceTeacherService absenceTeacherService = new AbsenceTeacherService();
                absenceTeacherDetails.TeacherID = (string)Session["teacherID"];
                GridViewRow row = this.GridViewAbsenceTeacher.Rows[e.RowIndex];
                TextBox textBoxAbsenceDate = (TextBox)row.Cells[2].Controls[0];
                string absencedate = textBoxAbsenceDate.Text;
                absenceTeacherDetails.AbsenceDate = DateTime.ParseExact(absencedate, "dd/MM/yyyy H:mm:ss", CultureInfo.InvariantCulture).Date;
                TextBox textBoxStarthour = (TextBox)row.Cells[1].Controls[0];
                string starthour = textBoxStarthour.Text;
                absenceTeacherDetails.StartHour = DateTime.ParseExact(starthour, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay;
                TextBox textBoxEndhour = (TextBox)row.Cells[0].Controls[0];
                string endhour = textBoxEndhour.Text;
                absenceTeacherDetails.EndHour = DateTime.ParseExact(endhour, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay;
                absenceTeacherService.UpdateTeacherWorkingHours(absenceTeacherDetails);
                this.GridViewAbsenceTeacher.EditIndex = -1;
                Populate_GridViewAbsenceTeacher();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GridViewAbsenceTeacher_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                AbsenceTeacherDetails absenceTeacherDetails = new AbsenceTeacherDetails();
                AbsenceTeacherService absenceTeacherService = new AbsenceTeacherService();
                GridViewRow row = this.GridViewAbsenceTeacher.Rows[e.RowIndex];
                absenceTeacherDetails.TeacherID = (string)Session["teacherID"];
                string dateString = row.Cells[2].Text;
                DateTime date = DateTime.ParseExact(row.Cells[2].Text, "M/d/yyyy", CultureInfo.InvariantCulture);
                absenceTeacherDetails.AbsenceDate = date.Date;
                absenceTeacherService.DeleteTeacherAbsence(absenceTeacherDetails);
                this.GridViewAbsenceTeacher.EditIndex = -1;
                Populate_GridViewAbsenceTeacher();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ButtonAddAbseceTeacher_Click(object sender, EventArgs e)
        {
            Page.Validate("Group1");
            if (Page.IsValid)
            {
                try
                {
                    AbsenceTeacherDetails absenceTeacherDetails = new AbsenceTeacherDetails();
                    AbsenceTeacherService absenceTeacherService = new AbsenceTeacherService();
                    absenceTeacherDetails.TeacherID = (string)Session["teacherID"];
                    absenceTeacherDetails.AbsenceDate = this.CalendarAbsenceTeacherDate.SelectedDate;
                    string starthour = this.DropDownListStartHour.Text;
                    DateTime startHour = DateTime.ParseExact(starthour, "HH:mm", CultureInfo.InvariantCulture);
                    absenceTeacherDetails.StartHour = startHour.TimeOfDay;
                    string endhour = this.DropDownListEndHour.Text;
                    DateTime Endhour = DateTime.ParseExact(endhour, "HH:mm", CultureInfo.InvariantCulture);
                    absenceTeacherDetails.EndHour = Endhour.TimeOfDay;
                    if (!absenceTeacherService.CheckIfAbsenceTeacherExist(absenceTeacherDetails))
                    {
                        absenceTeacherService.InsertNewAbsenceTeacher(absenceTeacherDetails);
                        Populate_GridViewAbsenceTeacher();
                        this.LabelMessage.Visible = true;
                        this.LabelMessage.Text = "הכנסת העידרות עברה בהצלחה";
                    }
                    else
                    {
                        this.LabelMessage.Visible = true;
                        this.LabelMessage.Text = "קיימת בעיה - ביום זה שעות העידרות אלה קיימות במערכת";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        protected void ButtonDeleteLesssons_Click(object sender, EventArgs e)
        {
            DataTable dataTableAbsenceTeacher = GetData();
            AbsenceTeacherDetails absenceTeacherDetails = new AbsenceTeacherDetails();
            AbsenceTeacherService absenceTeacherService = new AbsenceTeacherService();
            absenceTeacherDetails.TeacherID = (string)Session["teacherID"];
            try
            {
                foreach (DataRow row in dataTableAbsenceTeacher.Rows)
                {
                    DateTime dateNow = DateTime.Now;
                    DateTime dateLesson = DateTime.Parse((row["AbsenceDate"]).ToString());
                    int difference = dateLesson.Subtract(dateNow).Days;
                    if (difference < 0)
                    {
                        absenceTeacherDetails.AbsenceDate = DateTime.Parse((row["AbsenceDate"]).ToString());
                        absenceTeacherService.DeleteTeacherAbsence(absenceTeacherDetails);
                        Populate_GridViewAbsenceTeacher();
                        this.LabelMessage.Visible = true;
                        this.LabelMessage.Text = "העידרויות שתארכין עבר נמחקו";
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