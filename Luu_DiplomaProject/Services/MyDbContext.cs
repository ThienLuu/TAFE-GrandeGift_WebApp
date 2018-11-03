﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Luu_DiplomaProject.Models;

namespace Luu_DiplomaProject.Services
{
    public class MyDbContext : IdentityDbContext
    {
        public DbSet<Customer> TblCustomer { get; set; }
        public DbSet<Hamper> TblHamper { get; set; }
        public DbSet<Category> TblCategory { get; set; }
        public DbSet<CustomerHamper> TblCustomerHamper { get; set; }
        public DbSet<Address> TblAddress { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            option.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB ; Database=GrandeGift; Trusted_Connection=True");
        }
    }
}
