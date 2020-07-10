using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace ClinicianAutomation.ExtraClasses
{
    public class AIMS_RunCases
    {
        public string run_case(string file_path,string email_id)
        {
            try
            {
                string notepad_directory = ConfigurationManager.AppSettings["aims_runcases_notepad_path"].ToString();
                string bat_directory = ConfigurationManager.AppSettings["aims_runcases_bat_path"].ToString();
                string email_text_path = ConfigurationManager.AppSettings["email_text_path"].ToString();
                using (StreamWriter text = new StreamWriter(notepad_directory))
                {
                    text.Write(file_path);
                }
                Thread.Sleep(1000);
                FS_WS_WSCTFW.Helpers.Logger.Info(file_path);
                using (StreamWriter text = new StreamWriter(email_text_path))
                {
                    text.Write(email_id);
                }
                Thread.Sleep(1000);
                FS_WS_WSCTFW.Helpers.Logger.Info(email_id);
                FS_WS_WSCTFW.Helpers.Logger.Info(bat_directory);
                using (var fs = File.OpenRead(bat_directory))
                using (var reader = new StreamReader(fs))
                {
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        FS_WS_WSCTFW.Helpers.Logger.Info(line);
                        //Process.Start(line).WaitForExit();

                        FS_WS_WSCTFW.Helpers.BatchFIleCaller.ExecuteBatchFile(bat_directory);
                    }
                }
                return "Text saved in file and file executed Successfull -- Scenario execution started";
            }
            catch (Exception ex)
            {
                FS_WS_WSCTFW.Helpers.Logger.Error(ex);
                return ex.Message;
            }
        }
    }
}