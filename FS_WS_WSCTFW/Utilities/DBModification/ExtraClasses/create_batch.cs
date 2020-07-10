using System;
using System.IO;
using System.Configuration;

namespace ClinicianAutomation.ExtraClasses
{
    public class create_batch
    {
        sqlcmd_createfile sql = new sqlcmd_createfile();

        public string batch_create(string file_path, string connection)
        {
            try
            {
                FS_WS_WSCTFW.Helpers.Logger.Info(file_path);
                FS_WS_WSCTFW.Helpers.Logger.Info(connection);

                return sql.sqlcmd(file_path.ToString(), connection.ToString());
            }
            catch (Exception ex)
            {
                FS_WS_WSCTFW.Helpers.Logger.Error(ex);
                return ex.Message.ToString();
            }
        }
        public string batch_create(string file_path)
        {
            try
            {
                FS_WS_WSCTFW.Helpers.Logger.Info(file_path);
             
                return sql.sqlcmd(file_path);
            }
            catch(Exception ex)
            {
                FS_WS_WSCTFW.Helpers.Logger.Error(ex);
                return ex.Message.ToString();
            }
        }


        public string batch_create_Indication(string file_path, string connection)
        {
            try
            {
                FS_WS_WSCTFW.Helpers.Logger.Info(file_path);
                FS_WS_WSCTFW.Helpers.Logger.Info(connection);
                string sqlcmd = sql.sqlcmdIndication(file_path.ToString(), connection.ToString());
                FS_WS_WSCTFW.Helpers.Logger.Info(sqlcmd);
                return sqlcmd;
            }
            catch (Exception ex)
            {
                FS_WS_WSCTFW.Helpers.Logger.Error(ex);
                return ex.Message.ToString();
            }
        }

        public string run_batch(string process)
        {
            try
            {
                string path = ConfigurationManager.AppSettings["Workflow_Automation"].ToString();
                string filename = path + process + ".bat";
                string output = FS_WS_WSCTFW.Helpers.BatchFIleCaller.ExecuteBatchFile(filename);
                if ( output != null)
                //System.Diagnostics.Process.Start(path + process + ".bat");
                {
                    FS_WS_WSCTFW.Helpers.BatchFIleCaller.InsertMLog(process, "", filename, output, "Passed", "");
                    return File.ReadAllText(path + process + ".txt");
                }
                else

                {
                    FS_WS_WSCTFW.Helpers.BatchFIleCaller.InsertMLog(process, "", filename, output, "Failed", "");
                    return null;
                }
            }
            catch (Exception)
            {
                //return ex.Message.ToString();
                return "FAIL!!";
            }
        }
    }
}