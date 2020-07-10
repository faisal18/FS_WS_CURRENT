<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DHPO-ProviderUpdate.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.DHPO_ProviderUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
      <div>
        <div class="text-center" style="text-decoration: underline">
           <h1> <strong><em>DHPO- Provider Update
    </em></strong></h1>
    </div>
        <div>
        Provider License
    </div>
        <div>
            <asp:TextBox ID="txtProviderLic" runat="server"></asp:TextBox>
    </div>
        <div>
            <asp:RadioButtonList ID="rdOperations" runat="server" OnSelectedIndexChanged="rdOperations_SelectedIndexChanged" AutoPostBack="True">
                <asp:ListItem Selected="True" Value="Update Password">Update Password</asp:ListItem>
                <asp:ListItem Value="Update Username and Password">Update Username and Password</asp:ListItem>
                <%--<asp:ListItem Value="Set to Null">Update Credential to Null</asp:ListItem>--%>
            </asp:RadioButtonList>
    </div>
        <div>
        Username
    </div>
        <div>
        <asp:TextBox ID="txtProvUserName" runat="server" Enabled="False"></asp:TextBox>
    </div>
        <div>
        Password
    </div>
        <div>
        <asp:TextBox ID="txtProvPass" runat="server"></asp:TextBox>
    </div>
   <%--     <div>
        New License
    </div>--%>
        
      

        <div>
            <asp:Button ID="btnGenerateQuery" runat="server" Text="Generate Script" OnClick="btnGenerateQuery_Click" />
           
        </div>
             <div>
                  <asp:Button ID="btnClearQuery" runat="server" Text="Clear All" OnClick="btnClearQuery_Click" />
        </div>
         
        <div>
             <asp:TextBox ID="txtFullLog" runat="server" Height="196px" Width="921px" TextMode="MultiLine"  ></asp:TextBox>

        </div>
         <div>
                  <asp:Button ID="btnSaveScript" runat="server" Text="Save Script" OnClick="btnSaveScript_Click" />
        </div>
    </div>
</asp:Content>
