using System;
using System.IO;
using System.Configuration;

namespace FS_WS_WSCTFW.Utilities.DBModification
{
    public partial class SendEmail_BQS : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (Request.QueryString["to_address"] != null)
                {
                    string to_address = Request.QueryString["to_address"];
                    string cc_address = Request.QueryString["cc_address"];
                    string file_path = Request.QueryString["file_path"];
                    Helpers.Logger.Info(QueryString_Function(to_address, cc_address, file_path));
                }
            }
            catch(Exception ex)
            {
                lbl_result.Text = "Error Occured !\n" + ex.Message;
                Helpers.Logger.Error(ex);
            }
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_receipentAddress.Text != null && txt_filePath.Text != null)
                {
                    string result = QueryString_Function(txt_receipentAddress.Text, txt_ccAddress.Text, txt_filePath.Text);
                    Helpers.Logger.Info(result);
                    lbl_result.Text = result;
                }
                else
                {
                    lbl_result.Text = "Input fields are empty";
                }
            }
            catch(Exception ex)
            {
                lbl_result.Text = ex.Message;
                Helpers.Logger.Error(ex);
            }
        }

        private static string QueryString_Function(string to_address,string cc_address,string file_path)
        {
            Helpers.Logger.Info("Sending email through SendEmail_BQS");
            try
            {
                to_address = to_address.Replace("|", ",");
                cc_address = cc_address.Replace("|", ",");

                bool email_result = Helpers.EmailHelper.SendEmailwithAttachment_MultipleAddresses(to_address, cc_address, "TechSupport", "Dear Concern,\n\nPFA", file_path);
                Helpers.Logger.Info("Email Sent:" + email_result);


                if(Convert.ToBoolean(int.Parse(System.Configuration.ConfigurationManager.AppSettings["Move_file"].ToString())))
                {
                    string file_move_path = ConfigurationManager.AppSettings["Move_file_path"];
                    File.Move(file_path, file_move_path + Path.GetFileName(file_path));
                    Helpers.Logger.Info("File moved successfully to " + file_move_path + Path.GetFileName(file_path));
                }
                return "Email Sent:" + email_result;
            }
            catch(Exception ex)
            {
                Helpers.Logger.Error(ex);
                return ex.Message;
            }

        }
    }
}