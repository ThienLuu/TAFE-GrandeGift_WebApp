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
        public bool Discontinued { get; set; }

        //Picture/Image
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public long ContentSize { get; set; }

        public int CategoryId { get; set; }
        //public ICollection<CustomerHamper> CustomerHampers { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
