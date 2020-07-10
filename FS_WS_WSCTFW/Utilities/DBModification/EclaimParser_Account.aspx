<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EclaimParser_Account.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.DBModification.EclaimParser_Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Eclaim Parser Account Activation</h1>

    <div>
        <asp:Label runat="server" ID="lbl_providerlicense" Text="Provider License"></asp:Label>
    </div>

    <div>
        <asp:TextBox runat="server" ID="txt_providerlicense" TextMode="MultiLine"></asp:TextBox>
    </div>

    <div>
        <asp:RadioButtonList runat="server" ID="rd_list">
            <asp:ListItem Value="activate">Activate</asp:ListItem>
            <asp:ListItem Value="deactivate">Deactivate</asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div>
        <asp:Button runat="server" ID="btn_submit" Text="Submit" OnClick="btn_submit_Click" />
    </div>
    <div>
        <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
    </div>




</asp:Content>
