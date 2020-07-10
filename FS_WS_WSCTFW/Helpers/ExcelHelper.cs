using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
//using Microsoft.Office.Interop.Excel;




namespace FS_WS_WSCTFW.Helpers
{
    public class ExcelHelper
    {

    
        public static void getExcelFile(string filename, string sheetName)
        {
            try
            {
                

                //Create COM Objects. Create a COM object for everything that is referenced
                Excel.Application xlApp = new Excel.Application();
                //Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filename);
                Excel.Workbooks xlWorkbooks = xlApp.Workbooks;
                Excel.Workbook xlWorkbook = xlWorkbooks.Open(filename, 0, true, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);


                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[sheetName];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                //Workbook wb = wbs.Open(filePath, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);



                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                if (rowCount == 0 && colCount == 0)
                {
                    Logger.Info("Row and Columns not Found: Please check Data\r\n");

                }
                else
                {
                        string Output = "";
                    //iterate over the rows and columns and print to the console as it appears in the file
                    //excel is not zero based!!
                    for (int Rows = 1; Rows <= rowCount; Rows++)
                    {
                        for (int Cols = 1; Cols <= colCount; Cols++)
                        {
                            //new line
                            if (Cols == 1)
                                Output = Output + "\r\n"+sheetName+",";
                               // Logger.Info("\r\n");

                            //write the value to the console
                            if (xlRange.Cells[Rows, Cols] != null && xlRange.Cells[Rows, Cols].Value2 != null)
                                Output = Output + xlRange.Cells[Rows, Cols].Value2.ToString() + "," ;
                                
                        }
                        Output = Output  +DateTime.Now.ToString();
                    }
                    Logger.Info(Output);
                    xlWorkbook.Close();
                    xlWorkbooks.Close();

                    xlApp.Quit();
                    //cleanup
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    //rule of thumb for releasing com objects:
                    //  never use two dots, all COM objects must be referenced and released individually
                    //  ex: [somthing].[something].[something] is bad

                    //release com objects to fully kill excel process from running in the background
                    Marshal.ReleaseComObject(xlRange);
                    Marshal.ReleaseComObject(xlWorksheet);

                    //close and release
                 //   xlWorkbook.Close();
                    Marshal.ReleaseComObject(xlWorkbook);
                    Marshal.ReleaseComObject(xlWorkbooks);
                    //quit and release
                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlApp);
                    xlRange = null;
                    xlWorksheet = null;
                    xlWorkbook = null;
                    xlWorkbooks = null;
                    xlApp = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    //Process[] p =  Process.GetProcessesByName("Excel.exe");
                    // foreach (Process item in p)
                    // {
                    //     item.Kill();
                    // }


                }
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
            }
            finally
            {


                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();



            }
        }


        public static string getExcelDataCSV(string filename, string sheetName)
        {
            try
            {
                string Output = "";

                //Create COM Objects. Create a COM object for everything that is referenced
                Excel.Application xlApp = new Excel.Application();
                //Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(filename);
                Excel.Workbooks xlWorkbooks = xlApp.Workbooks;
                Excel.Workbook xlWorkbook = xlWorkbooks.Open(filename, 0, true, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);


                Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[sheetName];
                Excel.Range xlRange = xlWorksheet.UsedRange;

                //Workbook wb = wbs.Open(filePath, 0, false, 5, "", "", false, Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);



                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                if (rowCount == 0 && colCount == 0)
                {
                    Logger.Info("Row and Columns not Found: Please check Data\r\n");

                }
                else
                {
                   
                    //iterate over the rows and columns and print to the console as it appears in the file
                    //excel is not zero based!!
                    for (int Rows = 1; Rows <= rowCount; Rows++)
                    {
                        for (int Cols = 1; Cols <= colCount; Cols++)
                        {
                            //new line
                            if (Cols == 1)
                                Output = Output + "\r\n" + sheetName + ",";
                            // Logger.Info("\r\n");

                            //write the value to the console
                            if (xlRange.Cells[Rows, Cols] != null && xlRange.Cells[Rows, Cols].Value2 != null)
                                Output = Output + xlRange.Cells[Rows, Cols].Value2.ToString() + ",";

                        }
                        Output = Output + DateTime.Now.ToString();
                    }
                    Logger.Info(Output);
                    xlWorkbook.Close();
                    xlWorkbooks.Close();

                    xlApp.Quit();
                    //cleanup
                    GC.Collect();
                    GC.WaitForPendingFinalizers();

                    //rule of thumb for releasing com objects:
                    //  never use two dots, all COM objects must be referenced and released individually
                    //  ex: [somthing].[something].[something] is bad

                    //release com objects to fully kill excel process from running in the background
                    Marshal.ReleaseComObject(xlRange);
                    Marshal.ReleaseComObject(xlWorksheet);

                    //close and release
                    //   xlWorkbook.Close();
                    Marshal.ReleaseComObject(xlWorkbook);
                    Marshal.ReleaseComObject(xlWorkbooks);
                    //quit and release
                    xlApp.Quit();
                    Marshal.ReleaseComObject(xlApp);
                    xlRange = null;
                    xlWorksheet = null;
                    xlWorkbook = null;
                    xlWorkbooks = null;
                    xlApp = null;

                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    //Process[] p =  Process.GetProcessesByName("Excel.exe");
                    // foreach (Process item in p)
                    // {
                    //     item.Kill();
                    // }


                }
                return Output;
            }
            catch (Exception ex)
            {

                Logger.Error(ex);
                return null;
            }
            finally
            {


                //cleanup
                GC.Collect();
                GC.WaitForPendingFinalizers();



            }
        }
    }

}