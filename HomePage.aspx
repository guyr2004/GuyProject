<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="GuyProject.HomePage" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
    Home
</asp:Content>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 500px;
        }

        .auto-style4 {
            width: 300px;
        }

        .auto-style5 {
            width: 100%;
        }

        .auto-style6 {
            margin-left: 3px;
        }

        .auto-style10 {
            width: 200px;
        }
    </style>
    <script src="path/to/jquery.js"></script>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery/jquery-1.8.0.js"></script>
    <script src="https://ajax.aspnetcdn.com/ajax/jquery.ui/1.8.22/jquery-ui.js"></script>

    <script type="text/javascript">

        $(function () {
            $("#TextBoxSubject").autocomplete({
                source: function (request, response) {
                    var param = { subjectLevelsname: $('#TextBoxSubject').val() };
                    $ajax({
                        url: "HomePage.aspx/GetDataSourceForAutoComplete",
                        data: JSON.stringify(param),
                        type: "post",
                        contentType: "application/json; charset=utf-8 ",
                        datafilter: function (data) { return data; },
                        success: function (data) {
                            response($.ma(data.d, function (item) { return { value: item } }))
                        },
                    });
                },
                minlength: 1
            });
        });

        //const { ready } = require("jquery");

        //$(function () {
        //    // Attach autocomplete feature to textbox
        //    $("#TextBoxSubject").autocomplete({
        //        source: GetDataSourceForAutoComplete()
        //    });
        //});

        //$(document).ready(function () {
        //    $("#TextBoxSubject").autocomplete({
        //        source: function (request, response) {
        //            // Your autocomplete source code goes here
        //        }
        //    });
        //});

    </script>

</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Home Page</h1>
    <br />
    <br />
    <%--<asp:TextBox ID="TextBoxCities" runat="server" AutoCompleteType="Disabled" AutoCompleteMode="Suggest" AutoCompleteSource="CustomSource" OnTextChanged="TextBoxCities_TextChanged"></asp:TextBox>--%>
    <asp:TextBox ID="TextBoxSubject" runat="server" OnTextChanged="TextBoxSubject_TextChanged"></asp:TextBox>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <%--<asp:TextBox ID="TextBoxCities" runat="server" AutoCompleteType="Disabled" AutoCompleteMode="Suggest" AutoCompleteSource="CustomSource" OnTextChanged="TextBoxCities_TextChanged"></asp:TextBox>--%>

    <table class="auto-style5">
        <tr>
            <td class="auto-style10">
                <asp:Button ID="ButtonFind" runat="server" Text="search" CommandName="Search" OnClick="ButtonFind_Click" />
            </td>
            <td class="auto-style4">
                <asp:DropDownList ID="DropDownListSubjectsAndLevel" runat="server" OnSelectedIndexChanged="DropDownListSubjectsAndLevel_SelectedIndexChanged" AppendDataBoundItems="True" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td class="auto-style4">
                <asp:DropDownList ID="DropDownListArea" runat="server" CssClass="auto-style6"></asp:DropDownList>
                <br />
                <%--<asp:TextBox ID="TextBoxCities" runat="server" AutoCompleteType="Disabled" AutoCompleteMode="Suggest" AutoCompleteSource="CustomSource" OnTextChanged="TextBoxCities_TextChanged"></asp:TextBox>--%>
            </td>
            <td>
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

            </td>
        </tr>
    </table>
    <br />
    <br />
    <div align="center">
        <asp:DataList ID="DataListTeachers" runat="server" OnItemCommand="DataListTeachers_ItemCommand" CellPadding="4" ForeColor="#333333" OnItemDataBound="DataListTeachers_ItemDataBound" DataKeyField="TeacherID">
            <AlternatingItemStyle BackColor="White" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <ItemStyle BackColor="#EFF3FB" />
            <ItemTemplate>
                <div class="ClassDataList">
                    <asp:Label ID="LabelStatus" runat="server" Text='<%# Bind("FirstName")%>'></asp:Label>
                    <asp:Label ID="LabelLastName" runat="server" Text='<%# Bind("LastName")%>'></asp:Label>
                </div>
                <div>
                    <asp:Image ID="ImageTeacher" runat="server" Height="150px" ImageUrl='<%# Bind("ImageTeacher") %>' Width="150px" />
                    <table class="auto-style1">
                        <tr>
                            <td class="auto-style4">
                                <asp:Label ID="LabelLearnPlace" runat="server" Text='<%# Bind("LearnPlace") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LabelLearnPlaceMeesage" runat="server" Text="מקום השיעור" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <asp:Label ID="LabelCityName" runat="server" Text='<%# Bind("CityName") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="LabelCityNameMeesage" runat="server" Text="מקום מגורים" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style4">
                                <asp:Label ID="LabelDescription" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="תיאור בקצרה"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <center>
                        <asp:GridView ID="GridViewSubjectsAndLevels" runat="server" AutoGenerateColumns="False">
                            <columns>
                                <asp:BoundField DataField="SubjectName" HeaderText="מקצוע" />
                                <asp:BoundField DataField="levelName" HeaderText="רמת לימוד" />
                                <asp:BoundField DataField="PricePerHour" HeaderText="מחיר לשיעור" />
                            </columns>
                        </asp:GridView>
                        <asp:Button ID="ButtonShowTeacherDetails" runat="server" CommandName="ShowTeacher" Text="!!!קבע שיעור" />
                        <br />
                    </center>
                </div>
            </ItemTemplate>
            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        </asp:DataList>
    </div>

    <br />
    <br />
</asp:Content>
