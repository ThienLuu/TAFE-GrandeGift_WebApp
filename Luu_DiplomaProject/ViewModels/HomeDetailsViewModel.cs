using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using Luu_DiplomaProject.Models;

namespace Luu_DiplomaProject.ViewModels
{
    public class HomeDetailsViewModel
    {
        public int HamperId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public bool Discontinued { get; set; }
        public int Quantity { get; set; }

        public string FileName { get; set; }

        public int CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}
