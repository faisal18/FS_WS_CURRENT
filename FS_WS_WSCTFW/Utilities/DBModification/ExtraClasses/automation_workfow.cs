using System;
using System.Configuration;

namespace ClinicianAutomation.ExtraClasses
{
    public class automation_workfow
    {

        public string run_batch(string process)
        {

            try
            {
                string path = ConfigurationManager.AppSettings["Saalam_path"].ToString();
                System.Diagnostics.Process.Start(path + process + ".bat");
                //return File.ReadAllText(path + process + ".sql");
                return "Request of " + process + " will be processed and mailed";
            }
            catch (Exception)
            {
                //return ex.Message.ToString();
                return "FAIL!!";
            }
        }

    }
}