<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddTeacherAbsence.aspx.cs" Inherits="GuyProject.AddTeacherAbsence" %>

<asp:Content ID="title" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            direction: rtl;
        }

        .field-value {
            white-space: nowrap;
        }
    </style>
</asp:Content>
<asp:Content ID="body" ContentPlaceHolderID="body" runat="server">
    <h1>Add Your Absence Details</h1>
    <br />
    <table class="auto-style1">
        <tr class="auto-style2">
            <td>
                <asp:Label ID="LabelEndHour" runat="server" Text="בחר את שעת הסיום:"></asp:Label>
                <br />
                <asp:CustomValidator ID="CustomValidatorEndHour" runat="server" ControlToValidate="DropDownListEndHour" ErrorMessage="לא נבחרה שעת הסיום" OnServerValidate="CustomValidatorEndHour_ServerValidate" ValidationGroup="Group1"></asp:CustomValidator>
            </td>
            <td>
                <asp:Label ID="LabelStartHour" runat="server" Text="בחר את שעת ההתחלה:"></asp:Label>
                <br />
                <asp:CustomValidator ID="CustomValidatorStartHour" runat="server" ControlToValidate="DropDownListStartHour" ErrorMessage="לא נבחרה שעת ההתחלה" OnServerValidate="CustomValidatorStartHour_ServerValidate" ValidationGroup="Group1"></asp:CustomValidator>
            </td>
            <td>
                <asp:Label ID="LabelAbsenceDate" runat="server" Text="בחר את תאריך העידרות:"></asp:Label>
                <br />
            </td>
        </tr>
        <tr class="auto-style2">
            <td>
                <asp:DropDownList ID="DropDownListEndHour" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="DropDownListStartHour" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Calendar ID="CalendarAbsenceTeacherDate" runat="server"></asp:Calendar>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <center>
        <asp:GridView ID="GridViewAbsenceTeacher" runat="server" AutoGenerateColumns="False"
            OnRowCancelingEdit="GridViewAbsenceTeacher_RowCancelingEdit"
            OnRowDeleting="GridViewAbsenceTeacher_RowDeleting"
            OnRowEditing="GridViewAbsenceTeacher_RowEditing"
            OnRowUpdating="GridViewAbsenceTeacher_RowUpdating" CellPadding="4" ForeColor="#333333"
            GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Endhour" DataFormatString="{0:hh:mm tt}" HeaderText="שעת הסיום">
                    <ControlStyle CssClass="field-value" />
                    <ItemStyle CssClass="field-value" />
                </asp:BoundField>
                <asp:BoundField DataField="Starthour" DataFormatString="{0:hh:mm tt}" HeaderText="שעת ההתחלה">
                    <ControlStyle CssClass="field-value" />
                    <ItemStyle CssClass="field-value" />
                </asp:BoundField>
                <asp:BoundField DataField="AbsenceDate" DataFormatString="{0:dd/MM/yyyy}" HeaderText="תאריך העידרות">
                    <ControlStyle CssClass="field-value" />
                    <ItemStyle CssClass="field-value" />
                </asp:BoundField>
                <asp:CommandField ButtonType="Button" CancelText="בטל" DeleteText="מחק" EditText="שנה"
                    ShowDeleteButton="True" ShowEditButton="True" UpdateText="עדכן" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>
        <%-- <asp:GridView ID="GridViewAbsenceTeacher" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="GridViewAbsenceTeacher_RowCancelingEdit" OnRowDeleting="GridViewAbsenceTeacher_RowDeleting" OnRowEditing="GridViewAbsenceTeacher_RowEditing" OnRowUpdating="GridViewAbsenceTeacher_RowUpdating" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField DataField="Endhour" DataFormatString="{0:hh:mm tt}" HeaderText="שעת הסיום" />
                <asp:BoundField DataField="Starthour" DataFormatString="{0:hh:mm tt}" HeaderText="שעת ההתחלה" />
                <asp:BoundField DataField="AbsenceDate" DataFormatString="{0:d}" HeaderText="תאריך העידרות" />
                <asp:CommandField ButtonType="Button" CancelText="בטל" DeleteText="מחק" EditText="שנה" ShowDeleteButton="True" ShowEditButton="True" UpdateText="עדכן" />
            </Columns>
            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>--%>
        <asp:Button ID="ButtonDeleteLesssons" runat="server" Height="30px" OnClick="ButtonDeleteLesssons_Click" Text="נקה העדרויות קודמות" Width="200px" />
        
        <br />
        <asp:Button ID="ButtonAddAbseceTeacher" runat="server" Height="30px" OnClick="ButtonAddAbseceTeacher_Click" Text="הוסף העידרות" Width="200px" />
        <br />
        <asp:Label ID="LabelMessage" runat="server" Height="30px" Width="160px"></asp:Label>
        <br />
    </center>

</asp:Content>
