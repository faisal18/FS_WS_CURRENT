<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OIC_Process_Transaction.aspx.cs" Inherits="ClinicianAutomation.OIC_Process_Transaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="title">
        <h1>OIC Process Transaction</h1>

    </div>
    <div id="content">
        <div>
            <asp:RadioButton runat="server" ID="rd_processed" Text="For Processed Transaction" AutoPostBack="true" Checked="true" OnCheckedChanged="rd_processed_CheckedChanged" />
        </div>
        <div>
            <asp:RadioButton runat="server" ID="rd_new" Text="For New Transaction" AutoPostBack="true" OnCheckedChanged="rd_new_CheckedChanged" />
        </div>
        <div>
            <asp:Label runat="server" ID="lbl_payerid" Text="Payer ID"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_Payerid"></asp:TextBox></div>
        <div>
            <asp:Label runat="server" ID="lbl_auth_date" Text="Auhtourization Date(yyyy-MM-dd HH:mm)"></asp:Label></div>
        <div>
            <asp:TextBox runat="server" ID="txt_auth_Date"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" ID="lbl_tat" Text="TAT"></asp:Label></div>
        <div>
            <asp:TextBox runat="server" ID="txt_tat"></asp:TextBox></div>
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
