<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Get_DHPO_Transaction_Detail.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.DBModification.Get_DHPO_Transaction_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        DHPO TRANSACTION DETAIL
    </h1>
    <div>
        <asp:Label runat="server" Text="Data"></asp:Label>
    </div>
    <div>
        <asp:TextBox runat="server" ID="txt_data" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div>
        <asp:RadioButtonList runat="server" ID="rd_list_process">
            <asp:ListItem Enabled="true" Value="byClaimId">Search by ClaimId</asp:ListItem>
            <asp:ListItem Value="byFilename">Search by Filename</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div>
        <asp:Button runat="server" ID="btn_submit" Text="Submit" OnClick="btn_submit_Click" />
    </div>
    <div>
        <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
    </div>

</asp:Content>
