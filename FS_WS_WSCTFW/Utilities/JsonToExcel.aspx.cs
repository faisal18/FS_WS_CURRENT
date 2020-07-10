using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace FS_WS_WSCTFW.Utilities
{
    public partial class JsonToExcel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }

        }

        protected void btnProcessFiles_Click(object sender, EventArgs e)
        {
            string patth = "c:\\gatling\\results\\";

            string[] filenames = Directory.GetFiles(patth, "stats.json", SearchOption.AllDirectories);


            txtlog.Text = "";
            foreach (string item in filenames)
            {

                //   txtlog.Text = txtlog.Text + Environment.NewLine + item;
                txtlog.Text += Environment.NewLine;
                txtlog.Text += JSonParser(item);
            }


        }
        public class Item
        {
            
            public string name;
            public float meanResponseTime;
            public float meanNumberOfRequestsPerSecond;
        }
        public static string JSonParser (string JsonFilePath)
        {
            try
            {


                StreamReader r = new StreamReader(JsonFilePath);

                    string AllCheck = r.ReadToEnd();
                string master = "";
                //   using (StreamReader r = new StreamReader(JsonFilePath))
                //{
                //    string json = r.ReadToEnd();
                //    List<Item> items = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Item>>(json);
                //}

                //dynamic results = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(AllCheck);
                //var id = results.Id;
                //var name = results.Name;


                ////jsonString is your JSON-formatted string
                //JObject jsonObj =JObject.Parse(AllCheck);
                //Dictionary<string, object> dictObj = jsonObj.ToObject<Dictionary<string, object>>();


                //master += master + dictObj["stats"].ToString();

                Newtonsoft.Json.JsonTextReader reader = new Newtonsoft.Json.JsonTextReader(new StringReader(AllCheck));
                while (reader.Read())
                {

                    // master += Environment.NewLine;
                    //master +=  reader.TokenType.ToString(); 
                    //if (reader.ValueType == null)
                    //{

                    //}
                    //else
                    //{
                    //    master +=  "||" + reader.ValueType.ToString();
                    //}

                    if (reader.Value == null && (reader.TokenType.ToString() != "PropertyName" || reader.TokenType.ToString() != "StartObject"))
                    {

                    }
                    else
                    {
                      //  if (new[] { "type", "GROUP", "name", "Global Information", "path", "pathFormatted", "group_missing-name-b06d1", "stats", "name", "total", "contents", "REQUEST" }.Contains(reader.Value.ToString()))

                            if (new[] { "TEST" }.Contains(reader.Value.ToString()))
                        {
                            //x is either 1 or 2 or 3
                        }
                        else
                        {

                            master += reader.Value.ToString() + ",";
                        }
                    }
                    //  master += Environment.NewLine +  reader.ValueType ==null ? "": reader.ValueType.ToString();
                    //  master += Environment.NewLine + reader.Value == null ? "" : reader.Value.ToString();


                    //  string[] jsonArray = new string[]();
                    //  jsonArray.re reader.TokenType.ToString();
                    //  jsonArray
                    //
                }
             //   master += Environment.NewLine;        
 return master;

            }
            catch (Exception)
            {
                return null;
                
            }



        }

        protected void btnSaveFile_Click(object sender, EventArgs e)
        {

            if (txtlog.Text.Trim().Length > 1)
            {

                string path = System.Configuration.ConfigurationManager.AppSettings["ClinicianPath"];
                path = path + DateTime.Now.ToString("MMddyy_hhmmsstt") + "_JSON.CSV";


                //using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(Server.MapPath("~/Utilities/ClinicianUpdateScript/Script.SQL"), false))

                if (!System.IO.File.Exists(path))
                {
                    using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(path, false))
                    {
                        _testData.WriteLine(txtlog.Text); // Write the file.
                    }
                
                    txtlog.Text = "file save successfully";
                }
                else
                {

                    txtlog.Text = "Error Saving File !!!!!!!!!!!!!!!!!!";
                 
                }
            }
        }
    }
}