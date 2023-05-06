using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GuyProject.App_Code;

namespace GuyProject
{
    public partial class Update : System.Web.UI.Page
    {
        UserDetails userDetails = new UserDetails();
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (Session["teacherID"] == null || Session["studentID"] == null)
            {
                Session["page"] = "Update.aspx";
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                userDetails = (UserDetails)Page.Session["userDetails"];
                this.LabelMessage.Text = "Hello " + userDetails.FirstName + " " + userDetails.LastName;
                this.ButtonUpdate.Visible = true;
                this.ButtonUpdateTeacher.Visible = true;
                ShowUser();
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
        protected void ShowUser()
        {
            userDetails = (UserDetails)Session["userDetails"];
            this.TextBoxUserID.Text = userDetails.UserID;
            this.TextBoxFirstName.Text = userDetails.FirstName;
            this.TextBoxLastName.Text = userDetails.LastName;
            this.TextBoxPhone.Text = userDetails.Phone;
            //this.RadioButtonListGender.SelectedValue = userDetails.Gender.ToString();
            this.TextBoxAddress.Text = userDetails.Address;
            this.DropDownListCityID.SelectedValue = userDetails.CityID.ToString();
            this.DropDownListKindUser.SelectedValue = userDetails.UserType.ToString();
            this.TextBoxEmail.Text = userDetails.Email;
            this.TextBoxUserPassword.Text = userDetails.UserPassword;
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            userDetails = (UserDetails)Page.Session["userDetails"];
            userDetails.FirstName = this.TextBoxFirstName.Text;
            userDetails.LastName = this.TextBoxLastName.Text;
            userDetails.Phone = this.TextBoxPhone.Text;
            //userDetails.Gender = this.RadioButtonListGender.SelectedValue;
            userDetails.Address = this.TextBoxAddress.Text;
            userDetails.CityID = this.DropDownListCityID.SelectedIndex;
            userDetails.UserType = this.DropDownListKindUser.SelectedValue;
            userDetails.Email = this.TextBoxEmail.Text;
            userDetails.UserPassword = this.TextBoxUserPassword.Text;
            userDetails.UserID = this.TextBoxUserID.Text;
            try
            {
                UserService userService = new UserService();
                userService.UpdateUserDetails(userDetails);
                if (userDetails.UserType == "מורה" || userDetails.UserType == "שניהם")
                {
                    Session["webForm"] = "Update.aspx";
                    this.ButtonUpdateTeacher.Visible = true;
                }
                LabelMessage.Text = "פרטיך עודכנו בהצלחה";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ButtonUpdateTeacher_Click(object sender, EventArgs e)
        {
            Response.Redirect("UpdateTeacherDetails.aspx");
        }
    }
}