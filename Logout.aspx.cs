using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuyProject
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["teacherID"] != null || Session["sudentID"] != null)
            {
                Session["teacherID"] = null;
                Session["sudentID"] = null;
                Session["userID"] = null;
                Response.Redirect("HomePage.aspx");
            }
        }
    }
}