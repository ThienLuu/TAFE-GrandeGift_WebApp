using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using System.ComponentModel.DataAnnotations;

namespace Luu_DiplomaProject.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        [Required]
        public string UserId { get; set; } //FK from AspNetUsers table
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public DateTime DOB { get; set; }
        //public string Country { get; set; }
        //public ICollection<CustomerHamper> CustomerHampers { get; set; }
        public ICollection<Address> Addresses { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
