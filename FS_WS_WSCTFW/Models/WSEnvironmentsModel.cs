using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace FS_WS_WSCTFW.Models
{
    [Table("WSEnvironments")]
    public class WSEnvironmentsModel 
    {
        [Key]
        public int WSEnvID { get; set; }
        public string  WSURL { get; set; }
        [Required]
        public string WSEnvName { get; set; }
        public string WSEnvEndPoint { get; set; }
        [Required]
        public string WSEnvWSDL { get; set; }
        public string WSUsername { get; set; }
        public string WSPasswd { get; set; }
        public bool isPublic { get; set; }


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
