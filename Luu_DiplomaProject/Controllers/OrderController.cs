using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//..
using Luu_DiplomaProject.Models;
using Luu_DiplomaProject.ViewModels;
using Luu_DiplomaProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Luu_DiplomaProject.Controllers
{
    public class OrderController : Controller
    {
        private const string CartSessionKey = "_cart";

        private UserManager<IdentityUser> _userManagerService;
        private IDataService<Customer> _customerService;
        private IDataService<OrderDetail> _orderDetailService;
        private IDataService<Order> _orderService;
        private IDataService<Hamper> _hamperService;
        private IDataService<Cart> _cartService;
        private IDataService<Address> _addressService;

        public OrderController(UserManager<IdentityUser> userManager,
                                IDataService<Customer> customerService,
                                IDataService<OrderDetail> orderDetailService,
                                IDataService<Order> orderService,
                                IDataService<Hamper> hamperService,
                                IDataService<Cart> cartService,
                                IDataService<Address> addressService
                                )
        {
            _userManagerService = userManager;
            _customerService = customerService;
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _hamperService = hamperService;
            _cartService = cartService;
            _addressService = addressService;
        }
        public IActionResult Details(int id)
        {
            //DISPLAY ORDER DATETIME/THANKYOU/BUTTON RETURNS HOME/SUMMARY/RECEIPT?

            ////PASS ORDER ID
            //string sessionId = this.HttpContext.Session.Id;
            //IEnumerable<OrderDetail> orderDetails = _orderDetailService.GetAll().Where(od => od.OrderId == id);
            //IEnumerable<Hamper> hampers = _hamperService.GetAll();
            ////IEnumerable<Cart> carts = _cartService.GetAll().Where(c => c.SessionId == sessionId);

            //OrderDetailsViewModel vm = new OrderDetailsViewModel
            //{
            //    OrderDetails = orderDetails,
            //    Hampers = hampers
            //};

            //return View(vm);

            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            string id = _userManagerService.GetUserId(User);
            Customer customer = _customerService.GetSingle(c => c.UserId == id);
            IEnumerable<Address> address = _addressService.Query(a => (a.Favourite == true) && (a.CustomerId == customer.CustomerId));
            IEnumerable<Hamper> hampers = _hamperService.GetAll();

            var serializedCart = HttpContext.Session.GetString(CartSessionKey);

            if (serializedCart == null)
            {
                //REDIRECT TO WHERE IF SESSION TIMES OUT/OR DISPLAY MESSAGE
            }

            var carts = JsonConvert.DeserializeObject<List<Cart>>(serializedCart);

            OrderCreateViewModel vm = new OrderCreateViewModel
            {
                Hampers = hampers,
                Carts = carts,
                Addresses = address,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
            };
            return View(vm);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(OrderCreateViewModel vm)
        {
            //[IMPORTANT] DO NOT ALLOW USER TO CLICK 'BACK' AND CREATE DUPLICATE
            //Have check

            #region Attempt 1

            //////USE THIS IF USING 'USERID' NOT 'CUSTOMER' ID IN 'ORDERS TABLE'
            ////string id = _userManagerService.GetUserId(User);

            ////ALTERNATE OPTION - Search UserId and get CustomerId
            //string id = _userManagerService.GetUserId(User);
            //Customer customer = _customerService.GetSingle(c => c.UserId == id);



            //string sessionId = this.HttpContext.Session.Id;
            //IEnumerable<Cart> cartList = _cartService.GetAll();


            ////1. Creates an Order without OrderDetails and assigns to a customer
            //Order order = new Order
            //{
            //    OrderDateTime = DateTime.Today,
            //    CustomerId = customer.CustomerId
            //};

            //_orderService.Create(order);

            ////2. Gets 'Cart' records and adds them to 'OrderDetails'
            ////This variable will hold the order detail price sum
            //decimal orderTotal = 0;

            ////This is where 'Cart' records are added to 'OrderDetails' and assigned to a 'Order' id
            //foreach (var item in cartList)
            //{
            //    //Checks if current session Id matches session Id cart items were saved under
            //    if (sessionId == item.SessionId)
            //    {
            //        OrderDetail orderDetail = new OrderDetail
            //        {
            //            //Order Detail is assigned id
            //            OrderId = order.OrderId,
            //            HamperId = item.HamperId,
            //            Quantity = item.Quantity,
            //            TotalPrice = item.TotalPrice
            //        };

            //        //Adds the total price to the orderTotal
            //        orderTotal += item.TotalPrice;

            //        _orderDetailService.Create(orderDetail);
            //    }
            //}

            ////3. Updates 'Order', 'OrderPrice' field to orderTotal 
            //Order createdOrder = _orderService.GetSingle(c => c.OrderId == order.OrderId);

            //createdOrder.OrderPrice = orderTotal;

            //_orderService.Update(createdOrder);

            //return RedirectToAction("Details","Order", new { id = order.OrderId });

            #endregion

            #region Attempt 2

            ////USE THIS IF USING 'USERID' NOT 'CUSTOMER' ID IN 'ORDERS TABLE'
            //string id = _userManagerService.GetUserId(User);

            //ALTERNATE OPTION - Search UserId and get CustomerId
            string id = _userManagerService.GetUserId(User);
            Customer customer = _customerService.GetSingle(c => c.UserId == id);

            //string sessionId = this.HttpContext.Session.Id;
            //IEnumerable<Cart> cartList = _cartService.GetAll();


            //1. Creates an Order without OrderDetails and assigns to a customer
            Order order = new Order
            {
                OrderDateTime = DateTime.Today,
                CustomerId = customer.CustomerId,
                AddressId = vm.AddressId,
                OrderPrice = vm.OrderPrice
            };

            _orderService.Create(order);

            //2. Gets 'Cart' records and adds them to 'OrderDetails'

            var serializedCart = HttpContext.Session.GetString(CartSessionKey);

            if (serializedCart == null)
            {
                //REDIRECT TO WHERE IF SESSION TIMES OUT/OR DISPLAY MESSAGE
            }

            var carts = JsonConvert.DeserializeObject<List<Cart>>(serializedCart);

            //This variable will hold the order detail price sum

            decimal orderTotal = 0;
            //ORDER HERE

            //This is where 'Cart' records are added to 'OrderDetails' and assigned to a 'Order' id
            foreach (var item in carts)
            {
                //Checks if current session Id matches session Id cart items were saved under

                    OrderDetail orderDetail = new OrderDetail
                    {
                        //Order Detail is assigned id
                        OrderId = order.OrderId,
                        HamperId = item.HamperId,
                        Quantity = item.Quantity,
                        TotalPrice = item.TotalPrice
                    };

                    //Adds the total price to the orderTotal
                    orderTotal += item.TotalPrice;

                    _orderDetailService.Create(orderDetail);
            }

            return RedirectToAction("Details", "Order", new { id = order.OrderId });

            #endregion

            #region Attempt 3



            #endregion
        }
    }
}