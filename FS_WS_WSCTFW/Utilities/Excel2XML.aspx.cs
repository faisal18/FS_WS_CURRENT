using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Xml;
using System.Text;

namespace FS_WS_WSCTFW.Utilities
{
    public partial class Excel2XML : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/Login.aspx");
            }
        }

        protected void btnLoadExcel_Click(object sender, EventArgs e)
        
            {
                string day, day1 = "";
                string connStr = "";
                int i = 0;
                int oItem = 0;

                if (file1.HasFile)
                {
                    string filename = System.IO.Path.GetFileName(file1.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(file1.PostedFile.FileName);
                //string filelocation = "C:/Users/VKumar/Desktop/" + filename;

                string filelocation = file1.PostedFile.FileName;
                  if (fileExtension == ".xls" || fileExtension == ".XLS") 
                    {
                        connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";

                    }
                    else if (fileExtension == ".xlsx" || fileExtension == ".XLSX")
                    {
                        connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                    }

                else 
                {
                    connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                    //connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filelocation + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";

                }

                OleDbConnection conn = new OleDbConnection(connStr);
                    OleDbCommand cmd = new OleDbCommand();
                    cmd.Connection = conn;
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    conn.Open();
                    DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                    string sheetName = dtSheet.Rows[0]["table_name"].ToString();
                    cmd.CommandText = "select * from [" + sheetName + "]";
                    da.SelectCommand = cmd;
                    da.Fill(dt);
                    conn.Close();
                    grdExcel.DataSource = dt;
                    grdExcel.DataBind();

                    DataSet ds = new DataSet();
                    ds.Tables.Add(dt);
                    oItem = dt.Rows.Count;
                    oItem -= 1;


                    XmlDocument doc = new XmlDocument();
                    XmlDeclaration declaire = doc.CreateXmlDeclaration("1.0", "utf-8", null);
                    // -----------------------create root-----------------------------  
                    XmlElement rootnode = doc.CreateElement("root");
                    doc.InsertBefore(declaire, doc.DocumentElement);
                    doc.AppendChild(rootnode);

                    while (i < oItem)
                    {

                        day = dt.Rows[i].ItemArray[0].ToString();

                        if (day != day1)
                        {
                            day1 = day;
                            DateTime d = Convert.ToDateTime(dt.Rows[i].ItemArray[4]);

                            string str = Convert.ToDateTime(d.ToString("dd-MMMM-yyyy")).ToString("MMMM-dd-yyyy");

                            string finaldate = str.Replace(d.Year.ToString(), DateTime.Now.ToString("yyyy"));

                            XmlElement dobEle = doc.CreateElement("DOB");

                            dobEle.SetAttribute("date", finaldate);


                            do
                            {
                                XmlElement emp = doc.CreateElement("EmpDetails");
                                XmlElement name = doc.CreateElement("Name");
                                XmlElement desig = doc.CreateElement("Designation");
                                XmlElement dept = doc.CreateElement("Dept");
                                XmlElement loc = doc.CreateElement("Location");

                                name.InnerText = dt.Rows[i].ItemArray[1].ToString();
                                desig.InnerText = dt.Rows[i].ItemArray[2].ToString();
                                dept.InnerText = desig.InnerText;
                                loc.InnerText = dt.Rows[i].ItemArray[3].ToString();

                                emp.AppendChild(name);
                                emp.AppendChild(desig);
                                emp.AppendChild(dept);
                                emp.AppendChild(loc);

                                dobEle.AppendChild(emp);
                                i++;
                            } while (day1 == dt.Rows[i].ItemArray[0].ToString() && i < oItem);


                            doc.DocumentElement.AppendChild(dobEle);

                        }
                    }
                    doc.Save("C:/tmp/Output.xml");
                    Response.Write("Created");
                }
            }
    }
}



//using System;  
//using System.Collections.Generic;  
//using System.Linq;  
//using System.Web;  
//using System.Web.UI;  
//using System.Web.UI.WebControls;  
//using System.IO;  
//using System.Data.OleDb;  
//using System.Data;  
//using System.Xml;  
//using System.Text;  
  
//namespace ExcelXml
//{
//    public partial class _Default : System.Web.UI.Page
//    {
//        protected void Page_Load(object sender, EventArgs e)
//        {

//        }

//        protected void btncnvrt_Click(object sender, EventArgs e)

//        }
//    }
//}