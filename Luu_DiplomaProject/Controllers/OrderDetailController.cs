using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Luu_DiplomaProject.Controllers
{
    public class OrderDetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}