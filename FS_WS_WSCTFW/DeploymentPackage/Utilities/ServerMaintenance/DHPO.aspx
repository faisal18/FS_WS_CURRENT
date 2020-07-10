<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DHPO.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.ServerMaintenance.DHPO" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
       <asp:Panel runat="server" GroupingText="DHPO">
            <asp:Panel runat="server" GroupingText="DHPO - Web Server 1 "> 
                <div>
                     <asp:Button ID="btnRstrtIIS" runat="server" OnClick="btnRstrtIIS_Click" Text="Restart DHPO Web 1 - Production" Width="401px" />
    <%--<asp:label runat="server" Text =" " ID="lblRstrtSpooler"></asp:label>--%>
                     &nbsp;<%--<asp:Button ID="btnRstrtPBMSwtchPrdGlfish" runat="server" OnClick="btnRstrtPBMSwtchPrdGlfish_Click" Text="Restart PBM Switch Glassfish" Width="404px" />--%><br /></div>   </asp:Panel>  
          
          <asp:Panel runat="server" GroupingText="DHPO - Web Server 2 "> 
                <div>
                     <asp:Button ID="btnPBMProd" runat="server" OnClick="btnPBMProd_Click" Text="Restart DHPO Web 2 - Production" Width="401px" />
    <%--<asp:label runat="server" Text =" " ID="Label1"></asp:label>--%>
                     &nbsp;<%--<asp:Button ID="btnRstrtPBMSwtchPrdGlfish" runat="server" OnClick="btnRstrtPBMSwtchPrdGlfish_Click" Text="Restart PBM Switch Glassfish" Width="404px" />--%><br /></div>   </asp:Panel>  
          
               <asp:Panel runat="server" GroupingText="DHPO - SQL Server "> 
                <div>
                     <asp:Button ID="btnDHPODB" runat="server"  Text="Restart DHPO SQL Server" Width="401px" OnClick="btnDHPODB_Click" enabled="false"/>
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
