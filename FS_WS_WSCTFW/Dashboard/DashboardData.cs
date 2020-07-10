using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
namespace FS_WS_WSCTFW.Dashboard
{
    public class DashboardData
    {

        public static void DashboardProcess()
        {

            string MonitoringData = System.Configuration.ConfigurationManager.AppSettings["MonitoringData"];
            string MonitoringApplcationWorkflow = System.Configuration.ConfigurationManager.AppSettings["MonitoringApplcationWorkflow"];
            string DashboardData = System.Configuration.ConfigurationManager.AppSettings["DashboardData"];


            // Helpers.ExcelHelper.getExcelFile(MonitoringApplcationWorkflow, "Health Monitoring");
            // GC.Collect();
            // Helpers.ExcelHelper.getExcelFile(MonitoringData, "Webservice Monitoring");
            // GC.Collect();

            //Helpers.ExcelHelper.getExcelFile(MonitoringData, "PendingCount");
            // //Helpers.ExcelHelper.getExcelFile();
            // GC.Collect();


            string ExcelData = Helpers.ExcelHelper.getExcelDataCSV(MonitoringApplcationWorkflow, "Health Monitoring");
            GC.Collect();
            ExcelData = ExcelData + "\r\n" + Helpers.ExcelHelper.getExcelDataCSV(MonitoringData, "Webservice Monitoring");
            GC.Collect();

            ExcelData = ExcelData + "\r\n" + Helpers.ExcelHelper.getExcelDataCSV(MonitoringData, "PendingCount");
            //Helpers.ExcelHelper.getExcelFile();
            GC.Collect();

            Helpers.BatchFIleCaller.SaveAnyFile(Path.GetFileNameWithoutExtension(DashboardData), ExcelData, Path.GetDirectoryName(DashboardData) + "\\", true, Path.GetExtension(DashboardData));

        }

    }
}