using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Luu_DiplomaProject.Models
{
    public class Hamper
    {
        public int HamperId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Picture { get; set; }     //Datatype for image????
        public string Details { get; set; }

        public int CategoryId { get; set; }
        public ICollection<CustomerHamper> CustomerHampers { get; set; }
    }
}
