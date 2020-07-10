<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Generate_Batch_Transaction.aspx.cs" Inherits="ClinicianAutomation.Utilities_UI.Generate_Batch_Transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Generate Batch for Transactions</h1>
    </div>
    <div>
        <asp:RadioButton runat="server" ID="rd_byTransactionID" Text="By ID Payer" AutoPostBack="true" Checked="true" OnCheckedChanged="rd_byTransactionID_CheckedChanged" />
    </div>
    <div>
        <asp:RadioButton runat="server" ID="rd_byIDPayer" Text="By Transaction ID" AutoPostBack="true" OnCheckedChanged="rd_byIDPayer_CheckedChanged" />
    </div>
    <div>
        <asp:Label runat="server" ID="lbl_region" Text="Select Region"></asp:Label>
    </div>
    <div>
        <asp:DropDownList runat="server" ID="ddl_region">
            <asp:ListItem Value="1">Abu Dhabi</asp:ListItem>
            <asp:ListItem Value="2">Dubai & Northern Emirates</asp:ListItem>
            <asp:ListItem Value="3">All Regions</asp:ListItem>
            <asp:ListItem Value="4">Dubai</asp:ListItem>
            <asp:ListItem Value="5">Northern Emirates</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div>
        <asp:Label runat="server" ID="lbl_trans_type" Text="Select Transaction Type"></asp:Label>
    </div>
    <div>
        <asp:DropDownList runat="server" ID="ddl_trans_type">
            <asp:ListItem Value="1">Prior Request</asp:ListItem>
            <asp:ListItem Value="2">Prior Authourization</asp:ListItem>
            <asp:ListItem Value="3">Claim Submission</asp:ListItem>
            <asp:ListItem Value="4">Canceled Prior Request</asp:ListItem>
        </asp:DropDownList>
    </div>
    <div runat="server" id="div_payer" style="display:none">
        <div>
            <asp:Label runat="server" ID="lbl_idpayer" Text="Transaction ID"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_idpayer" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div runat="server" id="div_transaction">
        <div>
            <asp:Label runat="server" ID="lbl_transactionid" Text="ID Payer"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_transactionid" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div>
        <asp:Label runat="server" ID="lbl_dubailicenseno" Text="TPA Dubai Code No."></asp:Label>
    </div>
    <div>
        <asp:TextBox runat="server" ID="txt_dubailicenseno"></asp:TextBox>
    </div>
    <div>
        <asp:Button runat="server" ID="btn_submit" Text="Submit" OnClick="btn_submit_Click" />
    </div>
    <div>
        <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
    </div>
</asp:Content>
