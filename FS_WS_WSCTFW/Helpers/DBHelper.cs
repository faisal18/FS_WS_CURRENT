using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FS_WS_WSCTFW.Helpers
{
    public class DBHelper
    {

        public static int SaveRecord(object Modelname, Models.ApplicationDbContext db)
        {


            Models.WSWebServicesModel WSCaller = new Models.WSWebServicesModel();

           

            db.WSWebServicesModels.Add(WSCaller);
            int result = db.SaveChanges();
            if (result > 0)
            {
                //    Response.Write("<script>alert('Response Received Successfully');</script>");        

            }
            else
            {
                //     Response.Write("<script>alert('Error Occured Please try again.');</script>");
            }


            return result;
        }



        public static void ciperChecker (string pwd, string key)
        {



        }




    }
}