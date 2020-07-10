<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GetClaimDetails.aspx.cs" Inherits="ClinicianAutomation.Utilities_UI.GetClaimDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


     <div id="title">
        <h1>Get Claim Details</h1>

    </div>
    <div id="content">
        <div>
            <asp:Label runat="server" ID="lbl_data" Text="Please Enter Data" Enabled="false"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_data" TextMode="MultiLine"></asp:TextBox>
        </div>


        <div>
            <asp:RadioButtonList runat="server" ID="rd_list_process">
                <asp:ListItem Value="Transaction">Search By Transaction ID</asp:ListItem>
                <asp:ListItem Value="Payer">Search By ID_Payer</asp:ListItem>
                <asp:ListItem Value ="Claim">Search By Claim ID</asp:ListItem>
                <asp:ListItem Value ="DHPO">Search in DHPO by ClaimID</asp:ListItem>
            </asp:RadioButtonList>
        </div>


        <%--<div>
            <asp:RadioButton runat="server" ID ="rd_byTransactionId" Text ="Search By Transaction ID" AutoPostBack="true" Checked ="true" OnCheckedChanged="rd_byTransactionId_CheckedChanged" />
        </div>
        <div>
            <asp:RadioButton runat="server" ID ="rd_byPayerId" Text ="Search By ID_Payer" AutoPostBack="true" OnCheckedChanged="rd_byPayerId_CheckedChanged"/>
        </div>
        <div>
            <asp:RadioButton runat ="server" ID ="rd_byClaimId" Text ="Search By Claim ID" AutoPostBack="true" OnCheckedChanged="rd_byClaimId_CheckedChanged"/>
        </div>--%>
        <div>
            <asp:Button runat="server" ID="lbl_generate" Text="Generate Query" OnClick="lbl_generate_Click"></asp:Button>
        </div>
        <div>
            <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
        </div>
        <div>
            <asp:Button runat ="server" ID="btn_claimdetails" Text="Submit" OnClick="btn_claimdetails_Click" />
        </div>
    </div>


</asp:Content>
