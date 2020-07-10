<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BucketMappingGenerator.aspx.cs" Inherits="FS_WS_WSCTFW.Utilities.BucketMappingGenerator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <div>Backup Table Name</div>
                <div>
                    <asp:TextBox ID="tbBackupTabName" runat="server" Height="20px" Width="372px"></asp:TextBox>
                    <asp:CheckBox ID="chcktblBackup" runat="server" OnCheckedChanged="chckGeneral_CheckedChanged" Text="Include in Script" AutoPostBack="True" Enabled="True" />
                </div>
            </div>
            <div>
                <div>Payer</div>
                <div>
                    <asp:TextBox ID="tbProvider" runat="server" Height="16px" Width="369px"></asp:TextBox>
                </div>
            </div>
            <div>
                <div>Type</div>
                <div>
                    <asp:TextBox ID="tbType" runat="server" Height="16px" Width="364px"></asp:TextBox>&nbsp;
                <br />
                    <asp:CheckBox ID="chckGeneral" runat="server" OnCheckedChanged="chckGeneral_CheckedChanged" Text="General Type" AutoPostBack="True" />
                    <asp:CheckBox ID="checkResetBucket" runat="server" Text="Reset Whole Bucket" />
                    <asp:CheckBox ID="checkReset" runat="server" OnCheckedChanged="chckGeneral_CheckedChanged" Text="Reset each ICDs" />

                </div>
            </div>
            <div>
                <div>PCN</div>
                <div>
                    <asp:TextBox ID="tbPCN" runat="server"></asp:TextBox>
                </div>
            </div>
            <div>
                
                <asp:Panel runat="server" ID="grpRadionButton" GroupingText=" ICD Conversion ">
                    <div>
                        <asp:CheckBox ID="checkICDConversion" runat="server" Text="Convert ICD to "  AutoPostBack="true" />


                        <asp:RadioButton    ID="ICD9"                 runat="server"                 Text="ICD 10"                 GroupName="ICDs"                 AutoPostBack="true"                                 />
                        <asp:RadioButton                ID="ICD10"                 runat="server"                 Text="ICD 9"                GroupName="ICDs"                 AutoPostBack="true"                             />

                      </div>       
                </asp:Panel>
             
            </div>
            <div>
                <div>ICD Codes (Comma Seperated Values)</div>
                <div>
                    <asp:TextBox ID="txtICDCodes" runat="server" Height="190px" Width="997px" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
            <div>
                <div>
                    <asp:Button ID="btnGenerate" Text="Generate Mapping" runat="server" Height="50px" Width="200px" OnClick="btnGenerate_Click"></asp:Button>
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </div>
            </div>
            <div>
                <div>Generated Script </div>
                <div>
                    <asp:TextBox ID="txtGeneratedScript" runat="server" Height="200px" Width="1000px" TextMode="MultiLine"></asp:TextBox>
                </div>
                <div>
                       <asp:Button ID="btnSaveNewFile" Text="Save New File" runat="server" Height="50px" Width="200px" OnClick="btnSaveNewFile_Click" ></asp:Button>
                 
                       <asp:Label ID="lblFIlename" runat="server" Text="Label" Visible="False"></asp:Label>
                 
                </div>

                <div>
                       <asp:Button ID="btnAppendExistingFile" Text="Append to Last file" runat="server" Height="50px" Width="200px" OnClick="btnAppendExistingFile_Click" ></asp:Button>
                 
                </div>
                <div>
                       <asp:Button ID="btnFInalScrip" Text="Email Final Script" runat="server" Height="50px" Width="200px" OnClick="btnFInalScrip_Click"  ></asp:Button>
                 
                </div>
            </div>
            <%--  <div>
            <div>Insert Statements</div>
            <div>
            <asp:TextBox ID="TextBox4" runat="server" Height="200px" Width="1000px"></asp:TextBox></div>
        </div>
        <div>
            <div>Delete Statements</div>
            <div>
            <asp:TextBox ID="TextBox5" runat="server" Height="200px" Width="1000px"></asp:TextBox></div>
        </div>
         <div>
            <div>Delete Statements</div>
            <div>
            <asp:TextBox ID="TextBox1" runat="server" Height="200px" Width="1000px"></asp:TextBox></div>
        </div>
            --%>
        </div>
    </form>
</body>
</html>
