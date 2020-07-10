<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EmailCheck.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.EmailCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="txtToaddress" runat="server"></asp:TextBox>

    <asp:TextBox ID="txtMessage" runat="server"></asp:TextBox>


    <asp:Button ID="btnCheckEmailTest" runat="server" Text="Send Test Email" OnClick="btnCheckEmailTest_Click" />
    
</asp:Content>
