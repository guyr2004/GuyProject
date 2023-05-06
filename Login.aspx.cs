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
    public partial class Login : System.Web.UI.Page
    {
        UserService userService = new UserService();
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!Page.IsPostBack)
            {
                this.ButtonUpdate.Visible = false;
                this.ButtonUpdateTeacherDetails.Visible = false;
                this.LabeTextMesage.Visible = false;
            }
        }
        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                UserDetails userDetails = new UserDetails();
                userDetails = userService.GetUserByUserIDAndUserPassword(this.TextBoxUserID.Text, this.TextBoxUserPassword.Text);
                if (userDetails != null)
                {
                    this.LabeTextMesage.Visible = true;
                    Page.Session["userDetails"] = userDetails;
                    string page = (string)Session["page"];
                    if (page == "Login.aspx" || page == null)
                    {
                        page = "HomePage.aspx";
                    }
                    if (userDetails.UserType == "מורה")
                    {
                        Session["teacherID"] = userDetails.UserID;
                        Response.Redirect(page);
                    }
                    if (userDetails.UserType == "תלמיד")
                    {
                        Session["studentID"] = userDetails.UserID;
                        Response.Redirect(page);
                    }
                    if (userDetails.UserType == "שניהם")
                    {
                        Session["teacherID"] = userDetails.UserID;
                        Session["studentID"] = userDetails.UserID;
                        Response.Redirect(page);
                    }





                    //if (page == "UserLessons.aspx")
                    //{
                    //    Session["UserID"] = userDetails.UserID;
                    //    Response.Redirect("UserLessons.aspx");
                    //}
                    //if (userDetails.UserType == "תלמיד" || userDetails.UserType == "שניהם")
                    //{
                    //    if (page == "SetLesson.aspx")
                    //    {
                    //        Session["studentID"] = userDetails.UserID;
                    //        Response.Redirect("SetLesson.aspx");
                    //    }
                    //    if (page == "HomePage.aspx")
                    //    {
                    //        Session["studentID"] = userDetails.UserID;
                    //        if (Session["teacherID"] != null)
                    //        {
                    //            Response.Redirect("SetLesson.aspx");
                    //        }
                    //        Response.Redirect("HomePage.aspx");
                    //    }
                    //    }
                    //    if (userDetails.UserType == "מורה" || userDetails.UserType == "שניהם")
                    //    {
                    //        Session["teacherID"] = userDetails.UserID;
                    //        Response.Redirect(page);
                    //    }
                    //    this.ButtonUpdate.Visible = true;
                    //    this.ButtonUpdateTeacherDetails.Visible = true;
                    //    this.LabeTextMesage.Text = "Welcome " + userDetails.FirstName;
                }
                else
                {
                    this.LabeTextMesage.Visible = true;
                    this.LabeTextMesage.Text = "התחברות נכשלה תעודת זהות או סיסמא שגויים";
                }
            }
        }
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            Response.Redirect("Update.aspx");
        }
        protected void ButtonUpdateTeacherDetails_Click(object sender, EventArgs e)
        {
            UserDetails userDetails = new UserDetails();
            userDetails = (UserDetails)Session["userDetails"];
            if (userDetails.UserType == "מורה" || userDetails.UserType == "שניהם")
            {
                Session["webForm"] = "Login.aspx";
                Response.Redirect("UpdateTeacherDetails.aspx");
            }
        }
    }
}