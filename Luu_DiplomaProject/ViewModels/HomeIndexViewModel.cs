using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using Luu_DiplomaProject.Models;

namespace Luu_DiplomaProject.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Hamper> Hampers { get; set; }
        public IEnumerable<Item> Items { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        //ORDERDETAIL or CART
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int HamperId { get; set; }
    }
}
