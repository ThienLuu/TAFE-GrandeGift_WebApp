using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using Luu_DiplomaProject.Models;

namespace Luu_DiplomaProject.ViewModels
{
    public class OrderCreateViewModel
    {
        public IEnumerable<Hamper> Hampers { get; set; }
        //public IEnumerable<OrderDetail> OrderDetails { get; set; }
        public IEnumerable<Cart> Carts { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int AddressId { get; set; }
        public decimal OrderPrice { get; set; }
        //public string Email { get; set; }
        //ORDER DETAILS?
    }
}
