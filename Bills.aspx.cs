using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OleDb;
using GuyProject.App_Code;
using System.Globalization;

namespace GuyProject
{
    public partial class Bills1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if ((string)Session["phone"] == null)
            {
                Session["page"] = "Bills.aspx";
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                this.LabelMessage.Visible = false;
                this.TextBoxAmount.Text = ((int)Session["sumToPay"]).ToString();
                this.TextBoxPhoneGet.Text = ((string)Session["teacherPhone"]).ToString();
                this.TextBoxPayee.Text = "שיעור פרטי";
            }
        }

        public static DateTime ParseDateString(string dateString)
        {
            string[] formatStrings = new string[]
            {
        "dd/MM/yyyy HH:mm:ss",
        "dd/MM/yyyy H:mm:ss",
        "dd/MM/yyyy HH:mm",
        "dd/MM/yyyy H:mm",
        "dd/MM/yyyy"
            };

            foreach (string formatString in formatStrings)
            {
                if (DateTime.TryParseExact(dateString, formatString, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
                {
                    return dateTime;
                }
            }

            throw new ArgumentException("Invalid date string format.");
        }
        protected void ButtonPay_Click(object sender, EventArgs e)
        {
            string payment = "ביט";
            LessonService lessonService = new LessonService();
            LessonsDetails lessonsDetails = new LessonsDetails();
            localhostClientBankService.ClientBankService clientBankService = new localhostClientBankService.ClientBankService();
            GuyProject.localhostClientBankService.TransactionDetails transactionDetails = new GuyProject.localhostClientBankService.TransactionDetails();
            transactionDetails.PhonePayMoney = (string)Session["phone"];
            transactionDetails.PhoneGetMoney = this.TextBoxPhoneGet.Text;
            Decimal amount;
            string paymentStatus = "שולם";
            if (Decimal.TryParse(this.TextBoxAmount.Text, out amount))
            {
                transactionDetails.Payee = "שיעור פרטי " + (string)Session["teacherName"];
                try
                {
                    this.LabelMessage.Visible = true;
                    this.LabelMessage.Text = clientBankService.PostBillService(transactionDetails, amount);
                    DataSet dataSet = (DataSet)Session["dataSetLessonsNew"];
                    foreach (DataRow row in dataSet.Tables["UserLessons"].Rows)
                    {
                        string lessonDate = row["LessonDate"].ToString();
                        lessonsDetails.LessonDate = ParseDateString(lessonDate);
                        string starthour = row["StartHour"].ToString();
                        lessonsDetails.StartHour = DateTime.ParseExact(starthour, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture).TimeOfDay;
                        lessonsDetails.TeacherID = row["TeacherID"].ToString();
                        lessonsDetails.StudentID = row["StudentID"].ToString();
                        lessonService.UpdateLessonPaymentStatus(lessonsDetails, paymentStatus);
                        lessonService.InsertNewPayment(lessonsDetails.LessonDate, lessonsDetails.StudentID, lessonsDetails.TeacherID, (int)amount, payment);
                    }
                    dataSet.Tables["UserLessons"].Clear();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}