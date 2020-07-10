<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BatchFileGenerator.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.BatchFileGenerator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <div>
       Filename
    <asp:TextBox ID="txtFilename" runat="server" TextMode="MultiLine" Height="126px" Width="429px"></asp:TextBox>
       <br />
       <br />
       <br />
    </div>
    <div></div>

    <div>Content
     <asp:TextBox ID="txtFileContent" runat="server" TextMode="MultiLine" Height="158px" Width="636px"></asp:TextBox>
        <br />
        <br />
        <br />
        <br />
     </div>
    <div>
     <asp:TextBox ID="txtlog" runat="server" TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
        <br />
     </div>
    <div>
    <asp:Button ID="Button1" runat="server" Text="Create Files" OnClick="Button1_Click" />
        <br />
        <br />
    </div>
      <div>
    <asp:Button ID="Button2" runat="server" Text="Clear All" OnClick="Button2_Click" /></div>
</asp:Content>
