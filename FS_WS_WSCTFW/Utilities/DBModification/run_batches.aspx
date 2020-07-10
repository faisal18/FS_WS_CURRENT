<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="run_batches.aspx.cs" Inherits="ClinicianAutomation.run_batches" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="title">
        <h1>Application Flow</h1>
    </div>
    <div id="content">
        <asp:Table runat="server" ID="tbl_main" CellPadding="10" CellSpacing="1">
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" ID="lbl_pbmlink" Text="PBM Link" Enabled="false"></asp:Label></asp:TableCell>
                <asp:TableCell><asp:Button runat="server" ID="btn_pbmlink" Text="Submit" OnClick="btn_pbmlink_Click" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" ID="lbl_eclaim" Text="eClaim" Enabled="false"></asp:Label></asp:TableCell>
                <asp:TableCell><asp:Button runat="server" ID="btn_eclaim" Text="Submit" OnClick="btn_eclaim_Click" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" ID="lbl_erxPharmacy" Text="ERX Pharmacy" Enabled="false"></asp:Label></asp:TableCell>
                <asp:TableCell><asp:Button runat="server" ID="btn_erxPharmacy" Text="Submit" OnClick="btn_erxPharmacy_Click" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" ID="lbl_erxclinician" Text="ERX Clinician" Enabled="false"></asp:Label></asp:TableCell>
                <asp:TableCell> <asp:Button runat="server" ID="btn_erxclinician" Text="Submit" OnClick="btn_erxclinician_Click" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" ID="lbl_erxAuthourization" Text="ERX Authourization" Enabled="false"></asp:Label></asp:TableCell>
                <asp:TableCell><asp:Button runat="server" ID="btn_erxAuthourization" Text="Submit" OnClick="btn_erxAuthourization_Click" /></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" ID="lbl_oicProvider" Text="OIC Provider" Enabled="false"></asp:Label></asp:TableCell>
                <asp:TableCell><asp:Button runat="server" ID="btn_oicProvider" Text="Submit" OnClick="btn_oicProvider_Click" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <div>
            <asp:Label runat="server" ID="lbl_response"></asp:Label>
        </div>
    </div>
</asp:Content>
