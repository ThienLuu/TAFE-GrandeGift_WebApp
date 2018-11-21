using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//..
using Luu_DiplomaProject.Models;
using Luu_DiplomaProject.ViewModels;
using Luu_DiplomaProject.Services;

namespace Luu_DiplomaProject.Controllers
{
    public class CartController : Controller
    {
        private IDataService<Cart> _cartService;
        private IDataService<Hamper> _hamperService;

        public CartController(IDataService<Cart> cartService,
                            IDataService<Hamper> hamperService)
        {
            _cartService = cartService;
            _hamperService = hamperService;
        }
        public IActionResult Details()
        {
            string sessionId = this.HttpContext.Session.Id;
            IEnumerable<Cart> carts = _cartService.GetAll();
            IEnumerable<Hamper> hampers = _hamperService.GetAll();
            //IEnumerable<Cart> carts = _cartService.GetAll().Where(c => c.SessionId == sessionId);

            CartDetailViewModel vm = new CartDetailViewModel
            {
                Carts = carts,
                Hampers = hampers
            };

            return View(vm);
        }
    }
}