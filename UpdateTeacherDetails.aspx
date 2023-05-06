<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateTeacherDetails.aspx.cs" Inherits="GuyProject.UpdateTeacherDetails" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style5 {
            height: 28px;
        }

        .auto-style6 {
            width: 450px;
        }

        .auto-style7 {
            width: 450px;
            height: 28px;
        }

        .auto-style10 {
            margin-left: 3px;
        }

        .auto-style11 {
            width: 400px;
        }

        .auto-style12 {
            width: 400px;
            height: 28px;
        }

        .auto-style15 {
            width: 1045px;
        }

        .auto-style16 {
            width: 1000px;
        }

        #ImageTeacher {
            float: right;
            direction: rtl;
        }

    </style>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>UpdateTeacherDetails</h1>
    <br />
    <center>
        <asp:Label ID="LabelFirstMeesage" runat="server" Text="ברוכים הבאים"></asp:Label>
    </center>
    <br />
    <br />
    <asp:Image ID="ImageTeacher" runat="server" Height="150px" Width="150px" />
    <table class="auto-style1">
        <tr>
            <td class="auto-style11">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLearnPlace" runat="server" ControlToValidate="DropDownListlearnPlace" ErrorMessage="בחר את המקום ממנו תרצה ללמד"></asp:RequiredFieldValidator>
            </td>
            <td class="auto-style6">
                <asp:DropDownList ID="DropDownListlearnPlace" runat="server" CssClass="auto-style10">
                    <asp:ListItem>בחר מקום</asp:ListItem>
                    <asp:ListItem>מבית המורה</asp:ListItem>
                    <asp:ListItem>מבית התלמיד</asp:ListItem>
                    <asp:ListItem>מרחוק</asp:ListItem>
                    <asp:ListItem>לבחירת התלמיד</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="LabelLernPlace" runat="server" Text=":בחר את המקום ממנו תרצה להעביר את השיעורים"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style12">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorDescription" runat="server" ControlToValidate="TextBoxDescription" ErrorMessage="מלא את השדה הזה"></asp:RequiredFieldValidator>
            </td>
            <td class="auto-style7">
                <asp:TextBox ID="TextBoxDescription" runat="server" Height="100px" TextMode="MultiLine" Width="430px"></asp:TextBox>
            </td>
            <td class="auto-style5">
                <asp:Label ID="LabelLearnPlace" runat="server" Text=":ספר לנו קצת עליך "></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style11">&nbsp;</td>
            <td class="auto-style6">
                <asp:FileUpload ID="FileUploadImage" runat="server" Width="200px" />
                <br />
                <asp:Button ID="ButtonUploadFile" runat="server" OnClick="ButtonUploadFile_Click" Text="שנה תמונה" />
                <br />
                <asp:Label ID="LabelMeesageImage" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LabelImage" runat="server" Text=":תמונה"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style11">
                <asp:Label ID="LabelMeesage" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="auto-style6">
                <asp:Button ID="ButtonUpdate" runat="server" OnClick="ButtonUpdate_Click" Text="עדכן פרטים" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <br />
    <br />
  <%--  <table class="auto-style1">
        <tr>
            <td class="auto-style16">
                <asp:CheckBoxList ID="CheckBoxListLevels" runat="server">
                </asp:CheckBoxList>
            </td>
            <td class="auto-style15">
                <asp:DropDownList ID="DropDownListSubjects" runat="server" OnSelectedIndexChanged="DropDownListSubjects_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="ButtonSubjectsLevels" runat="server" Text="הראה רמות לימוד לקצוע" OnClick="ButtonSubjectsLevels_Click" />
            </td>
        </tr>
        <tr>
            <td class="auto-style16">
                <asp:Label ID="LabelMeesageAddSUbjectAndLevels" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="auto-style15">&nbsp;</td>
            <td>
                <asp:Button ID="ButtonAddSubjectsAndLevels" runat="server" Text="הוספת מקצועות ורמות לימוד למורה" OnClick="ButtonAddSubjectsAndLevels_Click" />
            </td>
        </tr>
    </table>
    <div align="center">
        <asp:GridView ID="GridViewSubjectsAndLevels" runat="server" AutoGenerateColumns="False" Height="150px" Width="400px" OnRowCancelingEdit="GridViewSubjectsAndLevels_RowCancelingEdit" OnRowEditing="GridViewSubjectsAndLevels_RowEditing" OnRowUpdating="GridViewSubjectsAndLevels_RowUpdating" OnRowDeleting="GridViewSubjectsAndLevels_RowDeleting">
            <Columns>
                <asp:BoundField HeaderText="רמת לימוד" DataField="LevelName" ReadOnly="True" />
                <asp:BoundField DataField="SubjectName" HeaderText="מקצוע לימוד" ReadOnly="True" />
                <asp:BoundField HeaderText="מחיר לשעה" DataField="PricePerHour" />
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
            </Columns>
        </asp:GridView>
    </div>
    <br />
    <br />
    <br />--%>
</asp:Content>
