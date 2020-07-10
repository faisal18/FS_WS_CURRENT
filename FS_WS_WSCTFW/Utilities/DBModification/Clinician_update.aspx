<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clinician_update.aspx.cs" Inherits="ClinicianAutomation.Clinician_update" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div id ="title">
        <h1>Clinician Update</h1>
    </div>

    <div id="content">
        <table>
            <tr>
                <td><asp:Label runat="server" ID="lbl_license" Text="Clinician license"></asp:Label></td>
                <td><asp:TextBox runat="server" ID="txt_license"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lbl_username" Text="Clinician Username"></asp:Label></td>
                <td><asp:TextBox runat="server" ID="txt_username"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Label runat="server" ID="lbl_password" Text="Clincian Password"></asp:Label></td>
                <td><asp:TextBox runat="server" ID="txt_password"></asp:TextBox></td>
            </tr>
            <tr>
                <td><asp:Button runat="server" ID="submit" Text="submit" OnClick="submit_Click" /></td>
                <td><asp:Label runat="server" ID="lbl_response" ></asp:Label></td>
            </tr>
            <tr>
                <td><asp:Button runat="server" ID="btn_clear" Text="Clear all fields" OnClick="btn_clear_Click" /></td>
            </tr>
        </table>
    </div>


</asp:Content>
