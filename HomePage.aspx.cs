using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using GuyProject.App_Code;
using System.Windows.Forms;
using System.Web.Services;

namespace GuyProject
{
    public partial class HomePage : System.Web.UI.Page
    {
        [System.ComponentModel.Browsable(true)]
        public System.Windows.Forms.AutoCompleteMode AutoCompleteMode { get; set; }
        public System.Windows.Forms.AutoCompleteStringCollection AutoCompleteCustomSource { get; set; }
        public System.Windows.Forms.AutoCompleteSource AutoCompleteSource { get; set; }

        SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //this.TextBoxSubject.Attributes.Add("AutoCompleteSource", "on");
                ////Enable AutoComplete on the TextBox control
                //this.TextBoxSubject.AutoCompleteMode = AutoCompleteMode.Suggest;
                //this.TextBoxSubject.AutoCompleteSource = AutoCompleteSource.CustomSource;

                //// Populate the AutoCompleteCustomSource with data
                //UserService userService = new UserService();
                //DataSet ds = userService.GetCities();

                //var autoFillList = (from data in ds.Tables[0].AsEnumerable()
                //                    select data.Field<string>("Name")).ToArray();

                //this.TextBoxSubject.AutoCompleteCustomSource.AddRange(autoFillList);
                //DataSet dataSet = subjectsLevelsService.GetSubjectsAndLevels();
                //DataView dataView = dataSet.Tables["SubjectAndLevelTbl"].DefaultView;
                //dataView.RowFilter = "SubjectName LIKE '%" + this.TextBoxSubject.Text + "%'";

                //List<string> myList = new List<string>();

                //foreach (DataRowView rowView in dataView)
                //{
                //    string myString = rowView["SubjectName"].ToString();
                //    myList.Add(myString);
                //}

                //string options = string.Join(",", myList.Select(s => "'" + s + "'"));
                //string script = $"document.getElementById('{TextBoxSubject.ClientID}').setAttribute('list', '{TextBoxSubject.ClientID}_list'); var datalist = document.createElement('datalist'); datalist.id = '{TextBoxSubject.ClientID}_list'; datalist.innerHTML = '<option value=\"{options}\"></option>'; document.body.appendChild(datalist);";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "addOptionsScript", script, true);
                GetDataSourceForAutoComplete(this.TextBoxSubject.Text);
                GetSubjects();
                GetCityName();
                if (Session["teacherID"] != null)
                {
                    Populate_DataListWithoutTeacher();
                }
                else
                {
                    Populate_DataList();
                }
            }
        }

        private DataSet GetDataWithoutTeacherDetails()
        {
            TeacherService teacherService = new TeacherService();
            DataSet ds = teacherService.GetTeachersWithoutMe((string)Session["teacherID"]);
            return ds;
        }
        protected void Populate_DataListWithoutTeacher()
        {
            this.DataListTeachers.DataSource = GetDataWithoutTeacherDetails();
            this.DataListTeachers.DataBind();

        }
        private DataSet GetDataTeachers()
        {
            TeacherService teacherService = new TeacherService();
            DataSet dataSet = teacherService.GetAllTeachersWithDistinct();
            return dataSet;
        }
        private void Populate_DataList()
        {
            TeacherService teacherService = new TeacherService();
            DataSet dataSetTeachersDetails = new DataSet();
            dataSetTeachersDetails = teacherService.GetAllTeachersWithDistinct();
            DataView dataView = dataSetTeachersDetails.Tables["TeacherTbl"].DefaultView;
            Session["dataView"] = null;
            string status = "מאושר ";
            dataView.RowFilter = "Status = '" + status + "'";
            this.DataListTeachers.DataSource = dataView;
            this.DataListTeachers.DataBind();
        }
        protected void GetSubjects()
        {
            DataSet dataSet = subjectsLevelsService.GetSubjectsAndLevels();
            this.DropDownListSubjectsAndLevel.DataSource = dataSet.Tables["SubjectAndLevelTbl"];
            this.DropDownListSubjectsAndLevel.DataTextField = "SubjectName";
            this.DropDownListSubjectsAndLevel.DataValueField = "SubjectName";

            this.DropDownListSubjectsAndLevel.DataBind();
        }
        protected void GetCityName()
        {
            UserService userService = new UserService();
            DataSet dataSet = userService.GetCities();
            DropDownListArea.DataSource = dataSet.Tables["CitiesTbl"];
            DropDownListArea.DataTextField = "CityName";
            DropDownListArea.DataValueField = "CityName";
            DropDownListArea.DataBind();
            this.DropDownListArea.Items.Add("בחר עיר");
            this.DropDownListArea.SelectedValue = "בחר עיר";
        }
        //protected void TextBoxCities_TextChanged(object sender, EventArgs e)
        //{
        //    // retrieve the text entered in the text box
        //    string searchText = this.TextBoxCities.Text;
        //    UserService userService = new UserService();
        //    DataSet ds = userService.GetCities();
        //    this.TextBoxCities.AutoCompleteType = AutoCompleteType.None;

        //    // query the dataset for matching data
        //    var query = from data in ds.Tables[0].AsEnumerable()
        //                where data.Field<string>("Name").StartsWith(searchText)
        //                select data.Field<string>("Name");

        //    // populate a list of matching data items
        //    List<string> autoFillList = query.ToList();

        //    // assign the list to the "AutoCompleteSource" property of the text box
        //    this.TextBoxCities.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //    this.TextBoxCities.AutoCompleteCustomSource.AddRange(autoFillList.ToArray());
        //    this.TextBoxCities.AutoCompleteMode = AutoCompleteMode.Suggest;
        //}
        protected void DataListTeachers_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "ShowTeacher")
            {
                int rowindex = e.Item.ItemIndex;
                string teacherID = "";
                if (rowindex < this.DataListTeachers.DataKeys.Count)
                {
                    teacherID = this.DataListTeachers.DataKeys[rowindex].ToString();
                }
                Session["teacherID"] = teacherID;
                if (Session["studentID"] == null)
                {
                    Session["page"] = "HomePage.aspx";
                    Response.Redirect("Login.aspx");
                }
                Response.Redirect("SetLesson.aspx");
            }
        }
        protected void ButtonFind_Click(object sender, EventArgs e)
        {
            try
            {
                TeacherService teacherService = new TeacherService();
                DataSet dataSet = new DataSet();
                if (Session["teacherID"] != null)
                {
                    dataSet = GetDataWithoutTeacherDetails();
                }
                else
                {
                    dataSet = GetDataTeachers();
                }
                DataView dataView = dataSet.Tables["TeacherTbl"].DefaultView;
                Session["dataView"] = null;
                string status = "מאושר ";
                dataView.RowFilter = "Status = '" + status + "'";
                if (this.DropDownListSubjectsAndLevel.Text != "בחר מקצוע")
                {
                    DataSet ds = new DataSet();
                    DataView view = new DataView();
                    int subjectID = (int)Session["SubjectID"];
                    int levelID = (int)Session["LevelID"];
                    ds = teacherService.GetAllTeachersBySubjectNameAndLevelName(status, subjectID, levelID);
                    view = ds.Tables["TeacherTbl"].DefaultView;
                    view.RowFilter = "Status = '" + status + "'";
                    if (this.DropDownListArea.SelectedValue != "בחר עיר")
                    {
                        string citiesSelectedValue = this.DropDownListArea.SelectedValue;
                        view.RowFilter = "And CityName = '" + citiesSelectedValue + "'";
                    }
                    if (this.TextBoxTeacherFirstName.Text != "")
                    {
                        string firstname = this.TextBoxTeacherFirstName.Text;
                        view.RowFilter += "And FirstName = '" + firstname + "'";
                    }
                    if (this.TextBoxTeacherLastName.Text != "")
                    {
                        string lastname = this.TextBoxTeacherLastName.Text;
                        view.RowFilter += "And LastName = '" + lastname + "'";
                    }
                    this.DataListTeachers.DataSource = view;
                    //this.DataListTeachers.DataBind();

                }
                else
                {
                    if (this.DropDownListArea.SelectedValue != "בחר עיר")
                    {
                        string citiesSelectedValue = this.DropDownListArea.SelectedValue;
                        dataView.RowFilter += "And CityName = '" + citiesSelectedValue + "'";
                    }
                    this.DataListTeachers.DataSource = dataView;
                    //this.DataListTeachers.DataBind();
                }


                if (this.TextBoxTeacherFirstName.Text != "" && this.DropDownListSubjectsAndLevel.Text == "בחר מקצוע")
                {
                    string firstname = this.TextBoxTeacherFirstName.Text;
                    dataView.RowFilter += "And FirstName = '" + firstname + "'";
                    this.DataListTeachers.DataSource = dataView;
                }
                if (this.TextBoxTeacherLastName.Text != "" && this.DropDownListSubjectsAndLevel.Text == "בחר מקצוע")
                {
                    string lastname = this.TextBoxTeacherLastName.Text;
                    dataView.RowFilter += "And LastName = '" + lastname + "'";
                    this.DataListTeachers.DataSource = dataView;
                }

                this.DataListTeachers.DataBind();
                if ((this.DropDownListArea.SelectedValue == "בחר עיר") && (this.DropDownListSubjectsAndLevel.SelectedValue == "בחר מקצוע") && (this.TextBoxTeacherFirstName.Text == "") && (this.TextBoxTeacherLastName.Text == ""))
                {
                    Populate_DataList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void DataListTeachers_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                // להגיע לשורה בדאטה ליסט 
                int rowIndex = e.Item.ItemIndex;
                string teacherID = DataBinder.Eval(e.Item.DataItem, "TeacherID").ToString();
                TeacherService teacherService = new TeacherService();
                GridView gridview = ((GridView)(e.Item.FindControl("GridViewSubjectsAndLevels")));
                gridview.DataSource = teacherService.GetTeacherWithSubjects(teacherID);
                gridview.DataBind();
            }
        }
        protected void DropDownListSubjectsAndLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Get the selected value from the dropdownlist
            string selectedValue = this.DropDownListSubjectsAndLevel.SelectedValue;
            DataSet ds = new DataSet();
            ds = subjectsLevelsService.GetSubjectsAndLevels();
            DataTable table = ds.Tables["SubjectAndLevelTbl"];
            this.DropDownListSubjectsAndLevel.DataSource = table;

            //DataRow dataRow = GetSelectedRow(selectedValue); דרך למצוא את השורה שנבחרה
            // Get the DataRow that corresponds to the selected value
            DataRow selectedRow = ((DataTable)DropDownListSubjectsAndLevel.DataSource).Rows.Find(selectedValue);

            // Create a new dataset with the same schema as the original dataset
            DataSet newDataset = new DataSet();
            DataTable newTable = selectedRow.Table.Clone();
            newDataset.Tables.Add(newTable);

            // Add a new row to the new dataset with the SubjectID and LevelID values
            DataRow newRow = newTable.NewRow();
            newRow["SubjectName"] = selectedRow["SubjectName"];
            newRow["SubjectID"] = selectedRow["SubjectID"];
            newRow["LevelID"] = selectedRow["LevelID"];
            newTable.Rows.Add(newRow);

            int subjectid = int.Parse(newTable.Rows[0][1].ToString());
            int levelid = int.Parse(newTable.Rows[0][2].ToString());
            Session["SubjectID"] = subjectid;
            Session["LevelID"] = levelid;
        }
        protected DataRow GetSelectedRow(string selectedValue)
        {
            // Find the row with the selected SubjectName value
            DataSet ds = new DataSet();
            ds = subjectsLevelsService.GetSubjectsAndLevels();
            DataTable table = ds.Tables["SubjectAndLevelTbl"];
            this.DropDownListSubjectsAndLevel.DataSource = table;
            DataRow[] selectedRows = table.Select($"SubjectName = '{selectedValue}'");

            if (selectedRows.Length > 0)
            {
                // Get the SubjectID and LevelID values from the selected row
                string subjectName = selectedRows[0]["SubjectName"].ToString();
                string subjectID = selectedRows[0]["SubjectID"].ToString();
                string levelID = selectedRows[0]["LevelID"].ToString();

                // Find the row with the matching SubjectID and LevelID values
                DataRow selectedRow = table.Rows.Find(new object[] { subjectName });
                return selectedRow;
            }
            else
            {
                return null;
            }
        }

        [WebMethod]
        protected List<string> GetDataSourceForAutoComplete(string subjectLevelsname)
        {
            subjectLevelsname = this.TextBoxSubject.Text;
            List<string> datalist = new List<string>();
            DataSet ds = new DataSet();
            SubjectsLevelsService subjectsLevelsService = new SubjectsLevelsService();
            ds = subjectsLevelsService.GetSubjectsAndLevels();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                datalist.Add(row["SubjectName"].ToString());
            }

            List<string> subjectLevelName = new List<string>();
            foreach (string str in datalist)
            {
                if (str != "" && str.Contains(subjectLevelsname))
                {
                    subjectLevelName.Add(str);
                }
            }

            return subjectLevelName;
        }
        protected void TextBoxSubject_TextChanged(object sender, EventArgs e)
        {
            string text = TextBoxSubject.Text;
            string script = $"alert('The current text is: {text}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertScript", script, true);
        }
    }
}

