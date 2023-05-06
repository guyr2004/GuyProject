using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GuyProject
{
    public partial class Bills : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (Session["phone"] == null)
            {
                Session["page"] = "Bills.aspx";
                Response.Redirect("WebServiceLogin.aspx");
            }
            this.LabelMessage.Visible = false;
        }

        protected void ButtonPay_Click(object sender, EventArgs e)
        {
            string phonepay = (string)Session["phone"];
            string phoneget = this.TextBoxPhoneGet.Text;
            Decimal amount;
            if (Decimal.TryParse(this.TextBoxAmount.Text, out amount))
            {
                string payee = this.TextBoxPayee.Text;
                localhostClientBankService.ClientBankService clientBankService = new localhostClientBankService.ClientBankService();
                try
                {
                    this.LabelMessage.Visible = true;
                    this.LabelMessage.Text = clientBankService.PostBillService(phonepay, payee, amount, phoneget);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}