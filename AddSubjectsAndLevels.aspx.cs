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
    public partial class AddSubjectsAndLevels : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["teacherID"] != "214777286")
            {
                Session["page"] = "AddSubjectsAndLevels.aspx";
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                PopulateDropDownListCity();
                PopulateDropDownListSubjects();
                this.LabelMeesage.Visible = false;
                this.LabelCityMessage.Visible = false;
            }
        }
        protected void PopulateDropDownListCity()
        {
            UserService userService = new UserService();
            DataSet dataSet = userService.GetCities();
            this.DropDownListCity.DataSource = dataSet.Tables["CitiesTbl"];
            this.DropDownListCity.DataTextField = "CityName";
            this.DropDownListCity.DataValueField = "CityID";
            this.DropDownListCity.DataBind();
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
        private DataSet GetData(int subID)
        {
            SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
            return subjectsLevelsService.GetAllLevelsNameAndSubNameBySubjectsID(subID);
        }
        private void PopulateGridView_SubjectsAndLevels(int subID)
        {
            this.GridViewSubjectsAndLevels.DataSource = GetData(subID);
            this.GridViewSubjectsAndLevels.DataBind();
        }
        protected void DropDownListSubjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateGridView_SubjectsAndLevels(int.Parse(this.DropDownListSubjects.SelectedValue));
        }
        protected void ButtonShowLevels_Click(object sender, EventArgs e)
        {
            PopulateGridView_SubjectsAndLevels(int.Parse(this.DropDownListSubjects.SelectedValue));
        }
        protected void ButtonInsertSubjectsLevels_Click(object sender, EventArgs e)
        {
            try
            {
                SubjectsLevels subjectsLevels = new SubjectsLevels();
                SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
                subjectsLevels.SubjectName = this.TextBoxSubject.Text;
                if (!subjectsLevelsService.CheckIfSubjectNameExist(subjectsLevels.SubjectName))
                {
                    subjectsLevelsService.InsertNewSubject(subjectsLevels.SubjectName);
                    subjectsLevels.SubjectID = subjectsLevelsService.GetSubjectIDBySubjectName(subjectsLevels.SubjectName);
                }
                else
                {
                    subjectsLevels.SubjectID = subjectsLevelsService.GetSubjectIDBySubjectName(subjectsLevels.SubjectName);
                }

                subjectsLevels.LevelName = this.TextBoxLevel.Text;
                if (!subjectsLevelsService.CheckIfLevelNameExist(subjectsLevels.LevelName))
                {
                    subjectsLevelsService.InsertNewLevel(subjectsLevels.LevelName);
                    subjectsLevels.LevelID = subjectsLevelsService.GetLevelIDByLevelName(subjectsLevels.LevelName);
                }
                else
                {
                    subjectsLevels.LevelID = subjectsLevelsService.GetLevelIDByLevelName(subjectsLevels.LevelName);
                }
                //לבדוק איך אפשר לאפשר בדיקה שהרמה וגם המצוע לא נמצאים כבר בקשרי הגומלין מבלי ליצור קשר כפול
                DataSet ds = new DataSet();
                ds = subjectsLevelsService.GetSubjectsAndLevels();
                DataView dataView = new DataView();
                dataView = ds.Tables["SubjectAndLevelTbl"].DefaultView;
                dataView.RowFilter = "SubjectID = '" + subjectsLevels.SubjectID + "'";
                dataView.RowFilter += "AND LevelID = '" + subjectsLevels.LevelID + "'";
                
                int count = dataView.Count;
                if (dataView.Count == 0)
                {
                    subjectsLevelsService.InsertNewSubjectLevel(subjectsLevels.SubjectID, subjectsLevels.LevelID);
                    this.LabelMeesage.Visible = true;
                    this.LabelMeesage.Text = "הוספת המקצוע עברה בהצלחה";
                    PopulateGridView_SubjectsAndLevels(int.Parse(this.DropDownListSubjects.SelectedValue));
                }
                else
                {
                    this.LabelMeesage.Visible = true;
                    this.LabelMeesage.Text = "מקצוע זה ורמת לימוד זאת קיימים במערכת - ולכן לא התווספו";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void ButtonAddCity_Click(object sender, EventArgs e)
        {
            try
            {
                UserService userService = new UserService();
                if (!userService.CheckIfCityNameExist(this.TextBoxCity.Text))
                {
                    userService.InsertNewCityName(this.TextBoxCity.Text);
                    PopulateDropDownListCity();
                    this.LabelCityMessage.Visible = true;
                    this.LabelCityMessage.Text = "הכנסת הערים עברה בהצלחה";
                }
                else
                {
                    this.LabelCityMessage.Visible = true;
                    this.LabelCityMessage.Text = "מקצוע זה נמצא במאגר";
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}