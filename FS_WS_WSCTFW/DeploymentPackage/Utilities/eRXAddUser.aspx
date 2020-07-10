<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="eRXAddUser.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.eRXAddUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Panel ID="CtrlPanel" runat ="server" GroupingText="Enter Values">
            <div>Provider License</div>
            <div> <asp:TextBox ID="txtProviderLicense" runat="server"></asp:TextBox></div>
            <div>Provider Username(Admin)</div>
            <div> <asp:TextBox ID="txtProviderUsername" runat="server"></asp:TextBox>
             
            </div>
            <div>   <asp:CheckBox ID="chkExecuteScript" runat="server" text="Check User Only" Checked ="true"/></div>
        <div>
            <asp:Button ID ="btnAdderxUser" runat="server" Text="Add User to ERX" OnClick="btnAdderxUser_Click" />
            <div>
             <asp:TextBox ID="txtFullLog" runat="server" Height="196px" Width="921px" TextMode="MultiLine"  ></asp:TextBox>

        </div>
        </div>
        </asp:Panel>
    </div>
</asp:Content>
