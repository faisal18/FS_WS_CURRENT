<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Dash_UploadData.aspx.cs" Inherits="FS_WS_WSCTFW.Dashboard.Dash_UploadData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <h1>Upload Data to Automation</h1>
    <div>
        <asp:Label runat="server" ID="lbl_result"></asp:Label>
    </div>

    <div>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="CheckAsync" />
    </div>
    <div>
        <asp:TextBox ID="TextBox1" runat="server" Height="162px" Width="709px"></asp:TextBox>
    </div>
</asp:Content>
