using System;
using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FS_WS_WSCTFW.Utilities
{
    public partial class DHPO_ProviderUpdate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            txtFullLog.ReadOnly = true;
        }

        protected void btnGenerateQuery_Click(object sender, EventArgs e)
        {
            txtFullLog.Text = "";
            string finalQuery = "";
            if (txtProviderLic.Text.Trim().Length < 1)
            {
                txtFullLog.Text = "Provider License Field should not be empty";
            }
            else
            {
                //------------------------------------------------------ To Check 
                #region AllChecks

                if (rdOperations.SelectedValue == "Update Password" && txtProvPass.Text.Length > 0)
                {

                  


                    finalQuery += Environment.NewLine;
                    finalQuery += " Print 'Updating Provider Password for License - " + txtProviderLic.Text + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += Environment.NewLine;
                    finalQuery += "  Update  top (1) [Provider] ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " Set  ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " [Password] = '" + txtProvPass.Text.Trim() + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += " Where  ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " [licenseid] =  '" + txtProviderLic.Text.Trim() + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += " GO ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " Print 'Query Executed of Provider Password update for License - " + txtProviderLic.Text + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += "-------------------------------------------------------------------------------------------------";
                    finalQuery += Environment.NewLine;



                }
                else
                if (rdOperations.SelectedValue == "Update Password" && txtProvPass.Text.Length < 1)
                {
                    txtFullLog.Text = "Password Cannot be Empty!!!";
                }
                else
                    if (rdOperations.SelectedValue == "Update Username and Password" && txtProvUserName.Text.Trim().Length > 0 && txtProvPass.Text.Trim().Length > 0)
                {


                    finalQuery += Environment.NewLine;
                    finalQuery += " Print 'Updating Provider Password for License- " + txtProviderLic.Text + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += Environment.NewLine;
                    finalQuery += "  Update  top (1) [provider] ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " Set  ";
                    finalQuery += Environment.NewLine;
                    finalQuery += "   [Username]  = '" + txtProvUserName.Text.Trim() + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += "  , ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " [Password] = '" + txtProvPass.Text.Trim() + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += " Where  ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " [licenseid] =  '" + txtProviderLic.Text.Trim() + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += " GO ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " Print 'Query Executed of Provider Password update for License - " + txtProviderLic.Text + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += "-------------------------------------------------------------------------------------------------";
                    finalQuery += Environment.NewLine;


                }
                else
                      if (rdOperations.SelectedValue == "Update Username and Password" && txtProvUserName.Text.Trim().Length < 1 && txtProvPass.Text.Trim().Length < 1)
                {
                    txtFullLog.Text = "Username and Password Cannot be Empty!!!";
                }

                else
                      if (rdOperations.SelectedValue == "Update Username and Password" && txtProvUserName.Text.Trim().Length < 1 && txtProvPass.Text.Trim().Length > 0)
                {
                    txtFullLog.Text = "Username Cannot be Empty!!!";
                }

                else
                      if (rdOperations.SelectedValue == "Update Username and Password" && txtProvUserName.Text.Trim().Length > 0 && txtProvPass.Text.Trim().Length < 1)
                {
                    txtFullLog.Text = "Password Cannot be Empty!!!";
                }

               

                #endregion

            }

            txtFullLog.Text = txtFullLog.Text + Environment.NewLine + finalQuery;
        }

        protected void btnClearQuery_Click(object sender, EventArgs e)
        {
            txtFullLog.Text = "";
        }

        protected void btnSaveScript_Click(object sender, EventArgs e)
        {
            txtFullLog.Text = "";
            string finalQuery = "";
            if (txtProviderLic.Text.Trim().Length < 1)
            {
                txtFullLog.Text = "Provider License Field should not be empty";
            }
            else
            {
                //------------------------------------------------------ To Check 
                #region AllChecks

                if (rdOperations.SelectedValue == "Update Password" && txtProvPass.Text.Length > 0)
                {




                    finalQuery += Environment.NewLine;
                    finalQuery += " Print 'Updating Provider Password for License - " + txtProviderLic.Text + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += Environment.NewLine;
                    finalQuery += "  Update top (1)  [Provider] ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " Set  ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " [Password] = '" + txtProvPass.Text.Trim() + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += " Where  ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " [licenseid] =  '" + txtProviderLic.Text.Trim() + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += " GO ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " Print 'Query Executed of Provider Password update for License - " + txtProviderLic.Text + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += "-------------------------------------------------------------------------------------------------";
                    finalQuery += Environment.NewLine;



                }
                else
                if (rdOperations.SelectedValue == "Update Password" && txtProvPass.Text.Length < 1)
                {
                    txtFullLog.Text = "Password Cannot be Empty!!!";
                }
                else
                    if (rdOperations.SelectedValue == "Update Username and Password" && txtProvUserName.Text.Trim().Length > 0 && txtProvPass.Text.Trim().Length > 0)
                {


                    finalQuery += Environment.NewLine;
                    finalQuery += " Print 'Updating Provider Password for License- " + txtProviderLic.Text + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += Environment.NewLine;
                    finalQuery += "  Update top (1) [provider] ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " Set  ";
                    finalQuery += Environment.NewLine;
                    finalQuery += "   [Username]  = '" + txtProvUserName.Text.Trim() + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += "  , ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " [Password] = '" + txtProvPass.Text.Trim() + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += " Where  ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " [licenseid] =  '" + txtProviderLic.Text.Trim() + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += " GO ";
                    finalQuery += Environment.NewLine;
                    finalQuery += " Print 'Query Executed of Provider Password update for License - " + txtProviderLic.Text + "'";
                    finalQuery += Environment.NewLine;
                    finalQuery += "-------------------------------------------------------------------------------------------------";
                    finalQuery += Environment.NewLine;


                }
                else
                      if (rdOperations.SelectedValue == "Update Username and Password" && txtProvUserName.Text.Trim().Length < 1 && txtProvPass.Text.Trim().Length < 1)
                {
                    txtFullLog.Text = "Username and Password Cannot be Empty!!!";
                }

                else
                      if (rdOperations.SelectedValue == "Update Username and Password" && txtProvUserName.Text.Trim().Length < 1 && txtProvPass.Text.Trim().Length > 0)
                {
                    txtFullLog.Text = "Username Cannot be Empty!!!";
                }

                else
                      if (rdOperations.SelectedValue == "Update Username and Password" && txtProvUserName.Text.Trim().Length > 0 && txtProvPass.Text.Trim().Length < 1)
                {
                    txtFullLog.Text = "Password Cannot be Empty!!!";
                }



                #endregion

            }

            if (txtFullLog.Text.Trim().Length < 1)
            {

                string path = System.Configuration.ConfigurationManager.AppSettings["ClinicianPath"];
                path = path + "DHPO-UpdateProviderScript.SQL";


                //using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(Server.MapPath("~/Utilities/ClinicianUpdateScript/Script.SQL"), false))

                if (!System.IO.File.Exists(path))
                {
                    using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(path, false))
                    {
                        _testData.WriteLine(finalQuery); // Write the file.
                    }
                    FS_WS_WSCTFW.Helpers.BatchFIleCaller.InsertMLog("DHPO-ProviderUpdate", User.Identity.GetUserId(), "DHPO-Provider-File", finalQuery, "0", "");

                    txtFullLog.Text = "file save successfully";
                }
                else
                {

                    txtFullLog.Text = "Another Script Already Schedule for Execution, Please Retry after Some time!!!";
                    //txtFullLog.Text = "Another Script Already Schedule for Execution, Created: " + Environment.NewLine + System.IO.File.GetCreationTime(path).ToString() + Environment.NewLine + "Please Retry after Some time!!!";
                    FS_WS_WSCTFW.Helpers.BatchFIleCaller.InsertMLog("ClincianUpdate", User.Identity.GetUserId(), "", finalQuery, "0", "File Already Exist, Cannot create new file.");

                }
            }
            else
            {

            }
        }

        protected void rdOperations_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFullLog.Text = "";

            if (rdOperations.SelectedValue == "Update Password")
            {
                txtProvUserName.Enabled = false;
                
                txtProvPass.Enabled = true;

            }
            else
                 if (rdOperations.SelectedValue == "Update Username and Password")
            {
                
                txtProvUserName.Enabled = true;
                txtProvPass.Enabled = true;
            }
           

        }
    }
}