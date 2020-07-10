<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"  AsyncTimeout="3600" AutoEventWireup="true" CodeBehind="Contra_Indication_Edit.aspx.cs" Inherits="ClinicianAutomation.Utilities_UI.Contra_Indication_Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div id="title">
        <h1>Edit Indication/Contra Indication</h1>
    </div>
    <div id="content">

        <div>
            <asp:Label runat="server" ID="lbl_data" Text="Please Enter Data" Enabled="false"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_data" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div>
            <asp:RadioButton runat="server" ID="ContraIndicaitonByScientificCode" Text="ContraIndicaiton By Scientific Code" Checked ="true" AutoPostBack="true" OnCheckedChanged="ContraIndicaitonByScientificCode_CheckedChanged" />
        </div>
        <div>
            <asp:RadioButton runat="server" ID="ContraIndicationByDrugCode" Text="ContraIndicaiton By Drug Code"  AutoPostBack="true" OnCheckedChanged="ContraIndicationByDrugCode_CheckedChanged" />
        </div>
        <div>
            <asp:RadioButton runat="server" ID="IndicationByScientificCode" Text="Indication By Scientific Code"  AutoPostBack="true" OnCheckedChanged="IndicationByScientificCode_CheckedChanged" />
        </div>
        <div>
            <asp:RadioButton runat="server" ID="IndicationByDrugCode" Text="Indication By Drug Code"  AutoPostBack="true" OnCheckedChanged="IndicationByDrugCode_CheckedChanged" />
        </div>
        <div>
            <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
        </div>
        <div>
            <asp:CheckBox runat="server" ID="chk_pbmm" Text ="PBM Switch" />
            <asp:CheckBox runat="server" ID ="chk_pbmuat" Text ="PBM UAT" />
        </div>
        <div>
            <asp:Button runat="server" ID ="btn_submit" Text="Submit" OnClick="btn_submit_Click" />
        </div>
    </div>
</asp:Content>
