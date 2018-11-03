﻿using System;
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
        public IActionResult Shop(int id, string search, decimal min, decimal max)
        {
            IEnumerable<Category> catList = _categoryService.GetAll();
            IEnumerable<Hamper> hamList = _hamperService.GetAll();

            IEnumerable<Hamper> hamSearch = _hamperService.Query(h => h.Name == search);
            IEnumerable<Hamper> hamMinMax = _hamperService.Query(h => h.Price >= min && h.Price <= max);
            IEnumerable<Hamper> hamCat = _hamperService.Query(h => h.CategoryId == id);

            if (min < max || max != 0)
            {
                HomeShopViewModel vm = new HomeShopViewModel
                {
                    Hampers = hamMinMax,
                    Categories = catList
                };
                return View(vm);
            }

            //else if (id != 0)
            //{
            //    HomeShopViewModel vm = new HomeShopViewModel
            //    {
            //        Hampers = hamCat,
            //        Categories = catList
            //    };
            //    return View(vm);
            //}

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

        //[HttpPost]
        //public IActionResult Shop(HomeShopViewModel vm)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        IEnumerable<Hamper> hamper = _hamperService.Query(h => h.CategoryId == vm.CategoryId)
        //    }
        //    return View();
        //}
    }
}