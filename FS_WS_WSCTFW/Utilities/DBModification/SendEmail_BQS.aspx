<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SendEmail_BQS.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.DBModification.SendEmail_BQS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Send Email </h1>
    <div>
        <div>
            <asp:Label runat="server" Text="Receipent Address"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_receipentAddress" TextMode="MultiLine"></asp:TextBox>
        </div>


        <div>
            <asp:Label runat="server" Text="CC Address"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_ccAddress" TextMode="MultiLine"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" Text="File Path"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_filePath"></asp:TextBox>
        </div>
        <div>
            <asp:Button runat="server" Text="Submit" ID="btn_Submit" OnClick="btn_Submit_Click" />
        </div>
        <div>
            <asp:Label runat="server" ID="lbl_result"></asp:Label>
        </div>
    </div>
</asp:Content>
