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
    public class OrderController : Controller
    {
        private IDataService<OrderDetail> _orderDetailService;
        private IDataService<Order> _orderService;
        private IDataService<Hamper> _hamperService;

        public OrderController(IDataService<OrderDetail> orderDetailService,
                                IDataService<Order> orderService,
                                IDataService<Hamper> hamperService)
        {
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _hamperService = hamperService;
        }
        public IActionResult Details(int id)
        {
            //PASS ORDER ID
            string sessionId = this.HttpContext.Session.Id;
            IEnumerable<OrderDetail> orderDetails = _orderDetailService.GetAll().Where(od => od.OrderId == id);
            IEnumerable<Hamper> hampers = _hamperService.GetAll();
            //IEnumerable<Cart> carts = _cartService.GetAll().Where(c => c.SessionId == sessionId);

            OrderDetailsViewModel vm = new OrderDetailsViewModel
            {
                OrderDetails = orderDetails,
                Hampers = hampers
            };

            return View(vm);
        }
    }
}