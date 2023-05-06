<%@ Page Title="" Language="C#" MasterPageFile="~/WebServiceMasterPage.Master" AutoEventWireup="true" CodeBehind="Bills.aspx.cs" Inherits="GuyProject.Bills" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 80%;
        }

        .auto-style4 {
            height: 50px;
            direction: rtl;
        }

        .auto-style5 {
            width: 300px;
            height: 50px;
        }

        .auto-style6 {
            width: 300px;
        }
        .auto-style7 {
            width: 80%;
        }
        .auto-style8 {
        width: 400px;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContent" runat="server">
    <h1>Bills</h1>
    <br />
    <br />
    <center>
        <h4>ברוך הבא לדף תשלום חשבונות הכנס את השדות הבאים ובצע את התשלום</h4>

        <br />
        <table class="auto-style1">
            <tr>
                <td class="auto-style6">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhoneGet" runat="server" ControlToValidate="TextBoxPhoneGet" ErrorMessage="הכנס מספר טלפון"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorPhoneGet" runat="server" ControlToValidate="TextBoxPhoneGet" ErrorMessage="מספר הטלפון אינו תקין" ValidationExpression="^05\d{8}$"></asp:RegularExpressionValidator>
                </td>
                <td class="auto-style6">
                    <asp:TextBox ID="TextBoxPhoneGet" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:Label ID="Label2" runat="server" Text="הכנס את מספר הטלפון שתרמה להעביר לו תשלום:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style5">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAmount" runat="server" ControlToValidate="TextBoxAmount" ErrorMessage="הכנס סכום"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorAmount" runat="server" ControlToValidate="TextBoxAmount" ErrorMessage="סכום זה לא תקין" ValidationExpression="[0-9]+"></asp:RegularExpressionValidator>
                </td>
                <td class="auto-style5">
                    <asp:TextBox ID="TextBoxAmount" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:Label ID="Label3" runat="server" Text="הכנס סכום:"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPayee" runat="server" ControlToValidate="TextBoxPayee" ErrorMessage="הכנס סיבה"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style6">
                    <asp:TextBox ID="TextBoxPayee" runat="server" TextMode="MultiLine"></asp:TextBox>
                </td>
                <td class="auto-style4">
                    <asp:Label ID="Label4" runat="server" Text="הכנס סיבה להעברה:"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="auto-style7">
            <tr>
                <td class="auto-style8">
                    <asp:Label ID="LabelMessage" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="ButtonPay" runat="server" OnClick="ButtonPay_Click" Text="שלם " />
                </td>
            </tr>
        </table>
        <br />

    </center>
</asp:Content>
