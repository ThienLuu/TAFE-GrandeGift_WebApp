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
    public class ItemController : Controller
    {
        private IDataService<Hamper> _hamperService;
        private IDataService<Item> _itemService;

        public ItemController(IDataService<Hamper> hamperService,
                                IDataService<Item> itemService)
        {
            _hamperService = hamperService;
            _itemService = itemService;
        }

        [HttpGet]
        [Authorize (Roles = "Admin")]
        public IActionResult Create(int id)
        {
            ItemCreateViewModel vm = new ItemCreateViewModel
            {
                HamperId = id
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ItemCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Item item = new Item
                {
                    HamperId = vm.HamperId,
                    Name = vm.Name,
                    Size = vm.Size
                };

                _itemService.Create(item);

                return RedirectToAction("Update", "Hamper", new { id = vm.HamperId });
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Details(int id)
        {

            IEnumerable<Item> items = _itemService.GetAll();

            if (items != null)
            {
                ItemDetailsViewModel vm = new ItemDetailsViewModel
                {
                    HamperId = id,
                    Items = items
                };
                return View(vm);
            }
            else
            {
                return RedirectToAction("Details","Hamper", new { hamperId = id });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            Item item = _itemService.GetSingle(h => h.ItemId == id);

            ItemUpdateViewModel vm = new ItemUpdateViewModel
            {
                HamperId = item.HamperId,
                Name = item.Name,
                Size = item.Size,
                ItemId = id
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(ItemUpdateViewModel vm)
        {
            Item item = _itemService.GetSingle(h => h.ItemId == vm.ItemId);

            if (ModelState.IsValid && item != null)
            {
                item.HamperId = vm.HamperId;
                item.ItemId = vm.ItemId;
                item.Name = vm.Name;
                item.Size = vm.Size;

                _itemService.Update(item);

                return RedirectToAction("Details", "Item", new { id = item.HamperId });
            }

            return View(vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(ItemDetailsViewModel vm)
        {
            Item item = _itemService.GetSingle(i => i.ItemId == vm.ItemId);

            _itemService.Delete(item);

            return RedirectToAction("Details", "Item", new { id = vm.HamperId });
        }
    }
}