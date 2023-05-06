using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GuyProject.App_Code;

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
            localhostClientBankService.ClientBankService clientBankService = new localhostClientBankService.ClientBankService();
            GuyProject.localhostClientBankService.TransactionDetails transactionDetails = new GuyProject.localhostClientBankService.TransactionDetails();
            transactionDetails.PhonePayMoney = (string)Session["phone"];
            transactionDetails.PhoneGetMoney = this.TextBoxPhoneGet.Text;
            Decimal amount;
            if (Decimal.TryParse(this.TextBoxAmount.Text, out amount))
            {
                transactionDetails.Payee = this.TextBoxPayee.Text;
                try
                {
                    this.LabelMessage.Visible = true;
                    this.LabelMessage.Text = clientBankService.PostBillService(transactionDetails, amount);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}