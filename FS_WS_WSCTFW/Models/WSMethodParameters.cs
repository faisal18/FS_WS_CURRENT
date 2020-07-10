using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FS_WS_WSCTFW.Models
{[Table("WSMethodParameters")]
    public class WSMethodParameters
    {
        
       
            [Key]
            public int WSMethodParametersID { get; set; }

            [Required]
            public string ParameterName { get; set; }
            public string ParameterDataType { get; set; }

            public string ParameterType { get; set; }
            public string ParameterDefaultValue { get; set; }
            public string ParameterDescrition { get; set; }

            //[ForeignKey("FKWSEnvRefID")]

            public int WSEnvID { get; set; }
            public WSEnvironmentsModel WSEnvironmentsModels { get; set; }
            // FOriegn Key for Methods
            public int WSMethodID { get; set; }
            public WSMethods WSMethodsModels { get; set; }

            public bool isActive { get; set; }
            public bool isDeleted { get; set; }
            public string CreatedBy { get; set; }
            public string UpdatedBy { get; set; }

            public DateTime? CreatedDate { get; set; }
            public DateTime? UpdatedDate { get; set; }

            [Timestamp]
            public Byte[] TimeStamp { get; set; }


        public static int AddMethodParameters(Models.WSMethodParameters MethodParameterDetails)
        {
            try
            {
                int MethodParameterID = 0;

                FS_WS_WSCTFW.Models.ApplicationDbContext db = new Models.ApplicationDbContext();

                db.WSMethodParametersModels.Add(MethodParameterDetails);
                int result = db.SaveChanges();

                if (result > 0)
                {

                    MethodParameterID = MethodParameterDetails.WSMethodParametersID;

                }
                else
                {
                    MethodParameterID = -1;
                }


                return MethodParameterID;
            }
            catch (Exception ex)
            {

                Helpers.Logger.Error(ex);
                return -1;
            }


        }




    }
}