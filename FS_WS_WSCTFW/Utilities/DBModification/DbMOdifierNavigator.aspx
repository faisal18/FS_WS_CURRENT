<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DbMOdifierNavigator.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.DBModification.DbMOdifierNavigator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div id="title">
        <h1>Utilities - Praise the developers</h1>
    </div>

    <div id="content">
        <fieldset>
            <legend>Miscellaneous</legend>
            <ul>
                <%--<li><a runat="server" href="Clinician_update.aspx">Clinician Update</a></li>--%>

                <li><a runat="server" href="get_credentials.aspx">Get Credentials</a></li>
                <li><a runat="server" href="Member_details.aspx">Get Member Details</a></li>
                <li><a runat="server" href="~/Utilities/DBModification/Get_Drug_Details.aspx">Get Drug Details</a></li>
                <li><a runat="server" href="~/Utilities/DBModification/Generate_CustomQuery.aspx">Generate Custom Query</a></li>
                <li><a runat="server" href="~/Utilities/DBModification/GetClaimDetails.aspx">Get Transaction/Claim Details</a></li>
                <li><a runat="server" href="~/Utilities/DrugDownloader.aspx">Drug Details Download</a></li>
                <li><a runat="server" href="~/Utilities/DBModification/SendEmail_BQS.aspx">Send Email</a></li>
                <li><a runat="server" href="~/Utilities/DBModification/Suspend_Clinician.aspx">Suspend Clinician</a></li>
                <li><a runat="server" href="~/Utilities/DBModification/Get_Drug_Details.aspx">Get Drug Details</a></li>
                <li><a runat="server" href="~/Utilities/DBModification/Get_Clinician_Details.aspx">Get Clinician Details</a></li>
                <li><a runat="server" href="~/Utilities/DBModification/Transaction_TAT.aspx">Transaction TAT Report</a></li>


            </ul>
        </fieldset>

        <fieldset>
            <legend>Reports</legend>
            <ul>
                <li><a runat="server" href="Get_TurnAroundTime.aspx">Get TAT time</a></li>
                <li><a runat="server" href="~/Utilities/Payer_SubmissionTransactions_Report.aspx">Payer Submission Report</a></li>
                <li><a runat="server" href="Transactions_Detail_Report.aspx">Payer Transaction Details (Count/Details)</a></li>
            </ul>
        </fieldset>
        <fieldset>
            <legend>Indication/Contra Indication</legend>
            <ul>
                <li><a runat="server" href="~/Utilities/DBModification/Contra_Indication.aspx">Get Indication/Contra Indication</a></li>
                <li><a runat="server" href="~/Utilities/DBModification/Contra_Indication_Edit.aspx">Indication/Contra Indication Edit</a></li>

            </ul>
        </fieldset>
        <fieldset>
            <legend>EclaimLink Portal Update</legend>
            <ul>
                <li>
                    <a runat="server" href="EclaimLinkPortal_login_update.aspx">EclaimLink Portal Login</a>
                </li>
            </ul>
        </fieldset>
        <fieldset>
            <legend>EclaimLink Update</legend>
            <ul>
                <li>
                    <a runat="server" href="EclaimLink_Login_Update.aspx">EclaimLink Login</a>
                </li>
                <li>
                    <a runat="server" href="EclaimLink_PO_Update.aspx">EclaimLink PostOffice</a>
                </li>
                <li><a runat="server" href="Submit_Claim.aspx">Eclaimlink Claim Submit update</a></li>
            </ul>
        </fieldset>
        <fieldset>
            <legend>Eclaim Parser</legend>
            <ul>
                <li><a runat="server" href="~/Utilities/DBModification/EclaimParser_Account.aspx">Eclaim Parser Account Registration</a></li>
            </ul>
        </fieldset>
        <fieldset>
            <legend>ERX Update</legend>
            <ul>
                <li>
                    <a runat="server" href="ERX_Login_Update.aspx">ERX Login</a>
                </li>
                <li>
                    <a runat="server" href="ERX_PO_update.aspx">ERX PostOffice</a>
                </li>
                <li>
                    <a runat="server" href="ERX_Cancel_Transaction.aspx">ERX Eauthorization Cancel Transaction</a>
                </li>
                <li>
                    <a runat="server" href="ERX_Payer_Login_Update.aspx">ERX Payer Login update</a>
                </li>
            </ul>
        </fieldset>
        <fieldset>
            <legend>AIMS</legend>
            <ul>
                <li>
                    <a runat="server" href="Aims_Insert_Clinician.aspx">Aims Insert Clinician</a>
                </li>
            </ul>
        </fieldset>
        <fieldset>
            <legend>DHPO Update</legend>
            <ul>
                <li>
                    <a runat="server" href="DHPO_login_update.aspx">DHPO Login</a>
                </li>
                <li>
                    <a runat="server" href="DHPO_payer_login_update.aspx">DHPO Payer Login Update</a>
                </li>
                <li>
                    <a runat="server" href="~/Utilities/DBModification/DHPO_Clinician_Insert.aspx">DHPO Insert Clinician</a>
                </li>
            </ul>
        </fieldset>
        <fieldset>
            <legend>PBM Switch Update</legend>
            <ul>
                <li><a runat="server" href="PBMM_PO_Update.aspx">PBM Provider PO Update</a>                </li>
                <li><a runat="server" href="~/Utilities/DBModification/PBMM_Cancel_Transaction.aspx">PBMSwitch Cancel Transaction</a></li>
                <li><a runat="server" href="PBMSwitch_TransactionUpdate.aspx">PBSwitch Update Transaction Status</a></li>
                <li><a runat="server" href="PBMSwitch_Sender_Update.aspx">PBSwitch Update Provider Sender License</a></li>
                <li><a runat="server" href="PBMSwitch_Accpted.aspx">PBSwitch Update Authorisation Status to Accepted</a></li>
                <li><a runat="server" href="get_ICDsMapping.aspx">Get PBMSwitch ICD Mapping</a></li>
                <li><a runat="server" href="http://automation.dimensions-healthcare.com/serviceall/Utilities_UI/Provider_Registration.aspx">PBMSwitch Provider Registration</a></li>

            </ul>
        </fieldset>
        <fieldset>
            <legend>Batch Transactions</legend>
            <ul>
                <li><a runat="server" href="Generate_Batch_Transaction.aspx">Generate Batch for Transactions</a></li>
                <li><a runat="server" href="PBMSwitch_Batch_Download.aspx">PBMSwitch Batch Transaction Details</a></li>

            </ul>
        </fieldset>
        <fieldset>
            <legend>OIC Payer</legend>
            <ul>
                <li>
                    <a runat="server" href="OIC_Cancel_Transaction.aspx">OIC Cancel</a>
                </li>
                <li>
                    <a runat="server" href="OIC_Process_Transaction.aspx">OIC Process</a>
                </li>
            </ul>
        </fieldset>
        <fieldset>
            <legend>Bucket Mapping</legend>
            <ul>
                <li>
                    <a runat="server" href="~/Utilities/BucketMappingGenerator.aspx">Bucket Mapping Generator</a></li>
                <li><a runat="server" href="Bucket_Mapping_Report.aspx">Payer Bucket Mapping Details</a>
                </li>
            </ul>
        </fieldset>
        <fieldset>
            <legend>File Upload</legend>
            <ul>
                <li>
                    <a runat="server" href="upload_file.aspx">File Upload for Clinician</a>
                </li>
            </ul>
        </fieldset>
        <%--   <fieldset>
            <legend>OIC Provider</legend>
            <ul>
                <li>
                    <a runat="server" href="OIC_Provider_Cancel_Transaction.aspx">OIC Provider Cancel</a>
                </li>
            </ul>
        </fieldset>--%>
    </div>
</asp:Content>
