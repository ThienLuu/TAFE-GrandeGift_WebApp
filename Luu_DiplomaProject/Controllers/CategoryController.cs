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
    public class CategoryController : Controller
    {
        private IDataService<Category> _categoryService;
        private IDataService<Hamper> _hamperService;

        public CategoryController(IDataService<Category> categoryService,
                                    IDataService<Hamper> hamperService)
        {
            _categoryService = categoryService;
            _hamperService = hamperService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CategoryCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Category category = new Category
                {
                    Name = vm.Name,
                    Description = vm.Description
                };

                _categoryService.Create(category);

                //REDIRECT TO WHERE?
                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(CategoryUpdateViewModel vm)
        {
            Category category = _categoryService.GetSingle(c => c.CategoryId == vm.CategoryId);

            if (ModelState.IsValid && category != null)
            {
                category.CategoryId = vm.CategoryId;
                category.Name = vm.Name;
                category.Description = vm.Description;

                _categoryService.Update(category);

                return RedirectToAction("Index", "Home", new { id = category.CategoryId });
            }

            return View(vm);
        }
    }
}