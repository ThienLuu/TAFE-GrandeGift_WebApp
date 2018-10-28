using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Luu_DiplomaProject.Models
{
    public class CustomerHamper
    {
        public int CustomerHamperId { get; set; }
        public int HamperId { get; set; }
        public int CustomerId { get; set; }
    }
}
