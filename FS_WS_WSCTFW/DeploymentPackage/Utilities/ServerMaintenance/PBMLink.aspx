﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PBMLink.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.ServerMaintenance.PBMLink" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Panel runat="server" GroupingText="PBMLink">
            <asp:Panel runat="server" GroupingText="PBMLink - IIS"> 
                <div>
                     <asp:Button ID="btnRstrtIIS" runat="server" OnClick="btnRstrtIIS_Click" Text="Restart PBMLink Service - IIS" Width="401px" />
    <%--<asp:label runat="server" Text =" " ID="lblRstrtSpooler"></asp:label>--%>
                     &nbsp;<%--<asp:Button ID="btnRstrtPBMSwtchPrdGlfish" runat="server" OnClick="btnRstrtPBMSwtchPrdGlfish_Click" Text="Restart PBM Switch Glassfish" Width="404px" />--%><br /></div>   </asp:Panel>  
          
          <asp:Panel runat="server" GroupingText="PBMLink - SQL Server"> 
                <div>
                     <asp:Button ID="btnPBMProd" runat="server" OnClick="btnPBMProd_Click" Text="Restart PBMLink Service - SQL Server" Width="401px" />
    <%--<asp:label runat="server" Text =" " ID="Label1"></asp:label>--%>
                     &nbsp;<%--<asp:Button ID="btnRstrtPBMSwtchPrdGlfish" runat="server" OnClick="btnRstrtPBMSwtchPrdGlfish_Click" Text="Restart PBM Switch Glassfish" Width="404px" />--%><br /></div>   </asp:Panel>  
          
                
               <div> 
                    <br /><div><asp:Label ID ="lblStatus" runat="server"></asp:Label></div>
                    <br />
                    <div><asp:TextBox ID="txtStatus" runat="server" Enabled="true" Height="350px" ReadOnly="True" TextMode="MultiLine" Width="1178px"></asp:TextBox></div>
                    
                    <br />
                </div>
        
            </asp:Panel>
</asp:Content>
