<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Suspend_Clinician.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.DBModification.Suspend_Clinician" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />

    <script type="text/javascript">
        $(function () {
            $("[id$=txt_startdate]").datepicker({
                //showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                //buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
            });
        });
        $(function () {
            $("[id$=txt_enddate]").datepicker({
                //showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                //buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
            });
        })
    </script>

    <div>
        <h1>Suspend Clinician</h1>
    </div>

    <div>
        <div>
            <asp:Label runat="server" Text="Clinician license"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_Clincian"></asp:TextBox>
        </div>
    </div>

    <div>
        <div>
            <asp:Label runat="server" Text="Start Date"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_startdate"></asp:TextBox>
        </div>
    </div>
    <div>
        <div>
            <asp:Label runat="server" Text="End Date"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_enddate"></asp:TextBox>
        </div>
    </div>
    <div>
        <asp:Label runat="server" Text="Result"></asp:Label>
    </div>
    <div>
        <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
    </div>
    <div>
        <asp:Button runat="server" ID="btn_submit" Text="Submit" OnClick="btn_submit_Click" />
    </div>
</asp:Content>
