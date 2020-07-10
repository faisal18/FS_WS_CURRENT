<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ERX_PO_update.aspx.cs" Inherits="ClinicianAutomation.erX_script" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="title">
        <h1>Provider Erx PO Update</h1>

    </div>
    <div id="content">
        <div>
            <asp:Label runat="server" ID="lbl_license" Text="Provider license" Enabled="false"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_license"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" ID="lbl_username" Text="Provider PO Username" Enabled="false"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_username"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" ID="lbl_password" Text="Provider PO Password" Enabled="false"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_password"></asp:TextBox>
        </div>
        <div>
            <asp:RadioButton runat="server" ID="rd_all" Text="Update username & password" OnCheckedChanged="rd_all_CheckedChanged" AutoPostBack="True" />
        </div>
        <div>
            <asp:RadioButton runat="server" ID="rd_passwod" Text="Update password" OnCheckedChanged="rd_passwod_CheckedChanged" AutoPostBack="True" />
        </div>
        <div>
            <asp:RadioButton runat="server" ID="rd_DHPO" Text="Update by DHPO" OnCheckedChanged="rd_DHPO_CheckedChanged" AutoPostBack="true" />
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
