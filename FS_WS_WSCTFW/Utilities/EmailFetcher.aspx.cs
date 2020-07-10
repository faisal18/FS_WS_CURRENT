using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FS_WS_WSCTFW.Utilities
{
    public partial class EmailFetcher : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnFetch_Click(object sender, EventArgs e)
        {
            try
            {

                FS_WS_WSCTFW.Helpers.EmailParserHelper.FetchEmailOpenPop(txtPopEmail.Text, txtPopPass.Text);
            }

            catch (Exception ex)
            {

                FS_WS_WSCTFW.Helpers.Logger.Error(ex);
            }
        }
    }
}