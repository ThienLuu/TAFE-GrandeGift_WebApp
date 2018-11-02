using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//..
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore; //Isn't used...
using Luu_DiplomaProject.ViewModels;
using Luu_DiplomaProject.Models;
using Luu_DiplomaProject.Services;
using Microsoft.AspNetCore.Authorization;

namespace Luu_DiplomaProject.Controllers
{
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> _userManagerService;
        private SignInManager<IdentityUser> _signInManagerService;
        private IDataService<Customer> _customerService;
        public AccountController(UserManager<IdentityUser> userManager,
                                 SignInManager<IdentityUser> signinManger,
                                 IDataService<Customer> customerService)
        {
            _userManagerService = userManager;
            _signInManagerService = signinManger;
            _customerService = customerService;
        }

        #region Register

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AccountRegisterViewModel vm)
        {
            if (ModelState.IsValid)
            {
                //add a new user
                IdentityUser user = new IdentityUser(vm.Username);
                user.Email = vm.Email;
                IdentityResult result = await _userManagerService.CreateAsync(user, vm.Password);
                if (result.Succeeded)
                {
                    //create customer object
                    Customer customer = new Customer
                    {
                        FirstName = vm.FirstName,
                        LastName = vm.LastName,
                        DOB = vm.DOB,
                        UserId = user.Id
                    };

                    _customerService.Create(customer);
                    //go to Home/Index
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    //show errors
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(vm);
        }

        #endregion

        #region Login/Logout

        [HttpGet]
        public IActionResult Login(string returnUrl = "")
        {
            AccountLoginViewModel vm = new AccountLoginViewModel
            {
                ReturnUrl = returnUrl
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Login(AccountLoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManagerService.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, false);
                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(vm.ReturnUrl))
                    {
                        var user = await _userManagerService.FindByNameAsync(vm.Username);

                        bool isAdmin = await _userManagerService.IsInRoleAsync(user, "Admin");

                        if (isAdmin)
                        {
                            return RedirectToAction("Admin", "Home");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                    else
                    {
                        return Redirect(vm.ReturnUrl);
                    }
                }
                ModelState.AddModelError("", "'Username' or 'Password' is incorrect");
            }
            return View(vm);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManagerService.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #endregion

        #region Update

        [HttpGet]
        [Authorize]
        public IActionResult Update()
        {
            //string id = User.Identity.Name;
            string id = _userManagerService.GetUserId(User);
            Customer customer = _customerService.GetSingle(c => c.UserId == id);

            if (customer != null)
            {
                AccountUpdateViewModel vm = new AccountUpdateViewModel
                {
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    DOB = customer.DOB
                };

                return View(vm);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            //return View(vm);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Update(AccountUpdateViewModel vm)
        {
            string id = _userManagerService.GetUserId(User);

            Customer customer = _customerService.GetSingle(c => c.UserId == id);

            if (ModelState.IsValid && customer != null)
            {
                customer.FirstName = vm.FirstName;
                customer.LastName = vm.LastName;
                customer.DOB = vm.DOB;

                _customerService.Update(customer);

                return RedirectToAction("Index", "Home");
            }
            return View(vm);
        }

        #endregion
    }
}