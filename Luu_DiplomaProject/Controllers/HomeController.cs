using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//..
using Luu_DiplomaProject.ViewModels;
using Luu_DiplomaProject.Models;
using Luu_DiplomaProject.Services;
using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;

namespace Luu_DiplomaProject.Controllers
{
    public class HomeController : Controller
    {
        private IDataService<Category> _categoryService;
        private IDataService<Hamper> _hamperService;
        //..
        //private UserManager<IdentityUser> _userManagerService;
        //private SignInManager<IdentityUser> _signInManagerService;
        //private IDataService<Customer> _customerService;
        //private IDataService<Address> _addressService;

        public HomeController(IDataService<Category> categoryService,
                                IDataService<Hamper> hamperService
                                //..
                                //,UserManager<IdentityUser> userManager,
                                //SignInManager<IdentityUser> signinManger,
                                //IDataService<Customer> customerService,
                                //IDataService<Address> addressService
                                )
        {
            _categoryService = categoryService;
            _hamperService = hamperService;
            //..
            //_userManagerService = userManager;
            //_signInManagerService = signinManger;
            //_customerService = customerService;
            //_addressService = addressService;
        }
        public IActionResult Index()
        {
            //string id = _userManagerService.GetUserId(User);
            //Customer customer = _customerService.GetSingle(c => c.UserId == id);
            //IEnumerable<Address> list = _addressService.GetAll().Where(a => a.CustomerId == customer.CustomerId);

            //if (customer != null)
            //{
            //    AccountUpdateViewModel vm = new AccountUpdateViewModel
            //    {
            //        FirstName = customer.FirstName,
            //        LastName = customer.LastName,
            //        DOB = customer.DOB,
            //        Addresses = list,
            //        CustomerId = customer.CustomerId
            //    };

            //    return View(vm);
            //}
            //else
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        #region Attempt 1

        [HttpGet]
        public IActionResult Shop(int categoryId, string search, decimal min, decimal max)
        {
            IEnumerable<Category> catList = _categoryService.GetAll();
            IEnumerable<Hamper> hamList = _hamperService.GetAll();

            IEnumerable<Hamper> hamSearch = _hamperService.Query(h => h.Name == search);
            IEnumerable<Hamper> hamMinMax = _hamperService.Query(h => h.Price >= min && h.Price <= max);
            IEnumerable<Hamper> hamCat = _hamperService.Query(h => h.CategoryId == categoryId);

            if (min < max && max != 0)
            {
                HomeShopViewModel vm = new HomeShopViewModel
                {
                    Hampers = hamMinMax,
                    Categories = catList
                };
                return View(vm);
            }

            if (categoryId != 0)
            {
                HomeShopViewModel vm = new HomeShopViewModel
                {
                    Hampers = hamCat,
                    Categories = catList
                };
                return View(vm);
            }

            //else if (search != null)
            //{
            //    HomeShopViewModel vm = new HomeShopViewModel
            //    {
            //        Hampers = hamSearch,
            //        Categories = catList
            //    };
            //    return View(vm);
            //}

            else
            {
                HomeShopViewModel vm = new HomeShopViewModel
                {
                    Hampers = hamList,
                    Categories = catList
                };
                return View(vm);
            }

            //HomeShopViewModel vm = new HomeShopViewModel
            //{
            //    Hampers = hamList,
            //    Categories = catList
            //};
            //return View(vm);
        }

        #endregion

        [HttpGet("images/{fileName}")]
        public IActionResult Read(string fileName)
        {
            Hamper hamper = _hamperService.GetSingle(h => h.FileName == fileName);

            if (hamper == null)
            {
                return NotFound();
            }

            return File(hamper.FileContent, hamper.ContentType);
        }
        #region Attempt 2

        //[HttpPost]
        //public IActionResult Shop(int CatId, /*string search,*/ decimal min, decimal max)
        //{
        //    IEnumerable<Category> catList = _categoryService.GetAll();
        //    IEnumerable<Hamper> hamList = _hamperService.GetAll();

        //    //IEnumerable<Hamper> hamSearch = _hamperService.Query(h => h.Name == search);
        //    IEnumerable<Hamper> hamCat = _hamperService.Query(h => h.CategoryId == CatId);
        //    IEnumerable<Hamper> hamMinMax = _hamperService.Query(h => h.Price >= min && h.Price <= max);

        //    if (CatId > 0)
        //    {
        //        HomeShopViewModel vm = new HomeShopViewModel
        //        {
        //            Hampers = hamCat,
        //            Categories = catList
        //        };
        //        return View(vm);
        //    }
        //    else if (min < max)
        //    {
        //        HomeShopViewModel vm = new HomeShopViewModel
        //        {
        //            Hampers = hamMinMax,
        //            Categories = catList
        //        };
        //        return View(vm);
        //    }
        //    else
        //    {
        //        HomeShopViewModel vm = new HomeShopViewModel
        //        {
        //            Hampers = hamList,
        //            Categories = catList
        //        };
        //        return View(vm);
        //    }
        //}

        //[HttpGet]
        //public IActionResult Shop()
        //{
        //    return RedirectToAction("Shop", "Home", new {  });
        //}
        #endregion
    }
}