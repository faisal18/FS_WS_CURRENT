using System;
using System.Collections.Generic;
using System.Globalization;

namespace ClinicianAutomation.Utilities_UI
{
    public partial class Transactions_Detail_Report : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        #region radio button
        protected void rd_byDubaiLicense_CheckedChanged(object sender, EventArgs e)
        {
            rd_byDubaiLicense.Checked = true;
            rd_byDates.Checked = false;
            rd_byProviderLicense.Checked = false;
            rd_byAll.Checked = false;

            pnl_Dates.Visible = true;
            pnl_DubaiLicense.Visible = true;
            pnl_ProviderLicense.Visible = false;
        }

        protected void rd_byProviderLicense_CheckedChanged(object sender, EventArgs e)
        {
            rd_byDubaiLicense.Checked = false;
            rd_byDates.Checked = false;
            rd_byProviderLicense.Checked = true;
            rd_byAll.Checked = false;

            pnl_Dates.Visible = true;
            pnl_DubaiLicense.Visible = false;
            pnl_ProviderLicense.Visible = true;
        }

        protected void rd_byDates_CheckedChanged(object sender, EventArgs e)
        {
            rd_byDubaiLicense.Checked = false;
            rd_byDates.Checked = true;
            rd_byProviderLicense.Checked = false;
            rd_byAll.Checked = false;

            pnl_Dates.Visible = true;
            pnl_DubaiLicense.Visible = false;
            pnl_ProviderLicense.Visible = false;
        }

        protected void rd_byAll_CheckedChanged(object sender, EventArgs e)
        {
            rd_byDubaiLicense.Checked = false;
            rd_byDates.Checked = false;
            rd_byProviderLicense.Checked = false;
            rd_byAll.Checked = true;

            pnl_Dates.Visible = true;
            pnl_DubaiLicense.Visible = true;
            pnl_ProviderLicense.Visible = true;
        }

        #endregion

        protected void btn_generatequery_Click(object sender, EventArgs e)
        {
            get_dateranges();
            ExtraClasses.Transactions_Detail_Report obj = new ExtraClasses.Transactions_Detail_Report();
            string process;

            if (chk_report.Checked)
            {
                process = "detail";
            }
            else
            {
                process = "count";
            }

            try
            {
                if (rd_byProviderLicense.Checked)
                {
                    if (txt_providerlicense.Text.Length > 0 && txt_startdate.Text.Length > 0 && txt_enddate.Text.Length > 0)
                    {
                        string input2 = txt_providerlicense.Text.ToString();
                        string[] differences2 = { ",", "\t", "\n", "\r" };
                        string[] provider_license = input2.Split(differences2, StringSplitOptions.RemoveEmptyEntries);
                        if (process == "detail")
                        {
                            for (int i = 0; i < global_start.Length; i++)
                            {
                                txt_richbox.InnerText = obj.generate_query_byProviderLicense(provider_license, global_start[i], global_end[i]);
                            }
                            //txt_richbox.InnerText = obj.generate_query_byProviderLicense(provider_license, txt_startdate.Text, txt_enddate.Text);
                            
                        }
                        else if(process == "count")
                        {
                            for (int i = 0; i < global_start.Length; i++)
                            {
                                txt_richbox.InnerText = obj.generate_query_count_byProviderLicense(provider_license, txt_startdate.Text, txt_enddate.Text);
                            }
                        }
                    }
                    else
                    {
                        txt_richbox.InnerText = "Please enter value in all fields";
                    }
                }

                else if (rd_byDubaiLicense.Checked)
                {
                    if (txt_dubailicenseno.Text.Length > 0 && txt_startdate.Text.Length > 0 && txt_enddate.Text.Length > 0)
                    {
                        string input = txt_dubailicenseno.Text.ToString();
                        string[] differences = { ",", "\t", "\n", "\r" };
                        string[] dubai_license = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);

                        if (process == "detail")
                        {
                            for (int i = 0; i < global_start.Length; i++)
                            {
                                txt_richbox.InnerText = obj.generate_query_byDubaiLicense(dubai_license, global_start[i], global_end[i]);
                            }
                        }
                        else if (process == "count")
                        {
                            for (int i = 0; i < global_start.Length; i++)
                            {
                                txt_richbox.InnerText = obj.generate_query_count_byDubaiLicense(dubai_license, global_start[i], global_end[i]);
                            }
                        }
                    }
                    else
                    {
                        txt_richbox.InnerText = "Please enter value in all fields";
                    }
                }
                else if (rd_byDates.Checked)
                {
                    if (txt_startdate.Text.Length > 0 && txt_enddate.Text.Length > 0)
                    {
                        DateTime start = Convert.ToDateTime(txt_startdate.Text); 
                        DateTime end = Convert.ToDateTime(txt_enddate.Text);
                        if ((end - start).TotalDays < 90)
                        {
                            txt_richbox.InnerText = "The difference in date is less than 90 days";
                            if (process == "detail")
                            {
                                for (int i = 0; i < global_start.Length; i++)
                                {
                                    txt_richbox.InnerText += obj.generate_query_byDate(global_start[i], global_end[i]);
                                }
                            }
                            else if(process == "count")
                            {
                                for (int i = 0; i < global_start.Length; i++)
                                {
                                    txt_richbox.InnerText += obj.generate_query_count_byDate(global_start[i], global_end[i]);
                                }
                            }
                        }
                        else if ((end - start).TotalDays > 90)
                        {
                            process = "count";
                            txt_richbox.InnerText = "The difference in date is more than 90 days";
                            for (int i = 0; i < global_start.Length; i++)
                            {
                                txt_richbox.InnerText += obj.generate_query_count_byDate(global_start[i], global_end[i]);
                            }
                        }
                    }
                    else
                    {
                        txt_richbox.InnerText = "Please enter values in date fields";
                    }
                }
                else if (rd_byAll.Checked)
                {
                    if (txt_providerlicense.Text.Length > 0 && txt_dubailicenseno.Text.Length > 0 && txt_startdate.Text.Length > 0 && txt_enddate.Text.Length > 0)
                    {
                        string input = txt_dubailicenseno.Text.ToString();
                        string[] differences = { ",", "\t", "\n", "\r" };
                        string[] dubai_license = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);

                        string input2 = txt_providerlicense.Text.ToString();
                        string[] differences2 = { ",", "\t", "\n", "\r" };
                        string[] provider_license = input2.Split(differences2, StringSplitOptions.RemoveEmptyEntries);

                        if (process == "detail")
                        {
                            for (int i = 0; i < global_start.Length; i++)
                            {
                                txt_richbox.InnerText = obj.generate_query_byAll(dubai_license, provider_license, global_start[i], global_end[i]);
                            }
                        }
                        else if(process == "count")
                        {
                            for (int i = 0; i < global_start.Length; i++)
                            {
                                txt_richbox.InnerText = obj.generate_query_count_byAll(dubai_license, provider_license, global_start[i], global_end[i]);
                            }
                        }
                    }
                    else
                    {
                        txt_richbox.InnerText = "Please enter values in all fields";
                    }
                }
            }

            catch (Exception ex)
            {

                txt_richbox.InnerText = ex.Message;
            }
        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            get_dateranges();
            ExtraClasses.Transactions_Detail_Report obj = new ExtraClasses.Transactions_Detail_Report();
            string process;

            if (chk_report.Checked)
            {
                process = "detail";
            }
            else
            {
                process = "count";
            }

            try
            {
                if (rd_byProviderLicense.Checked)
                {
                    if (txt_providerlicense.Text.Length > 0 && txt_startdate.Text.Length > 0 && txt_enddate.Text.Length > 0)
                    {
                        string input2 = txt_providerlicense.Text.ToString();
                        string[] differences2 = { ",", "\t", "\n", "\r" };
                        string[] provider_license = input2.Split(differences2, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < global_start.Length; i++)
                        {
                            txt_richbox.InnerText = obj.update_byProviderLicense(provider_license, global_start[i], global_end[i], process);
                        }
                    }
                    else
                    {
                        txt_richbox.InnerText = "Please enter value in all fields";
                    }
                }

                else if (rd_byDubaiLicense.Checked)
                {
                    if (txt_dubailicenseno.Text.Length > 0 && txt_startdate.Text.Length > 0 && txt_enddate.Text.Length > 0)
                    {
                        string input = txt_dubailicenseno.Text.ToString();
                        string[] differences = { ",", "\t", "\n", "\r" };
                        string[] dubai_license = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < global_start.Length; i++)
                        {
                            txt_richbox.InnerText = obj.update_byDubaiLicense(dubai_license, global_start[i], global_end[i], process);
                        }
                    }
                    else
                    {
                        txt_richbox.InnerText = "Please enter value in all fields";
                    }
                }
                else if (rd_byDates.Checked)
                {
                    if (txt_startdate.Text.Length > 0 && txt_enddate.Text.Length > 0)
                    {
                        DateTime start = Convert.ToDateTime(txt_startdate.Text);
                        DateTime end = Convert.ToDateTime(txt_enddate.Text);
                        

                        if ((end - start).TotalDays < 90)
                        {
                            txt_richbox.InnerText = "The difference in date is less than 90 days";

                            for (int i = 0; i < global_start.Length; i++)
                            {
                                txt_richbox.InnerText += obj.update_byDate(global_start[i], global_end[i], process);
                            }
                        }
                        else if ((end - start).TotalDays > 90)
                        {
                            process = "count";
                            txt_richbox.InnerText = "The difference in date is more than 90 days";

                            for (int i = 0; i < global_start.Length; i++)
                            {
                                txt_richbox.InnerText += obj.update_byDate(global_start[i], global_end[i], process);
                            }
                        }
                    }
                    else
                    {
                        txt_richbox.InnerText = "Please enter values in date fields";
                    }
                }
                else if (rd_byAll.Checked)
                {
                    if (txt_providerlicense.Text.Length > 0 && txt_dubailicenseno.Text.Length > 0 && txt_startdate.Text.Length > 0 && txt_enddate.Text.Length > 0)
                    {
                        string input = txt_dubailicenseno.Text.ToString();
                        string[] differences = { ",", "\t", "\n", "\r" };
                        string[] dubai_license = input.Split(differences, StringSplitOptions.RemoveEmptyEntries);

                        string input2 = txt_providerlicense.Text.ToString();
                        string[] differences2 = { ",", "\t", "\n", "\r" };
                        string[] provider_license = input2.Split(differences2, StringSplitOptions.RemoveEmptyEntries);

                        for (int i = 0; i < global_start.Length; i++)
                        {
                            txt_richbox.InnerText = obj.update_byAll(dubai_license, provider_license, global_start[i], global_end[i], process);
                        }
                    }
                    else
                    {
                        txt_richbox.InnerText = "Please enter values in all fields";
                    }
                }
            }

            catch (Exception ex)
            {

                txt_richbox.InnerText = ex.Message;
            }
        }

        private void get_dateranges()
        {
            DateTime start = Convert.ToDateTime(txt_startdate.Text);
            DateTime end = Convert.ToDateTime(txt_enddate.Text);
            List<string> start_dates = new List<string>();
            List<string> end_dates = new List<string>();

            while ((end - start).TotalDays > 0)
            {
                string new_Startdate;
                string lower_enddate;
                if (end.Month - start.Month > 0)
                {
                    new_Startdate = Convert.ToString(start.Year) + "-";
                    new_Startdate += Convert.ToString(start.Month.ToString("d2")) + "-";
                    new_Startdate += Convert.ToString(start.Day.ToString("d2"));

                    lower_enddate = Convert.ToString(start.Year) + "-";
                    lower_enddate += Convert.ToString(start.Month.ToString("d2")) + "-";
                    lower_enddate += DateTime.DaysInMonth(start.Year, start.Month).ToString("d2");

                    start_dates.Add(new_Startdate);
                    end_dates.Add(lower_enddate);

                    double increment = Convert.ToDouble((DateTime.DaysInMonth(start.Year, start.Month) - start.Day));
                    start = start.AddDays(increment + 1);

                }
                else
                {
                    lower_enddate = Convert.ToString(end.Year) + "-";
                    lower_enddate += Convert.ToString(end.Month.ToString("d2")) + "-";
                    lower_enddate += Convert.ToString(end.Day.ToString("d2"));

                    new_Startdate = Convert.ToString(start.Year) + "-";
                    new_Startdate += Convert.ToString(start.Month.ToString("d2")) + "-";
                    new_Startdate += Convert.ToString(start.Day.ToString("d2"));

                    start_dates.Add(new_Startdate);
                    end_dates.Add(lower_enddate);

                    break;
                }

            }

            global_start =  start_dates.ToArray();
            global_end = end_dates.ToArray();
        }
        
        private string[] global_start;
        private string[] global_end;

    }
}

