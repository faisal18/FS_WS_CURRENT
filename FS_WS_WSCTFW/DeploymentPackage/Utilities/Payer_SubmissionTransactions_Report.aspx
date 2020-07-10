<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Payer_SubmissionTransactions_Report.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.Payer_SubmissionTransactions_Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div><h1> Payer Submission Transactions Report  </h1></div>
    <div>Payer License</div>
    <div><asp:TextBox runat ="server" ID ="txtPayerLicense" ></asp:TextBox></div>


<div>Date Range</div>
<div>Start Date</div>
<div>
    <asp:Calendar ID="DTStart" runat="server"></asp:Calendar>
    </div>
<div>End Date</div>
<div>
    <asp:Calendar ID="DTEnd" runat="server"></asp:Calendar>
    </div>
<div></div>
<div>
    <asp:Button ID="btnGenerateReport" runat="server" Text="Generate Report" OnClick="btnGenerateReport_Click" />
    </div>
<div></div>
<div></div>
<div><asp:TextBox runat ="server" ID ="txtLog" textMode ="MultiLine" Height="180px" Width="868px"></asp:TextBox></div>
<div></div>
<div></div>
<div></div>
<div></div>
<div></div>
<div></div>
<div></div>
<div></div>

</asp:Content>
