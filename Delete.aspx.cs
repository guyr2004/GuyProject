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
    public partial class Delete : System.Web.UI.Page
    {
        UserDetails userDetails = new UserDetails(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            this.LabelUserMeesage.Visible = false;
        }

        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                TeacherService teacherservice = new TeacherService();
                try
                {
                    if (teacherservice.CheckIfTeacherhaveLessons(this.TextBoxUserID.Text))
                    {//לא נמחק - נשנה סטטוס למחוק
                        teacherservice.UpdateTeacherStatus(this.TextBoxUserID.Text, this.TextBoxStatus.Text); //נשנה את הסטטוס שלו למחוק
                        this.LabelUserMeesage.Visible = true;
                        this.LabelUserMeesage.Text = "הסטטוס של המורה השתנה";
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}