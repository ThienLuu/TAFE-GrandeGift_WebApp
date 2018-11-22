using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//..
using Luu_DiplomaProject.Models;
using Luu_DiplomaProject.ViewModels;
using Luu_DiplomaProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Luu_DiplomaProject.Controllers
{
    public class CartController : Controller
    {
        private const string CartSessionKey = "_cart";

        private UserManager<IdentityUser> _userManagerService;
        private IDataService<Customer> _customerService;

        private IDataService<Cart> _cartService;
        private IDataService<Hamper> _hamperService;
        private IDataService<Order> _orderService;
        private IDataService<OrderDetail> _orderDetailService;

        public CartController(UserManager<IdentityUser> userManager,
                            IDataService<Cart> cartService,
                            IDataService<Hamper> hamperService,
                            IDataService<Customer> customerService,
                            IDataService<Order> orderService,
                            IDataService<OrderDetail> orderDetailService
                            )
        {
            _userManagerService = userManager;
            _cartService = cartService;
            _hamperService = hamperService;
            _customerService = customerService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
        }
        public IActionResult Details()
        {
            //string sessionId = this.HttpContext.Session.Id;
            //IEnumerable<Cart> carts = _cartService.GetAll();
            //IEnumerable<Hamper> hampers = _hamperService.GetAll();
            ////IEnumerable<Cart> carts = _cartService.GetAll().Where(c => c.SessionId == sessionId);

            //CartDetailViewModel vm = new CartDetailViewModel
            //{
            //    Carts = carts,
            //    Hampers = hampers
            //};

            //return View(vm);

            //string id = _userManagerService.GetUserId(User);
            //Customer customer = _customerService.GetSingle(c => c.UserId == id);
            //Order order = _orderService.GetSingle(o => o.CustomerId == customer.CustomerId);
            //IEnumerable<OrderDetail> orderDetails = _orderDetailService.Query(od => od.OrderId == order.OrderId);
            IEnumerable<Hamper> hampers = _hamperService.GetAll();

            var serializedCart = HttpContext.Session.GetString(CartSessionKey);

            if (serializedCart == null)
            {
                //REDIRECT TO WHERE IF SESSION TIMES OUT/OR DISPLAY MESSAGE
            }

            var carts = JsonConvert.DeserializeObject<List<Cart>>(serializedCart);

            CartDetailViewModel vm = new CartDetailViewModel
            {
                //OrderDetails = orderDetails,
                Hampers = hampers,
                Carts = carts
            };

            return View(vm);
        }
    }
}