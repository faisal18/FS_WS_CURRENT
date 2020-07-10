<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AIMS_RunCases.aspx.cs" Inherits="ClinicianAutomation.Utilities_UI.AIMS_RunCases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="title">
        <h1>&nbsp;</h1>
        <h1>AIMS RUN CASES</h1>
    </div>
    <div>
        <asp:Label runat="server" ID="lbl_filedirectory" Text="URL"></asp:Label>
    </div>
    <div>
        <asp:TextBox runat="server" ID="txt_filedirectory" text="http://jenkins:8000/#/access/signin"></asp:TextBox>
    </div>
    <div>
        <asp:Label runat="server" ID="lbl_sendEmail" Text="Send Email"></asp:Label>
    </div>
    <div>
        <asp:TextBox runat="server" ID="txt_emailid"></asp:TextBox>
    </div>
    <div>
        <asp:Button runat="server" ID="btn_submit" Text="Submit" OnClick="btn_submit_Click" />
    </div>
    <div>
        <asp:Label runat="server" ID="lbl_status"></asp:Label>
    </div>
</asp:Content>
