using System;
using System.IO;
using System.Configuration;

namespace ClinicianAutomation.ExtraClasses
{
    public class sqlcmd_createfile
    {

        string output_file = ConfigurationManager.AppSettings["sqlcmd_output"].ToString() + "techsupport_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_output.csv";

        public string sqlcmd(string path_file,string connection)
        {
            Connections conn = new Connections();
            string argument = String.Format("SQLCMD {0} -i \"{1}\" -o \"{2}\" -W -s,-h-1", conn.run_connection(connection), path_file + ".sql", output_file);
            FS_WS_WSCTFW.Helpers.Logger.Info(argument);
            try
            {
                using (StreamWriter sw = File.CreateText(path_file + ".bat"))
                {
                    sw.WriteLine(argument);
                }
                return path_file + ".bat";
            }
            catch (Exception ex)
            {
                //return ex.Message.ToString();
                FS_WS_WSCTFW.Helpers.Logger.Error(ex);
                return null;
            }

        }



        public string sqlcmdIndication(string path_file, string connection)
        {
            FS_WS_WSCTFW.Helpers.Logger.Info(" sqlcmdindication method called  "  );

            Connections conn = new Connections();
            string argument = String.Format("SQLCMD {0} -i \"{1}\"  -W -s,-h-1", conn.run_connection(connection), path_file + ".sql", output_file);
            FS_WS_WSCTFW.Helpers.Logger.Info(argument);
            try
            {
                using (StreamWriter sw = File.CreateText(path_file + ".bat"))
                {
                    sw.WriteLine(argument);
                }
                return path_file + ".bat";
            }
            catch (Exception ex)
            {
                FS_WS_WSCTFW.Helpers.Logger.Error(ex);
                //return ex.Message.ToString();
                return null;
            }

        }

        //Special case for get credentials
        public string  sqlcmd(string path_file)
        {
            string argument = String.Format("SQLCMD -i \"{0}\" -o \"{1}\" -W -s,-h-1", path_file + ".sql", output_file);
            try
            {
                using (StreamWriter sw = File.CreateText(path_file + ".bat"))
                {
                    sw.WriteLine(argument);
                }
                return path_file + ".bat";
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}