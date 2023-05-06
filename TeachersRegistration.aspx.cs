using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GuyProject.App_Code;
using System.IO;
using System.Data.OleDb;

namespace GuyProject
{
    public partial class TeachersRegistration : System.Web.UI.Page
    {
        SubjectsList subjectsList = new SubjectsList();
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (Page.Session["teacherID"] == null)
            {
                Session["page"] = "TeachersRegistration.aspx";
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                this.LabelMeesageImage.Visible = false;
                this.LabelMeesage.Visible = false;
            }
        }
        protected void ButtonUploadFile_Click(object sender, EventArgs e)
        {
            if (this.FileUploadImage.HasFile)
            {
                string fileName = Path.GetFileName(this.FileUploadImage.FileName);
                string fileExtantion = Path.GetExtension(this.FileUploadImage.FileName);
                Session["image"] = fileName;

                if (fileExtantion == ".jpg" || fileExtantion == ".jpeg" || fileExtantion == ".png" || fileExtantion == ".gif")
                {
                    this.FileUploadImage.SaveAs(Server.MapPath("~/ImagesTeachers/") + fileName);
                    this.LabelMeesageImage.Visible = true;
                    this.LabelMeesageImage.Text = "התמונה הועלתה בהצלחה";
                }
                else
                {
                    this.LabelMeesageImage.Visible = true;
                    this.LabelMeesageImage.Text = "קיימת בעיה - הקובץ שהועלה אינו תמונה";
                }
            }
            else
            {
                Session["image"] = "Anonymy.png";
            }
        }
        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            TeacherDetails teacherDetails = new TeacherDetails();
            UserDetails userDetails = new UserDetails();
            userDetails = (UserDetails)Session["userDetails"];
            teacherDetails.TeacherID = userDetails.UserID;
            //teacherDetails.TeacherID = "325428217";
            teacherDetails.Status = "לא מאושר";
            teacherDetails.LearnPlace = this.DropDownListlearnPlace.SelectedValue;
            string imageName = (string)Session["image"];
            teacherDetails.ImageTeacher = "~/ImagesTeachers/Anonymy.png" + imageName;
            Session["image"] = teacherDetails.ImageTeacher;
            teacherDetails.Description = this.TextBoxDescription.Text;
            try
            {
                TeacherService teacherService = new TeacherService();
                teacherService.AddTeacher(teacherDetails);
                this.LabelMeesage.Visible = true;
                this.LabelMeesage.Text = "הרישום הועבר בהצלחה";
                Session["TeacherDetails"] = teacherDetails;
                Session["taecherID"] = teacherDetails.TeacherID;
                //this.PanelSubjects.Visible = true;
                subjectsList = new SubjectsList();
                Page.Session["subjectsList"] = subjectsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //בהתחלה הכנסת המקצועות ורמות הלימוד הייתה פה
        //protected void PopulateDropDownListSubjects()
        //{
        //    UserService userService = new UserService();
        //    DataSet dataSet = userService.GetAllSubjects();
        //    this.DropDownListSubjects.DataSource = dataSet.Tables["SubjectsTbl"];
        //    this.DropDownListSubjects.DataTextField = "SubjectName";
        //    this.DropDownListSubjects.DataValueField = "SubjectID";
        //    this.DropDownListSubjects.DataBind();
        //}
        //protected void PopulateCheckBoxListLevels(int subjectID)
        //{
        //    SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
        //    DataSet dataSet = subjectsLevelsService.GetAllLevelsNameBySubjectsID(subjectID);
        //    this.CheckBoxListLevels.DataSource = dataSet.Tables["LevelsTbl"];
        //    this.CheckBoxListLevels.DataTextField = "LevelName";
        //    this.CheckBoxListLevels.DataValueField = "LevelID";
        //    this.CheckBoxListLevels.DataBind();
        //}
        //protected void DropDownListSubjects_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Subject subject = new Subject();
        //    subject.SubjectID = int.Parse(this.DropDownListSubjects.SelectedValue);
        //    subject.SubjectName = this.DropDownListSubjects.SelectedItem.Text;
        //    Page.Session["subject"] = subject;
        //    PopulateCheckBoxListLevels(int.Parse(this.DropDownListSubjects.SelectedValue));
        //}
        //protected void Populate_GridViewSubjectsAndLevels()
        //{
        //    this.GridViewSubjectsAndLevels.DataSource = subjectsList.SubList;
        //    this.GridViewSubjectsAndLevels.DataBind();
        //}
        //protected void ButtonSubjectsLevels_Click(object sender, EventArgs e)
        //{
        //    // לואלה על הרשימה וכל רמה נבחרת להוסיף לרשימה
        //    subjectsList = (SubjectsList)Page.Session["subjectsList"];
        //    Subject subject = (Subject)Page.Session["subject"];
        //    SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
        //    for (int i = 0; i < this.CheckBoxListLevels.Items.Count; i++)
        //    {
        //        if (this.CheckBoxListLevels.Items[i].Selected)
        //        {
        //            SubjectsLevels subjectsLevels = new SubjectsLevels();
        //            subjectsLevels.SubjectID = subject.SubjectID;
        //            subjectsLevels.SubjectName = subject.SubjectName;
        //            subjectsLevels.LevelID = int.Parse(this.CheckBoxListLevels.Items[i].Value);
        //            subjectsLevels.LevelName = subjectsLevelsService.GetLevelNameByLevelID(subjectsLevels.LevelID);
        //            subjectsLevels.PricePerHour = 0; 
        //            subjectsList.AddSubjectLevel(subjectsLevels);
        //            Page.Session["subjectsList"] = subjectsList;
        //        }
        //    }
        //    Populate_GridViewSubjectsAndLevels();
        //}
        //protected void ButtonAddSubjectsAndLevels_Click(object sender, EventArgs e)
        //{
        //    TeacherService teacherService = new TeacherService();
        //    subjectsList = (SubjectsList)Page.Session["subjectsList"];
        //    TeacherDetails teacherDetails = new TeacherDetails();
        //    teacherDetails = (TeacherDetails)Session["TeacherDetails"];
        //    //string teacherID = "214777286";

        //    try
        //    {
        //        teacherService.AddSubjectsAndLevelsTeacher(subjectsList, teacherDetails.TeacherID /*teacherID*/);
        //        this.LabelMeesageAddSUbjectAndLevels.Visible = true;
        //        this.LabelMeesageAddSUbjectAndLevels.Text = "הכנסת מקצועות ורמות הלימוד עברו בהצלחה";
        //    }
        //    catch (Exception ex)
        //    {
        //        this.LabelMeesageAddSUbjectAndLevels.Visible = true;
        //        this.LabelMeesageAddSUbjectAndLevels.Text = "יש בעיה המקצועות והרמות לא נכנסו";
        //        throw ex;
        //    }
        //}
        //protected void GridViewSubjectsAndLevels_RowEditing(object sender, GridViewEditEventArgs e)
        //{
        //    this.GridViewSubjectsAndLevels.EditIndex = e.NewEditIndex;
        //    Populate_GridViewSubjectsAndLevels();
        //}
        //protected void GridViewSubjectsAndLevels_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        //{
        //    this.GridViewSubjectsAndLevels.EditIndex = -1;
        //    Populate_GridViewSubjectsAndLevels();
        //}
        //protected void GridViewSubjectsAndLevels_RowUpdating(object sender, GridViewUpdateEventArgs e)
        //{
        //    SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
        //    subjectsList = (SubjectsList)Session["subjectsList"];
        //    SubjectsLevels subjectsLevels = new SubjectsLevels();
        //    GridViewRow row = GridViewSubjectsAndLevels.Rows[e.RowIndex];
        //    subjectsLevels.SubjectID = subjectsLevelsService.GetSubjectIDBySubjectName(row.Cells[0].Text);
        //    subjectsLevels.LevelID = subjectsLevelsService.GetLevelIDByLevelName(row.Cells[1].Text);
        //    subjectsLevels.SubjectName = row.Cells[0].Text;
        //    subjectsLevels.LevelName = row.Cells[1].Text;
        //    int price = short.Parse(((TextBox)row.Cells[2].Controls[0]).Text);
        //    TeacherDetails teacherDetails = new TeacherDetails();
        //    teacherDetails = (TeacherDetails)Session["TeacherDetails"];
        //    subjectsList.UpdateSubjectLevelPrice(subjectsLevels, price, teacherDetails.TeacherID);
        //    Session["subjectsList"] = subjectsList;
        //    this.GridViewSubjectsAndLevels.EditIndex = -1;
        //    Populate_GridViewSubjectsAndLevels();
        //}
        //protected void GridViewSubjectsAndLevels_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
        //    subjectsList = (SubjectsList)Session["subjectsList"];
        //    SubjectsLevels subjectsLevels = new SubjectsLevels();
        //    GridViewRow row = GridViewSubjectsAndLevels.Rows[e.RowIndex];
        //    subjectsLevels.SubjectID = subjectsLevelsService.GetSubjectIDBySubjectName(row.Cells[0].Text);
        //    subjectsLevels.LevelID = subjectsLevelsService.GetLevelIDByLevelName(row.Cells[1].Text);
        //    TeacherDetails teacherDetails = new TeacherDetails();
        //    teacherDetails = (TeacherDetails)Session["TeacherDetails"];
        //    subjectsList.DeleteSubjectLevel(subjectsLevels, teacherDetails.TeacherID);
        //    Session["subjectsList"] = subjectsList;
        //    this.GridViewSubjectsAndLevels.EditIndex = -1;
        //    Populate_GridViewSubjectsAndLevels();
        //}
    }
}