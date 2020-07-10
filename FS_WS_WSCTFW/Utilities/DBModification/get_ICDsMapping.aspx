<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="get_ICDsMapping.aspx.cs" Inherits="ClinicianAutomation.Utilities_UI.get_ICDsMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>ICD's Mapping
        </h1>
    </div>
    <div>
        <asp:RadioButton runat="server" ID="rd_byICD9" Text="By ICD 9" Checked="true" AutoPostBack="true" OnCheckedChanged="rd_byICD9_CheckedChanged" />
    </div>
    <div>
        <asp:RadioButton runat="server" ID="rd_byICD10" Text="By ICD 10"  AutoPostBack="true" OnCheckedChanged="rd_byICD10_CheckedChanged" />
    </div>
    <div>
        <asp:RadioButton runat="server" ID="rd_byBoth" Text="Get All ICDs from PBMSwitch" AutoPostBack="true" OnCheckedChanged="rd_byBoth_CheckedChanged" />
    </div>
    <div runat="server" id="div_icd9" style="display: block">
        <div>
            <asp:Label runat="server" ID="lbl_icd9" Text="Enter ICD 9"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_icd9" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div runat="server" id="div_icd10" style="display: none">
        <div>
            <asp:Label runat="server" ID="lbl_icd10" Text="Enter ICD 10"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_icd10" TextMode="MultiLine"></asp:TextBox>
        </div>
    </div>
    <div>
        <asp:Button runat="server" ID="btn_submit" Text="Submit" OnClick="btn_submit_Click" />
 
    </div>
    <div>
        <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
    </div>
           <asp:Button runat="server" ID="btn_SaveCSV" Visible="false" Text="Save CSV" OnClick="btn_SaveCSV_Click"  />
</asp:Content>
