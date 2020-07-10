using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace FS_WS_WSCTFW.Models
{
    
        [Table("WSResponse")]
        public class WSResponse
        {



            [Key]
            public int WSResponseID { get; set; }

            [Required]
            public string RequestDetails { get; set; }
            public string ResponseDetails { get; set; }
            public int WSResponseHeaderStatusCode { get; set; }
            public int WSResponseTime { get; set; }
            public int WSResponseSize { get; set; }

        //[ForeignKey("FKWSEnvRefID")]

            public int WSRequestID { get; set; }
            public WSWebServicesModel WSRequestsModels { get; set; }
    




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