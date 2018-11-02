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

namespace Luu_DiplomaProject.Controllers
{
    public class HomeController : Controller
    {
        private IDataService<Category> _categoryService;
        private IDataService<Hamper> _hamperService;

        public HomeController(IDataService<Category> categoryService,
                                    IDataService<Hamper> hamperService)
        {
            _categoryService = categoryService;
            _hamperService = hamperService;
        }
        public IActionResult Index()
        {
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

        [HttpGet]
        public IActionResult Shop()
        {
            IEnumerable<Hamper> hamList = _hamperService.GetAll();
            IEnumerable<Category> catList = _categoryService.GetAll();

            HomeShopViewModel vm = new HomeShopViewModel
            {
                Hampers = hamList,
                Categories = catList
            };
            return View(vm);
        }

        //[HttpPost]
        //public IActionResult Shop(string search)
        //{
        //    return View();
        //}
    }
}