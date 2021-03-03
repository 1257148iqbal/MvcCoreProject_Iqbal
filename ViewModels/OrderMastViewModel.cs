using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreProject_Iqbal.ViewModels
{
    public class OrderMastViewModel
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime OrderDate { get; set; }

        public List<OrderDetlViewModel> OrderDetlViewModel { get; set; }
    }
}
