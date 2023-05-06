using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuyProject
{
    public partial class WebServiceMasterPage : System.Web.UI.MasterPage
    {
        protected string protectedLink;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["phone"] != null)
            {
                protectedLink = "<li><a href='Account.aspx'>החשבון שלי</a></li>";
                protectedLink += "<li><a href='Bills.aspx'>העבר כסף</a></li>";
            }

        }
    }
}