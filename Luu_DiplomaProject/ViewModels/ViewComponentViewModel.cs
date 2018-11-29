using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using Luu_DiplomaProject.Models;

namespace Luu_DiplomaProject.ViewModels
{
    public class ViewComponentViewModel
    {
        public IEnumerable<Cart> Carts { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Hamper> Hampers { get; set; }
        public int TotalItem { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
