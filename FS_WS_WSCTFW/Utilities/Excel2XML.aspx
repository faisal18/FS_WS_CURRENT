<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Excel2XML.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.Excel2XML" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
    <div>
    <asp:FileUpload ID="file1" runat="server" />
        <asp:Button ID="btnLoadExcel" runat="server" Text="LoadExcel" OnClick="btnLoadExcel_Click" />
        <br />
        <br />
        <asp:CheckBox ID="chkFristRowHeader" runat="server" Text="First Row Contains Headers" />
        <br />
        <asp:GridView ID="grdExcel" runat="server">
        </asp:GridView>
        <br />
    </div>
    </form>
</body>
</html>
