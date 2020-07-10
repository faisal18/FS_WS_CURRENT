using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ClinicianAutomation.Utilities_UI
{
    public partial class Bucket_Mapping_Report : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void btn_generate_query_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_dubailicense.Text != "")
                {
                    ExtraClasses.Bucket_Mapping_Report obj = new ExtraClasses.Bucket_Mapping_Report();
                    txt_richbox.InnerText = obj.generate_query(txt_dubailicense.Text);
                }
            }
            catch(Exception ex)
            {
                txt_richbox.InnerText = ex.Message;
            }

        }

        protected void btn_submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_dubailicense.Text != "")
                {
                    ExtraClasses.Bucket_Mapping_Report obj = new ExtraClasses.Bucket_Mapping_Report();
                    txt_richbox.InnerText = obj.update(txt_dubailicense.Text);
                }
            }
            catch (Exception ex)
            {
                txt_richbox.InnerText = ex.Message;
            }
        }
    }
}