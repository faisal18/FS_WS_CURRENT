using System;
using System.Configuration;
using System.IO;

namespace ClinicianAutomation
{
    public partial class upload_file : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }


        protected void btn_upload_Click(object sender, EventArgs e)
        {
            try
            {
                if (uploadfile.HasFile)
                {
                    if(uploadfile.PostedFile.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                    {
                        if(uploadfile.PostedFile.ContentLength <= 102400)
                        {
                            string path = "";
                            string extension = Path.GetExtension(uploadfile.PostedFile.FileName);
                            string filename = Path.GetFileNameWithoutExtension(uploadfile.PostedFile.FileName);
                            string prefix = null;

                            if (rdbtn_clinician.Checked == true)
                            {
                                path = ConfigurationManager.AppSettings["file_upload_Clinician"];
                                prefix = "ClinicianData";
                            }
                            else if (rdbtn_erx.Checked == true)
                            {
                                path = ConfigurationManager.AppSettings["file_upload_ERX"];
                                prefix = "ERX";
                            }

                            uploadfile.SaveAs(path + prefix + extension);
                            uploadfile.SaveAs(path + "backup\\" + filename + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + extension);

                            lbl_response.Text = string.Format("File {0} uploaded successfully !!", filename + extension);
                            FS_WS_WSCTFW.Helpers.BatchFIleCaller.InsertMLog("FileUPload", "", path + prefix + extension, lbl_response.Text.ToString(), "1", "0");
                        }
                        else
                        {
                            lbl_response.Text = "File size exceeded !";
                        }
                    }
                    else
                    {
                        lbl_response.Text = "File is not in XLSX format";
                    }  
                }
                else
                {
                    lbl_response.Text = "File not selected";
                }
            }
            catch (Exception ex)
            {
                lbl_response.Text = ex.Message.ToString();
            }

        }

        protected void rdbtn_clinician_CheckedChanged(object sender, EventArgs e)
        {
            rdbtn_clinician.Checked = true;
            rdbtn_erx.Checked = false;
        }

        protected void rdbtn_erx_CheckedChanged(object sender, EventArgs e)
        {
            rdbtn_erx.Checked = true;
            rdbtn_clinician.Checked = false;
        }
    }
}