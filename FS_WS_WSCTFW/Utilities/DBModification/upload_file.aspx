<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="upload_file.aspx.cs" Inherits="ClinicianAutomation.upload_file" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div id="title">
        <h1>Upload File</h1>
    </div>
    <div id="content">
        <div>
            <asp:FileUpload runat ="server" ID="uploadfile" />
        </div>
        <div>
            <asp:Button runat="server" ID="btn_upload" Text="Upload" OnClick="btn_upload_Click" />
        </div>
        <div>
            <asp:RadioButton runat ="server" ID ="rdbtn_clinician" Text ="Clinician"  Checked="true" OnCheckedChanged="rdbtn_clinician_CheckedChanged" AutoPostBack="true" />
        </div>
        <div>
            <asp:RadioButton runat="server" ID="rdbtn_erx" Text ="ERX" OnCheckedChanged="rdbtn_erx_CheckedChanged" AutoPostBack="true"  />
         </div>
       
        <div>
            <asp:Label runat="server" ID="lbl_response" ></asp:Label>
        </div>
    </div>
</asp:Content>
