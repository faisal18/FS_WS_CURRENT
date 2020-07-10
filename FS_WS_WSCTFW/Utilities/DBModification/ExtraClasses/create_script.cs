using System;
using System.IO;

namespace ClinicianAutomation.ExtraClasses
{
    public class create_script
    {
        public string script_create(string query,string file_path)
        {
            try
            {
                using (StreamWriter sw = File.CreateText(file_path + ".sql"))
                {
                    sw.WriteLine(query);
                }
                return "File created successfully in directory " + Environment.NewLine + "" + file_path + ".sql".ToString();
                
            }
            catch (Exception ex)
            {

                return ex.Message.ToString();
            }
        }
    }
}