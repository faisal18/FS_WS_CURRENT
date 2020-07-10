<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transactions_Detail_Report.aspx.cs" Inherits="ClinicianAutomation.Utilities_UI.Transactions_Detail_Report" %>

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
        <h1>Transactions Detail Report</h1>
    </div>
     <div>
        <asp:RadioButton runat="server" ID="rd_byAll" Text ="By All Values" AutoPostBack="true" Checked="true" OnCheckedChanged="rd_byAll_CheckedChanged" />
    </div>
    <div>
        <asp:RadioButton runat="server" ID="rd_byDubaiLicense" Text="By Payer Dubai License" AutoPostBack="true" OnCheckedChanged="rd_byDubaiLicense_CheckedChanged" />
    </div>
    <div>
        <asp:RadioButton runat="server" ID="rd_byProviderLicense" Text="By Provider License" AutoPostBack="true" OnCheckedChanged="rd_byProviderLicense_CheckedChanged" />
    </div>
    <div>
        <asp:RadioButton runat="server" ID="rd_byDates" Text="By Dates Only" AutoPostBack="true" OnCheckedChanged="rd_byDates_CheckedChanged" />
    </div>
   

    <asp:Panel runat="server" ID="pnl_DubaiLicense">
        <div>
            <asp:Label runat="server" ID="lbl_dubailicenseno" Text="Payer Dubai License Number"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_dubailicenseno" TextMode="MultiLine"></asp:TextBox>
        </div>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnl_Dates" >
        <div>
            <asp:Label runat="server" ID="lbl_startdate" Text="Star Date"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_startdate"></asp:TextBox>
        </div>
        <div>
            <asp:Label runat="server" ID="lbl_enddate" Text="End Date"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_enddate"></asp:TextBox>
        </div>
    </asp:Panel>

    <asp:Panel runat="server" ID="pnl_ProviderLicense" >
        <div>
            <asp:Label runat="server" ID="lbl_providerlicense" Text="Provider License"></asp:Label>
        </div>
        <div>
            <asp:TextBox runat="server" ID="txt_providerlicense" TextMode="MultiLine"></asp:TextBox>
        </div>
    </asp:Panel>

    <div>
        <asp:CheckBox runat="server" ID="chk_report" Text="Detail Report" />
    </div>
    <div>
        <asp:Button runat="server" ID="btn_generatequery" Text="Generate Query" OnClick="btn_generatequery_Click" />
    </div>
    <div>
        <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 921px; margin: 0px;" readonly="readonly"></textarea>
    </div>
    <div>
        <asp:Button runat="server" ID="btn_submit" Text="Submit" OnClick="btn_submit_Click" />
    </div>
</asp:Content>
