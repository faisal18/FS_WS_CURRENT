<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="WSNavigator.aspx.cs" Inherits="FS_WS_WSCTFW.WSCaller.WSNavigator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div id="title">
        <h1>Web Service Navigator</h1>
    </div>

    <div id="content">
        <fieldset>
            <legend>LInks</legend>
            <ul>
                <li><a runat="server" href="ManageEnvironments.aspx">Manage WS Endpoints/Environment</a></li>
                <li><a runat="server" href="ManageWebServices.aspx">Web Service Executor</a></li>
            </ul>
        </fieldset>
        
    </div>
</asp:Content>
