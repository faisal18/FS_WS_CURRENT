<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Submit_Claim.aspx.cs" Inherits="ClinicianAutomation.Submit_Claim" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="title">
        <h1>Eclaimlink Claims Submission Update</h1>

    </div>
    <div id="content">
        <div>
            <asp:Label runat="server" ID="lbl_claimid" Text="Claim ID"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_claimid"></asp:TextBox>
        </div>
        <div>
            <asp:Button runat="server" ID="lbl_generate" Text="Generate Query" OnClick="lbl_generate_Click"></asp:Button>
        </div>
        <div>
            <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
        </div>
        <div>
            <asp:Button runat="server" ID="submit" Text="Update" OnClick="submit_Click" />
        </div>
    </div>


</asp:Content>
