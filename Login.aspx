<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="GuyProject.Login" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    Login
</asp:Content>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            margin-left: 10px;
        }

        .auto-style4 {
            margin-left: 0px;
        }

        .auto-style8 {
            width: 520px;
        }

        .auto-style11 {
            width: 560px;
        }

        .auto-style12 {
            width: 540px;
        }
    </style>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Login</h1>
    <br />
        <center>
            <asp:Label ID="LabelFirstMessage" runat="server" Text="Login"></asp:Label>
        </center>
    <br />  
    <table class="auto-style1">
        <tr>
            <td class="auto-style8">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserID" runat="server" ControlToValidate="TextBoxUserID" ErrorMessage="הכנס תעודת זהות"></asp:RequiredFieldValidator>
            </td>
            <td class="auto-style11">
                <asp:TextBox ID="TextBoxUserID" runat="server" CssClass="auto-style2"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="LabelUserID" runat="server" Text="הכנס תעודת זהות"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserPassword" runat="server" ControlToValidate="TextBoxUserPassword" ErrorMessage="הכנס סימא"></asp:RequiredFieldValidator>
            </td>
            <td class="auto-style11">
                <asp:TextBox ID="TextBoxUserPassword" runat="server" CssClass="auto-style2"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="Label2" runat="server" Text="הכנס סיסמא"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <table class="auto-style1">
        <tr>
            <td class="auto-style12">
                <asp:Label ID="LabeTextMesage" runat="server" Text="LabeTextMesage"></asp:Label>
            </td>
            <td>
                <asp:Button ID="ButtonLogin" runat="server" CssClass="auto-style4" Text="התחבר" OnClick="ButtonLogin_Click" Width="120px" />
                <br />
                <asp:Button ID="ButtonUpdate" runat="server" OnClick="ButtonUpdate_Click" Text="שנה פרטי משתמש" Width="163px" />
                <br />
                <asp:Button ID="ButtonUpdateTeacherDetails" runat="server" OnClick="ButtonUpdateTeacherDetails_Click" Text="שנה פרטי מורה" Width="163px" />
            </td>
        </tr>
    </table>
</asp:Content>
