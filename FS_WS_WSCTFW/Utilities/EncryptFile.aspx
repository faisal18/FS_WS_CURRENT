<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EncryptFile.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.EncryptFile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 <br />
    <br />
    <br />
     <br />
    <br />
    <br />
    <asp:Button ID="btnEncrypt" runat="server" Text="Encrypt File" OnClick="btnEncrypt_Click" />
    
    <br />
    <br />
    <br />
    
    <asp:Button ID="btnDecrypt" runat="server" Text="Decrypt File" OnClick="btnDecrypt_Click" />  <br />
    <br />
    <br />
    
    <asp:Button ID="btnRSA" runat="server" Text="RSA File" OnClick="btnRSA_Click"  /> 
     <br />
    <br />
    <br />
    
    <asp:Button ID="btnunRSA" runat="server" Text="UNRSA File" OnClick="btnunRSA_Click" />
</asp:Content>

