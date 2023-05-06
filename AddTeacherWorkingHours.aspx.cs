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
    public partial class AddTeacherWorkingHours : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (Session["teacherID"] == null)
            {
                Session["page"] = "AddTeacherWorkingHours.aspx";
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                PopulateDropDownListStartHourAndEndHours();
                Populate_GridViewWorkHours();
                this.LabelMessage.Visible = false;
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
        public int GetNumByDay(string day)
        {
            if (day == "Sunday")
                return 1;
            if (day == "Monday")
                return 2;
            if (day == "Tuesday")
                return 3;
            if (day == "wednesday")
                return 4;
            if (day == "Thursday")
                return 5;
            if (day == "Friday")
                return 6;
            if (day == "Saturday")
                return 7;
            return 0;
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
        protected void CustomValidatorDaysOfWorking_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (this.DropDownListDayOfWorking.Text == "בחר יום")
            {
                args.IsValid = false;
            }
        }
        protected void CustomValidatorStartHour_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (this.DropDownListStartHour.Text == "בחר שעה")
            {
                args.IsValid = false;
            }
        }
        protected void CustomValidatorEndHour_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (this.DropDownListStartHour.Text == "בחר שעה")
            {
                args.IsValid = false;
            }
        }
        private DataTable GetData()
        {
            WorkingService workingService = new WorkingService();
            string teacherID = (string)Session["teacherID"];
            DataSet ds = workingService.GetAllDaysOfWorkingByTeacherID(teacherID);
            DataTable dataTable = ds.Tables["WorkingHoursTbl"];
            dataTable.Columns.Add("DayInWeekInNum");
            foreach (DataRow row in dataTable.Rows)
            {
                row["DayInWeekInNum"] = GetNumByDay(row["DayInWeek"].ToString());
            }
            DataView dataView = dataTable.DefaultView;
            dataView.Sort = "DayInWeekInNum ASC";
            return dataTable;
        }
        private void Populate_GridViewWorkHours()
        {
            this.GridViewWorkingHours.DataSource = GetData();
            this.GridViewWorkingHours.DataBind();
        }
        protected void GridViewWorkingHours_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.GridViewWorkingHours.EditIndex = -1;
            Populate_GridViewWorkHours();
        }
        protected void GridViewWorkingHours_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GridViewWorkingHours.EditIndex = e.NewEditIndex;
            Populate_GridViewWorkHours();
        }
        protected void GridViewWorkingHours_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                WorkingDetails workingDetails = new WorkingDetails();
                WorkingService workingService = new WorkingService();
                workingDetails.TeacherID = (string)Session["teacherID"];
                GridViewRow row = GridViewWorkingHours.Rows[e.RowIndex];
                workingDetails.DayOfWeek = row.Cells[2].Text;
                TextBox textBoxStartHour = (TextBox)row.Cells[1].Controls[0];
                string starthour = textBoxStartHour.Text;
                workingDetails.StartHour = DateTime.ParseExact(starthour, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay;
                TextBox textBoxEndHour = (TextBox)row.Cells[0].Controls[0];
                string endhour = textBoxEndHour.Text;
                workingDetails.EndHour = DateTime.ParseExact(endhour, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay;
                workingService.UpdateTeacherWorkingHours(workingDetails);
                this.GridViewWorkingHours.EditIndex = -1;
                Populate_GridViewWorkHours();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void GridViewWorkingHours_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                WorkingDetails workingDetails = new WorkingDetails();
                WorkingService workingService = new WorkingService();
                workingDetails.TeacherID = (string)Session["teacherID"];
                GridViewRow row = GridViewWorkingHours.Rows[e.RowIndex];
                workingDetails.DayOfWeek = row.Cells[2].Text;
                string starthour = row.Cells[1].Text;
                workingDetails.StartHour = DateTime.ParseExact(starthour, "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                string endhour = row.Cells[0].Text;
                workingDetails.EndHour = DateTime.ParseExact(endhour, "h:mm tt", CultureInfo.InvariantCulture).TimeOfDay;
                workingService.DeleteTeacherDaysOfWeek(workingDetails);
                this.GridViewWorkingHours.EditIndex = -1;
                Populate_GridViewWorkHours();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ButtonAddHours_Click(object sender, EventArgs e)
        {
            Page.Validate("Group1");
            if (Page.IsValid)
            {
                try
                {
                    WorkingService workingService = new WorkingService();
                    WorkingDetails workingDetails = new WorkingDetails();
                    workingDetails.TeacherID = (string)Session["teacherID"];
                    workingDetails.DayOfWeek = GetDayByNum(this.DropDownListDayOfWorking.Text);
                    string starthour = this.DropDownListStartHour.Text;
                    DateTime startHour = DateTime.ParseExact(starthour, "HH:mm", CultureInfo.InvariantCulture);
                    workingDetails.StartHour = startHour.TimeOfDay;
                    string endhour = this.DropDownListEndHour.Text;
                    DateTime Endhour = DateTime.ParseExact(endhour, "HH:mm", CultureInfo.InvariantCulture);
                    workingDetails.EndHour = Endhour.TimeOfDay;
                    if (!workingService.CheckIfDaysInWeekWithHoursExist(workingDetails))
                    {
                        workingService.InsertNewWorkingHours(workingDetails);
                        Populate_GridViewWorkHours();
                        this.LabelMessage.Visible = true;
                        this.LabelMessage.Text = "הכנסת היום והשעות עברה בהצלחה";
                    }
                    else
                    {
                        this.LabelMessage.Visible = true;
                        this.LabelMessage.Text = "קיימת בעיה-הכנסת המקצועות והשעות לא עברה בהצלחה";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
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

    }
}