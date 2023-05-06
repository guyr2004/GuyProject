<%@ Page Title="AddTeacherSubjectsAndLevels" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddTeacherSubjectsAndLevels.aspx.cs" Inherits="GuyProject.AddTeacherSubjectsAndLevels" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style15 {
            width: 1045px;
        }

        .auto-style16 {
            width: 1000px;
        }

        #LabelAddSubjectAndLevels {
            direction: rtl;
        }
    </style>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Add Your Subjects And Levels</h1>
    <br />
    <br />
    <table class="auto-style1">
        <tr>
            <td class="auto-style16">
                <asp:CheckBoxList ID="CheckBoxListLevels" runat="server">
                </asp:CheckBoxList>
            </td>
            <td class="auto-style15">
                <asp:DropDownList ID="DropDownListSubjects" runat="server" OnSelectedIndexChanged="DropDownListSubjects_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>
                <asp:Label ID="LabelAddSubjectAndLevels" runat="server" Text="לאחר שבחרת את רמות הלימוד לחץ על הכפתור שלמטה לפני הוספת המקצועות"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style16">
                <asp:Label ID="LabelMeesageAddSUbjectAndLevels" runat="server" Text="Label"></asp:Label>
            </td>
            <td class="auto-style15">&nbsp;</td>
            <td>

                <asp:Button ID="ButtonSubjectsLevels" runat="server" Text="הוסף רמות לימוד למקצוע" OnClick="ButtonSubjectsLevels_Click" />

            </td>
        </tr>
    </table>
    <center>
        <asp:GridView ID="GridViewSubjectsAndLevels" runat="server" AutoGenerateColumns="False" Height="150px" Width="400px" OnRowCancelingEdit="GridViewSubjectsAndLevels_RowCancelingEdit" OnRowEditing="GridViewSubjectsAndLevels_RowEditing" OnRowUpdating="GridViewSubjectsAndLevels_RowUpdating" OnRowDeleting="GridViewSubjectsAndLevels_RowDeleting" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:BoundField DataField="SubjectName" HeaderText="מקצוע לימוד" ReadOnly="True" />
                <asp:BoundField HeaderText="רמת לימוד" DataField="LevelName" ReadOnly="True" />
                <asp:BoundField HeaderText="מחיר לשעה" DataField="PricePerHour" />
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True" ShowEditButton="True" />
            </Columns>
            <FooterStyle BackColor="#CCCCCC" />
            <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#F1F1F1" />
            <SortedAscendingHeaderStyle BackColor="#808080" />
            <SortedDescendingCellStyle BackColor="#CAC9C9" />
            <SortedDescendingHeaderStyle BackColor="#383838" />
        </asp:GridView>
        <asp:Button ID="ButtonAddSubjectsAndLevels" runat="server" Text="הוספת מקצועות ורמות לימוד למורה" OnClick="ButtonAddSubjectsAndLevels_Click" />
    </center>
    <br />
    <br />
    <br />
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
