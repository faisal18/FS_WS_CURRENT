<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Get_TurnAroundTime.aspx.cs" Inherits="ClinicianAutomation.Get_TurnAroundTime" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    Faisal Ansari (fansari@dimensions-healthcare.com)
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
     <div id="title">
        <h1>Turn Aroun Time TAT</h1>
    </div>
    <div id="content">
        <div>
            <asp:Label runat="server" ID="lbl_license" Text="TPA_Dubai_Code" Enabled="false"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_license"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" ID="lbl_startdate" Text="Start Date" Enabled="false"></asp:Label>
        </div>
        <div>
            <%--<asp:Calendar runat="server" ID="cldr_startdate" OnSelectionChanged="cldr_startdate_SelectionChanged"></asp:Calendar>--%>
            <asp:TextBox runat="server" ID="txt_startdate" Enabled="true"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" ID="lbl_enddate" Text="End Date" Enabled="false"></asp:Label>
        </div>
        <div>
            <%--<asp:Calendar runat ="server" ID ="cldr_enddate" OnSelectionChanged="cldr_enddate_SelectionChanged"></asp:Calendar>--%>
            <asp:TextBox runat="server" ID="txt_enddate" Enabled="TRUE"></asp:TextBox>
        </div>
        <div>
            <asp:RadioButton runat="server" ID="rd_getdata" Text="Get Data" Checked ="true" OnCheckedChanged="rd_getdata_CheckedChanged" AutoPostBack="True" />
        </div>
        <div>
            <asp:RadioButton runat="server" ID="rd_getcount" Text="Get Count" OnCheckedChanged="rd_getcount_CheckedChanged" AutoPostBack="True" />
        </div>
       <%-- <div>
            <asp:Button runat="server" ID="lbl_generate" Text="Generate Query" OnClick="lbl_generate_Click"></asp:Button>
        </div>--%>
        <div>
            <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
        </div>
        <div>
            <asp:Button runat="server" ID="submit" Text="Update" OnClick="submit_Click" />
        </div>

    </div>
</asp:Content>
