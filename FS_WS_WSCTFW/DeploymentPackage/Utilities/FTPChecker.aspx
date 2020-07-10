<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FTPChecker.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.FTPChecker" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        FTP Tester</p>
    <p>
        <asp:Button ID="btnFTPCaller" runat="server" OnClick="btnFTPCaller_Click" Text="Check FTP" />
    </p>
    <p>
        <asp:TextBox ID="txtFTPLog" runat="server" Height="329px" Width="864px" TextMode="MultiLine" ></asp:TextBox>
    </p>
    <p>
        <asp:Button ID="btnFTPRestart" runat="server" Text="Restart FTP" OnClick="btnFTPRestart_Click" />
    </p>
   
    <p>
        &nbsp;</p>
</asp:Content>
