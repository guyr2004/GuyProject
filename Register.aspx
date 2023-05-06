<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="GuyProject.Register1" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    Register
</asp:Content>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 528px;
            margin-bottom: 7px;
        }

        .auto-style2 {
            width: 502px;
        }

        .auto-style3 {
            width: 470px;
        }

        .auto-style4 {
            width: 100%;
        }

        .auto-style5 {
            width: 540px;
        }

        .auto-style6 {
            width: 470px;
            height: 35px;
        }

        .auto-style7 {
            width: 502px;
            height: 35px;
        }

        .auto-style8 {
            height: 35px;
        }

        .auto-style9 {
            width: 470px;
            height: 31px;
        }

        .auto-style10 {
            width: 502px;
            height: 31px;
        }

        .auto-style11 {
            height: 31px;
        }

        .auto-style12 {
            width: 470px;
            height: 37px;
        }

        .auto-style13 {
            width: 502px;
            height: 37px;
        }

        .auto-style14 {
            height: 37px;
        }
    </style>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Register</h1>
    <div>
        <center>
            <asp:Label ID="LabelFirstMessage" runat="server" Height="30px" Text="הרשמה" Width="50px"></asp:Label>
        </center>
        <table class="auto-style1">
            <tr>
                <td class="auto-style6">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserID" runat="server" ControlToValidate="TextBoxUserID" ErrorMessage="תעודת זהות צריכה להכיל 9 ספרות" ValidationGroup="group1"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CustomValidator ID="CustomValidatorId" runat="server" ControlToValidate="TextBoxUserID" ErrorMessage="תעודת זהות לא תקינה" OnServerValidate="CustomValidatorId_ServerValidate" ValidationGroup="group1"></asp:CustomValidator>
                    <br />
                </td>
                <td class="auto-style7">
                    <asp:TextBox ID="TextBoxUserID" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style8">
                    <asp:Label ID="LabelUserID" runat="server" Text=":הכנס תעודת זהות"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxFirstName" ErrorMessage="הכנס שם פרטי" ValidationGroup="group1"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style10">
                    <asp:TextBox ID="TextBoxFirstName" runat="server"></asp:TextBox>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="LabelFirstName" runat="server" Text=":הכנס שם פרטי"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxLastName" ErrorMessage="הכנס שם משפחה" ValidationGroup="group1"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBoxLastName" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="LabelLastName" runat="server" Text=":הכנס שם משפחה"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style12">
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorMonths" runat="server" ErrorMessage="בחר חודש" ControlToValidate="DropDownListMonths" ValidationGroup="group1"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorDays" runat="server" ErrorMessage="בחר יום" ControlToValidate="DropDownListDays" ValidationGroup="group1"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style13">
                    <asp:DropDownList ID="DropDownListYears" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListYears_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;<asp:DropDownList ID="DropDownListMonths" runat="server" OnSelectedIndexChanged="DropDownListMonths_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                    &nbsp;<asp:DropDownList ID="DropDownListDays" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="auto-style14">
                    <asp:Label ID="LabelBirthDate" runat="server" Text=":הכנס תאריך לידה"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="TextBoxPhone" ErrorMessage="מספר טלפון מורכב מ-10 מספרים" ValidationExpression="05\d{8}" ValidationGroup="group1"></asp:RegularExpressionValidator>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorphone" runat="server" ControlToValidate="TextBoxPhone" ErrorMessage="הכנס מספר טלפון" ValidationGroup="group1"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBoxPhone" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="LabelPhone" runat="server" Text=":הכנס מספר טלפון"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style6">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorGender" runat="server" ControlToValidate="RadioButtonListGender" ErrorMessage="בחר מגדר" ValidationGroup="group1"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style7">
                    <asp:RadioButtonList ID="RadioButtonListGender" runat="server" Width="81px">
                        <asp:ListItem>זכר</asp:ListItem>
                        <asp:ListItem>נקבה</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td class="auto-style8">
                    <asp:Label ID="LabelGender" runat="server" Text=":הכנס מגדר"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorAddress" runat="server" ControlToValidate="TextBoxAddress" ErrorMessage="הכנס כתובת מגורים" ValidationGroup="group1"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBoxAddress" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="LabelAddress" runat="server" Text=":הכנס כתובת"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorCityID" runat="server" ControlToValidate="DropDownListCityID" ErrorMessage="בחר עיר" ValidationGroup="group1"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CustomValidator ID="CustomValidatorCityID" runat="server" ControlToValidate="DropDownListCityID" ErrorMessage="בחר עיר" OnServerValidate="CustomValidatorCityID_ServerValidate" ValidationGroup="group1"></asp:CustomValidator>
                </td>
                <td class="auto-style2">
                    <asp:DropDownList ID="DropDownListCityID" runat="server">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="LabelCityID" runat="server" Text=":בחר עיר"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:CustomValidator ID="CustomValidatorKindUser" runat="server" ControlToValidate="DropDownListKindUser" ErrorMessage="בחר סוג" OnServerValidate="CustomValidatorKindUser_ServerValidate" ValidationGroup="group1"></asp:CustomValidator>
                </td>
                <td class="auto-style2">
                    <asp:DropDownList ID="DropDownListKindUser" runat="server">
                        <asp:ListItem>בחר סוג</asp:ListItem>
                        <asp:ListItem>מורה</asp:ListItem>
                        <asp:ListItem>תלמיד</asp:ListItem>
                        <asp:ListItem>שניהם</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="LabelUserType" runat="server" Text=":בחר סוג משתמש"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorEmail" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="כתובת האימייל לא עומדת בתנאים הנדרשים" ValidationExpression="\w+\@\w+\.com" ValidationGroup="group1"></asp:RegularExpressionValidator>
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorEmail" runat="server" ControlToValidate="TextBoxEmail" ErrorMessage="הכנס אימייל" ValidationGroup="group1"></asp:RequiredFieldValidator>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBoxEmail" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="LabelEmail" runat="server" Text=":הכנס אימייל"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorUserPassword" runat="server" ControlToValidate="TextBoxUserPassword" ErrorMessage="הכנס סיסמא" ValidationGroup="group1"></asp:RequiredFieldValidator>
                    <br />
                    <asp:RegularExpressionValidator ID="RegularExpressionValidatorUserPassword" runat="server" ControlToValidate="TextBoxUserPassword" ErrorMessage="סיסמא צריכה להכיל לפחות 6 תווים" ValidationExpression="\w{6,}" ValidationGroup="group1"></asp:RegularExpressionValidator>
                </td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBoxUserPassword" runat="server" TextMode="Password"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="LabelUserPassword" runat="server" Text=":הכנס סיסמא"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table class="auto-style4">
            <tr>
                <td class="auto-style5">
                    <asp:Label ID="LabelMessage" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="ButtonRegister" runat="server" OnClick="ButtonRegister_Click" Text="הירשם" Width="100px" />
                    <br />
                    <asp:Button ID="ButtonTeacherRegistration" runat="server" OnClick="ButtonTeacherRegistration_Click" Text="הירשם כמורה" Width="100px" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
