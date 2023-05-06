<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TableCalender.aspx.cs" Inherits="GuyProject.TableCalender" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            margin-left: 3px;
        }

        .auto-style4 {
            width: 500px;
        }
        .auto-style5 {
            width: 400px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>TeacherDetailsAndAddLessons</h1>
    <br />
    <table class="auto-style1">
        <tr>
            <td class="auto-style5">
                <asp:DropDownList ID="DropDownListHours" runat="server" CssClass="auto-style2">
                    <asp:ListItem Value="0">בחר שעה</asp:ListItem>
                    <asp:ListItem Value="1">7:00-8:00</asp:ListItem>
                    <asp:ListItem Value="2">8:00-9:00</asp:ListItem>
                    <asp:ListItem Value="3">9:00-10:00</asp:ListItem>
                    <asp:ListItem Value="4">10:00-11:00</asp:ListItem>
                    <asp:ListItem Value="5">11:00-12:00</asp:ListItem>
                    <asp:ListItem Value="6">12:00-13:00</asp:ListItem>
                    <asp:ListItem Value="7">13:00-14:00</asp:ListItem>
                    <asp:ListItem Value="8">14:00-15:00</asp:ListItem>
                    <asp:ListItem Value="9">15:00-16:00</asp:ListItem>
                    <asp:ListItem Value="10">16:00-17:00</asp:ListItem>
                    <asp:ListItem Value="11">17:00-18:00</asp:ListItem>
                    <asp:ListItem Value="12">18:00-19:00</asp:ListItem>
                    <asp:ListItem Value="13">19:00-20:00</asp:ListItem>
                    <asp:ListItem Value="14">20:00-21:00</asp:ListItem>
                    <asp:ListItem Value="15">21:00-22:00</asp:ListItem>
                    <asp:ListItem Value="16">22:00-23:00</asp:ListItem>
                    <asp:ListItem Value="17">23:00-24:00</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="auto-style4">
                
                <asp:Calendar ID="CalendarLessons" runat="server"></asp:Calendar>
                
            </td>
            <td>
                <asp:Label ID="LabelMeesage" runat="server" Text=":בחר את היום ואת השעה שתרצה לקבוע את השיעור"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <br />
</asp:Content>
