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
    public class AddressController : Controller
    {
        private IDataService<Customer> _customerService;
        private IDataService<Address> _addressService;
        public AddressController(IDataService<Customer> customerService,
                                    IDataService<Address> addressService)
        {
            _customerService = customerService;
            _addressService = addressService;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create(int id)
        {
            AddressCreateViewModel vm = new AddressCreateViewModel
            {
                CustomerId = id
            };
            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Create(AddressCreateViewModel vm)
        {
            if (ModelState.IsValid)
            {
                Address address = new Address
                {
                    CustomerId = vm.CustomerId,
                    City = vm.City,
                    State = vm.State.ToUpper(),
                    Postcode = vm.Postcode,
                    Country = vm.Country,
                    StreetAddress = vm.StreetAddress,
                    Favourite = vm.Favourite
                };

                _addressService.Create(address);

                return RedirectToAction("Update", "Account");
            }
            return View(vm);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Update(int addressId, int custId)
        {
            Address address = _addressService.GetSingle(a => a.AddressId == addressId);

            AddressUpdateViewModel vm = new AddressUpdateViewModel
            {
                CustomerId = custId,
                AddressId = addressId,
                City = address.City,
                State = address.State,
                Postcode = address.Postcode,
                Country = address.Country,
                StreetAddress = address.StreetAddress,
                Favourite = address.Favourite
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(AddressUpdateViewModel vm)
        {
            Address address = _addressService.GetSingle(a => a.AddressId == vm.AddressId);

            if (ModelState.IsValid && address != null)
            {
                address.CustomerId = vm.CustomerId;
                address.AddressId = vm.AddressId;
                address.City = vm.City;
                address.State = vm.State;
                address.Postcode = vm.Postcode;
                address.Country = vm.Country;
                address.StreetAddress = vm.StreetAddress;
                address.Favourite = vm.Favourite;

                _addressService.Update(address);

                return RedirectToAction("Update", "Account");
            }

            return View(vm);
        }

        //[HttpPost]
        //public IActionResult Delete(int id)
        //{
        //    Address address = _addressService.GetSingle(a => a.AddressId == id);

        //    _addressService.Delete(address);

        //    return View();
        //}
    }
}