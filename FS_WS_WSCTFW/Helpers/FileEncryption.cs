using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace FS_WS_WSCTFW.Helpers
{
    public class FileEncryption
    {

        public static void encryptFile(string filepath)
        {
            try
            {
                File.Encrypt(filepath);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }


        public static void DecryptFile(string filepath)
        {



            try
            {
                File.Decrypt(filepath);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

        }

    }
}