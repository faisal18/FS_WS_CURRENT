<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageWebServices.aspx.cs" Inherits="FS_WS_WSCTFW.WSCaller.ManageWebServices" EnableViewState="True" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1> Manage Web Service Requests</h1>

     <asp:Panel ID="PnlWSRequest" runat="server" CssClass="panel"  >
         <div class="form-group">
                    <div>  <div>Environment Name</div>
                    <div><asp:DropDownList ID="WSEnv" runat="server" ForeColor="Black" BackColor="White" AutoPostBack="True" OnSelectedIndexChanged="WSEnv_SelectedIndexChanged"></asp:DropDownList></div></div>
              <div>  <div>Method Name</div>
                    <div><asp:DropDownList ID="wsMethod" runat="server" ForeColor="Black" BackColor="White" AutoPostBack="True" OnSelectedIndexChanged="wsMethod_SelectedIndexChanged"></asp:DropDownList></div></div>
                 <%--   <div> <div>Soap Operation</div>
                    <div><asp:DropDownList ID ="WSOperation" runat="server"></asp:DropDownList> </div></div>--%>
                    <div>  <div>Request</div>
                    <div style="direction: ltr"><asp:TextBox ID="txtWSRequest" runat="server" Width="808px" Height="129px" TextMode="MultiLine" Visible ="false"></asp:TextBox></div></div>
                    <asp:Panel ID="pnldHtml" runat="server"></asp:Panel>
              <div>
                    <asp:Button ID="btnSubmit" runat="server" Text="Call WebService" OnClick="btnSubmit_Click" EnableViewState ="true"/>
                    </div>
             
             <div>  <div>Response</div>
                    <div><asp:TextBox ID="txtWSResponse" runat="server" Width="812px" Height="135px" TextMode="MultiLine" Visible="false"></asp:TextBox></div></div>
                

                   
         </div>
    </asp:Panel>

</asp:Content>
