using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using Luu_DiplomaProject.Models;

namespace Luu_DiplomaProject.ViewModels
{
    public class HomeShopViewModel
    {
        public int CategoryId { get; set; }
        public string Search { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public IEnumerable<Hamper> Hampers { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Item> Items { get; set; }
    }
}
