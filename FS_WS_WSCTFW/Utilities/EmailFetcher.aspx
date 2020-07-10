<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmailFetcher.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.EmailFetcher" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br>
    <br>
    <br>
    <br>
    <br>

    Email Address
    <asp:TextBox ID="txtPopEmail" runat="server"></asp:TextBox>
    <br>
    Password: 
    <asp:TextBox ID="txtPopPass" runat="server"></asp:TextBox>
    <br>
    <asp:Button ID="btnFetch" runat="server" Text="Fetch Emails" OnClick="btnFetch_Click" />
    <br>
     <br>
     <br>
     <br>
     <br>
     <br>
    <asp:TextBox ID="txtLog" runat="server" TextMode="MultiLine"></asp:TextBox>
</asp:Content>
