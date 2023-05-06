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
    public partial class AdminWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((string)Session["teacherID"] != "214777286")
            {
                Session["page"] = "AdminWebForm.aspx";
                Response.Redirect("Login.aspx");
            }
            if (!Page.IsPostBack)
            {
                PopulateGridView();
            }
        }

        protected DataSet GetData()
        {
            TeacherService teacherService = new TeacherService();
            return teacherService.GetAllTeachersDataFromUsersAndTeachersTbl();
        }
        protected void PopulateGridView()
        {
            this.GridViewTeachersNotAprrove.DataSource = GetData();
            this.GridViewTeachersNotAprrove.DataBind();
        }
        protected void ButtonShowTeachersArntApproved_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = GetData();
            DataView dataView = ds.Tables["UsersTbl"].DefaultView;
            Session["dataView"] = null;
            string status = "לא מאושר";
            dataView.RowFilter = "Status LIKE '%" + status + "%'";
            this.GridViewTeachersNotAprrove.DataSource = dataView;
            this.GridViewTeachersNotAprrove.DataBind();
        }
        protected void ButtonSearchByName_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = GetData();
            DataView dataView = ds.Tables["UsersTbl"].DefaultView;
            if (this.TextBoxTeacherFirstName.Text != "")
            {
                string firstname = this.TextBoxTeacherFirstName.Text;
                dataView.RowFilter = "FirstName = '" + firstname + "'";
                if (this.TextBoxTeacherLastName.Text != "")
                {
                    string lastname = this.TextBoxTeacherLastName.Text;
                    dataView.RowFilter += "And LastName = '" + lastname + "'";
                }
            }
            if (this.TextBoxTeacherLastName.Text != "")
            {
                string lastname = this.TextBoxTeacherLastName.Text;
                dataView.RowFilter = "LastName = '" + lastname + "'";
                if (this.TextBoxTeacherFirstName.Text != "")
                {
                    string firstname = this.TextBoxTeacherFirstName.Text;
                    dataView.RowFilter += "And FirstName = '" + firstname + "'";
                }
            }
            this.GridViewTeachersNotAprrove.DataSource = dataView;
            this.GridViewTeachersNotAprrove.DataBind();
        }
        protected void ButtonShowAllTeachers_Click(object sender, EventArgs e)
        {
            PopulateGridView();
        }
        protected void GridViewTeachersNotAprrove_RowEditing(object sender, GridViewEditEventArgs e)
        {
            this.GridViewTeachersNotAprrove.EditIndex = e.NewEditIndex;
            PopulateGridView();
        }
        protected void GridViewTeachersNotAprrove_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            this.GridViewTeachersNotAprrove.EditIndex = -1;
            PopulateGridView();
        }
        protected void GridViewTeachersNotAprrove_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                TeacherDetails teacherDetails = new TeacherDetails();
                teacherDetails.TeacherID = this.GridViewTeachersNotAprrove.Rows[e.RowIndex].Cells[0].Text;
                teacherDetails.Status = ((DropDownList)(GridViewTeachersNotAprrove.Rows[e.RowIndex].Cells[8].FindControl("DropDownListStatus"))).SelectedValue;

                TeacherService teacherService = new TeacherService();
                teacherService.UpdateTeacherStatus(teacherDetails.TeacherID, teacherDetails.Status);

                this.GridViewTeachersNotAprrove.EditIndex = -1;
                PopulateGridView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}