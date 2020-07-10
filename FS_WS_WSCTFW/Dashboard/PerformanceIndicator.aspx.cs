using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;


namespace FS_WS_WSCTFW.Dashboard
{
    public partial class PerformanceIndicator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            DashboardData.DashboardProcess();
        }



    }
}