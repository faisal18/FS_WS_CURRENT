using System;
using System.Threading.Tasks;
using FS_WS_WSCTFW.Helpers;
using System.Configuration;

namespace FS_WS_WSCTFW.Dashboard
{
    public partial class Dash_UploadData : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Run_UploadData();
            }
        }

        public void Run_UploadData()
        {
            try
            {
                Logger.Info("RUN UPLOAD DATA");
                int is_asynch = Convert.ToInt32(ConfigurationManager.AppSettings["Dashboard_isAsynch"]);
                if (is_asynch == 1)
                {
                    Logger.Info("Running ASYNCH Process");
                    lbl_result.Text = Run_ProcessesAsync();

                }
                else
                {
                    Logger.Info("Rnning Synchronous Process");
                    lbl_result.Text = "Process started at:" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    lbl_result.Text += Run_Processes();
                }
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                lbl_result.Text = "Failiure Occured !<br/>" + ex.Message;
            }
        }

        private static string Run_Processes()
        {
            bool[] result = new bool[5];
            try
            {
                result[0] = ClinicianAutomation.ExtraClasses.Monitoring_UploadData.InsertFrom_PBM();
                result[1] = ClinicianAutomation.ExtraClasses.Monitoring_UploadData.InsertFrom_PBM_Pending();
                result[2] = ClinicianAutomation.ExtraClasses.Monitoring_UploadData.InsertFrom_ERX();
                result[3] = ClinicianAutomation.ExtraClasses.Monitoring_UploadData.InsertFrom_ERX_Pending();
                result[4] = ClinicianAutomation.ExtraClasses.Monitoring_UploadData.InsertFrom_DHPO();
                return (
                    "<br/>PBM:" + result[0] + "<br/>PBM Pending:" + result[1] + 
                    "<br/>ERX:" + result[2] + "<br/>ERX Pending:" + result[3] +
                    "<br/>DHPO:" + result[4] +
                    "<br />Process finished at:" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    );
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return ("<br />Error Occured: " + ex.Message);
            }
        }
        private static string Run_ProcessesAsync()
        {
            try
            {
                var PBMresult = InsertPBM();
                var ERXresult = InsertERX();
                var DHPOresult = InsertDHPO();
                var PBMPending = InsertPBM_Pending();
                var ERXPending = InsertERX_Pending();
                return (
                    "PBM:" + PBMresult.Result +
                    "<br/>PBM Pending:" + PBMPending.Result+
                    "<br/>ERX:" + ERXresult.Result +
                    "<br/>ERX Pending:" + ERXPending.Result +
                    "<br/>DHPO:" + ERXresult.Result);
            }
            catch(Exception ex)
            {
                Logger.Error(ex);
                return ("<br />Error Occured: " + ex.Message);
            }
        }

        private static async Task<bool> InsertPBM()
        {
            try
            {
              
                bool result = ClinicianAutomation.ExtraClasses.Monitoring_UploadData.InsertFrom_PBM();
                await Task.Delay(10);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }
        private static async Task<bool> InsertERX()
        {
            try
            {
                bool result = ClinicianAutomation.ExtraClasses.Monitoring_UploadData.InsertFrom_ERX();
                await Task.Delay(10);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }
        private static async Task<bool> InsertDHPO()
        {
            try
            {
                bool result = ClinicianAutomation.ExtraClasses.Monitoring_UploadData.InsertFrom_DHPO();
                await Task.Delay(10);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }
        private static async Task<bool> InsertPBM_Pending()
        {
            try
            {
                bool result = ClinicianAutomation.ExtraClasses.Monitoring_UploadData.InsertFrom_PBM_Pending();
                await Task.Delay(10);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }
        private static async Task<bool> InsertERX_Pending()
        {
            try
            {
                bool result = ClinicianAutomation.ExtraClasses.Monitoring_UploadData.InsertFrom_ERX_Pending();
                await Task.Delay(10);
                return result;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return false;
            }
        }




        protected void Button1_Click(object sender, EventArgs e)
        {
            //TextBox1.Text = DateTime.Now.ToString();
            Run_UploadData();
            //TextBox1.Text = TextBox1.Text + "<br/>" + DateTime.Now.ToString();
        }
    }
}