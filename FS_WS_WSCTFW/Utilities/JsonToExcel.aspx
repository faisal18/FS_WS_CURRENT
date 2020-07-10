<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="JsonToExcel.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.JsonToExcel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:FileUpload ID="FileUpload1" runat="server" />
    <br />
    <br />
    <br />
    <asp:Button ID="btnProcessFiles" runat="server" Text="Proces FIles" OnClick="btnProcessFiles_Click" />
    &nbsp;<br />
    <br />
    <br />
    <br />
    <asp:TextBox ID="txtlog" runat="server"  TextMode="MultiLine" Height="295px" Width="1157px" ></asp:TextBox>
    <br />
    <asp:Button ID="btnSaveFile" runat="server" Text="Save to Text File" OnClick="btnSaveFile_Click"  />
    <br />
</asp:Content>
