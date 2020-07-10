using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FS_WS_WSCTFW.Models
{  [Table("WSRequests")]
    public class WSWebServicesModel
    {



        [Key]
        public int WSWebServiceRequestID { get; set; }
      
        [Required]
        public string RequestDetails { get; set; }
        public string ResponseDetails { get; set; }
        public int WSRequestSize { get; set; }
        //[ForeignKey("FKWSEnvRefID")]

        public int WSEnvID { get; set; }
        public WSEnvironmentsModel WSEnvironmentsModels { get; set; }
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


    }

        
    }