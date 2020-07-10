using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Xml;

namespace FS_WS_WSCTFW.Utilities.DBModification.ExtraClasses
{
    public class Aims_Insert_Clinician
    {
        public string Execute(string license,string name,string username,string password,string start,string end,string env, string Source)
        {
            string result = string.Empty;
            try
            {
                string path = System.Configuration.ConfigurationManager.AppSettings["dhpo_path"] + "Dhpo_login_" + license.ToString() + "_" + DateTime.Now.ToString("yyyyMMddHHmmss");
                string query = generate_query(license, username, password, start, end);
                FS_WS_WSCTFW.Helpers.BatchProcess.createBatchProcess(query, "eHDF", "_AIMSDHPO_Login_Update_" + license, "DHPO", 2);


                string body = Post_DirectClinician(name, license, end, Source);
                string post_Result1 = API("clinician/clinicianSync", body, "POST", env);

                result = JObject.Parse(post_Result1).ToString();

                string get_Result0 = API("clinician/pagination?searchData=" + name.Replace(" ", "+"), "", "GET", env);
                if(get_Result0.Length>1)
                {
                    JObject j_res = JObject.Parse(get_Result0);
                    if(j_res["status"].ToString() == "NO_CONTENT")
                    {
                        //string body = Post_RegisterClinician(name);
                        //string post_Result = API("clinician", body, "POST", env);

                        //if (post_Result.Length > 1)
                        //{
                        //    if (JObject.Parse(post_Result)["id"].ToString().Length > 1)
                        //    {
                        //        string ClinId = JObject.Parse(post_Result)["id"].ToString();

                        //        string body1 = Post_DetailsClinician(license, ClinId, username, password, start, end);
                        //        string post_Result1 = API("clinicianRegInfo", body1, "POST", env);
                        //    }
                        //}

                        //string body = Post_DirectClinician(name,license,end,Source);
                        //string post_Result1 = API("clinician/clinicianSync", body, "POST", env);

                        //result = JObject.Parse(post_Result1).ToString();

                    }
                    else if(j_res["status"].ToString() == "OK")
                    {
                        result = "The clinician exists in AIMS";
                    }

                }

            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        private string Post_DirectClinician(string name,string license, string date, string source)
        {
            string result = string.Empty;
            date = Convert.ToDateTime(date).ToString("yyyy-MM-dd");
            try
            {
                result = "[{    \"name\": \"" + name + "\",    \"license\":\""+ license +"\",    \"effective_date\":\"" + date + "\",    \"regulator_code\":\"" + source + "\"}]";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public string generate_query(string license ,string user, string password,string start,string end)
        {
            //'" + license + "'"
            string query = "\nUSE DHPO " +
            "\nGO " +
            "\ndeclare @LicenseID varchar(15);declare @Username varchar(50);declare @Password varchar(50);declare @Start varchar(50);declare @End varchar(50); DECLARE @xmltmp xml " +
            "\ndeclare @LicenseStartDate varchar(40);declare @LicenseEndate varchar(40) " +
            "\nset @LicenseID = '" + license+ "';set @Username = '" + user + "';set @Password = '" + password + "';set @Start = '" + start + "';set @End = '" + end + "' " +
            " " +
            "\nif exists(select ClinicianLicense from DHPO.dbo.Clinicians where ClinicianLicense = @LicenseID) " +
            "\n	begin " +
            "\n			Print 'License exists in DHPO' " +
            "\n			Set @LicenseStartDate  = (select LicenseStartDate from DHPO.dbo.Clinicians where ClinicianLicense = @LicenseID) " +
            "\n			Set @LicenseEndate  = (select LicenseEndDate from DHPO.dbo.Clinicians where ClinicianLicense = @LicenseID) " +
            "\n			if(@LicenseStartDate is null) " +
            "\n			begin " +
            "\n				Print 'License start date & end date are null in DHPO' " +
            "\n				Print 'Updating dates in DHPO' " +
            "\n				update top(1) DHPO.dbo.Clinicians set LicenseStartDate = @Start, LicenseEndDate = @End " +
            "\n				where ClinicianLicense = @LicenseID  " +
            "\n				Print 'Dates updated in DHPO' " +
            "\n				Set @xmltmp = (Select * from DHPO.dbo.Clinicians where  ClinicianLicense = @LicenseID FOR XML AUTO) " +
            "\n				PRINT CONVERT(NVARCHAR(MAX), @xmltmp) " +
            "\n			end " +
            "\n	end " +
            "\nelse " +
            "\nbegin " +
            "\n	Print 'Clinician does not exists in DHPO.' " +
            "\n	Print 'Please insert using the Automation or Manual Script' " +
            "\n	Print 'Please alert L2 for this issue' " +
            "\nend ";
            return query;
        }
        private string Post_RegisterClinician(string name)
        {
            string result = string.Empty;
            try
            {
                result = "{\"active\":true,\"specialityList\":[],\"name\":\"" + name + "\"}";
            }
            catch (Exception ex)
            {
                //lbl_message.Text = ex.Message;
                result = ex.Message;
            }
            return result;
        }
        private string Post_DetailsClinician(string license,string Id, string user, string password, string start, string end)
        {
            string result = string.Empty;
            try
            {
                result = "{ " +
                "\"active\": true, " +
                "\"clinician\": {\"id\": " + Id + "}," +
                "\"effFromDate\": \"" + start + "\"," +
                "\"effToDate\": \"" + end + "\"," +
                "\"licenseNo\": \"" + license + "\"," +
                "\"postOfficePassword\": \"" + password + "\"," +
                "\"postOfficeUsername\": \"" + user + "\"}";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }

            return result;
        }

        public static string API(string resource, string data, string method,string enviorment)
        {
            string result = string.Empty;

            string URL = AIMS_GetSingle(enviorment, "URL");
            string URLResource = AIMS_GetSingle(enviorment, "resource");
            URL = URL + URLResource + resource;
            string auth = AIMS_GetSingle(enviorment, "bearer");
            try
            {
                using (WebClient wc = new WebClient())
                {
                    wc.Headers[HttpRequestHeader.ContentType] = "application/json";
                    wc.Headers[HttpRequestHeader.Authorization] = auth;
                    wc.Headers.Add("User-Agent: Other");

                    switch (method.ToUpper())
                    {
                        case "GET":
                            result = wc.DownloadString(URL);
                            break;
                        case "POST":
                            result = wc.UploadString(URL, data);
                            break;
                        case "DELETE":
                            result = wc.UploadString(URL, method, data);
                            break;
                        case "PUT":
                            result = wc.UploadString(URL, method, data);
                            break;
                    }
                }
            }
            catch (WebException ex)
            {
                string result1 = new StreamReader(ex.Response.GetResponseStream()).ReadToEnd();
            }
            catch (Exception ex)
            {
                result = ex.Message;
                result = string.Empty;
            }
            return result;
        }
        private static string AIMS_GetSingle(string connect, string node)
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();
                string path = HttpContext.Current.ApplicationInstance.Server.MapPath("~/App_Data");
                string FileDir = path + "\\AIMS_API.xml" ;
                xdoc.Load(FileDir);

                string query = string.Format("//*[@name='{0}']", connect);
                XmlNode node1 = xdoc.SelectSingleNode(query);
                if (node1.ChildNodes.Count > 0)
                {
                    return node1[node].InnerText;
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}