<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PBMSwitch_Sender_Update.aspx.cs" Inherits="ClinicianAutomation.Utilities_UI.PBMSwitch_Sender_Update" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>PBMSwitch Sender License Update</h1>
    </div>
    <div>
        <div>
            <asp:Label runat="server" ID="lbl_providerlicense" Text="Provider License"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_providerlicense"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" ID="lbl_Senderlicense" Text="Sender License"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_senderLicense"></asp:TextBox>
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
