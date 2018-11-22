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
            IEnumerable<Category> list = _categoryService.GetAll();

            if (ModelState.IsValid)
            {
                foreach (var cat in list)
                {
                    if (vm.Name.ToLower() == cat.Name.ToLower())
                    {
                        ModelState.AddModelError("", "Category already exists.");
                        return View(vm);
                    }
                }
                Category category = new Category
                {
                    Name = vm.Name,
                    Description = vm.Description
                };

                _categoryService.Create(category);

                return RedirectToAction("Details", "Category");
            }

            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Details()
        {
            IEnumerable<Category> list = _categoryService.GetAll();

            CategoryDetailsViewModel vm = new CategoryDetailsViewModel
            {
                Categories = list
            };
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            Category category = _categoryService.GetSingle(c => c.CategoryId == id);

            CategoryUpdateViewModel vm = new CategoryUpdateViewModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description
            };

            return View(vm);
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

                return RedirectToAction("Details", "Category");
            }

            return View(vm);
        }
    }
}