<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DHPO_Clinician_Insert.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.DBModification.Insert_Clinician_Dhpo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
    <link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        $(function () {
            $("[id$=txt_LicenseStartDate]").datepicker({
                //showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                dateFormat: "yy-mm-dd 00:00:00.000",
                //buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
            });
        });
        $(function () {
            $("[id$=txt_LicenseEndDate]").datepicker({
                //showOn: 'button',
                buttonImageOnly: true,
                changeMonth: true,
                dateFormat: "yy-mm-dd 00:00:00.000",
                //buttonImage: 'http://jqueryui.com/demos/datepicker/images/calendar.gif'
            });
        })
    </script>

    <h1>Insert Clinician in DHPO</h1>

    <div>
        <asp:Table runat="server" ID="tbl_Clinician" CellSpacing="100" CellPadding="10">
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="Clinician License"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_ClinicianLicense"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="Clinician Name"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_ClinicianName"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="Gender"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:DropDownList runat="server" ID="ddl_gender">
                        <asp:ListItem Value="1">Male</asp:ListItem>
                        <asp:ListItem Value="1">Female</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="Facility Name"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_FacilityName"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="Location"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_Location"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="License Start Date"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_LicenseStartDate" Enabled="true"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="License End Date"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_LicenseEndDate" Enabled="true"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="Source"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_Source"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="Is Active"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:DropDownList runat="server" ID="ddl_isActive">
                        <asp:ListItem Value="1">Active</asp:ListItem>
                        <asp:ListItem Value="0">InActive</asp:ListItem>
                    </asp:DropDownList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="User Name"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_UserName"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="Password"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_Password"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="Speciality Id"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_SpecialityId"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="Speciality Group"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:TextBox runat="server" ID="txt_SpecialityGroup"></asp:TextBox></asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label runat="server" Text="Enviorment"></asp:Label></asp:TableCell><asp:TableCell>
                    <asp:RadioButtonList runat="server" ID="rd_enviorment">
                        <asp:ListItem Value="DHPO" Selected="True">Production DHPO</asp:ListItem>
                        <asp:ListItem Value="QA">QA DHPO2</asp:ListItem>
                    </asp:RadioButtonList>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell></asp:TableCell><asp:TableCell></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>

    <div>
        <asp:Button runat="server" ID="btn_submit" Text="Submit" OnClick="btn_submit_Click" />
    </div>
    <div>
        <asp:Label runat="server" ID="lbl_result"></asp:Label>
    </div>

</asp:Content>
