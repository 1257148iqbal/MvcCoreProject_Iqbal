using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProject_Iqbal.Models
{
    public class OrderMaster
    {
        [Key]
        public int Id { get; set; }
        public string CustomerName { get; set; }

        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        public List<OrderDetails> OrderDetails { get; set; }
    }
}
