using System;
using System.Collections.Generic;
using System.Data;
using FS_WS_WSCTFW.Helpers;

namespace FS_WS_WSCTFW.Dashboard
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        [System.Web.Services.WebMethod]
        public static string GETRANDOM()
        {
            Random yo = new Random();
            return yo.Next(5, 10).ToString();
        }

        [System.Web.Services.WebMethod]
        public static Results[] GetTransactionsCount()
        {
            List<Results> list_res = new List<Results>();
            try
            {
                DataTable dt = ClinicianAutomation.ExtraClasses.Monitoring_Transactions.GetTransactionsCount();
                Results res;
                if (dt.Rows.Count > 0)
                {
                    Logger.Info("Get Transactions has been called. Datatable has " + dt.Rows.Count + " rows.");
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        res = new Results();
                        res.TimeStamp = dt.Rows[i]["PBM_Time"].ToString();

                        if (dt.Rows[i]["PBM_Count"] != System.DBNull.Value)
                        {
                            res.PBM_Count = (int)dt.Rows[i]["PBM_Count"];
                        }
                        else
                        {
                            res.PBM_Count = 0;
                        }
                        if (dt.Rows[i]["ERX_Count"] != System.DBNull.Value)
                        {
                            res.ERX_Count = (int)dt.Rows[i]["ERX_Count"];
                        }
                        else
                        {
                            res.ERX_Count = 0;
                        }
                        if (dt.Rows[i]["OIC_Count"] != System.DBNull.Value)
                        {
                            res.OIC_Count = (int)dt.Rows[i]["OIC_Count"];
                        }
                        else
                        {
                            res.OIC_Count = 0;
                        }

                        list_res.Add(res);
                    }
                }
                Results[] ho = list_res.ToArray();
                Logger.Info("Results array has been populated.");
                return ho;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception Occured!\n" + ex.Message);
            }

            return null;
        }

        [System.Web.Services.WebMethod]
        public static Status_Results[] GetAppStatuses()
        {
            List<Status_Results> list_res = new List<Status_Results>();
            try
            {
                DataTable dt = ClinicianAutomation.ExtraClasses.Monitoring_Transactions.GetAppStatus();
                Status_Results stat_res;
                double[] items = new double[13];
                if (dt.Rows.Count > 0)
                {
                    Logger.Info("Get Applications Statuses called. Datatable has " + dt.Rows.Count + " rows. DHPO result is " + dt.Rows[0]["DHPOValidate_Status"]);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        stat_res = new Status_Results();
                        stat_res.TimeStamp = dt.Rows[i]["ClinicianValidate_Time"].ToString();


                        if (dt.Rows[i]["Claims_Status"] != DBNull.Value)
                        {
                            if (dt.Rows[i]["Claims_Status"].ToString() == "OK")
                            {
                                stat_res.Claims_Count = 1 * 1;
                            }
                            else
                            {
                                stat_res.Claims_Count = -1 * 1;
                            }
                        }
                        else
                        {
                            stat_res.Claims_Count = 0;
                        }

                        if (dt.Rows[i]["Clinician_Status"] != DBNull.Value)
                        {
                            if (dt.Rows[i]["Clinician_Status"].ToString() == "OK")
                            {
                                stat_res.Clinician_Count = 1 * 2;
                            }
                            else
                            {
                                stat_res.Clinician_Count = -1 * 2;
                            }
                        }
                        else
                        {
                            stat_res.Clinician_Count = 0;
                        }

                        if (dt.Rows[i]["ClinicianValidate_Status"] != DBNull.Value)
                        {
                            if (dt.Rows[i]["ClinicianValidate_Status"].ToString() == "OK")
                            {
                                stat_res.Clinician_Validate_Count = 1 * 3;
                            }
                            else
                            {
                                stat_res.Clinician_Validate_Count = -1 * 3;
                            }
                        }
                        else
                        {
                            stat_res.Clinician_Validate_Count = 0;
                        }

                        if (dt.Rows[i]["DHPOValidate_Status"] != DBNull.Value)
                        {
                            if (dt.Rows[i]["DHPOValidate_Status"].ToString() == "OK")
                            {
                                stat_res.DHPO_Validate_Count = 1 * 4;
                            }
                            else
                            {
                                stat_res.DHPO_Validate_Count = -1 * 4;
                            }
                        }
                        else
                        {
                            stat_res.DHPO_Validate_Count = 0;
                        }

                        if (dt.Rows[i]["Eauthorization_Status"] != DBNull.Value)
                        {
                            if (dt.Rows[i]["Eauthorization_Status"].ToString() == "OK")
                            {
                                stat_res.Eauthorization_Count = 1 * 5;
                            }
                            else
                            {
                                stat_res.Eauthorization_Count = -1 * 5;
                            }
                        }
                        else
                        {
                            stat_res.Eauthorization_Count = 0;
                        }

                        if (dt.Rows[i]["ERXValidate_Status"] != DBNull.Value)
                        {
                            if (dt.Rows[i]["ERXValidate_Status"].ToString() == "OK")
                            {
                                stat_res.ERX_Validate_Count = 1 * 6;
                            }
                            else
                            {
                                stat_res.ERX_Validate_Count = -1 * 6;
                            }
                        }
                        else
                        {
                            stat_res.ERX_Validate_Count = 0;
                        }

                        if (dt.Rows[i]["ERXPharmacy_Status"] != DBNull.Value)
                        {
                            if (dt.Rows[i]["ERXPharmacy_Status"].ToString() == "OK")
                            {
                                stat_res.ERX_Pharmacy_Count = 1 * 7;
                            }
                            else
                            {
                                stat_res.ERX_Pharmacy_Count = -1 * 7;
                            }
                        }
                        else
                        {
                            stat_res.ERX_Pharmacy_Count = 0;
                        }

                        if (dt.Rows[i]["LMUValidate_Status"] != DBNull.Value)
                        {
                            if (dt.Rows[i]["LMUValidate_Status"].ToString() == "OK")
                            {
                                stat_res.LMU_Validate_Count = 1 * 8;
                            }
                            else
                            {
                                stat_res.LMU_Validate_Count = -1 * 8;
                            }
                        }
                        else
                        {
                            stat_res.LMU_Validate_Count = 0;
                        }

                        if (dt.Rows[i]["MemberRegistration_Status"] != DBNull.Value)
                        {
                            if (dt.Rows[i]["MemberRegistration_Status"].ToString() == "OK")
                            {
                                stat_res.Member_Registration_Count = 1 * 9;
                            }
                            else
                            {
                                stat_res.Member_Registration_Count = -1 * 9;
                            }
                        }
                        else
                        {
                            stat_res.Member_Registration_Count = 0;
                        }

                        if (dt.Rows[i]["PBMLink_Status"] != DBNull.Value)
                        {
                            if (dt.Rows[i]["PBMLink_Status"].ToString() == "OK")
                            {
                                stat_res.PBMLINK_Count = 1 * 10;
                            }
                            else
                            {
                                stat_res.PBMLINK_Count = -1 * 10;
                            }
                        }
                        else
                        {
                            stat_res.PBMLINK_Count = 0;
                        }

                        if (dt.Rows[i]["PBMSwitchDimensions_Status"] != DBNull.Value)
                        {
                            if (dt.Rows[i]["PBMSwitchDimensions_Status"].ToString() == "OK")
                            {
                                stat_res.PBMSwitch_Dimensions_Count = 1 * 11;
                            }
                            else
                            {
                                stat_res.PBMSwitch_Dimensions_Count = -1 * 11;
                            }
                        }
                        else
                        {
                            stat_res.PBMSwitch_Dimensions_Count = 0;
                        }

                        if (dt.Rows[i]["PBMSwitchPayer_Status"] != DBNull.Value)
                        {
                            if (dt.Rows[i]["PBMSwitchPayer_Status"].ToString() == "OK")
                            {
                                stat_res.PBMSwitch_Pyaer_Count = 1 * 12;
                            }
                            else
                            {
                                stat_res.PBMSwitch_Pyaer_Count = -1 * 12;
                            }
                        }
                        else
                        {
                            stat_res.PBMSwitch_Pyaer_Count = 0;
                        }

                        if (dt.Rows[i]["ShafafiyaValidate_Status"] != DBNull.Value)
                        {
                            if (dt.Rows[i]["ShafafiyaValidate_Status"].ToString() == "OK")
                            {
                                stat_res.Shafafiya_Validate_Count = 1 * 13;
                            }
                            else
                            {
                                stat_res.Shafafiya_Validate_Count = -1 * 13;
                            }
                        }
                        else
                        {
                            stat_res.Shafafiya_Validate_Count = 0;
                        }
                        list_res.Add(stat_res);
                    }
                }

                Status_Results[] stat_dt = list_res.ToArray();
                Logger.Info("Status Results Array has been populated");
                return stat_dt;
            }
            catch (Exception ex)
            {
                Logger.Error("Exception Occured!\n" + ex.Message);
            }
            return null;
        }

        [System.Web.Services.WebMethod]
        public static Bar_PBMERX_Result[] GetPBMERXCount()
        {
            List<Bar_PBMERX_Result> list_pbmErx = new List<Bar_PBMERX_Result>();
            try
            {
                DataTable dt = ClinicianAutomation.ExtraClasses.Monitoring_Transactions.GetPBMERXCount();
                Bar_PBMERX_Result bar_res;

                if (dt.Rows.Count > 0)
                {
                    Logger.Info("Get PBM ERX called.Datatable has " + dt.Rows.Count + " rows.<br/>ERX_TOTAL:" + (int)dt.Rows[0]["ERX_TOTAL"]);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        bar_res = new Bar_PBMERX_Result();
                        bar_res.PBMERX_CheckingTime = dt.Rows[i]["CheckingTime"].ToString();
                        bar_res.ERX_Total_Count = (int)dt.Rows[i]["ERX_TOTAL"];
                        bar_res.ERX_Total_Processed_Count = (int)dt.Rows[i]["ERX_PROCESSED"];
                        bar_res.ERX_Total_OurPayer_Count = (int)dt.Rows[i]["ERX_PAYER"];

                        bar_res.PBM_Total_Count = (int)dt.Rows[i]["PBMLink_TOTAL"];
                        bar_res.PBM_Total_Processed_Count = (int)dt.Rows[i]["PBMLink_PROCESSED"];
                        bar_res.PBM_Total_OurPayer_Count = (int)dt.Rows[i]["PBMLink_PAYER"];

                        list_pbmErx.Add(bar_res);
                    }
                }
                Bar_PBMERX_Result[] bar = list_pbmErx.ToArray();
                return bar;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        [System.Web.Services.WebMethod]
        public static Bar_DHPO_Result[] GetDHPOCount()
        {
            try
            {
                DataTable dt = ClinicianAutomation.ExtraClasses.Monitoring_Transactions.GetDHPOCount();
                List<Bar_DHPO_Result> list_DHPO = new List<Bar_DHPO_Result>();

                Bar_DHPO_Result obj_DHPO;
                if (dt.Rows.Count > 0)
                {
                    Logger.Info("Get Dhpo Count called. Datatable returned:" + dt.Rows.Count + " rows.DHPO CS TOTAL: " + (int)dt.Rows[0]["DHPO_Total_CS"]);

                    obj_DHPO = new Bar_DHPO_Result();

                    obj_DHPO.DHPO_CheckingTime = dt.Rows[0]["CheckingTime"].ToString();
                    obj_DHPO.DHPO_Total_CS = (int)dt.Rows[0]["DHPO_Total_CS"];
                    obj_DHPO.DHPO_CS_Downloaded = (int)dt.Rows[0]["DHPO_CS_Downloaded"];
                    obj_DHPO.DHPO_CS_NotDownloaded = (int)dt.Rows[0]["DHPO_CS_NotDownloaded"];

                    obj_DHPO.DHPO_Total_PA = (int)dt.Rows[0]["DHPO_Total_PA"];
                    obj_DHPO.DHPO_PA_Downloaded = (int)dt.Rows[0]["DHPO_PA_Downloaded"];
                    obj_DHPO.DHPO_PA_NotDownloaded = (int)dt.Rows[0]["DHPO_PA_NotDownloaded"];

                    obj_DHPO.DHPO_Total_PR = (int)dt.Rows[0]["DHPO_Total_PR"];
                    obj_DHPO.DHPO_PR_Downloaded = (int)dt.Rows[0]["DHPO_PR_Downloaded"];
                    obj_DHPO.DHPO_PR_NotDownloaded = (int)dt.Rows[0]["DHPO_PR_NotDownloaded"];

                    list_DHPO.Add(obj_DHPO);
                }
                
                Bar_DHPO_Result[] barDHPO = list_DHPO.ToArray();
                Logger.Info("DHPO list updated.BAR_DHPO ");
                return barDHPO;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message);
                return null;
            }
        }

        [System.Web.Services.WebMethod]
        public static Bar_PBMERX_Pending_Count[] GetPBMERX_Pending_Count()
        {
            List<Bar_PBMERX_Pending_Count> list_pbmErx_pending = new List<Bar_PBMERX_Pending_Count>();
            try
            {
                Logger.Info("Get PBM ERX Pending Started");
                DataTable dt = ClinicianAutomation.ExtraClasses.Monitoring_Transactions.GetPBMERX_Pending_Count();
                Bar_PBMERX_Pending_Count bar_res_pending;
                if(dt.Rows.Count>0)
                {
                    Logger.Info("Get PBM ERX Pending called.Datatable has " + dt.Rows.Count + " rows.");
                    for (int i =0;i<dt.Rows.Count;i++)
                    {
                        bar_res_pending = new Bar_PBMERX_Pending_Count();
                        bar_res_pending.Bar_PBMERX_CheckingTime = dt.Rows[i]["CheckingTime"].ToString();
                        bar_res_pending.ERX_Payer_Count = (int)dt.Rows[i]["ERX_Payers_Pending_Count"];
                        bar_res_pending.ERX_NonPayer_Count = (int)dt.Rows[i]["ERX_NonPayers_Pending_Count"];

                        bar_res_pending.PBM_Payer_Count = (int)dt.Rows[i]["PBMLink_Payers_Pending_Count"];
                        bar_res_pending.PBM_NonPayer_Count = (int)dt.Rows[i]["PBMLink_NonPayers_Pending_Count"];

                        list_pbmErx_pending.Add(bar_res_pending);
                    }
                }
                Bar_PBMERX_Pending_Count[] bar_PEN = list_pbmErx_pending.ToArray();
                return bar_PEN;
            }
            catch(Exception ex)
            {
                Logger.Info(ex.Message);
                Logger.Error(ex);
                return null;
            }
        }
    }

    public class Results
    {
        public string TimeStamp { get; set; }
        public int PBM_Count { get; set; }
        public int ERX_Count { get; set; }
        public int OIC_Count { get; set; }
    }

    public class Status_Results
    {
        public string TimeStamp { get; set; }
        public double Claims_Count { get; set; }
        public double Clinician_Validate_Count { get; set; }
        public double Clinician_Count { get; set; }
        public double DHPO_Validate_Count { get; set; }
        public double Eauthorization_Count { get; set; }
        public double ERX_Validate_Count { get; set; }
        public double ERX_Pharmacy_Count { get; set; }
        public double LMU_Validate_Count { get; set; }
        public double Member_Registration_Count { get; set; }
        public double PBMLINK_Count { get; set; }
        public double PBMSwitch_Dimensions_Count { get; set; }
        public double PBMSwitch_Pyaer_Count { get; set; }
        public double Shafafiya_Validate_Count { get; set; }
    }

    public class Bar_PBMERX_Result
    {
        //public string System_Name { get; set; }

        public string PBMERX_CheckingTime { get; set; }
        public int ERX_Total_Count { get; set; }
        public int ERX_Total_Processed_Count { get; set; }
        public int ERX_Total_OurPayer_Count { get; set; }
        public int PBM_Total_Count { get; set; }
        public int PBM_Total_Processed_Count { get; set; }
        public int PBM_Total_OurPayer_Count { get; set; }
    }

    public class Bar_DHPO_Result
    {
        public string DHPO_CheckingTime { get; set; }

        public int DHPO_Total_CS { get; set; }
        public int DHPO_CS_Downloaded { get; set; }
        public int DHPO_CS_NotDownloaded { get; set; }

        public int DHPO_Total_PA { get; set; }
        public int DHPO_PA_Downloaded { get; set; }
        public int DHPO_PA_NotDownloaded { get; set; }

        public int DHPO_Total_PR { get; set; }
        public int DHPO_PR_Downloaded { get; set; }
        public int DHPO_PR_NotDownloaded { get; set; }
    }

    public class Bar_PBMERX_Pending_Count
    {
        public string Bar_PBMERX_CheckingTime { get; set; }
        public int ERX_Payer_Count { get; set; }
        public int ERX_NonPayer_Count { get; set; }

        public int PBM_Payer_Count { get; set; }
        public int PBM_NonPayer_Count { get; set; }
    }

}