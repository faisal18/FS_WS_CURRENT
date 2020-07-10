<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OIC_Provider_Cancel_Transaction.aspx.cs" Inherits="ClinicianAutomation.OIC_Provider_Cancel_Transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="title">
        <h1>OIC Provider: Cancel Transaction</h1>

    </div>
    <div id="content">
        <div>
            <asp:Label runat="server" ID="lbl_transactionid" Text="Transaction ID" Enabled="false"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_transactionid"></asp:TextBox>
        </div>
        <div>
            <asp:Button runat="server" ID="lbl_generate" Text="Generate Query" OnClick="lbl_generate_Click"></asp:Button>
        </div>
        <div>
            <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
        </div>
        <div>
            <asp:Button runat="server" ID="submit" Text="Update" OnClick="submit_Click" />
        </div>
    </div>

</asp:Content>
