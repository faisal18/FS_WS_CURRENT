<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Transaction_TAT.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.DBModification.Transaction_TAT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=txt_datetime]").datepicker({
                //showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                dateFormat: "yy-mm-dd",
                //buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
            });
        });

    </script>

    <h1>Transaction TAT Report</h1>


    <div>
        <asp:Panel runat="server" ID="pnl_transactionsid" Visible="true">
            <div>
                <asp:Label runat="server" Text="Transaction ID"></asp:Label>
            </div>
            <div>
                <asp:TextBox runat="server" ID="txt_transactionids" TextMode="MultiLine"></asp:TextBox>
            </div>
        </asp:Panel>
    </div>

    <div>
        <asp:Panel runat="server" ID="pnl_datetime" Visible="false">
            <div>
                <asp:Label runat="server" Text="DateTime"></asp:Label>
            </div>
            <div>
                <asp:TextBox runat="server" ID="txt_datetime"></asp:TextBox>
            </div>
        </asp:Panel>
    </div>

    <div>
        <asp:RadioButtonList runat="server" ID="rd_tat_method" AutoPostBack="true" OnSelectedIndexChanged="rd_tat_method_SelectedIndexChanged">
            <asp:ListItem Value="">Select Method</asp:ListItem>
            <asp:ListItem Value="prod_bydate">By DateTime in Production</asp:ListItem>
            <asp:ListItem Value="prod_bytransaction">By Transactions in Production</asp:ListItem>
            <asp:ListItem Value="qa_bydate">By DateTime in QA</asp:ListItem>
            <asp:ListItem Value="qa_bytransaction">By Transactions in QA</asp:ListItem>
        </asp:RadioButtonList>
    </div>

    <div>
        <asp:Button runat="server" ID="btn_submit" OnClick="btn_submit_Click" Text="Submit" />
    </div>

    <div>
        <asp:Label runat="server" ID="lbl_result"></asp:Label>
    </div>

</asp:Content>
