<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Get_Drug_Details.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.DBModification.Get_Drug_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h1>Get Drug Details</h1>
    </div>

    <div>
        <asp:Label runat="server" Text="Drug ID"></asp:Label>
    </div>
    <div>
        <asp:TextBox runat="server" ID="txt_drug_id" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div>
        <asp:Button runat="server" ID="btn_submit" Text="Submit" OnClick="btn_submit_Click" />
    </div>
    <div>
        <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
    </div>
</asp:Content>
