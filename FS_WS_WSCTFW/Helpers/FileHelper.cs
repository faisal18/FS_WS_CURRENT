using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace FS_WS_WSCTFW.Helpers
{
    public class FileHelper
    {

        public static bool filecopy (string source, string destination)
        {
            try
            {

                File.Copy(source, destination);

                return true;




            }
            catch (Exception ex)
            {

                Logger.Error(ex);
                return false; 
            }
        }

    }
}