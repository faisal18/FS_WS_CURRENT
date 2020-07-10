<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Utilities.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.Utilities" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Panel ID="Cloud" GroupingText="Cloud Servers" runat="server">
        <ul>
            <li><a runat="server" href="~/Utilities/ServerMaintenance/CEED.aspx">CEED</a></li>
            <li><a runat="server" href="~/Utilities/ServerMaintenance/eClaimParser.aspx">eClaimParser</a></li>
            <li><a runat="server" href="~/Utilities/ServerMaintenance/ClearingHouse-eClaimExpress.aspx">ClearingHouse/eClaimExpress</a></li>
            <li><a runat="server" href="~/Utilities/ServerMaintenance/LMU.aspx">LMU</a></li>
        </ul>
    </asp:Panel>



    <asp:Panel ID="eHDF" GroupingText="eHDF Servers" runat="server">
        <ul>

            <li><a runat="server" href="~/Utilities/ServerMaintenance/DHPO.aspx">DHPO</a></li>
            <li><a runat="server" href="~/Utilities/ServerMaintenance/eClaimLink.aspx">eClaimLink</a></li>
            <li><a runat="server" href="~/Utilities/ServerMaintenance/eRxPharmacy-eRxClinician.aspx">eRX Pharmacy/Clinician</a></li>

        </ul>
    </asp:Panel>


    <asp:Panel ID="HypeV" GroupingText="Hyper V Servers" runat="server">
        <ul>
            <li><a runat="server" href="~/Utilities/ServerMaintenance/PBMSwitch.aspx">PBMSwitch</a></li>
            <li><a runat="server" href="~/Utilities/ServerMaintenance/PBMLink.aspx">PBMLINK</a></li>

        </ul>
    </asp:Panel>

    <asp:Panel ID="FTP" GroupingText="FTP Server" runat="server">
        <ul>
            <li><a runat="server" href="~/Utilities/FTPCHecker.aspx">FTP Check</a></li>


        </ul>
    </asp:Panel>
    <asp:Panel ID="Automation_Services" GroupingText="Automation Server Services" runat="server">
        <ul>
            <li><a runat="server" href="~/Utilities/CiscoVPN.aspx">Cisco Service Restart</a></li>
            <li><a runat="server" href="~/Utilities/EmailCheck.aspx">Email Executor</a></li>
        </ul>
    </asp:Panel>

    <asp:Panel ID="WS_Services" GroupingText="Webservices Utilities" runat="server">
        <ul>
            <li><a runat="server" href="http://automation.dimensions-healthcare.com/serviceall/Default.aspx">Webservices Portal</a></li>
            <li><a runat="server" href="~/Dashboard/Dashboard_UI.aspx">Dimensions Dashboard</a></li>
            <li><a runat="server" href="http://automation.dimensions-healthcare.com/serviceall/utilities_UI/Greenrain.aspx">Shafafiya</a></li>
            <li><a runat="server" href="http://automation.dimensions-healthcare.com/serviceall/utilities_UI/NahdiVPN.aspx">AlNahdi Webservices</a></li>
            <li><a runat="server" href="~/Utilities/DBModification/Generate_CustomQuery.aspx">Query Generator</a></li>
        </ul>
    </asp:Panel>

    <%-- <div class="navbar-collapse collapse">
             <ul >
             <li><a runat="server" href="~/Utilities/ServerMaintenance/PBMSwitch.aspx">PBMSwitch</a></li>
             <li><a runat="server" href="~/Utilities/ServerMaintenance/PBMLink.aspx">PBMLINK</a></li>
             <li><a runat="server" href="~/Utilities/ServerMaintenance/DHPO.aspx">DHPO</a></li>
             <li><a runat="server" href="~/Utilities/ServerMaintenance/eClaimLink.aspx">eClaimLink</a></li>
             <li><a runat="server" href="~/Utilities/ServerMaintenance/eRxPharmacy-eRxClinician.aspx">eRX Pharmacy/Clinician</a></li>
             <li><a runat="server" href="~/Utilities/ServerMaintenance/CEED.aspx">CEED</a></li>
             <li><a runat="server" href="~/Utilities/ServerMaintenance/eClaimParser.aspx">eClaimParser</a></li>
             <li><a runat="server" href="~/Utilities/ServerMaintenance/ClearingHouse-eClaimExpress.aspx">ClearingHouse/eClaimExpress</a></li>
            <li><a runat="server" href="~/Utilities/ServerMaintenance/LMU.aspx">LMU</a></li>
             </ul>
         </div>--%>

    <%-- <div class="navbar-collapse collapse"> 
        <a runat="server" href="BucketMappingGenerator.aspx">Bucket Mapping SQL Generator</a>

    </div>--%>
</asp:Content>
