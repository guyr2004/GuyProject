<%@ Page Title="AddSubjectsAndLevels" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddSubjectsAndLevels.aspx.cs" Inherits="GuyProject.AddSubjectsAndLevels" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 436px;
        }

        .auto-style3 {
            width: 500px;
        }

        .auto-style5 {
            margin-left: 0px;
        }

        .auto-style6 {
            width: 470px;
        }

        .auto-style7 {
            width: 520px;
        }
    </style>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>AddSubjectsAndLevels</h1>
    <br />
    <br />
    <br />
    <div>
        <asp:DropDownList ID="DropDownListSubjects" runat="server">
        </asp:DropDownList>
        <asp:Button ID="ButtonShowLevels" runat="server" OnClick="ButtonShowLevels_Click" Text="הצג רמות לימוד" />
        <br />
        <asp:GridView ID="GridViewSubjectsAndLevels" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="LevelName" HeaderText="רמת לימוד" />
                <asp:BoundField DataField="SubjectName" HeaderText="מקצוע" />
            </Columns>
        </asp:GridView>

        <br />
        <table class="auto-style1">
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBoxSubject" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="LabelSubject" runat="server" Text="הוסף מקצוע"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style2">
                    <asp:TextBox ID="TextBoxLevel" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="LabelLevels" runat="server" Text="הוסף רמת לימוד"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="auto-style1">
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="LabelMeesage" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="ButtonInsertSubjectsLevels" runat="server" CssClass="auto-style5" Text="הוסף מקצוע ורמות לימוד" OnClick="ButtonInsertSubjectsLevels_Click" />
                </td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <table class="auto-style1">
            <tr>
                <td class="auto-style3">
                    <asp:DropDownList ID="DropDownListCity" runat="server">
                    </asp:DropDownList>
                </td>
                <td class="auto-style6">
                    <asp:TextBox ID="TextBoxCity" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="LabelCity" runat="server" Text="הכנס עיר:"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="auto-style1">
            <tr>
                <td class="auto-style7">
                    <asp:Label ID="LabelCityMessage" runat="server"></asp:Label>
                </td>
                <td>
                    <asp:Button ID="ButtonAddCity" runat="server" OnClick="ButtonAddCity_Click" Text=":הוסף עיר" />
                </td>
            </tr>
        </table>



    </div>
</asp:Content>
