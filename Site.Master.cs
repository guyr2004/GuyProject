using System;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace GuyProject
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected string protectedLink = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                populateMainMenu();
            }
        }

        private void populateMainMenu()
        {
            if (Session["studentID"] == null && Session["userID"] == null && Session["teacherID"] == null)
            {
                MenuItem MenuItemHomePage = new MenuItem();
                MenuItemHomePage.Text = "Home Page";
                MenuItemHomePage.NavigateUrl = "HomePage.aspx";
                MenuMain.Items.Add(MenuItemHomePage);

                MenuItem MenuItemRegister = new MenuItem();
                MenuItemRegister.Text = "Register";
                MenuItemRegister.NavigateUrl = "Register.aspx";
                MenuMain.Items.Add(MenuItemRegister);

                MenuItem MenuItemLogin = new MenuItem();
                MenuItemLogin.Text = "Log In";
                MenuItemLogin.NavigateUrl = "Login.aspx";
                MenuMain.Items.Add(MenuItemLogin);
            }

            if (Session["studentID"] != null || Session["userID"] != null)
            {
                MenuMain.Items.Clear();
                MenuItem MenuItemSubscription = new MenuItem();
                MenuItemSubscription.Text = "Home Page";
                MenuItemSubscription.NavigateUrl = "HomePage.aspx";
                MenuMain.Items.Add(MenuItemSubscription);

                MenuItem MenuItemLogOut = new MenuItem();
                MenuItemLogOut.Text = "Log Out";
                MenuItemLogOut.NavigateUrl = "LogOut.aspx";
                MenuMain.Items.Add(MenuItemLogOut);

                MenuItem MenuItemUpdate = new MenuItem();
                MenuItemUpdate.Text = "Update";
                MenuItemUpdate.NavigateUrl = "Update.aspx";
                MenuMain.Items.Add(MenuItemUpdate);

                MenuItem MenuItemMyLessons = new MenuItem();
                MenuItemMyLessons.Text = "My Lessons";
                MenuItemMyLessons.NavigateUrl = "UserLessons.aspx";
                MenuMain.Items.Add(MenuItemMyLessons);

                MenuItem MenuItemBills = new MenuItem();
                MenuItemBills.Text = "Bills";
                MenuItemBills.NavigateUrl = "Bills.aspx";
                MenuMain.Items.Add(MenuItemBills);

                MenuItem MenuItemAccount = new MenuItem();
                MenuItemAccount.Text = "Account";
                MenuItemAccount.NavigateUrl = "Account.aspx";
                MenuMain.Items.Add(MenuItemAccount);
            }

            if (Session["teacherID"] != null)
            {
                MenuMain.Items.Clear();
                MenuItem MenuItemSubscription = new MenuItem();
                MenuItemSubscription.Text = "Home Page";
                MenuItemSubscription.NavigateUrl = "HomePage.aspx";
                MenuMain.Items.Add(MenuItemSubscription);

                MenuItem MenuItemLogOut = new MenuItem();
                MenuItemLogOut.Text = "Log Out";
                MenuItemLogOut.NavigateUrl = "LogOut.aspx";
                MenuMain.Items.Add(MenuItemLogOut);

                MenuItem MenuItemUpdateDetails = new MenuItem();
                MenuItemUpdateDetails.Text = "Update";
                MenuMain.Items.Add(MenuItemUpdateDetails);

                MenuItem MenuItemUpdate = new MenuItem();
                MenuItemUpdate.Text = "Update";
                MenuItemUpdate.NavigateUrl = "Update.aspx";
                MenuItemUpdateDetails.ChildItems.Add(MenuItemUpdate);

                MenuItem MenuItemUpdateTeacher = new MenuItem();
                MenuItemUpdateTeacher.Text = "UpdateTeacherDetails";
                MenuItemUpdateTeacher.NavigateUrl = "UpdateTeacherDetails.aspx";
                MenuItemUpdateDetails.ChildItems.Add(MenuItemUpdateTeacher);

                MenuItem MenuItemAddSubjectLevel = new MenuItem();
                MenuItemAddSubjectLevel.Text = "Add Subject Level";
                MenuItemAddSubjectLevel.NavigateUrl = "AddTeacherSubjectsAndLevels.aspx";
                MenuItemUpdateDetails.ChildItems.Add(MenuItemAddSubjectLevel);

                MenuItem MenuItemWorkingDetails = new MenuItem();
                MenuItemWorkingDetails.Text = "My Working Details";
                MenuMain.Items.Add(MenuItemWorkingDetails);

                MenuItem MenuItemTeacherWorkingHours = new MenuItem();
                MenuItemTeacherWorkingHours.Text = "AddTeacherWorkingHours";
                MenuItemTeacherWorkingHours.NavigateUrl = "AddTeacherWorkingHours.aspx";
                MenuItemWorkingDetails.ChildItems.Add(MenuItemTeacherWorkingHours);

                MenuItem MenuItemTeacherAbsence = new MenuItem();
                MenuItemTeacherAbsence.Text = "AddTeacherAbsence";
                MenuItemTeacherAbsence.NavigateUrl = "AddTeacherAbsence.aspx";
                MenuItemWorkingDetails.ChildItems.Add(MenuItemTeacherAbsence);

                MenuItem MenuItemMyLessons = new MenuItem();
                MenuItemMyLessons.Text = "My Lessons";
                MenuItemMyLessons.NavigateUrl = "UserLessons.aspx";
                MenuMain.Items.Add(MenuItemMyLessons);

                MenuItem MenuItemBills = new MenuItem();
                MenuItemBills.Text = "Bills";
                MenuItemBills.NavigateUrl = "Bills.aspx";
                MenuMain.Items.Add(MenuItemBills);

                MenuItem MenuItemAccount = new MenuItem();
                MenuItemAccount.Text = "Account";
                MenuItemAccount.NavigateUrl = "Account.aspx";
                MenuMain.Items.Add(MenuItemAccount);
            }

            if (((string)Session["teacherID"]) == "214777286")
            {
                MenuItem MenuItemAdmin = new MenuItem();
                MenuItemAdmin.Text = "My Working Details";
                MenuMain.Items.Add(MenuItemAdmin);

                MenuItem MenuItemAdminAddSubLevel = new MenuItem();
                MenuItemAdminAddSubLevel.Text = "AddSubjectsAndLevels";
                MenuItemAdminAddSubLevel.NavigateUrl = "AddSubjectsAndLevels.aspx";
                MenuItemAdmin.ChildItems.Add(MenuItemAdminAddSubLevel);

                MenuItem MenuItemAdminTeachers = new MenuItem();
                MenuItemAdminTeachers.Text = "AdminWebForm";
                MenuItemAdminTeachers.NavigateUrl = "AdminWebForm.aspx";
                MenuItemAdmin.ChildItems.Add(MenuItemAdminTeachers);
            }
        }
    }
}


//if (Session["studentID"] == null && Session["userID"] == null && Session["teacherID"] == null)
//{
//    protectedLink = "<li><a href='HomePage.aspx'>דף הבית</a></li>";
//    protectedLink += "<li><a href ='Register.aspx'>הרשמה</a></li>";
//    protectedLink += "<li><a href='Login.aspx'>התחברות</a></li>";
//}

//if (Session["studentID"] != null || Session["userID"] != null)
//{
//    protectedLink = "<li><a href='Logout.aspx'>התנתקות</a></li>";
//    protectedLink += "<li><a href='HomePage.aspx'>דף הבית</a></li>";
//    protectedLink += "<li> <a href='Update.aspx'>עדכון נתונים</a> </li>";
//    protectedLink += "<li> <a href='UserLessons.aspx'>השיעורים שלי</a></li>";
//}

//if (Session["teacherID"] != null)
//{
//    protectedLink = "<li><a href='Logout.aspx'>התנתקות</a></li>";
//    protectedLink += "<li><a href='HomePage.aspx'>דף הבית</a></li>";
//    protectedLink += "<li> <a href='Update.aspx'>עדכון נתונים</a></li>";
//    protectedLink += "<li> <a href='UpdateTeacherDetails.aspx'>עדכון נתוני מורה</a></li>";
//    protectedLink += "<li> <a href='UserLessons.aspx'>השיעורים שלי</a></li>";
//    protectedLink += "<li> <a href='AddTeacherSubjectsAndLevels.aspx'>הוסף מקצועות לימוד</a></li>";
//    protectedLink += "<li> <a href='AddTeacherWorkingHours.aspx'>הוסף ימי לימוד</a></li>";
//    protectedLink += "<li> <a href='AddTeacherAbsence.aspx'>הוסף העידרות</a></li>";
//    if ((string)Session["teacherID"] == "214777286")
//    {
//        protectedLink += "<li> <a href='AddSubjectsAndLevels.aspx'>הוסף מקצועות למערכת</a></li>";
//        protectedLink += "<li> <a href='AdminWebForm.aspx'>עמוד למנהל</a></li>";
//    }
//}
