<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="get_credentials.aspx.cs" Inherits="ClinicianAutomation.get_credentials" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div id ="title">
        <h1>Get Credentials</h1>

    </div>
    <div id="content">
        <table>
            <tr>
                <td><asp:Label runat="server" ID="lbl_providerid" Text="Provider id" ></asp:Label></td>
                <td><asp:TextBox runat="server" ID="txt_providerid" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lbl_payerid" Text="Payer id" ></asp:Label></td>
                <td><asp:TextBox runat="server" ID="txt_payerid" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
             <tr>
                <td><asp:RadioButton runat="server" ID="rd_provider" Text="Get by Provider"  OnCheckedChanged="rd_provider_CheckedChanged" AutoPostBack="True" /></td>
            </tr>
            <tr>
                <td><asp:RadioButton runat="server" ID="rd_payer"  Text="Get by Payer" OnCheckedChanged="rd_payer_CheckedChanged" AutoPostBack="True" /></td>
            </tr>

             <tr>
                <td><asp:Button runat="server" ID="submit" Text="Update" OnClick="submit_Click" /></td>
                <td><asp:Label runat="server" ID="lbl_response" ></asp:Label></td>
            </tr>
        </table>
   </div>
</asp:Content>
