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

namespace Luu_DiplomaProject.Controllers
{
    public class OrderController : Controller
    {
        private UserManager<IdentityUser> _userManagerService;
        private IDataService<Customer> _customerService;
        private IDataService<OrderDetail> _orderDetailService;
        private IDataService<Order> _orderService;
        private IDataService<Hamper> _hamperService;
        private IDataService<Cart> _cartService;

        public OrderController(UserManager<IdentityUser> userManager,
                                IDataService<Customer> customerService,
                                IDataService<OrderDetail> orderDetailService,
                                IDataService<Order> orderService,
                                IDataService<Hamper> hamperService,
                                IDataService<Cart> cartService
                                )
        {
            _userManagerService = userManager;
            _customerService = customerService;
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _hamperService = hamperService;
            _cartService = cartService;
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

        [Authorize]
        public IActionResult Create()
        {
            ////USE THIS IF USING 'USERID' NOT 'CUSTOMER' ID IN 'ORDERS TABLE'
            //string id = _userManagerService.GetUserId(User);

            //ALTERNATE OPTION
            string id = _userManagerService.GetUserId(User);
            Customer customer = _customerService.GetSingle(c => c.UserId == id);



            string sessionId = this.HttpContext.Session.Id;
            IEnumerable<Cart> cartList = _cartService.GetAll();


            //1. Creates an Order without OrderDetails and assigns to a customer
            Order order = new Order
            {
                OrderDateTime = DateTime.Today,
                CustomerId = customer.CustomerId
            };

            _orderService.Create(order);

            //2. Gets 'Cart' records and adds them to 'OrderDetails'
            //This variable will hold the order detail price sum
            decimal orderTotal = 0;

            //This is where 'Cart' records are added to 'OrderDetails' and assigned to a 'Order' id
            foreach (var item in cartList)
            {
                //Checks if current session Id matches session Id cart items were saved under
                if (sessionId == item.SessionId)
                {
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
            }

            //3. Updates 'Order', 'OrderPrice' field to orderTotal 
            Order createdOrder = _orderService.GetSingle(c => c.OrderId == order.OrderId);

            createdOrder.OrderPrice = orderTotal;

            _orderService.Update(createdOrder);

            return RedirectToAction("Details","Order", new { id = order.OrderId });
        }
    }
}