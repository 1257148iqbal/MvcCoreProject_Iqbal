using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProject_Iqbal.Models
{
    public class OrderDetails
    {
        [Key]
        public int Id { get; set; }
        public int MastId { get; set; }
        public string ProductName { get; set; }
        public int Qty { get; set; }
        public decimal Rate { get; set; }

        [ForeignKey("MastId")]
        public virtual OrderMaster OrderMaster { get; set; }
    }
}
