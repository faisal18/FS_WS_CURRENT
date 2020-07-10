using System;

namespace ClinicianAutomation
{
    public partial class Get_TurnAroundTime : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
         //   txtFullLog.ReadOnly = true;

        }

        ExtraClasses.get_turnaroundtime get_tat = new ExtraClasses.get_turnaroundtime();

        protected void submit_Click(object sender, EventArgs e)
        {
            if(lbl_license.Text.ToString() != null && lbl_startdate.Text.ToString() != null && lbl_enddate.Text.ToString() != null)
            {
                int status;
                if(rd_getdata.Checked == true)
                {
                    status = 1;
                    txt_richbox.InnerText = get_tat.script(txt_license.Text.ToString(), txt_startdate.Text.ToString(), txt_enddate.Text.ToString(), status);
                }
                else if(rd_getcount.Checked == true)
                {
                    status = 0;
                    txt_richbox.InnerText = get_tat.script(txt_license.Text.ToString(), txt_startdate.Text.ToString(), txt_enddate.Text.ToString(), status);
                }
            }
        }

        protected void rd_getdata_CheckedChanged(object sender, EventArgs e)
        {
            rd_getdata.Checked = true;
            rd_getcount.Checked = false;
        }

        protected void rd_getcount_CheckedChanged(object sender, EventArgs e)
        {
            rd_getcount.Checked = true;
            rd_getdata.Checked = false;
        }

        //protected void cldr_enddate_SelectionChanged(object sender, EventArgs e)
        //{
        //    txt_enddate.Text = cldr_enddate.SelectedDate.ToString("s");
        //    txt_enddate.Text = txt_enddate.Text.Replace('T', ' ');
        //}

        //protected void cldr_startdate_SelectionChanged(object sender, EventArgs e)
        //{
        //    txt_startdate.Text = cldr_startdate.SelectedDate.ToString("s");
        //    txt_startdate.Text = txt_startdate.Text.Replace('T', ' ');
        //}
    }
}