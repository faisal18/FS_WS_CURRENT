<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ManageEnvironments.aspx.cs" Inherits="FS_WS_WSCTFW.WSCaller.ManageEnvironments" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../Scripts/ManageEnvironments.js"></script>
    <script src="../Scripts/knockout-3.3.0.js"></script>
    <script src="../Scripts/jquery-1.10.2.js"></script>
    <h1> Manage Web Service Environments</h1>
    

    <asp:Panel ID="PnlWSEnvironment" runat="server" CssClass="panel-body"  >

        <div>Environment Name</div>
        
        <div><asp:TextBox ID="txtEnvName" runat="server" Width="500px"></asp:TextBox></div>
        <div>WS URL</div>
     <div><asp:TextBox ID="txtEnvUrl" runat="server" Width="500px"></asp:TextBox></div>
        <div>WS Endpoint</div>
      <div><asp:TextBox ID="txtEnvEndPoint" runat="server" Width="500px"></asp:TextBox></div>
        <div>WS WSDL</div>
      <div><asp:TextBox ID="txtEnvWSDL" runat="server" Width="500px"></asp:TextBox></div>
        <div>WS Username</div>
         <div><asp:TextBox ID="txtUseName" runat="server" Width="500px"></asp:TextBox></div>
           <div>WS Password</div>
         <div><asp:TextBox ID="txtUserPsswrd" runat="server" Width="500px" ></asp:TextBox></div>
         <div></div>
         <div><asp:CheckBox ID="chkPublic" runat="server" Width="500px" Text="Allow Public Sharing"></asp:CheckBox></div>
        <div></div>
         <div><asp:CheckBox ID="checkisActive" runat="server" Width="500px" Text="Active"></asp:CheckBox></div>
        <div></div>
         <div>
             <asp:Button ID="btnSubmit" runat="server" Text="Save" Height="30px" OnClick="btnSubmit_Click" Width="100px" />
         </div>

    </asp:Panel>
    <asp:Panel ID="PnlGridView" runat ="server"> 

        <div class="table">
        <asp:GridView ID="DG_AllData" runat="server" 
            Width ="100%" 
            AlternatingRowStyle-BackColor="#0099cc" 
            HeaderStyle-BorderStyle="Solid" 
            HeaderStyle-BackColor="#000000" 
            BorderStyle ="Solid"
            allowpaging ="true">
          
        </asp:GridView>  </div>
    </asp:Panel>


</asp:Content>
