using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GuyProject.App_Code;
using System.Data;
using System.Data.OleDb;

namespace GuyProject
{
    public partial class AddTeacherSubjectsAndLevels : System.Web.UI.Page
    {
        SubjectsList subjectsList = new SubjectsList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["teacherID"] == null)
            {
                Session["page"] = "AddTeacherSubjectsAndLevels.aspx";
                Response.Redirect("Login.aspx");
            }
            if (Page.Session["subjectsList"] == null)
            {
                TeacherService teacherService = new TeacherService();
                DataSet ds = teacherService.GetTeacherWithSubjects((string)Session["teacherID"]);
                subjectsList = ConvertDataSetToArrayList(ds);
                Page.Session["subjectsList"] = subjectsList;
            }
            if (!Page.IsPostBack)
            {
                Populate_GridViewSubjectsAndLevels();
                PopulateDropDownListSubjects();
            }
        }

        protected void PopulateDropDownListSubjects()
        {
            UserService userService = new UserService();
            DataSet dataSet = userService.GetAllSubjects();
            this.DropDownListSubjects.DataSource = dataSet.Tables["SubjectsTbl"];
            this.DropDownListSubjects.DataTextField = "SubjectName";
            this.DropDownListSubjects.DataValueField = "SubjectID";
            this.DropDownListSubjects.DataBind();
        }
        protected void PopulateCheckBoxListLevels(int subjectID)
        {
            SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
            DataSet dataSet = subjectsLevelsService.GetAllLevelsNameBySubjectsID(subjectID);
            this.CheckBoxListLevels.DataSource = dataSet.Tables["LevelsTbl"];
            this.CheckBoxListLevels.DataTextField = "LevelName";
            this.CheckBoxListLevels.DataValueField = "LevelID";
            this.CheckBoxListLevels.DataBind();
        }
        protected void DropDownListSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            Subject subject = new Subject();
            subject.SubjectID = int.Parse(this.DropDownListSubjects.SelectedValue);
            subject.SubjectName = this.DropDownListSubjects.SelectedItem.Text;
            Page.Session["subject"] = subject;
            PopulateCheckBoxListLevels(int.Parse(this.DropDownListSubjects.SelectedValue));
        }
        protected void Populate_GridViewSubjectsAndLevels()
        {
            SubjectsList subjectsList = (SubjectsList)Session["subjectsList"];
            this.GridViewSubjectsAndLevels.DataSource = subjectsList.SubList;
            this.GridViewSubjectsAndLevels.DataBind();
        }
        protected SubjectsList ConvertDataSetToArrayList(DataSet ds)
        {//מעביר את DataSet עם כל המקצועות ורמות הלימוד לאובייקט מסוג SubjectsList
            SubjectsList subjectsList = new SubjectsList();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                SubjectsLevels subjectsLevels = new SubjectsLevels();
                subjectsLevels.SubjectID = int.Parse(row["SubjectID"].ToString());
                subjectsLevels.LevelID = int.Parse(row["LevelID"].ToString());
                subjectsLevels.SubjectName = row["SubjectName"].ToString();
                subjectsLevels.LevelName = row["LevelName"].ToString();
                subjectsLevels.PricePerHour = int.Parse(row["PricePerHour"].ToString());

                subjectsList.AddSubjectLevel(subjectsLevels);//מוסיף את האטובייקטים לרשימה אך לא למסד הנתונים
            }
            return subjectsList;
        }
        protected void GridViewSubjectsAndLevels_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GridViewSubjectsAndLevels.EditIndex = e.NewEditIndex;
            Populate_GridViewSubjectsAndLevels();
        }
        protected void GridViewSubjectsAndLevels_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.GridViewSubjectsAndLevels.EditIndex = -1;
            Populate_GridViewSubjectsAndLevels();
        }
        protected void GridViewSubjectsAndLevels_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
            subjectsList = (SubjectsList)Session["subjectsList"];
            SubjectsLevels subjectsLevels = new SubjectsLevels();
            GridViewRow row = GridViewSubjectsAndLevels.Rows[e.RowIndex];
            subjectsLevels.SubjectID = subjectsLevelsService.GetSubjectIDBySubjectName(row.Cells[0].Text);
            subjectsLevels.LevelID = subjectsLevelsService.GetLevelIDByLevelName(row.Cells[1].Text);
            subjectsLevels.SubjectName = row.Cells[0].Text;
            subjectsLevels.LevelName = row.Cells[1].Text;
            int price = short.Parse(((TextBox)row.Cells[2].Controls[0]).Text);
            TeacherDetails teacherDetails = new TeacherDetails();
            teacherDetails = (TeacherDetails)Session["TeacherDetails"];
            subjectsList.UpdateSubjectLevelPrice(subjectsLevels, price, (string)Session["teacherID"]);
            Session["subjectsList"] = subjectsList;
            this.GridViewSubjectsAndLevels.EditIndex = -1;
            Populate_GridViewSubjectsAndLevels();
        }
        protected void GridViewSubjectsAndLevels_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
            subjectsList = (SubjectsList)Session["subjectsList"];
            SubjectsLevels subjectsLevels = new SubjectsLevels();
            GridViewRow row = GridViewSubjectsAndLevels.Rows[e.RowIndex];
            subjectsLevels.SubjectID = subjectsLevelsService.GetSubjectIDBySubjectName(row.Cells[0].Text);
            subjectsLevels.LevelID = subjectsLevelsService.GetLevelIDByLevelName(row.Cells[1].Text);
            TeacherDetails teacherDetails = new TeacherDetails();
            teacherDetails = (TeacherDetails)Session["TeacherDetails"];
            subjectsList.DeleteSubjectLevel(subjectsLevels, (string)Session["teacherID"]);
            Session["subjectsList"] = subjectsList;
            this.GridViewSubjectsAndLevels.EditIndex = -1;
            Populate_GridViewSubjectsAndLevels();
        }
        protected void ButtonSubjectsLevels_Click(object sender, EventArgs e)
        {
            // לואלה על הרשימה וכל רמה נבחרת להוסיף לרשימה
            subjectsList = (SubjectsList)Page.Session["subjectsList"];
            Subject subject = (Subject)Page.Session["subject"];
            SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
            for (int i = 0; i < this.CheckBoxListLevels.Items.Count; i++)
            {
                if (this.CheckBoxListLevels.Items[i].Selected)
                {
                    SubjectsLevels subjectsLevels = new SubjectsLevels();
                    subjectsLevels.SubjectID = subject.SubjectID;
                    subjectsLevels.SubjectName = subject.SubjectName;
                    subjectsLevels.LevelID = int.Parse(this.CheckBoxListLevels.Items[i].Value);
                    subjectsLevels.LevelName = subjectsLevelsService.GetLevelNameByLevelID(subjectsLevels.LevelID);
                    subjectsLevels.PricePerHour = 0;
                    subjectsList.InsertNewSubjectLevelTeacher(subjectsLevels, (string)Session["teacherID"]);
                    Page.Session["subjectsList"] = subjectsList;
                }
            }
            Populate_GridViewSubjectsAndLevels();
            this.LabelMeesageAddSUbjectAndLevels.Visible = false;
        }
        protected void ButtonAddSubjectsAndLevels_Click(object sender, EventArgs e)
        {
            TeacherService teacherService = new TeacherService();
            SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
            Subject subject = (Subject)Page.Session["subject"];
            subjectsList = (SubjectsList)Page.Session["subjectsList"];
            string teacherID = (string)Session["teacherID"];

            try
            {
                for (int i = 0; i < this.CheckBoxListLevels.Items.Count; i++)
                {
                    if (this.CheckBoxListLevels.Items[i].Selected)
                    {
                        SubjectsLevels subjectsLevels = new SubjectsLevels();
                        subjectsLevels.SubjectID = subject.SubjectID;
                        subjectsLevels.SubjectName = subject.SubjectName;
                        subjectsLevels.LevelID = int.Parse(this.CheckBoxListLevels.Items[i].Value);
                        subjectsLevels.LevelName = subjectsLevelsService.GetLevelNameByLevelID(subjectsLevels.LevelID);
                        subjectsLevels.PricePerHour = 0;
                        if (!subjectsLevelsService.CheckIfTeacherSubjectLevelExist(subjectsLevels, teacherID))
                        {
                            subjectsList.InsertNewSubjectLevelTeacher(subjectsLevels, (string)Session["teacherID"]);
                            Page.Session["subjectsList"] = subjectsList;
                        }
                    }
                }
                //teacherService.AddSubjectsAndLevelsTeacher(subjectsList, teacherID);
                this.LabelMeesageAddSUbjectAndLevels.Visible = true;
                this.LabelMeesageAddSUbjectAndLevels.Text = "הכנסת מקצועות ורמות הלימוד עברו בהצלחה";
                Populate_GridViewSubjectsAndLevels();
            }
            catch (Exception ex)
            {
                this.LabelMeesageAddSUbjectAndLevels.Visible = true;
                this.LabelMeesageAddSUbjectAndLevels.Text = "יש בעיה המקצועות והרמות לא נכנסו";
                throw ex;
            }
        }
    }
}