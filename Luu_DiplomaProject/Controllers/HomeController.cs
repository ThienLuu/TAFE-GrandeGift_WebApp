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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Luu_DiplomaProject.Controllers
{
    public class HomeController : Controller
    {
        private const string CartSessionKey = "_cart";

        private UserManager<IdentityUser> _userManagerService;
        private SignInManager<IdentityUser> _signInManagerService;
        private IDataService<Category> _categoryService;
        private IDataService<Hamper> _hamperService;
        private IDataService<Item> _itemService;
        private IDataService<Cart> _cartService;
        private IDataService<Customer> _customerService;
        private IDataService<OrderDetail> _orderDetailService;
        private IDataService<Order> _orderService;

        public HomeController(UserManager<IdentityUser> userManager,
                                SignInManager<IdentityUser> signinManger,
                                IDataService<Category> categoryService,
                                IDataService<Hamper> hamperService,
                                IDataService<Item> itemService,
                                IDataService<Cart> cartService,
                                IDataService<OrderDetail> orderDetailService,
                                IDataService<Order> orderService,
                                IDataService<Customer> customerService
                                )
        {
            _userManagerService = userManager;
            _signInManagerService = signinManger;
            _categoryService = categoryService;
            _hamperService = hamperService;
            _itemService = itemService;
            _cartService = cartService;
            _orderDetailService = orderDetailService;
            _orderService = orderService;
            _customerService = customerService;
        }
        public IActionResult Index()
        {
            IEnumerable<Hamper> hampers = _hamperService.GetAll().Take(3);
            IEnumerable<Item> items = _itemService.GetAll();

            HomeIndexViewModel vm = new HomeIndexViewModel
            {
                Hampers = hampers,
                Items = items
            };

            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Shop(int categoryId, string search, decimal min, decimal max)
        {
            IEnumerable<Category> catList = _categoryService.GetAll();
            IEnumerable<Hamper> hamList = _hamperService.GetAll().Where(h => h.Discontinued != true);
            IEnumerable<Item> itemList = _itemService.GetAll();

            #region Individual Filter

            ////IEnumerable<Hamper> hamSearch = _hamperService.Query(h => h.Name == search && h.Discontinued != true);
            //IEnumerable<Hamper> hamMinMax = _hamperService.Query(h => h.Price >= min && h.Price <= max && h.Discontinued != true);
            //IEnumerable<Hamper> hamCat = _hamperService.Query(h => h.CategoryId == categoryId && h.Discontinued != true);

            //if (min < max && max != 0)
            //{
            //    HomeShopViewModel vm = new HomeShopViewModel
            //    {
            //        Hampers = hamMinMax,
            //        Categories = catList,
            //        Items = itemList
            //    };
            //    return View(vm);
            //}

            //else if (categoryId != 0)
            //{
            //    HomeShopViewModel vm = new HomeShopViewModel
            //    {
            //        Hampers = hamCat,
            //        Categories = catList,
            //        Items = itemList
            //    };
            //    return View(vm);
            //}

            ////else if (search != null)
            ////{
            ////    HomeShopViewModel vm = new HomeShopViewModel
            ////    {
            ////        Hampers = hamSearch,
            ////        Categories = catList
            ////    };
            ////    return View(vm);
            ////}

            //else
            //{
            //    HomeShopViewModel vm = new HomeShopViewModel
            //    {
            //        Hampers = hamList,
            //        Categories = catList,
            //        Items = itemList
            //    };
            //    return View(vm);
            //}

            #endregion

            #region Combined Filter

            IEnumerable<Hamper> hamFilter = _hamperService.Query(h =>
                                                                    (h.Price >= min && h.Price <= max) &&
                                                                    (h.CategoryId == categoryId) &&
                                                                    (h.Discontinued != true)
                                                                );

            if (min < max && max != 0 && categoryId != 0)
            {
                HomeShopViewModel vm = new HomeShopViewModel
                {
                    Hampers = hamFilter,
                    Categories = catList,
                    Items = itemList
                };
                return View(vm);
            }
            else
            {
                HomeShopViewModel vm = new HomeShopViewModel
                {
                    Hampers = hamList,
                    Categories = catList,
                    Items = itemList
                };
                return View(vm);
            }

            #endregion
        }

        [HttpGet("images/{fileName}")]
        public IActionResult Read(string fileName)
        {
            Hamper hamper = _hamperService.GetSingle(h => h.FileName == fileName);

            if (hamper == null)
            {
                return NotFound();
            }

            return File(hamper.FileContent, hamper.ContentType);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            IEnumerable<Category> categoryList = _categoryService.GetAll();
            Hamper hamper = _hamperService.GetSingle(h => h.HamperId == id);
            IEnumerable<Item> itemList = _itemService.GetAll().Where(i => i.HamperId == hamper.HamperId);

            HomeDetailsViewModel vm = new HomeDetailsViewModel
            {
                HamperId = hamper.HamperId,
                Name = hamper.Name,
                Price = hamper.Price,
                Discontinued = hamper.Discontinued,
                CategoryId = hamper.CategoryId,
                Categories = categoryList,
                FileName = hamper.FileName,
                Items = itemList
            };
            return View(vm);
        }

        //ADDS TO CART
        [HttpPost]
        public IActionResult Create(HomeShopViewModel vm)
        {
            #region ATTEMPT 1 - CREATES TO CART
            ////ATTEMPT 1 - CREATES TO CART
            //string sessionId = this.HttpContext.Session.Id;

            //decimal total = vm.Quantity * vm.Price;

            //if (ModelState.IsValid)
            //{
            //    Cart cart = new Cart
            //    {
            //        SessionId = sessionId,
            //        Quantity = vm.Quantity,
            //        TotalPrice = total,
            //        HamperId = vm.HamperId
            //    };

            //    _cartService.Create(cart);
            //}
            //return RedirectToAction("Shop", "Home");
            #endregion

            #region ATTEMP 2 - CREATES TO ORDER DETAILS
            ////ATTEMP 2 - CREATES TO ORDER DETAILS
            //decimal total = vm.Quantity * vm.Price;

            //string id = _userManagerService.GetUserId(User);
            //Customer customer = _customerService.GetSingle(c => c.UserId == id);
            //Order order = _orderService.GetSingle(o => o.CustomerId == customer.CustomerId);

            //if (ModelState.IsValid)
            //{
            //    OrderDetail orderDetail = new OrderDetail
            //    {
            //        OrderId = order.OrderId,
            //        Quantity = vm.Quantity,
            //        TotalPrice = total,
            //        HamperId = vm.HamperId
            //    };

            //    _orderDetailService.Create(orderDetail);

            //    return RedirectToAction("Shop", "Home");
            //}

            //return View(vm);

            //string sessionId = this.HttpContext.Session.Id;
            //decimal total = vm.Quantity * vm.Price;

            //if (ModelState.IsValid)
            //{
            //    Cart cart = new Cart
            //    {
            //        SessionId = sessionId,
            //        HamperId = vm.HamperId,
            //        Quantity = vm.Quantity,
            //        TotalPrice = total,
            //    };
            //}
            #endregion

            #region ATTEMPT 3 - SESSIONS

            decimal total = vm.Quantity * vm.Price;

            if (String.IsNullOrEmpty(HttpContext.Session.GetString(CartSessionKey)))
            {
                List<Cart> carts = new List<Cart>();

                carts.Add(new Cart()
                {
                    //SessionId = sessionId,
                    Quantity = vm.Quantity,
                    TotalPrice = total,
                    HamperId = vm.HamperId
                });

                var serializedCart = JsonConvert.SerializeObject(carts);

                HttpContext.Session.SetString(CartSessionKey, serializedCart);
            }
            else
            {
                var serializedCart = HttpContext.Session.GetString(CartSessionKey);

                var carts = JsonConvert.DeserializeObject<List<Cart>>(serializedCart);

                carts.Add(new Cart()
                {
                    //SessionId = sessionId,
                    Quantity = vm.Quantity,
                    TotalPrice = total,
                    HamperId = vm.HamperId
                });

                serializedCart = JsonConvert.SerializeObject(carts);

                HttpContext.Session.SetString(CartSessionKey, serializedCart);
            }
            #endregion
            return RedirectToAction("Details", "Cart");
        }
    }
}