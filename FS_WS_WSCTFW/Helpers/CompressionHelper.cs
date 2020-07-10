using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ionic.Zip;
using System.IO;



namespace FS_WS_WSCTFW.Helpers
{
    public class CompressionHelper
    {

        public static string CompressZip(string filePath)
        {
            try
            {
                FileInfo fi = new FileInfo(filePath);
                string compresssedFilePath = "";

                ZipFile myZip = new ZipFile();
                myZip.AddFile(filePath);
                compresssedFilePath = Path.GetDirectoryName(filePath) + "\\"+ Path.GetFileNameWithoutExtension(filePath) + ".ZIP";
                myZip.Save(compresssedFilePath);


                return compresssedFilePath;


            }
            catch (Exception ex)
            {

                Logger.Error(ex);
                return null;
            }

        }
    }
}