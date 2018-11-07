using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using System.ComponentModel.DataAnnotations;

namespace Luu_DiplomaProject.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Postcode { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        public bool Favourite { get; set; }
        public int CustomerId { get; set; }
    }
}
