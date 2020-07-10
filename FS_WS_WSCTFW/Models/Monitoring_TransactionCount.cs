using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FS_WS_WSCTFW.Models
{
    [Table("Monitoring_TransactionCount")]

    public class Monitoring_TransactionCount
    {

        [Key]
        public int Monitoring_TransactionCount_ID { get; set; }

        [Required]
        public string ApplicationName { get; set; }
        public DateTime? CheckingTime { get; set; }
        public int Count { get; set; }


        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }


        [Timestamp]
        public Byte[] TimeStamp { get; set; }

    }
}