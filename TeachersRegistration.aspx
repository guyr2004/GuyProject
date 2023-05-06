<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TeachersRegistration.aspx.cs" Inherits="GuyProject.TeachersRegistration" %>


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
            width: 460px;
        }

        .auto-style12 {
            width: 460px;
            height: 28px;
        }

        .auto-style15 {
            width: 1045px;
        }

        .auto-style16 {
            width: 1000px;
        }
    </style>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>TeacherRegistration</h1>
    <br />
    <center>
        <asp:Label ID="LabelFirstMeesage" runat="server" Text="ברוכים הבאים"></asp:Label>
    </center>
    <br />
    <br />
    <br />
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
                <asp:Button ID="ButtonUploadFile" runat="server" OnClick="ButtonUploadFile_Click" Text="UploadImage" />
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
                <asp:Button ID="ButtonRegister" runat="server" OnClick="ButtonRegister_Click" Text="הירשם כמורה" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <br />
    <br />
 <%--   <table class="auto-style1">
        <tr>
            <td class="auto-style16">
                <asp:CheckBoxList ID="CheckBoxListLevels" runat="server">
                </asp:CheckBoxList>
            </td>
            <td class="auto-style15">
                <asp:DropDownList ID="DropDownListSubjects" runat="server" OnSelectedIndexChanged="DropDownListSubjects_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="ButtonSubjectsLevels" runat="server" Text="הוסף רמות לימוד למקצוע" OnClick="ButtonSubjectsLevels_Click" />
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
    <asp:GridView ID="GridViewSubjectsAndLevels" runat="server" AutoGenerateColumns="False" Height="150px" Width="400px" OnRowCancelingEdit="GridViewSubjectsAndLevels_RowCancelingEdit" OnRowEditing="GridViewSubjectsAndLevels_RowEditing" OnRowUpdating="GridViewSubjectsAndLevels_RowUpdating" OnRowDeleting="GridViewSubjectsAndLevels_RowDeleting">
        <Columns>
            <asp:BoundField DataField="SubjectName" HeaderText="מקצוע לימוד" ReadOnly="True" />
            <asp:BoundField HeaderText="רמת לימוד" DataField="LevelName" ReadOnly="True" />
            <asp:BoundField HeaderText="מחיר לשעה" DataField="Price" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
        </Columns>
    </asp:GridView>
    <br />
    <br />
    <br />--%>
    <%--  <asp:Panel ID="PanelSubjects" runat="server" Visible="False">--%><%--<table class="auto-style1">
        <tr>
            <td class="auto-style13">&nbsp;</td>
            <td></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style13">&nbsp;</td>
            <td>
                <asp:Panel ID="PanelSubjects" runat="server" Visible="False">
                    <table class="auto-style1">
                        <tr>
                            <td>
                                <asp:CheckBoxList ID="CheckBoxListLevels" runat="server">
                                </asp:CheckBoxList>
                            </td>
                            <td>
                                <asp:DropDownList ID="DropDownListSubjects" runat="server" OnSelectedIndexChanged="DropDownListSubjects_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="ButtonSubjectsLevels" runat="server" Text="הוסף רמות לימוד למקצוע" OnClick="ButtonSubjectsLevels_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="ButtonAddSubjectsAndLevels" runat="server" Text="הוסף מקצועות ורמות לימוד" />
                            </td>
                            <td>
                                <asp:GridView ID="GridViewSubjectsAndLevels" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField HeaderText="מקצוע לימוד" />
                                        <asp:BoundField HeaderText="רמת לימוד" />
                                        <asp:CommandField />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td></td>
                        </tr>
                    </table>


                </asp:Panel>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    </asp:Panel>--%>
</asp:Content>
