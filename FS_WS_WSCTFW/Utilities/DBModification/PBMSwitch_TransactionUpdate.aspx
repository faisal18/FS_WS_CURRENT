<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PBMSwitch_TransactionUpdate.aspx.cs" Inherits="ClinicianAutomation.Utilities_UI.PBMSwitch_TransactionUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>PBM Switch Transaction Update</h1>
    </div>
    <div>
        <div>
            <asp:Label runat="server" Text ="Data">
            </asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_data" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div>
            <asp:DropDownList runat="server" ID="ddl_status">
                <asp:ListItem Text="AUTHORIZATION_DOWNLOADED" Value="3"></asp:ListItem>
                <asp:ListItem Text="PRESCRIPTION_DISPENCED" Value="4"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div>
            <asp:RadioButton runat="server" ID="rd_requestid" Text="Update by Request ID" AutoPostBack="true" Checked="true" OnCheckedChanged="rd_requestid_CheckedChanged" />
        </div>
        <div>
            <asp:RadioButton runat="server" ID="rd_payerid" Text ="Update by ID_Payer" AutoPostBack="true" OnCheckedChanged="rd_payerid_CheckedChanged" />
        </div>
        <div>
            <asp:Button runat="server" ID="btn_generatequery" Text="Generate Query" OnClick="btn_generatequery_Click" />
        </div>
         <div>
            <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
        </div>
         <div>
            <asp:Button runat="server" ID="btn_submit" Text="Submit" OnClick="btn_submit_Click" />
        </div>
    </div>
</asp:Content>
