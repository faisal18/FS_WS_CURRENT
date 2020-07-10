using System;
using System.Collections;
using System.IO;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace ClinicianAutomation.ExtraClasses
{
    public class Connections
    {
        public string run_connection(string key)
        {
            //return hash_it(key).ToString();

            return get_conn(key);
        }
        public string get_inline_connection(string key)
        {
            return get_inline_conn(key);
        }
        public static string run_singlevalue(string connect,string node)
        {
            return get_Single_node(connect, node);
        }

        private static string hash_it(string key)
        {
            Hashtable conn_hash = new Hashtable();
            conn_hash.Add("ECLAIMLINKPORTAL", "-S 10.156.62.42 -d EclaimLinkPortal -U fazeel -P Dell@123");
            conn_hash.Add("ECLAIMLINK", "-S 10.156.62.42 -d Eclaimlink -U fazeel -P Dell@123");
            conn_hash.Add("ERX", "-S 10.156.62.42 -d ERX -U fazeel -P Dell@123");
            conn_hash.Add("DHPO", "-S 10.162.176.24 -d DHPO -U fshaikh -P Dell@777");
            conn_hash.Add("PBMM", "-S 10.5.3.42 -d PBMM -U fshaikh -P Dell@123");
            conn_hash.Add("OICProvider", "-S 10.11.13.163 -d OIC.Eauth -U fshaikh -P Dell@123");
            conn_hash.Add("OICPayer", "-S 10.162.176.29 -d eAuth -U fshaikh -P Dell@123");

            return conn_hash[key].ToString();
        }
        private static string get_conn(string connect)
        {

            try
            {
                XmlDocument xdoc = new XmlDocument();
                string path = System.Configuration.ConfigurationManager.AppSettings["ConnectionsXML"];

                FS_WS_WSCTFW.Helpers.FileEncryption.DecryptFile(path);
                xdoc.Load(path);

                FS_WS_WSCTFW.Helpers.FileEncryption.encryptFile(path);

                //  xdoc.Load(HttpContext.Current.Server.MapPath("~/App_Data/connections_server.xml"));
                //XmlNodeList boi = xdoc.GetElementsByTagName("connection");
                string query = string.Format("//*[@name='{0}']", connect);
                return xdoc.SelectSingleNode(query).InnerText.ToString();
            }
            catch (Exception ex)
            {
                FS_WS_WSCTFW.Helpers.Logger.Error("Connection_get_conn" + ex);
                throw;
            }

        }
        private static string get_inline_conn(string connect)
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();
                string path = System.Configuration.ConfigurationManager.AppSettings["ConnectionsXML"];

                FS_WS_WSCTFW.Helpers.FileEncryption.DecryptFile(path);
                xdoc.Load(path);

                FS_WS_WSCTFW.Helpers.FileEncryption.encryptFile(path);

                string query = string.Format("//*[@name='{0}']", connect);
                string output = "";
                output += xdoc.SelectSingleNode(query).ChildNodes[0].InnerText.ToString();
                output += xdoc.SelectSingleNode(query).ChildNodes[2].InnerText.ToString();
                output += xdoc.SelectSingleNode(query).ChildNodes[3].InnerText.ToString();
                output = output.Remove(0, 2);
                return output;
            }
            catch (Exception ex)
            {

                FS_WS_WSCTFW.Helpers.Logger.Error("get_inline_conn " + ex.Message);
                throw;
            }
        }
        private static string get_Single_node(string connect, string node)
        {
            try
            {
                XmlDocument xdoc = new XmlDocument();
                string path = System.Configuration.ConfigurationManager.AppSettings["ConnectionsXML"];

                FS_WS_WSCTFW.Helpers.FileEncryption.DecryptFile(path);
                xdoc.Load(path);

                FS_WS_WSCTFW.Helpers.FileEncryption.encryptFile(path);
                string query = string.Format("//*[@name='{0}']", connect);
                XmlNode node1 = xdoc.SelectSingleNode(query);
                if (node1.ChildNodes.Count > 0)
                {
                    return node1[node].InnerText.Remove(0, 3);
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