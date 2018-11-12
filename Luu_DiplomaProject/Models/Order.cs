using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luu_DiplomaProject.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        //public int MyProperty { get; set; } shopping cart id???
        public DateTime OrderDateTime { get; set; }
        //public int AddressId { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public decimal OrderPrice { get; set; }
    }
}
