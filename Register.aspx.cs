using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GuyProject.App_Code;
using System.Data;

namespace GuyProject
{
    public partial class Register1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!Page.IsPostBack)
            {
                this.LabelMessage.Visible = false;
                Load_Years();
                GetCityName();
            }
        }
        protected void GetCityName()
        {
            UserService userService = new UserService();
            DataSet dataSet = userService.GetCities();
            DropDownListCityID.DataSource = dataSet.Tables["CitiesTbl"];
            DropDownListCityID.DataTextField = "CityName";
            DropDownListCityID.DataValueField = "CityID";
            DropDownListCityID.DataBind();
        }
        protected void Load_Years()
        {
            this.DropDownListDays = null;
            int n = 0;
            int year = DateTime.Now.Year;
            //DropDownListYears.Items.Add("בחר שנה");
            for (int i = year - 7; i > year - 90; i--)
            {
                DropDownListYears.Items.Add(i.ToString());
                n++;
            }
        }
        protected void Load_Days()
        {
            int numOfDays = DateTime.DaysInMonth(int.Parse(this.DropDownListYears.SelectedValue), int.Parse(this.DropDownListMonths.SelectedValue));

            for (int i = 1; i <= numOfDays; i++)
            {
                DropDownListDays.Items.Add(i.ToString());
            }
        }
        protected void DropDownListMonths_SelectedIndexChanged(object sender, EventArgs e)
        {

            DropDownListDays.Items.Clear();
            //DropDownListDays.Items.Add("בחר יום");
            Load_Days();
        }
        protected void DropDownListYears_SelectedIndexChanged(object sender, EventArgs e)
        {
            //DropDownListMonths.Items.Add("בחר חודש");
            for (int i = 1; i < 13; i++)
            {
                DropDownListMonths.Items.Add(i.ToString());
            }
        }
        protected bool IsIdValid(string numberID)
        {
            int sum = 0;
            int x, y;
            int checkdigits = numberID[numberID.Length - 1] - '0';
            for (int i = 0; i < numberID.Length - 1; i++)
            {
                //בדיקה האם מקומו זוגי או אי זוגי
                if (i % 2 == 0)
                {
                    x = numberID[i] - '0';
                }
                else
                {
                    x = numberID[i] - '0';
                    x = x * 2;
                }
                //בדיקה האם המספר מורכב יותר מספרה אחת
                if (x > 9)
                {
                    y = x % 10 + x / 10;
                }
                else
                {
                    y = x;
                }
                sum += y;
            }
            if ((sum + checkdigits) % 10 == 0)
            {
                return true;
            }
            return false;
        }
        protected void CustomValidatorId_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = IsIdValid(TextBoxUserID.Text);
        }
        protected void CustomValidatorKindUser_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (DropDownListKindUser.SelectedValue == "בחר סוג")
            {
                args.IsValid = false;
            }
        }
        protected void CustomValidatorCityID_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (this.DropDownListCityID.Text == "בחר עיר")
            {
                args.IsValid = false;
            }
        }
        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            Page.Validate("group1");
            if (Page.IsValid)
            {
                UserDetails userDetails = new UserDetails();
                //string time = this.DropDownListYears.SelectedValue + "/" +this.DropDownListMonths.SelectedValue + "/" + this.DropDownListDays.SelectedValue;
                DateTime time = new DateTime(int.Parse(this.DropDownListYears.SelectedValue), int.Parse(this.DropDownListMonths.SelectedValue), int.Parse(this.DropDownListDays.SelectedValue));
                userDetails.UserID = this.TextBoxUserID.Text;
                userDetails.FirstName = this.TextBoxFirstName.Text;
                userDetails.LastName = this.TextBoxLastName.Text;
                userDetails.BirthDate = time;
                userDetails.Phone = this.TextBoxPhone.Text;
                userDetails.Gender = this.RadioButtonListGender.SelectedValue;
                userDetails.Address = this.TextBoxAddress.Text;
                userDetails.CityID = this.DropDownListCityID.SelectedIndex;
                userDetails.UserType = this.DropDownListKindUser.SelectedValue;
                userDetails.Email = this.TextBoxEmail.Text;
                userDetails.UserPassword = this.TextBoxUserPassword.Text;
                try
                {
                    UserService userService = new UserService();
                    if (!userService.CheckIfUserIDExist(userDetails.UserID))
                    {
                        userService.AddUser(userDetails);
                        this.LabelMessage.Visible = true;
                        this.LabelMessage.Text = "נוסף בהצלחה";
                        Page.Session["userDetails"] = userDetails;
                        if (userDetails.UserType == "מורה" || userDetails.UserType == "שניהם")
                        {
                            Response.Redirect("TeachersRegistration.aspx");
                        }
                    }
                    else
                        this.LabelMessage.Text = "משתמש קיים - תעודת זהות קיימת במערכת";
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected void ButtonTeacherRegistration_Click(object sender, EventArgs e)
        {
            if (Session["teacherID"] == null && Session["studentID"] == null)
            {
                Session["page"] = "Register.aspx";
                Response.Redirect("TeachersRegistration.aspx");
            }
            if (Session["teacherID"] == null)
            {
                this.LabelMessage.Text = "מצטערים לא ניתן לעבור לעמוד הבא בתור תלמידים";
            }
        }
    }
}