<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ERX_Cancel_Transaction.aspx.cs" Inherits="ClinicianAutomation.ERX_Cancel_Transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="title">
        <h1>ERX Cancel Transaction</h1>

    </div>
    <div id="content">
        <div>
            <asp:Label runat="server" ID="lbl_authourizationid" Text="Authourizationid ID" Enabled="false"></asp:Label></div>
        <div>
            <asp:TextBox runat="server" ID="txt_authourizationid"></asp:TextBox></div>
        <div>
            <asp:Button runat="server" ID="lbl_generate" Text="Generate Query" OnClick="lbl_generate_Click"></asp:Button>
        </div>
        <div>
            <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
        </div>
        <div>
            <asp:Button runat="server" ID="submit" Text="Update" OnClick="submit_Click" /></div>
    </div>
</asp:Content>
