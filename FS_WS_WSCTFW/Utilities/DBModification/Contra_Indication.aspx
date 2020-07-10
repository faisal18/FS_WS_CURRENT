<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contra_Indication.aspx.cs" Inherits="ClinicianAutomation.Contra_Indication" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div id="title">
        <h1>Indication/Contra Indication</h1>
    </div>
    <div id="content">
       
        <div>
            <asp:Label runat="server" ID="lbl_data" Text="Please Enter Data" Enabled="false"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_data" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div>======================= ContraIndications ==================================</div>
        <div>
            <asp:RadioButton runat="server" ID="rd_ContraIndicationByClaimId" Text =" ContraIndication By ClaimId" AutoPostBack="true" OnCheckedChanged="rd_ContraIndicationByClaimId_CheckedChanged" Checked="true" />
        </div>
        <div>
            <asp:RadioButton runat="server" ID ="rd_ContraIndicationByDrugCode" Text =" ContraIndication By DrugCode" AutoPostBack ="true" OnCheckedChanged="rd_ContraIndicationByDrugCode_CheckedChanged" />
        </div>
        <div>
            <asp:RadioButton runat="server" ID ="rd_ContraIndicationByDiagnosisCode" Text =" ContraIndication By DiagnosisCode" AutoPostBack="true" OnCheckedChanged="rd_ContraIndicationByDiagnosisCode_CheckedChanged" />
        </div>
        <div>======================= Indications ==================================</div>
        <div>
            <asp:RadioButton runat="server" ID="rd_IndicationByClaimId" Text =" Indication By ClaimId" AutoPostBack="true" OnCheckedChanged="rd_IndicationByClaimId_CheckedChanged" />
        </div>
        <div>
            <asp:RadioButton runat="server" ID="rd_IndicationByDrugCode" Text=" Indication By DrugCode" AutoPostBack="true" OnCheckedChanged="rd_IndicationByDrugCode_CheckedChanged" />
        </div>
        <div>
            <asp:RadioButton runat="server" ID ="rd_IndicationByDiagnosisCode" Text =" Indication By DiagnosisCode" AutoPostBack="true" OnCheckedChanged="rd_IndicationByDiagnosisCode_CheckedChanged" />
        </div>
        <div>
            <asp:Button runat="server" ID="lbl_generate" Text="Generate Query" OnClick="lbl_generate_Click"></asp:Button>
        </div>
        <div>
            <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
        </div>
        <div>
            <asp:Button runat="server" ID="btn_data_submit" Text="Submit" OnClick="btn_data_submit_Click" />
        </div>
    </div>


</asp:Content>
