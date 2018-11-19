using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using Luu_DiplomaProject.Models;

namespace Luu_DiplomaProject.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public string SessionId { get; set; }
        public int Quantity { get; set; }
        //TOTAL PRICE OF HAMPER X QUANTITY
        public decimal TotalPrice { get; set; }
        public int HamperId { get; set; }
        //public Hamper Hamper { get; set; }
    }
}
