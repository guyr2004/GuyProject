<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddTeacherWorkingHours.aspx.cs" Inherits="GuyProject.AddTeacherWorkingHours" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2{
            direction: rtl;
        }
    </style>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Add Your Days And Hours Of Working</h1>
    <br />
    <table class="auto-style1">
        <tr class="auto-style2">
            <td>
                <asp:Label ID="LabelEndHour" runat="server" Text=":בחר שעת סיום"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LabelStartHour" runat="server" Text=":בחר את שעת ההתחלה"></asp:Label>
            </td>
            <td>
                <asp:Label ID="LabelDaysOfWorking" runat="server" Text=":בחר את יום העבודה"></asp:Label>
            </td>
        </tr>
        <tr class="auto-style2">
            <td>
                <asp:DropDownList ID="DropDownListEndHour" runat="server">
                </asp:DropDownList>
                <br />
            </td>
            <td>
                <asp:DropDownList ID="DropDownListStartHour" runat="server">
                </asp:DropDownList>
                <br />
            </td>
            <td>
                <asp:DropDownList ID="DropDownListDayOfWorking" runat="server">
                    <asp:ListItem Value="0">בחר יום</asp:ListItem>
                    <asp:ListItem Value="1">Sunday</asp:ListItem>
                    <asp:ListItem Value="2">Monday</asp:ListItem>
                    <asp:ListItem Value="3">Tuesday</asp:ListItem>
                    <asp:ListItem Value="4">Wednesday</asp:ListItem>
                    <asp:ListItem Value="5">Thursday</asp:ListItem>
                    <asp:ListItem Value="6">Friday</asp:ListItem>
                    <asp:ListItem Value="7">Saturday</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr class="auto-style2">
            <td>
                <asp:CustomValidator ID="CustomValidatorEndHour" runat="server" ControlToValidate="DropDownListEndHour" ErrorMessage="לא בחרת את שעת הסיום" OnServerValidate="CustomValidatorEndHour_ServerValidate" ValidationGroup="Group1"></asp:CustomValidator>
            </td>
            <td>
                <asp:CustomValidator ID="CustomValidatorStartHour" runat="server" ControlToValidate="DropDownListStartHour" ErrorMessage="לא בחרת את שעת ההתחלה" OnServerValidate="CustomValidatorStartHour_ServerValidate" ValidationGroup="Group1"></asp:CustomValidator>
            </td>
            <td>
                <asp:CustomValidator ID="CustomValidatorDaysOfWorking" runat="server" ControlToValidate="DropDownListDayOfWorking" ErrorMessage="לא בחרת יום" OnServerValidate="CustomValidatorDaysOfWorking_ServerValidate"></asp:CustomValidator>
            </td>
        </tr>
    </table>
    <center>
        <asp:GridView ID="GridViewWorkingHours" runat="server" AutoGenerateColumns="False" CellPadding="4" OnRowCancelingEdit="GridViewWorkingHours_RowCancelingEdit" OnRowEditing="GridViewWorkingHours_RowEditing" OnRowDeleting="GridViewWorkingHours_RowDeleting" OnRowUpdating="GridViewWorkingHours_RowUpdating" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="EndHour" DataFormatString="{0:hh:mm tt}" HeaderText="שעת סיום" />
                <asp:BoundField DataField="StartHour" DataFormatString="{0:hh:mm tt}" HeaderText="שעת התחלה" />
                <asp:BoundField DataField="DayInWeek" HeaderText="יום עבודה" ReadOnly="True" />
                <asp:CommandField ButtonType="Button" CancelText="בטל" DeleteText="מחק" EditText="שנה" InsertText="הכנס" ShowDeleteButton="True" ShowEditButton="True" />
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
        <asp:Label Text="text" runat="server" ID="LabelMessage" Height="35px" Width="160px" />
        <br />
        <asp:Button Text="הוסף שעות לימוד" runat="server" Width="160px" ID="ButtonAddHours" OnClick="ButtonAddHours_Click" />
    </center>
    <br />
</asp:Content>
