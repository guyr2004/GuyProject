<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="GuyProject.Account1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="title" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="body" runat="server">
     <h1>AccountDetails</h1>
    <center>
        <asp:Label ID="LabelUserName" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="LabelBalance" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:GridView ID="GridViewTransactions" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="TransactionStatus" HeaderText="סטטוס העברה" />
                <asp:BoundField DataField="PhoneGetMoney" HeaderText="מספר טלפון מקבל" />
                <asp:BoundField DataField="PhonePayMoney" HeaderText="מספר טלפון משלם" />
                <asp:BoundField DataField="Payee" HeaderText="סיבת העברה" />
                <asp:BoundField DataField="Amount" HeaderText="סכום העברה" />
                <asp:BoundField DataField="DatePosted" DataFormatString="{0:dd/MM/yyyy}" HeaderText="תאריך הפעולה" />
            </Columns>
        </asp:GridView>
        
    </center>
</asp:Content>
