using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Luu_DiplomaProject.Models;

namespace Luu_DiplomaProject.Services
{
    public class SeedHelper
    {
        public static async Task Seed(IServiceProvider provider)
        {
            //set up the scope of our services that used 
            //our DI container
            var scopeFactory = provider.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                IDataService<Category> categoryService = scope.ServiceProvider.GetRequiredService<IDataService<Category>>();
                IDataService<Hamper> hamperService = scope.ServiceProvider.GetRequiredService<IDataService<Hamper>>();
                IDataService<Item> itemService = scope.ServiceProvider.GetRequiredService<IDataService<Item>>();

                #region Admin/Customer
                //......sample data......
                //add Admin role
                if (await roleManager.FindByNameAsync("Admin") == null)
                {
                    await roleManager.CreateAsync(new IdentityRole("Admin"));
                }
                //add default admin
                if (await userManager.FindByNameAsync("admin1") == null)
                {
                    IdentityUser admin = new IdentityUser("grandeAdmin");
                    admin.Email = "admin1@yahoo.com";
                    await userManager.CreateAsync(admin, "Asdf#1234");//add user to Users tabel
                    await userManager.AddToRoleAsync(admin, "Admin"); //add admin1 to role Admin
                }
                ////add Customer role
                //if (await roleManager.FindByNameAsync("Customer") == null)
                //{
                //    await roleManager.CreateAsync(new IdentityRole("Customer"));
                //}
                ////add default customer
                //if (await userManager.FindByNameAsync("customer1") == null)
                //{
                //    IdentityUser cust = new IdentityUser("customer1");
                //    cust.Email = "customer1@yahoo.com";
                //    await userManager.CreateAsync(cust, "Apple#333");//add user to Users tabel
                //    await userManager.AddToRoleAsync(cust, "Customer"); //add customer1 to role Customer
                //}
                #endregion

                #region Hampers
                int catCount = categoryService.GetAll().Count();
                int hamCount = hamperService.GetAll().Count();
                int iteCount = itemService.GetAll().Count();

                if (catCount == 0 && iteCount == 0 && hamCount == 0)
                {
                    #region Baby Hamper

                        //CATEGORY
                        Category category = new Category
                        {
                            Name = "Baby",
                            Description = ""
                        };

                        categoryService.Create(category);
                        
                        //HAMPER
                        Hamper hamper = new Hamper
                        {
                            Name = "Newborn Baby Hamper",
                            CategoryId = 1,
                            Price = 75,
                            Discontinued = false
                        };

                        hamperService.Create(hamper);
                        
                        //ITEMS
                        Item item1 = new Item
                        {
                            HamperId = 1,
                            Name = "Unscented baby wipes",
                            Size = "480 pack"
                        };

                        Item item2 = new Item
                        {
                            HamperId = 1,
                            Name = "Scented nappy bags",
                            Size = "200 pack"
                        };

                        Item item3 = new Item
                        {
                            HamperId = 1,
                            Name = "Ultra dry jumbo nappies for newborn",
                            Size = "100 pack"
                        };

                        Item item4 = new Item
                        {
                            HamperId = 1,
                            Name = "Baby formula",
                            Size = "800 gram"
                        };

                        Item item5 = new Item
                        {
                            HamperId = 1,
                            Name = "Baby gentle wash & shampoo",
                            Size = "500 ml"
                        };

                        Item item6 = new Item
                        {
                            HamperId = 1,
                            Name = "Feeding bottle twin pack",
                            Size = "260 ml"
                        };

                        Item item7 = new Item
                        {
                            HamperId = 1,
                            Name = "Anti-rash baby powder",
                            Size = "100 g"
                        };

                        itemService.Create(item1);
                        itemService.Create(item2);
                        itemService.Create(item3);
                        itemService.Create(item4);
                        itemService.Create(item5);
                        itemService.Create(item6);
                        itemService.Create(item7);

                    #endregion

                    #region Christmas Hamper

                    //CATEGORY
                    Category category2 = new Category
                    {
                        Name = "Christmas",
                        Description = ""
                    };

                    categoryService.Create(category2);

                    //HAMPER
                    Hamper hamper2 = new Hamper
                    {
                        Name = "Christmas Dinner",
                        CategoryId = 2,
                        Price = 100,
                        Discontinued = false
                    };

                    hamperService.Create(hamper2);

                    //ITEMS
                    Item item8 = new Item
                    {
                        HamperId = 2,
                        Name = "Multi-purpose Bag with Handles",
                        Size = ""
                    };

                    Item item9 = new Item
                    {
                        HamperId = 2,
                        Name = "BBQ Tools in Metal Carry Case",
                        Size = "3"
                    };

                    Item item10 = new Item
                    {
                        HamperId = 2,
                        Name = "Asahi Beer 330ml ",
                        Size = "4"
                    };

                    Item item11 = new Item
                    {
                        HamperId = 2,
                        Name = "Tarralini Cheese and Black Pepper Snack",
                        Size = "230 g"
                    };

                    Item item12 = new Item
                    {
                        HamperId = 2,
                        Name = "Indulgence Milk Chocolate Biscuits",
                        Size = "100 g"
                    };

                    Item item13 = new Item
                    {
                        HamperId = 2,
                        Name = "Basil + Barrow Roasted Salted Peanuts",
                        Size = "100 g"
                    };

                    Item item14 = new Item
                    {
                        HamperId = 2,
                        Name = "Basil + Barrow Salted Pretzels",
                        Size = "80 g"
                    };

                    itemService.Create(item8);
                    itemService.Create(item9);
                    itemService.Create(item10);
                    itemService.Create(item11);
                    itemService.Create(item12);
                    itemService.Create(item13);
                    itemService.Create(item14);

                    #endregion

                    #region Cooking Hamper

                    //CATEGORY
                    Category category3 = new Category
                    {
                        Name = "Cooking",
                        Description = ""
                    };

                    categoryService.Create(category3);

                    //HAMPER
                    Hamper hamper3 = new Hamper
                    {
                        Name = "Rookie Chef Hamper",
                        CategoryId = 3,
                        Price = 80,
                        Discontinued = false
                    };

                    hamperService.Create(hamper3);

                    //ITEMS
                    Item item15 = new Item
                    {
                        HamperId = 3,
                        Name = "Shopper Bag",
                        Size = "480 pack"
                    };

                    Item item16 = new Item
                    {
                        HamperId = 3,
                        Name = "Lyrebird Shiraz",
                        Size = " 750 ml "
                    };

                    Item item17 = new Item
                    {
                        HamperId = 3,
                        Name = "Salt & River Tuscan Herb Wafer Crackers",
                        Size = "100 g"
                    };

                    Item item18 = new Item
                    {
                        HamperId = 3,
                        Name = "Sugar Street Snakes",
                        Size = "100 g"
                    };

                    Item item19 = new Item
                    {
                        HamperId = 3,
                        Name = "Basil + Barrow Salted Pretzels",
                        Size = "80 g"
                    };

                    Item item20 = new Item
                    {
                        HamperId = 3,
                        Name = "The Kernel Factory Butter Salt Popcorn",
                        Size = "40 g"
                    };

                    Item item21 = new Item
                    {
                        HamperId = 3,
                        Name = "Tarallino Italian Sesame Grissini Snack",
                        Size = "35 g"
                    };

                    itemService.Create(item15);
                    itemService.Create(item16);
                    itemService.Create(item17);
                    itemService.Create(item18);
                    itemService.Create(item19);
                    itemService.Create(item20);
                    itemService.Create(item21);

                    #endregion
                }

                #endregion
            }
        }
    }
}
