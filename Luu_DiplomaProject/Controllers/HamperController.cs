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
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Luu_DiplomaProject.Controllers
{
    public class HamperController : Controller
    {
        private IDataService<Category> _categoryService;
        private IDataService<Hamper> _hamperService;
        private IDataService<Item> _itemService;

        public HamperController(IDataService<Category> categoryService,
                                    IDataService<Hamper> hamperService,
                                    IDataService<Item> itemService)
        {
            _categoryService = categoryService;
            _hamperService = hamperService;
            _itemService = itemService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            IEnumerable<Category> list = _categoryService.GetAll();

            HamperCreateViewModel vm = new HamperCreateViewModel
            {
                Categories = list
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(IFormFile upload, HamperCreateViewModel vm)
        {
            BinaryReader binaryReader = new BinaryReader(upload.OpenReadStream());
            byte[] fileData = binaryReader.ReadBytes((int)upload.Length);
            var fileName = Path.GetFileName(upload.FileName);
            IEnumerable<Category> list = _categoryService.GetAll();

            if (ModelState.IsValid)
            {
                Hamper hamper = new Hamper
                {
                    Name = vm.Name,
                    Price = vm.Price,
                    Details = vm.Details,
                    Discontinued = vm.Discontinued,
                    CategoryId = vm.CategoryId,
                    //Image
                    FileName = fileName,
                    ContentSize = upload.Length,
                    ContentType = upload.ContentType,
                    FileContent = fileData
                };

                _hamperService.Create(hamper);

                //IEnumerable<Hamper> hamperId = _hamperService.GetSingle(h => h.HamperId == );

                //return RedirectToAction("Details", "Hamper");
                return RedirectToAction("Create", "Item", new { id = hamper.HamperId });
            }
            vm.Categories = list;

            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Details()
        {
            IEnumerable<Hamper> hamList = _hamperService.GetAll();
            IEnumerable<Category> catList = _categoryService.GetAll();
            IEnumerable<Item> itemList = _itemService.GetAll();

            HamperDetailsViewModel vm = new HamperDetailsViewModel
            {
                Hampers = hamList,
                Categories = catList,
                Items = itemList
            };
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            IEnumerable<Category> categoryList = _categoryService.GetAll();
            Hamper hamper = _hamperService.GetSingle(h => h.HamperId == id);
            IEnumerable<Item> itemList = _itemService.GetAll().Where(i => i.HamperId == hamper.HamperId);

            HamperUpdateViewModel vm = new HamperUpdateViewModel
            {
                HamperId = hamper.HamperId,
                Name = hamper.Name,
                Price = hamper.Price,
                Details = hamper.Details,
                Discontinued = hamper.Discontinued,
                CategoryId = hamper.CategoryId,
                Categories = categoryList,
                Items = itemList
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(HamperUpdateViewModel vm)
        {
            IEnumerable<Category> list = _categoryService.GetAll();
            Hamper hamper = _hamperService.GetSingle(h => h.HamperId == vm.HamperId);

            if (ModelState.IsValid && hamper != null)
            {
                hamper.HamperId = vm.HamperId;
                hamper.Name = vm.Name;
                hamper.Price = vm.Price;
                hamper.Details = vm.Details;
                hamper.Discontinued = vm.Discontinued;
                hamper.CategoryId = vm.CategoryId;

                _hamperService.Update(hamper);

                return RedirectToAction("Details", "Hamper");
            }
            vm.Categories = list;

            return View(vm);
        }
    }
}