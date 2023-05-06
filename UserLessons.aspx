<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserLessons.aspx.cs" Inherits="GuyProject.UserLessons" %>

<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #LabelFirstMessage {
            direction: rtl;
            float: right;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
    <h1>UserDetails</h1>
    <br />
    <br />
    <center>
        <h2>כאן תוכל לראות את כל השיעורים שנקבעו לך</h2>
        <h6>חשוב להדגיש: כי אורך כל שיעור הוא כשעה אחת בלבד</h6>
        <br />
        <asp:GridView ID="GridViewShowLessons" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="GridViewShowLessons_RowDeleting">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField HeaderText="מחיר השיעור" DataField="Price" />
                <asp:BoundField DataField="SubjectName" HeaderText="מקצוע לימוד" />
                <asp:BoundField HeaderText="מקום השיעור" DataField="Address" />
                <asp:BoundField HeaderText="מספר טלפון תלמיד" DataField="StudentPhone" />
                <asp:BoundField HeaderText="שם התלמיד" DataField="StudentName" />
                <asp:BoundField HeaderText="מספר טלפון מורה" DataField="TeacherPhone" />
                <asp:BoundField HeaderText="שם המורה" DataField="TeacherName" />
                <asp:BoundField HeaderText="שעת תחילת השיעור" DataFormatString="{0:hh:mm tt}" DataField="StartHour" />
                <asp:BoundField HeaderText="תאריך השיעור" DataFormatString="{0:d}" DataField="LessonDate" />
                <asp:CommandField ButtonType="Button" DeleteText="מחק" ShowCancelButton="False" ShowDeleteButton="True" />
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
        <br />
        <asp:Label ID="LabelDeleteMessage" Text="text" runat="server" Height="30px" Width="150px" />
        <br />
        <br />
        <asp:Button ID="ButtonDeleteLastLessons" runat="server" OnClick="ButtonDeleteLastLessons_Click" Text="מחק העידרויות קודמות" Visible="true" />

    </center>
    <br />
</asp:Content>
