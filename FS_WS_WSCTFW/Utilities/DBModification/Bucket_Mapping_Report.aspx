<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Bucket_Mapping_Report.aspx.cs" Inherits="ClinicianAutomation.Utilities_UI.Bucket_Mapping_Report" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>
            Bucket Mapping Report
        </h1>
    </div>
    <div>
        <div>
            <asp:Label runat="server" ID="lbl_dubailicense" Text="Dubai License No"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_dubailicense" ></asp:TextBox>
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
