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
        public string City { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string StreetAddress { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderDateTime { get; set; }
        //PAYMENT DETAILS?
    }
}
