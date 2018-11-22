using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using Luu_DiplomaProject.Models;

namespace Luu_DiplomaProject.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        //OR USERID?
        //public int UserId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int AddressId { get; set; }
        //ORDER TOTAL PRICE
        public decimal OrderPrice { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
