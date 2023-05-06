<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SetLesson.aspx.cs" Inherits="GuyProject.SetLesson" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style2 {
            margin-left: 3px;
        }

        #ImageTeacher {
            float: right;
            direction: rtl;
        }

        #LabelFullName {
            float: right;
        }

        #LabelPhoneNumber {
            float: right;
        }

        #LabelLearnPlace {
            float: right;
        }

        #LabelDescription {
            float: right;
        }

        #GridViewSubjectsAndLevels {
            float: right;
        }

        #LabelMeesage {
            direction: rtl;
        }

        .auto-style7 {
            width: 100%;
        }

        .auto-style8 {
            direction: rtl;
        }

        .auto-style9 {
            width: 900px;
        }
    </style>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>TeacherDetailsAndAddLessons</h1>
    <br />
    <asp:Image ID="ImageTeacher" Class="auto-style8" runat="server" Height="100px" Width="100px" />
    <center>

        <table class="auto-style7">
            <tr class="auto-style8">
                <td>
                    <asp:Label ID="LabelFullName" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LabelFullNameMessage" runat="server" Text="שם מלא:"></asp:Label>
                </td>
            </tr>
            <tr class="auto-style8">
                <td>
                    <asp:Label ID="LabelPhoneNumber" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LabelPhoneMessage" runat="server" Text="מספר טלפון מורה:"></asp:Label>
                </td>
            </tr>
            <tr class="auto-style8">
                <td>
                    <asp:Label ID="LabelLearnPlace" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LabelShowLearnPlace" runat="server" Text="מקום לימוד:"></asp:Label>
                </td>
            </tr>
            <tr class="auto-style8">
                <td>
                    <asp:Label ID="LabelDescription" runat="server" Text="Label"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="LabelDescriptionMessage" Text="תיאור:" runat="server" />
                </td>
            </tr>
            <br />
        </table>
    </center>
    <table class="auto-style7">
        <tr>
            <td class="auto-style8">
                <br />
                <asp:Label ID="LabelMeesage" runat="server" Text=":בחר את היום ואת השעה שתרצה לקבוע את השיעור" Width="250px"></asp:Label>
                <br />
                <asp:Calendar ID="CalendarLessons" runat="server" OnSelectionChanged="CalendarLessons_SelectionChanged"></asp:Calendar>
                <br />
                <asp:Label ID="LabelTeacherHours" runat="server" Text="כאן תוכל לראות את השעות בהם המורה שבחרת עובד במידה ורוצים שיעור של יותר משעה יש לקבוע מספר שיעורים:" Width="250px"></asp:Label>
                <br />
                <asp:DropDownList ID="DropDownListHours" runat="server" CssClass="auto-style2" OnSelectedIndexChanged="DropDownListHours_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
            </td>
            <td class="auto-style8">
                <asp:Label ID="LabelMessageSubjectLevel" runat="server" Text="מתוך המקצועות שמופיעים פה עליך לבחור את המקצוע שתרצה ללמוד בשיעור"></asp:Label>
                <br />
                <asp:GridView ID="GridViewSubjectsAndLevels" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4" OnRowCommand="GridViewSubjectsAndLevels_RowCommand">
                    <Columns>
                        <asp:ButtonField ButtonType="Button" CommandName="ShowSubjectLevel" HeaderText="בחר את המקצוע" ShowHeader="True" Text="בחר" />
                        <asp:BoundField DataField="SubjectName" HeaderText="נושא" ReadOnly="True" />
                        <asp:BoundField DataField="LevelName" HeaderText="רמה" ReadOnly="True" />
                        <asp:BoundField DataField="PricePerHour" HeaderText="מחיר לשיעור" ReadOnly="True" />
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    <SortedAscendingCellStyle BackColor="#EDF6F6" />
                    <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                    <SortedDescendingCellStyle BackColor="#D6DFDF" />
                    <SortedDescendingHeaderStyle BackColor="#002876" />
                </asp:GridView>
            </td>
        </tr>
    </table>
    <table class="auto-style7">
        <tr class="auto-style8">
            <td class="auto-style9">
                <asp:Label ID="LabelSubjectLevel" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LabelShowSubjectLevel" runat="server" Text="המקצוע שנבחר לשיעור:"></asp:Label>
            </td>
        </tr>
        <tr class="auto-style8">
            <td class="auto-style9">
                <asp:Label ID="LabelDate" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LabelShowDate" runat="server" Text="התאריך שנבחר לשיעור:"></asp:Label>
            </td>
        </tr>
        <tr class="auto-style8">
            <td class="auto-style9">
                <asp:Label ID="LabelShowHourLesson" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LabelHourLesson" runat="server" Text="זמן השיעור:"></asp:Label>
            </td>
        </tr>
        <tr class="auto-style8">
            <td class="auto-style9">
                <asp:Label ID="LabelPricePerHour" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LabelShowPricePerHour" runat="server" Text="מחיר השיעור:"></asp:Label>
            </td>
        </tr>
        <tr class="auto-style8">
            <td class="auto-style9">
                <asp:Label ID="LabelShowLessonPlace" runat="server" Text="Label"></asp:Label>
                <br />
                <asp:TextBox ID="TextBoxShowLessonPlace" runat="server"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="LabelLessonPlace" runat="server" Text="מקום השיעור:"></asp:Label>
            </td>
        </tr>
        <tr class="auto-style8">
            <td class="auto-style9">
                <asp:Label ID="LabelShowTeacherPhoneNumber" runat="server" Text="Label"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LabelTeacherPhoneNumber" runat="server" Text="מספר טלפון של המורה:"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <center>
        <asp:Button Text="קבע שיעור" runat="server" ID="ButtonSetLesson" OnClick="ButtonSetLesson_Click" />
        <br />

        <asp:Label ID="LabelMessageSetLesson" runat="server" Height="35px" Text="Label" Width="130px"></asp:Label>
        <br />

    </center>
</asp:Content>
