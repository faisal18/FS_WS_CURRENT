using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.VisualBasic.FileIO;
using System.Collections;

namespace FS_WS_WSCTFW.Helpers
{

    public class ICDs
    {
        public string ICD9 { get; set; }
        public string ICD10 { get; set; }

    }
    public class CSVHelper
    {


        public static Dictionary<int, ICDs> getCSVinMemory()
        {
            try
            {
                System.Collections.Generic.Dictionary<int, ICDs> ICDDict = new Dictionary<int, ICDs>();

                string path = System.Configuration.ConfigurationManager.AppSettings["ICD910Mapping"];

               // var path = @"C:\tmp\ICD9-10_mapping.csv";
                using (TextFieldParser csvParser = new TextFieldParser(path))
                {
                    csvParser.CommentTokens = new string[] { "#" };
                    csvParser.SetDelimiters(new string[] { "," });
                    csvParser.HasFieldsEnclosedInQuotes = true;

                    // Skip the row with the column names
                    csvParser.ReadLine();

                    while (!csvParser.EndOfData)
                    {
                        // Read current line fields, pointer moves to the next line.
                        string[] fields = csvParser.ReadFields();

                        ICDDict.Add(int.Parse(fields[0]), new ICDs { ICD10 = fields[1], ICD9 = fields[2] });

                    }
                }

                return ICDDict;
            }
            catch (Exception ex)
            {
                Logger.Error("CSV parser error", ex);

                return null;
            }




        }


        public static ArrayList ConvertICD(Dictionary<int, ICDs> ICDDict,string ICDtoFind, bool isICD10)
        {
            try
            {
                ArrayList icdList = new ArrayList();

                if (isICD10)
                {
                    ICDs icd = new ICDs();
                    icd.ICD10 = ICDtoFind;
                    foreach (var item in ICDDict)
                    {

                        ICDs IIcd = item.Value;

                        if (IIcd.ICD10 == icd.ICD10)
                        {
                            icdList.Add(IIcd.ICD9);
                        }




                    }


                }
                else
                {
                    ICDs icd = new ICDs();
                    icd.ICD9 = ICDtoFind;
                    foreach (var item in ICDDict)
                    {
                       
                            ICDs IIcd = item.Value;

                        if (IIcd.ICD9 == icd.ICD9)
                        {
                            icdList.Add(IIcd.ICD10);
                        }
                       



                    }

                }
                
                
            
                return icdList;
            }
            catch (Exception ex)
            {
                Logger.Error("CSV parser error", ex);

                return null;
            }
        }




    }
}