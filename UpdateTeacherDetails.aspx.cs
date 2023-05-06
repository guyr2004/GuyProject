using System;
using System.Collections;
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
    public partial class UpdateTeacherDetails : System.Web.UI.Page
    {
        SubjectsList subjectsList = new SubjectsList();
        TeacherDetails teacherDetails = new TeacherDetails();
        TeacherService teacherService = new TeacherService();
        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (Page.Session["teacherID"] == null)
            {
                Session["page"] = "UpdateTeacherDetails.aspx";
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                UserDetails userDetails = new UserDetails();
                userDetails = (UserDetails)Session["userDetails"];
                //Page.Session["subjectsList"] = subjectsList;
                teacherDetails = teacherService.GetTeacherByTeacherID(userDetails.UserID);
                Session["TeacherDetails"] = teacherDetails;
                this.LabelMeesageImage.Visible = false;
                this.LabelMeesage.Visible = false;
                ShowTeacherDetails();
                //this.LabelMeesageAddSUbjectAndLevels.Visible = false;
                //PopulateDropDownListSubjects();
                //this.DropDownListSubjects.Items.Add("בחר מקצוע");
                //this.DropDownListSubjects.SelectedValue = "בחר מקצוע";
                //Populate_GridViewSubjectsAndLevels();
            }
        }
        protected void ShowTeacherDetails()
        {
            teacherDetails = (TeacherDetails)Session["TeacherDetails"];
            this.DropDownListlearnPlace.SelectedValue = teacherDetails.LearnPlace.ToString();
            this.TextBoxDescription.Text = teacherDetails.Description.ToString();
            string fileImageName = teacherDetails.ImageTeacher.ToString();
            this.ImageTeacher.ImageUrl = fileImageName;
            Page.Session["imagefile"] = fileImageName;
        }
        protected void ButtonUploadFile_Click(object sender, EventArgs e)
        {
            string fileImageName = (string)Page.Session["imagefile"];
            if (this.FileUploadImage.HasFile)
            {
                string currentfileName = Path.GetFileName(this.FileUploadImage.FileName);
                string fileExtantion = Path.GetExtension(this.FileUploadImage.FileName);
                Session["image"] = currentfileName;

                if ("~/ImagesTeachers/" + currentfileName != fileImageName)
                {
                    if (fileExtantion == ".jpg" || fileExtantion == ".jpeg" || fileExtantion == ".png" || fileExtantion == ".gif")
                    {
                        this.FileUploadImage.SaveAs(Server.MapPath("~/ImagesTeachers/") + currentfileName);
                        this.LabelMeesageImage.Visible = true;
                        this.LabelMeesageImage.Text = "התמונה הועלתה בהצלחה";
                        Session["image"] = "~/ImagesTeachers/" + currentfileName;
                    }
                    else
                    {
                        this.LabelMeesageImage.Visible = true;
                        this.LabelMeesageImage.Text = "קיימת בעיה- הקובץ שהועלה אינו תמונה";
                    }
                }
                else
                {
                    this.LabelMeesageImage.Visible = true;
                    this.LabelMeesageImage.Text = "התמונה הנוכחית קיימת במערכת";
                    Session["image"] = fileImageName;
                }
            }
        }
        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {//עדכון פרטיים אישיים של המורה
            teacherDetails = (TeacherDetails)Session["TeacherDetails"];
            teacherDetails.LearnPlace = this.DropDownListlearnPlace.SelectedValue;
            teacherDetails.ImageTeacher = Path.GetFileName(this.FileUploadImage.FileName);
            teacherDetails.ImageTeacher = (string)Session["image"]; 
            if (teacherDetails.ImageTeacher == null)
            {
                teacherDetails.ImageTeacher = "~/ImagesTeachers/Anonymy.png";
            }
            teacherDetails.Description = this.TextBoxDescription.Text;
            try
            {
                teacherService.UpdateTeacherDetails(teacherDetails);
                this.LabelMeesage.Visible = true;
                this.LabelMeesage.Text = "פרטיך עודכנו בהצלחה";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //עדכון הכנסת המקצועות והרמות של המורה
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
        //protected DataSet GetData()
        //{
        //    teacherDetails = (TeacherDetails)Session["TeacherDetails"];
        //    return teacherService.GetTeacherWithSubjects(teacherDetails.TeacherID);
        //}
        //protected void Populate_GridViewSubjectsAndLevels()
        //{
        //    this.GridViewSubjectsAndLevels.DataSource = GetData();
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
        //    //Populate_GridViewSubjectsAndLevels();
        //}
        //protected SubjectsList ConvertDataSetToArrayList(DataSet ds)
        //{
        //    SubjectsList subjectsList = new SubjectsList();

        //    foreach (DataRow row in ds.Tables[0].Rows)
        //    {
        //        SubjectsLevels subjectsLevels = new SubjectsLevels();
        //        subjectsLevels.SubjectID = int.Parse(row["SubjectID"].ToString());
        //        subjectsLevels.LevelID = int.Parse(row["LevelID"].ToString());
        //        subjectsLevels.SubjectName = row["SubjectName"].ToString();
        //        subjectsLevels.LevelName = row["LevelName"].ToString();
        //        subjectsLevels.PricePerHour = int.Parse(row["PricePerHour"].ToString());


        //        subjectsList.AddSubjectLevel(subjectsLevels);
        //    }
        //    return subjectsList;
        //}
        //protected void ButtonAddSubjectsAndLevels_Click(object sender, EventArgs e)
        //{//צריך לשנות את המקצועות עבור כל מורה מחיקה או הכנסה או שינוי המחיר -עדכון
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

        //    TeacherService teacherService = new TeacherService();
        //    subjectsList = (SubjectsList)Page.Session["subjectsList"];
        //    TeacherDetails teacherDetails = new TeacherDetails();
        //    teacherDetails = (TeacherDetails)Session["TeacherDetails"];

        //    try
        //    {
        //        teacherService.AddSubjectsAndLevelsTeacher(subjectsList, teacherDetails.TeacherID);
        //        this.LabelMeesageAddSUbjectAndLevels.Visible = true;
        //        this.LabelMeesageAddSUbjectAndLevels.Text = "הכנסת מקצועות ורמות הלימוד עברו בהצלחה";
        //        Populate_GridViewSubjectsAndLevels();
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
        //    //subjectsList = (SubjectsList)Session["subjectsList"];
        //    DataSet ds = GetData();
        //    subjectsList = (SubjectsList)ConvertDataSetToArrayList(ds);
        //    SubjectsLevels subjectsLevels = new SubjectsLevels();
        //    GridViewRow row = GridViewSubjectsAndLevels.Rows[e.RowIndex];
        //    subjectsLevels.SubjectName = row.Cells[1].Text;
        //    subjectsLevels.LevelName = row.Cells[0].Text;
        //    subjectsLevels.SubjectID = subjectsLevelsService.GetSubjectIDBySubjectName(subjectsLevels.SubjectName);
        //    subjectsLevels.LevelID = subjectsLevelsService.GetLevelIDByLevelName(subjectsLevels.LevelName);
        //    int price = short.Parse(((TextBox)row.Cells[2].Controls[0]).Text);
        //    TeacherDetails teacherDetails = new TeacherDetails();
        //    teacherDetails = (TeacherDetails)Session["TeacherDetails"];
        //    TeacherService teacherService = new TeacherService();
        //    if (teacherService.CheckIfTeacherhaveLessons(teacherDetails.TeacherID) == false)
        //    {
        //        try
        //        {
        //            subjectsList.UpdateSubjectLevelPrice(subjectsLevels, price, teacherDetails.TeacherID);
        //            Session["subjectsList"] = subjectsList;
        //            this.GridViewSubjectsAndLevels.EditIndex = -1;
        //            Populate_GridViewSubjectsAndLevels();
        //            this.LabelMeesage.Visible = true;
        //            this.LabelMeesage.Text = "עדכון המקצועות עבר בהצלחה";
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    else
        //    {
        //        this.LabelMeesage.Visible = true;
        //        this.LabelMeesage.Text = "קיימת בעיה - עדכון המקצועות לא הצליחה";
        //    }
        //}
        //protected void GridViewSubjectsAndLevels_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
        //    DataSet ds = GetData();
        //    subjectsList = (SubjectsList)ConvertDataSetToArrayList(ds);
        //    SubjectsLevels subjectsLevels = new SubjectsLevels();
        //    GridViewRow row = GridViewSubjectsAndLevels.Rows[e.RowIndex];
        //    subjectsLevels.SubjectName = row.Cells[1].Text;
        //    subjectsLevels.LevelName = row.Cells[0].Text;
        //    subjectsLevels.SubjectID = subjectsLevelsService.GetSubjectIDBySubjectName(subjectsLevels.SubjectName);
        //    subjectsLevels.LevelID = subjectsLevelsService.GetLevelIDByLevelName(subjectsLevels.LevelName);
        //    //int price = short.Parse(((TextBox)row.Cells[2].Controls[0]).Text);
        //    teacherDetails = (TeacherDetails)Session["TeacherDetails"];
        //    TeacherService teacherService = new TeacherService();
        //    if (teacherService.CheckIfTeacherhaveLessons(teacherDetails.TeacherID) == false)
        //    {
        //        try
        //        {
        //            subjectsList.DeleteSubjectLevel(subjectsLevels, teacherDetails.TeacherID);
        //            Session["subjectsList"] = subjectsList;
        //            this.GridViewSubjectsAndLevels.EditIndex = -1;
        //            Populate_GridViewSubjectsAndLevels();
        //            this.LabelMeesage.Visible = true;
        //            this.LabelMeesage.Text = "מחיקת מקצועות הלימוד עבר בהצלחה";
        //        }
        //        catch (Exception ex)
        //        {
        //            throw ex;
        //        }
        //    }
        //    else
        //    {
        //        this.LabelMeesage.Visible = true;
        //        this.LabelMeesage.Text = "קיימת בעיה - מחיקת המקצועות לא הצליחה";
        //    }
        //}
    }
}