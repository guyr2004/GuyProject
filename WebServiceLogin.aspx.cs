using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuyProject
{
    public partial class WebServiceLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
        }

        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            localhostBankService.BankService bankService = new localhostBankService.BankService();
            string username = this.TextBoxUserID.Text;
            string pass = this.TextBoxUserPassword.Text;
            string phone = bankService.GetPhoneNumber(username, pass);
            if (phone != "")
            {
                Session["phone"] = phone;
                this.LabeTextMesage.Visible = true;
                this.LabeTextMesage.Text = "ההתחברות עברה בהצלחה";
                string page = (string)Session["page"];
                if (page != null)
                {
                    Session["phone"] = phone;
                    Response.Redirect(page);
                }
            }
            else
            {
                this.LabeTextMesage.Visible = true;
                this.LabeTextMesage.Text = "קיימת בעיה - ההתחברות נכשלה";
            }
        }
    }
}