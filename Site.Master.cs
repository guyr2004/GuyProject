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
            if (Session["studentID"] == null && Session["userID"] == null && Session["teacherID"] == null)
            {
                protectedLink = "<li><a href='HomePage.aspx'>דף הבית</a></li>";
                protectedLink += "<li><a href ='Register.aspx'>הרשמה</a></li>";
                protectedLink += "<li><a href='Login.aspx'>התחברות</a></li>";
            }

            if (Session["studentID"] != null || Session["userID"] != null)
            {
                protectedLink = "<li><a href='Logout.aspx'>התנתקות</a></li>";
                protectedLink += "<li><a href='HomePage.aspx'>דף הבית</a></li>";
                protectedLink += "<li> <a href='Update.aspx'>עדכון נתונים</a> </li>";
                protectedLink += "<li> <a href='UserLessons.aspx'>השיעורים שלי</a></li>";
            }

            if (Session["teacherID"] != null)
            {
                protectedLink = "<li><a href='Logout.aspx'>התנתקות</a></li>";
                protectedLink += "<li><a href='HomePage.aspx'>דף הבית</a></li>";
                protectedLink += "<li> <a href='Update.aspx'>עדכון נתונים</a></li>";
                protectedLink += "<li> <a href='UpdateTeacherDetails.aspx'>עדכון נתוני מורה</a></li>";
                protectedLink += "<li> <a href='UserLessons.aspx'>השיעורים שלי</a></li>";
                protectedLink += "<li> <a href='AddTeacherSubjectsAndLevels.aspx'>הוסף מקצועות לימוד</a></li>";
                protectedLink += "<li> <a href='AddTeacherWorkingHours.aspx'>הוסף ימי לימוד</a></li>";
                protectedLink += "<li> <a href='AddTeacherAbsence.aspx'>הוסף העידרות</a></li>";
                if ((string)Session["teacherID"] == "214777286")
                {
                    protectedLink += "<li> <a href='AddSubjectsAndLevels.aspx'>הוסף מקצועות למערכת</a></li>";
                    protectedLink += "<li> <a href='AdminWebForm.aspx'>עמוד למנהל</a></li>";
                }
            }








            //if (Session["teacherID"] != null)
            //{
            //    var additionalItem = new System.Web.UI.HtmlControls.HtmlGenericControl("li");
            //    additionalItem.Attributes.Add("class", "navigation_s");

            //    var link = new System.Web.UI.HtmlControls.HtmlAnchor();
            //    link.HRef = "UserLessons.aspx";
            //    link.Controls.Add(new LiteralControl("השיעורים שלי"));

            //    additionalItem.Controls.Add(link);

            //    var menu = FindControlRecursive(Page, "myMenu") as System.Web.UI.HtmlControls.HtmlControl;
            //    //var menu = Master.FindControl("myMenu") as System.Web.UI.HtmlControls.HtmlControl;

            //    if (menu != null)
            //    {
            //        var menuList = menu.FindControl("menuList") as System.Web.UI.HtmlControls.HtmlControl;
            //        if (menuList != null)
            //        {
            //            menuList.Controls.Add(additionalItem);
            //        }
            //    }
            //}
        }
   
    }
}

