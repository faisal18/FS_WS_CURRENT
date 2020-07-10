<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" ValidateRequest="false" AutoEventWireup="true" CodeBehind="Generate_CustomQuery.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.DBModification.Generate_CustomQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


     <div>
        <h1>Generate Custom Query</h1>
    </div>

    <div>

        <div>
            <asp:Label runat="server" ID="lbl_input" Text="Input Data (val1,val2...)"></asp:Label>
        </div>
        <div>
        <asp:TextBox runat="server" ID="txt_input" TextMode ="MultiLine" Width="996px"></asp:TextBox>
            </div>
        <div>
            <asp:Label runat="server" ID="lbl_query" Text ="Enter Query with @1,@2 as parameters"></asp:Label>
        </div>
        <div>
            NOTE: Please add single quotes (') as per the query requirements.
            <br />
            NOTE:If number of fields are greater than 9 use # with fields,if greater than 19 use $ with fields.
            <asp:TextBox runat ="server" ID="txt_query" TextMode="MultiLine" Height="74px" Width="1001px"></asp:TextBox>
        </div>

        <div>
            <asp:Button runat="server" ID="btn_generate_query" Text="Generate Query" OnClick="btn_generate_query_Click"/>
        </div>
        <div>
            <textarea runat="server" id="txt_richbox" style="resize: none; height: 196px; width: 1003px; margin: 0px;" readonly="readonly"></textarea>
        </div>
    </div>


</asp:Content>
