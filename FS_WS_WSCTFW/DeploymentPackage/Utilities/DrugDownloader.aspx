<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DrugDownloader.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.DrugDownloader" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div> <h1>KSA Drug Details Download</h1> </div>

    <div> Drug Number: Start Range&nbsp;
        <div><asp:TextBox ID="txtStart" runat="server"></asp:TextBox></div>
    </div>

    <div> Drug Number: End  
        <div><asp:TextBox ID="txtEnd" runat="server"></asp:TextBox></div>
  </div>
    <div>
        <br />
    </div>
    <div>
        Drug Type : <asp:DropDownList ID="cmbType" runat="server" Height="16px" Width="216px">
            <asp:ListItem Value="vitamin">Vitamin</asp:ListItem>
            <asp:ListItem Value="herbal">Herbal</asp:ListItem>
            <asp:ListItem Value="human">Human</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        &nbsp;
        <br />
    </div>
    <div>

    </div>
    
    <%--<div><asp:TextBox ID="txtURL" runat="server" Width="748px" Text ="http://www.sfda.gov.sa/en/drug/search/Pages/default.aspx?sm=human&PageIndex="></asp:TextBox></div>--%>
    <div><asp:Button ID="btnDownloadHTML" runat="server" Text="Download KSA Drug Details" OnClick="btnDownloadHTML_Click" />
    <br />
    <br />
    <br />
    <asp:TextBox ID="txtLog" runat="server" Height="326px" Width="841px" TextMode ="MultiLine"></asp:TextBox>
    <br />
    </div>
    <%--<asp:Button ID="btnSaveCSV" runat="server" Text="SAVE CSV FIle" OnClick="btnSaveCSV_Click" />--%>

</asp:Content>
