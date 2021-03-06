﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using System.ComponentModel.DataAnnotations;
using Luu_DiplomaProject.Models;

namespace Luu_DiplomaProject.ViewModels
{
    public class HamperCreateViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public bool Discontinued { get; set; }

        public int CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
