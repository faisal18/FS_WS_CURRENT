<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Member_details.aspx.cs" Inherits="ClinicianAutomation.Member_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="title">
        <h1>Get Member details</h1>

    </div>
    <div id="content">
        <div>
            <asp:Label runat="server" ID="lbl_memberid" Text="Member ID" Enabled="false"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_memberid" TextMode="MultiLine"></asp:TextBox>
        </div>
        <%--<div>
            <asp:Button runat="server" ID="lbl_generate" Text="Generate Query" OnClick="lbl_generate_Click"></asp:Button>
        </div>--%>
        <div>
            <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
        </div>
        <div>
            <asp:Button runat ="server" ID="btn_memberdetail" Text="Submit" OnClick="btn_memberdetail_Click" />
        </div>
    </div>
</asp:Content>
