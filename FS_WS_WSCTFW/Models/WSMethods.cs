using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace FS_WS_WSCTFW.Models
{
    [Table("WSMethods")]
    public class WSMethods
    {

        [Key]
        public int WSMethodsID { get; set; }

        [Required]
        public string MethodName { get; set; }
        public string Binding { get; set; }
        public string BindingAddress { get; set; }
        public string SOAPAction { get; set; }
        public string MethodDetails { get; set; }

        //[ForeignKey("FKWSEnvRefID")]

        public int WSEnvID { get; set; }
        public WSEnvironmentsModel WSEnvironmentsModels { get; set; }



        public bool isActive { get; set; }
        public bool isDeleted { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }

        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [Timestamp]
        public Byte[] TimeStamp { get; set; }


        public static int AddMethodDetails(Models.WSMethods MethodDetails)
        {
            try
            {
                int MethodID = 0;

                

                FS_WS_WSCTFW.Models.ApplicationDbContext db = new Models.ApplicationDbContext();

                db.WSMethodsModels.Add(MethodDetails);
                int result = db.SaveChanges();

                if (result > 0)
                {

                    MethodID = MethodDetails.WSMethodsID;

                }
                else
                {
                    MethodID = -1;
                }


                return MethodID;
            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
                return -1;
            }


        }
    }
}