<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Get_Clinician_Details.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.DBModification.Get_Clinician_Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Clincian Details</h1>

    <div>
        <asp:Label runat="server" Text="Enter License(s)"></asp:Label>
    </div>
    <div>
        <asp:TextBox runat="server" ID="txt_license" TextMode="MultiLine"></asp:TextBox>
    </div>
    <div>
        <asp:Button runat="server" ID="btn_submit" Text="Submit" OnClick="btn_submit_Click" />
    </div>
    <div>
        <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
    </div>


</asp:Content>
