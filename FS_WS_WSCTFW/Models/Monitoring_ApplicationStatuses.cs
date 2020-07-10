using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FS_WS_WSCTFW.Models
{
    [Table("Monitoring_ApplicationStatuses")]
    public class Monitoring_ApplicationStatuses
    {


        [Key]
        public int Monitoring_ApplicationStatuses_ID { get; set; }

        [Required]
        public string ApplicationName { get; set; }
       
        public string Status { get; set; }
        public bool isUI { get; set; }
        public DateTime? CheckingTime { get; set; }


        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }


        [Timestamp]
        public Byte[] TimeStamp { get; set; }

    }
}