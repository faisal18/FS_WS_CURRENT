<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PBMSwitch_Accpted.aspx.cs" Inherits="ClinicianAutomation.Utilities_UI.PBMSwitch_Accpted" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h1>Prior Authourization Accepted</h1>
    </div>

     <div>
         <div>
             <asp:Label runat="server" ID="lbl_data" Text="Transaction IDs"></asp:Label>
         </div>
         <div>
             <asp:TextBox runat="server" ID="txt_data" TextMode="MultiLine" ></asp:TextBox>
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
