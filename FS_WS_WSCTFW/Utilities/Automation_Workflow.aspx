<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Automation_Workflow.aspx.cs" Inherits="ClinicianAutomation.Automation_Workflow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="title">
        <h1>Automation Workflow</h1>
    </div>

    <div id="content">
        <fieldset>
            <legend>Workflows</legend>
            <ul>
                <li><a runat="server" href="~/Utilities/DBModification/run_batches.aspx">Application UI Workflow</a></li>
                <li><a runat="server" href="http://automation.dimensions-healthcare.com/serviceall/utilities_UI/Validate_Services.aspx">Services Workflow</a></li>
                <li><a runat="server" href="~/Utilities/DBModification/AIMS_RunCases.aspx">AIMS Workflow</a></li>
            
            </ul>
        </fieldset>
        
    </div>


</asp:Content>
