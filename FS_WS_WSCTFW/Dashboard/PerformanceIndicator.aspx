<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PerformanceIndicator.aspx.cs" Inherits="FS_WS_WSCTFW.Dashboard.PerformanceIndicator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel ID="DataPanel" runat="server">
       
    </asp:Panel>
     <asp:Panel ID="BtnPanel" runat="server">
         <div>
             <asp:Button ID ="BtnReload" runat="server" Text="Refresh"/>
         </div>
     <div><asp:Button ID ="BtnEmail" runat="server" Text="Send Email to Techsupport" Enabled ="false"/>
         </div>
           
     </asp:Panel>
</asp:Content>
