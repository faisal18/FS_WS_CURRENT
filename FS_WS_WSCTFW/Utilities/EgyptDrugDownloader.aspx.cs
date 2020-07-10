using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HtmlAgilityPack;
using System.Diagnostics;
using System.Net;
using Microsoft.AspNet.Identity;
namespace FS_WS_WSCTFW.Utilities
{
    public partial class EgyptDrugDownloader : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
            // txtFullLog.ReadOnly = true;
        }

        protected void btnDownloadHTML_Click(object sender, EventArgs e)
        {
           txtLog.Text +=  EGDrugDetailParser("", "10");
        }

        public static string EGDrugDetailParser(string type, string ItemNumber)
        {
            // string ParsedText = "Registration No|Trade Name|GenericName|Strength Value|Dosage Form|Route of Administration|Volume|Unit of Volume|Package Type|Shelf Life|Package Size|Legal Status|Product Control|Storage Conditions|Manufacturer Name|Country of Manufacturer|Marketing Company|Marketing Company Nationality|Agent Name|Authorization Status|Marketing Status|اللون|الشكل|رمز تعريف الدواء|Price|ATC Code1|ATC Code2|DrugID|" + Environment.NewLine;
            string ParsedText = "";
            // string path = DateTime.Now.ToString("MMddyy_hhmmsstt") + "_Drugs_KSA.CSV";




            string txtURL = "http://www.eda.mohealth.gov.eg/SearchRegDrugs.aspx"; // + ItemNumber + "&sm=" + type + "";
      

            Helpers.Logger.Info(txtURL);

            WebClient webClient = new WebClient();
            string page = webClient.DownloadString(txtURL);
            //txtLog.Text += Environment.NewLine + txtURL.Text;
            //txtLog.Text += Environment.NewLine + PageNumber.ToString();

            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(page);
            doc.GetElementbyId("ctl00_ContentPlaceHolder1_ui_btnSearch");
            Helpers.Logger.Info(doc.GetElementbyId("ctl00_ContentPlaceHolder1_ui_btnSearch"));

        //    Helpers.Logger.Info(doc.DocumentNode.("ctl00_ContentPlaceHolder1_ui_btnSearch"));


            //List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='druglistgrid']")
            if (doc.DocumentNode.SelectSingleNode("//div[@ID='ctl00_ContentPlaceHolder1_GridView1']") != null)
            {
                List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@ID='ctl00_ContentPlaceHolder1_GridView1']")
                            .Descendants("tr")
                            .Skip(1)
                            .Where(tr => tr.Elements("td").Count() > 1)
                            .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                            .ToList();
                if (table != null)
                {

                    for (int i = 0; i < table.Count; i++)
                    {
                        string[] item = table[i].ToArray();
                        string updatedText = item[1];

                        if (updatedText.Contains("\r\n               "))
                        {
                            updatedText = updatedText.Replace("\r\n               ", " ");
                        }


                        if (updatedText.Contains(","))
                        {
                            updatedText = updatedText.Replace(",", " ");
                        }




                        ParsedText += updatedText + "|";



                        //   ParsedText += item[1] + "|";

                        //foreach (var item in table[i])
                        //{
                        //    ParsedText += item + "|";
                        //}

                    }
                    //      ParsedText += "|";
                    ParsedText += ItemNumber + "|";
                    //ParsedText += Environment.NewLine;
                    //AppendFIle(path,ParsedText);
                }
                else
                {
                    Helpers.Logger.Error("********* Error in this LInk ***********************88");
                }
            }
            else
            {
                Helpers.Logger.Error("********************** Error *****************************");
            }

            return ParsedText;
        }
    }
}