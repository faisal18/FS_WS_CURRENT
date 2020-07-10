using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FS_WS_WSCTFW.Utilities
{
    public partial class BatchFileGenerator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            char[] dataChar = new char[] { '\r', '\n'};
            string[] filename = txtFilename.Text.Split(dataChar, StringSplitOptions.RemoveEmptyEntries);
            string[] fileContent = txtFileContent.Text.Split(dataChar, StringSplitOptions.RemoveEmptyEntries);


            if (fileContent.Length == filename.Length)
            {

                for (int i = 0; i < fileContent.Length; i++)
                {



                    txtlog.Text = "";
                    string path = System.Configuration.ConfigurationManager.AppSettings["ClinicianPath"];
                    path = path + filename[i];
                    //path = path + txtFilename.Text + ".Bat";


                    //using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(Server.MapPath("~/Utilities/ClinicianUpdateScript/Script.SQL"), false))

                    if (!System.IO.File.Exists(path))
                    {
                        using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(path, false))
                        {
                            _testData.WriteLine(fileContent[i]); // Write the file.
                        }

                        txtlog.Text = "file save successfully";
                    }


                }

               
            }

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            txtFileContent.Text = "";
            txtFilename.Text = "";
            txtlog.Text = "";
        }
    }
}