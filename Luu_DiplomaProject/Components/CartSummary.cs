using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//..
using Luu_DiplomaProject.Models;
using Luu_DiplomaProject.ViewModels;
using Luu_DiplomaProject.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Luu_DiplomaProject.Components
{
    public class CartSummary : ViewComponent
    {
        private const string CartSessionKey = "_cart";

        private UserManager<IdentityUser> _userManagerService;
        private SignInManager<IdentityUser> _signInManagerService;
        private IDataService<Customer> _customerService;
        //private IDataService<Cart> _cartService;
        private IDataService<Hamper> _hamperService;
        private IDataService<OrderDetail> _orderDetailService;
        private IDataService<Order> _orderService;

        public CartSummary(UserManager<IdentityUser> userManager,
                            SignInManager<IdentityUser> signinManger,
                            IDataService<Customer> customerService,
                            //IDataService<Cart> cartService,
                            IDataService<Hamper> hamperService,
                            IDataService<OrderDetail> orderDetailService,
                            IDataService<Order> orderService)
        {
            //_cartService = cartService;
            _userManagerService = userManager;
            _signInManagerService = signinManger;
            _customerService = customerService;
            _hamperService = hamperService;
            _orderDetailService = orderDetailService;
            _orderService = orderService;
        }

        public object JsonConverter { get; private set; }

        public IViewComponentResult Invoke()
        {
            //return View();

            //string id = _userManagerService.GetUserId(UserClaimsPrincipal);
            //Customer customer = _customerService.GetSingle(c => c.UserId == id);
            //Order order = _orderService.GetSingle(o => o.CustomerId == customer.CustomerId);
            //IEnumerable<OrderDetail> orderDetails = _orderDetailService.Query(od => od.OrderId == order.OrderId);

            //IEnumerable<Hamper> hampers = _hamperService.GetAll();

            //ViewComponentViewModel vm = new ViewComponentViewModel
            //{
            //    OrderDetails = orderDetails,
            //    Hampers = hampers
            //};

            //return View(vm);


            //string sessionId = this.HttpContext.Session.Id;
            //IEnumerable<Cart> carts = _cartService.GetAll();
            //IEnumerable<Hamper> hampers = _hamperService.GetAll();
            ////IEnumerable<Cart> carts = _cartService.GetAll().Where(c => c.SessionId == sessionId);

            //ViewComponentViewModel vm = new ViewComponentViewModel
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
                ViewComponentViewModel vm1 = new ViewComponentViewModel
                {
                    //OrderDetails = orderDetails,
                    Hampers = null,
                    Carts = null
                };

                return View(vm1);
            }

            var carts = JsonConvert.DeserializeObject<List<Cart>>(serializedCart);

            int totalItem = carts.Count();

            decimal totalPrice = 0;

            foreach (var item in carts)
            {
                totalPrice += item.TotalPrice;
            }

            ViewComponentViewModel vm2 = new ViewComponentViewModel
            {
                //OrderDetails = orderDetails,
                Hampers = hampers,
                Carts = carts,
                TotalItem = totalItem,
                TotalPrice = totalPrice
            };

            return View(vm2);
        }
    }
}
