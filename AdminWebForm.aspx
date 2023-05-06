<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminWebForm.aspx.cs" Inherits="GuyProject.AdminWebForm" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            margin-right: 70px;
        }

        .auto-style4 {
            width: 400px;
        }

        .auto-style5 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Hello Admin</h1>
    <br />
    <br />
    <p>
        <table class="auto-style5">
            <tr>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBoxTeacherFirstName" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="LabelFirstName" runat="server" Text="הכנס שם פרטי:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:TextBox ID="TextBoxTeacherLastName" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="LabelLastName" runat="server" Text="הכנס שם משפחה:"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Button ID="ButtonSearchByName" runat="server" OnClick="ButtonSearchByName_Click" Text="חפש לפי שם" Width="200px" />
        <br />
        <asp:Button ID="ButtonShowTeachersArntApproved" runat="server" Text="הצג מורים שאינם מאושרים" OnClick="ButtonShowTeachersArntApproved_Click" Width="200px" />
        <br />
        <asp:Button ID="ButtonShowAllTeachers" runat="server" Text="הצג את כל המורים" OnClick="ButtonShowAllTeachers_Click" Width="200px" />
        <p>
            <asp:GridView ID="GridViewTeachersNotAprrove" runat="server" AutoGenerateColumns="False" CssClass="auto-style1" OnRowCancelingEdit="GridViewTeachersNotAprrove_RowCancelingEdit" OnRowEditing="GridViewTeachersNotAprrove_RowEditing" OnRowUpdating="GridViewTeachersNotAprrove_RowUpdating">
                <Columns>
                    <asp:BoundField DataField="TeacherID" HeaderText="תעודת זהות" ReadOnly="True" Visible="False" />
                    <asp:BoundField DataField="FirstName" HeaderText="שם פרטי" ReadOnly="True" />
                    <asp:BoundField DataField="LastName" HeaderText="שם משפחה" ReadOnly="True" />
                    <asp:BoundField DataField="Phone" HeaderText="מספר טלפון" ReadOnly="True" />
                    <asp:BoundField DataField="Gender" HeaderText="מגדר" ReadOnly="True" />
                    <asp:BoundField DataField="BirthDate" HeaderText="תאריך לידה" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" />
                    <asp:BoundField DataField="Address" HeaderText="כתובת" ReadOnly="True" Visible="False" />
                    <asp:BoundField DataField="CityName" HeaderText="עיר" ReadOnly="True" Visible="False" />
                    <asp:TemplateField HeaderText="סטטוס">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownListStatus" runat="server" AutoPostBack="True" Text='<%# Bind("Status") %>'>
                                <asp:ListItem Value="מאושר"></asp:ListItem>
                                <asp:ListItem Value="לא מאושר"></asp:ListItem>
                                <asp:ListItem Value="לא פעיל"></asp:ListItem>
                            </asp:DropDownList>
                        </EditItemTemplate>
                        <%-- <ItemTemplate>
                            <br />
                            <asp:DropDownList ID="DropDownListStatus" runat="server" Text='<%# Bind("Status") %>' AutoPostBack="True">
                                <asp:ListItem Value="מאושר"></asp:ListItem>
                                <asp:ListItem Value="לא מאושר"></asp:ListItem>
                                <asp:ListItem Value="לא פעיל"></asp:ListItem>
                            </asp:DropDownList>
                        </ItemTemplate>--%>
                    </asp:TemplateField>
                    <asp:BoundField DataField="LearnPlace" HeaderText="מקום למידה" ReadOnly="True" Visible="False" />
                    <asp:BoundField DataField="UserType" HeaderText="סוג משתמש" ReadOnly="True" />
                    <asp:BoundField DataField="Email" HeaderText="אימייל" ReadOnly="True" Visible="False" />
                    <asp:BoundField DataField="Description" HeaderText="תיאור" ReadOnly="True" />
                    <asp:ImageField DataImageUrlField="ImageTeacher" HeaderText="תמונה" ReadOnly="True">
                    </asp:ImageField>
                    <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
                </Columns>
            </asp:GridView>
        </p>
</asp:Content>
