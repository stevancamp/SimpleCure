using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models.OrderModels
{
    public class PaidOrders_Models
    {
        public int OrderID { get; set; }
        public string Company { get; set; }
        public string Customer { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PaidDate { get; set; }
    }
}
