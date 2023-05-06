<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delete.aspx.cs" Inherits="GuyProject.Delete" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 99%;
        }

        .auto-style3 {
            width: 520px;
        }

        .auto-style4 {
            width: 445px;
        }
    </style>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <p>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="LabelArticle" runat="server" Text="מחיקת משתמש"></asp:Label>
        &nbsp;<br />
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        &nbsp;
    </p>
    <p>
        &nbsp;
    </p>
    <table class="auto-style1">
        <tr>
            <td class="auto-style4">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserID" runat="server" ControlToValidate="TextBoxUserID" ErrorMessage="הכנס תעודת זהות"></asp:RequiredFieldValidator>
            </td>
            <td class="auto-style3">
                <asp:TextBox runat="server" ID="TextBoxUserID" />
            </td>
            <td>
                <asp:Label ID="LabelUserID" runat="server" Text=":הכנס תעודת זהות למחיקת משתמש או שינוי סטטוס"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorStatus" runat="server" ControlToValidate="TextBoxStatus" ErrorMessage="הכנס סטטוס"></asp:RequiredFieldValidator>
            </td>
            <td class="auto-style3">
                <asp:TextBox ID="TextBoxStatus" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="LabelStatus" runat="server" Text=":הכנס סטטוס עבור במשתמש שאתה רוצה למחוק"></asp:Label>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;
    </p>
    <p>
        <asp:Label ID="LabelUserMeesage" runat="server" Text="Label"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="ButtonDelete" runat="server" Text="מחק משתמש" OnClick="ButtonDelete_Click" />
    </p>
</asp:Content>
