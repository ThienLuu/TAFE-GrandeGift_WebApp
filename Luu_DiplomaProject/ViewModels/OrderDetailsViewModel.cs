using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using Luu_DiplomaProject.Models;

namespace Luu_DiplomaProject.ViewModels
{
    public class OrderDetailsViewModel
    {
        public IEnumerable<Hamper> Hampers { get; set; }
        public IEnumerable<OrderDetail> OrderDetails { get; set; }
    }
}
