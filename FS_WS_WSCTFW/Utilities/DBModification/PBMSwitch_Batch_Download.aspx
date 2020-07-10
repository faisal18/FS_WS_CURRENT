<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PBMSwitch_Batch_Download.aspx.cs" Inherits="ClinicianAutomation.Utilities_UI.PBMSwitch_Batch_Download" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>PBMSwitch Batch Download</h1>
    </div>
     <div>
        <div>
            <asp:Label runat="server" ID="lbl_batch" Text="Batch ID"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_batch_licenses" TextMode="MultiLine" ></asp:TextBox>
        </div>
         <div>
            <asp:Button runat="server" ID="btn_generate_query" Text="Generate Query" OnClick="btn_generate_query_Click" />
        </div>
        <div>
            <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
        </div>
         <div>
            <asp:Button runat="server" ID="btn_submit" Text="Submit" OnClick="btn_submit_Click" />
        </div>
    </div>
</asp:Content>
