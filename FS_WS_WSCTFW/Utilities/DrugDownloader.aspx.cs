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

using System.Text.RegularExpressions;


namespace FS_WS_WSCTFW.Utilities
{
    public partial class DrugDownloader : System.Web.UI.Page
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

            if ( txtStart.Text.Length > 0 && txtEnd.Text.Length > 0 && int.Parse(txtStart.Text) <= int.Parse(txtEnd.Text) && int.Parse(txtStart.Text) > 0)
            {
                string filename = DateTime.Now.ToString("MMddyy_hhmmsstt") + "_Drugs_KSA_" + cmbType.SelectedValue.ToString() + ".CSV";
                txtLog.Text = ("Items will be downloaded and will be saved at given folder with filename:  " + filename);
                ParseKSADetailPages(cmbType.SelectedValue.ToString(), int.Parse(txtStart.Text), int.Parse(txtEnd.Text),filename);
            }
            else
            {
                txtLog.Text = "Please enter correct  Range !!! " + Environment.NewLine;
            }

        }


        public static string ParseItemDetails(string url, int count)
        {
            string ParsedText = "Registration No|Trade Name|GenericName|Strength Value|Dosage Form|Route of Administration|Volume|Unit of Volume|Package Type|Shelf Life|Package Size|Legal Status|Product Control|Storage Conditions|Manufacturer Name|Country of Manufacturer|Marketing Company|Marketing Company Nationality|Agent Name|Authorization Status|Marketing Status|اللون|الشكل|رمز تعريف الدواء|Price|ATC Code1|ATC Code2|DrugID|" + Environment.NewLine;
            string path = DateTime.Now.ToString("MMddyy_hhmmsstt") + "_Drugs_KSA.CSV";


            for (int PageNumber = 1; PageNumber < count; PageNumber++)
            {

                string txtURL = "http://www.sfda.gov.sa/en/drug/search/Pages/drugdetails.aspx?did=" + PageNumber.ToString() + "&sm=human";
                Helpers.Logger.Info(txtURL);

                WebClient webClient = new WebClient();
                string page = webClient.DownloadString(txtURL);
                //txtLog.Text += Environment.NewLine + txtURL.Text;
                //txtLog.Text += Environment.NewLine + PageNumber.ToString();

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);
                //List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='druglistgrid']")
                if (doc.DocumentNode.SelectSingleNode("//table[@class='grid2']") != null)
                {
                    List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='grid2']")
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

                            ParsedText += item[1] + "|";

                            //foreach (var item in table[i])
                            //{
                            //    ParsedText += item + "|";
                            //}

                        }
                  //      ParsedText += "|";
                        ParsedText += PageNumber + "|";
                        ParsedText += Environment.NewLine;
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
            }
            return ParsedText;
        }


        public static void ParseKSAMainPages(string type, int count, string className, string filename)
        {
           // string ParsedText = " Trade Name| Strength Value| Dosage Form| Marketing Company| Price| Details | Identifier1 | Identifier 2 | Registration No|Trade Name|Strength Value|Dosage Form|Route of Administration|Volume|Unit of Volume|Package Type|Shelf Life|Package Size|Legal Status|Product Control|Storage Conditions|Manufacturer Name|Country of Manufacturer|Marketing Company|Marketing Company Nationality|Agent Name|Authorization Status|Marketing Status|اللون|الشكل|رمز تعريف الدواء|Price|ATC Code1|ATC Code2|DrugID|" + Environment.NewLine;

        string ParsedText = "Generic Name| Trade Name| Strength Value| Dosage Form| Marketing Company| Price| Details | Identifier1 | Identifier 2 | Registration No|Trade Name|GenericName|Strength Value|Dosage Form|Route of Administration|Volume|Unit of Volume|Package Type|Shelf Life|Package Size|Legal Status|Product Control|Storage Conditions|Manufacturer Name|Country of Manufacturer|Marketing Company|Marketing Company Nationality|Agent Name|Authorization Status|Marketing Status|اللون|الشكل|رمز تعريف الدواء|Price|ATC Code1|ATC Code2|DrugID|" + Environment.NewLine;
           AppendFIle(filename, ParsedText);

            for (int PageNumber = 1; PageNumber < count+1; PageNumber++)
            {
                ParsedText = "";
                string txtURL = "http://www.sfda.gov.sa/en/drug/search/Pages/default.aspx?sm=" + type + "&PageIndex=" + PageNumber.ToString();
                ParsedText += KSADrugMainPage(txtURL, className, PageNumber.ToString(), "",type) ;
                AppendFIle(filename, ParsedText);
            }

          //  return ParsedText;

        }

        public static void ParseKSADetailPages(string type, int start, int end, string filename)
        {
            string ParsedText = "";

            // string 
            if (type == "human")
            {
                ParsedText = "Registration No|Trade Name|GenericName|Strength Value|Dosage Form|Route of Administration|Volume|Unit of Volume|Package Type|Shelf Life|Package Size|Legal Status|Product Control|Storage Conditions|Manufacturer Name|Country of Manufacturer|Marketing Company|Marketing Company Nationality|Agent Name|Authorization Status|Marketing Status|اللون|الشكل|رمز تعريف الدواء|Price|ATC Code1|ATC Code2|DrugID|" + Environment.NewLine;

            }
            else
            {
                ParsedText = "Registration No|Trade Name|Strength Value|Dosage Form|Route of Administration|Volume|Unit of Volume|Package Type|Shelf Life|Package Size|Legal Status|Product Control|Storage Conditions|Manufacturer Name|Country of Manufacturer|Marketing Company|Marketing Company Nationality|Agent Name|Authorization Status|Marketing Status|اللون|الشكل|رمز تعريف الدواء|Price|ATC Code1|ATC Code2|DrugID|" + Environment.NewLine;

            }

            AppendFIle(filename, ParsedText);

            for (int PageNumber = start; PageNumber < end + 1; PageNumber++)
            {
                ParsedText = "";

                ParsedText += KSADrugDetailParser(type, PageNumber.ToString());
                AppendFIle(filename, ParsedText);
            }

            //  return ParsedText;

        }

        protected void btnSaveCSV_Click(object sender, EventArgs e)
        {
            if (txtLog.Text.Trim().Length > 1)
            {

                string path = System.Configuration.ConfigurationManager.AppSettings["DrugDetailsDownload"];
                path = path + DateTime.Now.ToString("MMddyy_hhmmsstt") + "_Drugs_KSA.CSV";


                //using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(Server.MapPath("~/Utilities/ClinicianUpdateScript/Script.SQL"), false))

                if (!System.IO.File.Exists(path))
                {
                    using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(path, false))
                    {
                        _testData.WriteLine(txtLog.Text); // Write the file.
                    }

                    txtLog.Text = "file save successfully";
                }
                else
                {

                    txtLog.Text = "Error Saving File !!!!!!!!!!!!!!!!!!";

                }
            }
        }



        public static void AppendFIle(string filename, string texttoAppend)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["DrugDetailsDownload"];
            path = path + filename;


            //using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(Server.MapPath("~/Utilities/ClinicianUpdateScript/Script.SQL"), false))

            if (System.IO.File.Exists(path))
            {
                using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(path, true))
                {
                    _testData.WriteLine(texttoAppend); // Write the file.
                }

             //   txtLog.Text = "file save successfully";
            }
            else
            {

                using (System.IO.StreamWriter _testData = new System.IO.StreamWriter(path, true))
                {
                    _testData.WriteLine(texttoAppend); // Write the file.
                }

                //     txtLog.Text = "Error Saving File !!!!!!!!!!!!!!!!!!";

            }
        }


        public static string KSADrugDetailsPage (string url,string className, string identifier1, string identifier2)
        {


            try
            {
                string ParsedText = "";
              
                Helpers.Logger.Info(url);

                WebClient webClient = new WebClient();
                string page = webClient.DownloadString(url);
                //txtLog.Text += Environment.NewLine + txtURL.Text;
                //txtLog.Text += Environment.NewLine + PageNumber.ToString();

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);
                //List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='druglistgrid']")
                if (doc.DocumentNode.SelectSingleNode("//table[@class='"+ className + "']") != null)
                {
                    List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='" + className + "']")
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

                            ParsedText += item[1] + "|";

                            //foreach (var item in table[i])
                            //{
                            //    ParsedText += item + "|";
                            //}

                        }
                        //      ParsedText += "|";
                        ParsedText += identifier1 + "|";
                        ParsedText += identifier2 + "|";
                        ParsedText += Environment.NewLine;
                        
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
            catch (Exception)
            {

                Helpers.Logger.Error("Error in GetDetails method, kindly check input values");
                return null;
            
            }


        }


        public static string KSADrugMainPage(string url, string className, string identifier1, string identifier2,string type)
        {


            try
            {
                string ParsedText = "";

                Helpers.Logger.Info(url);

                WebClient webClient = new WebClient();
                string page = webClient.DownloadString(url);
                //txtLog.Text += Environment.NewLine + txtURL.Text;
                //txtLog.Text += Environment.NewLine + PageNumber.ToString();

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);
                //List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='druglistgrid']")
                if (doc.DocumentNode.SelectSingleNode("//table[@class='" + className + "']") != null)
                {
                    List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='" + className + "']")
                                .Descendants("tr")
                                .Skip(1)
                                .Where(tr => tr.Elements("td").Count() > 1)
                                .Select(tr => tr.Elements("td").Select(td => td.InnerText.Trim()).ToList())
                                .ToList();
                    if (table != null)
                    {

                        for (int i = 0; i < table.Count; i++)
                        {
                            //string[] item = table[i].ToArray();

                            //ParsedText += item[1] + "|";

                            foreach (var item in table[i])
                            {

                                string updatedText = item;

                                if (updatedText.Contains("\r\n               "))
                                {
                                    updatedText = updatedText.Replace("\r\n               ", " ");
                                 }


                                if (updatedText.Contains(","))
                                {
                                    updatedText = updatedText.Replace(",", " ");
                                }


                              
                                
                                    ParsedText += updatedText + "|";
                               
                                int id = int.Parse(identifier1);
                                id = id - 1;
                                id = id * 10;
                                 identifier2 = (id + (i + 1)).ToString();
                                
                            }
                            ParsedText += identifier1 + "|";
                            ParsedText += identifier2 + "|";
                            ParsedText += KSADrugDetailParser(type, identifier2);
                            ParsedText += Environment.NewLine;
                        }
                        //      ParsedText += "|";
                      
                       

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
            catch (Exception)
            {

                Helpers.Logger.Error("Error in GetDetails method, kindly check input values");
                return null;

            }


        }



        public static string KSADrugDetailParser(string type, string ItemNumber)
        {
            // string ParsedText = "Registration No|Trade Name|GenericName|Strength Value|Dosage Form|Route of Administration|Volume|Unit of Volume|Package Type|Shelf Life|Package Size|Legal Status|Product Control|Storage Conditions|Manufacturer Name|Country of Manufacturer|Marketing Company|Marketing Company Nationality|Agent Name|Authorization Status|Marketing Status|اللون|الشكل|رمز تعريف الدواء|Price|ATC Code1|ATC Code2|DrugID|" + Environment.NewLine;
            string ParsedText = "";
            // string path = DateTime.Now.ToString("MMddyy_hhmmsstt") + "_Drugs_KSA.CSV";




            string txtURL = "http://www.sfda.gov.sa/en/drug/search/Pages/drugdetails.aspx?did=" + ItemNumber + "&sm="+type+"";
                Helpers.Logger.Info(txtURL);

                WebClient webClient = new WebClient();
                string page = webClient.DownloadString(txtURL);
                //txtLog.Text += Environment.NewLine + txtURL.Text;
                //txtLog.Text += Environment.NewLine + PageNumber.ToString();

                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(page);
                //List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='druglistgrid']")
                if (doc.DocumentNode.SelectSingleNode("//table[@class='grid2']") != null)
                {
                    List<List<string>> table = doc.DocumentNode.SelectSingleNode("//table[@class='grid2']")
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