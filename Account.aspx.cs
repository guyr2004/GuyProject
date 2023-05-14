using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using GuyProject.App_Code;

namespace GuyProject
{
    public partial class Account1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["phone"] == null)
            {
                Session["page"] = "Account1.aspx";
                Response.Redirect("WebServiceLogin.aspx");
            }
            string phone = "0526896861";
            Populate_GridViewTransactions(phone);
            ShowAccount();
        }

        protected DataSet GetData(string phoneNumber)
        {
            localhostBankService.BankService bankService = new localhostBankService.BankService();
            DataSet dataSetTransactions = new DataSet();
            dataSetTransactions = bankService.GetTransaction(phoneNumber);
            return dataSetTransactions;
        }

        public void ShowAccount()
        {
            string phone = (string)Session["phone"];
            //phone = "0526896861";
            localhostBankService.BankService bankService = new localhostBankService.BankService();
            Decimal balance = bankService.GetBalance(phone);
            this.LabelBalance.Text = "היתרה שלך היא" + ": " + balance.ToString() + " שקלים";
            string username = bankService.GetUserName(phone);
            this.LabelUserName.Text = "שלום " + username;
        }
        protected void Populate_GridViewTransactions(string phoneNumber)
        {
            this.GridViewTransactions.DataSource = GetData(phoneNumber);
            this.GridViewTransactions.DataBind();
        }
    }
}