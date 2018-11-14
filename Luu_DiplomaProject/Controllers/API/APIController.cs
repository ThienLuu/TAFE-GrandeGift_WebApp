using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//..
using Luu_DiplomaProject.Models;
using Luu_DiplomaProject.Services;

namespace Luu_DiplomaProject.Controllers.API
{
    [Route("api")]
    public class APIController : Controller
    {
        private IDataService<Hamper> _hamperService;
        private IDataService<Category> _categoryServices;

        public APIController(IDataService<Hamper> hamperService,
                                IDataService<Category> categoryService)
        {
            _hamperService = hamperService;
            _categoryServices = categoryService;
        }

        [HttpGet("hampers")]
        public IEnumerable<Hamper> HamperList()
        {
            IEnumerable<Hamper> hamList = _hamperService.GetAll();

            return hamList;
        }

        [HttpGet("categories")]
        public IEnumerable<Category> CategoryList()
        {
            IEnumerable<Category> catList = _categoryServices.GetAll();

            return catList;
        }

        //[HttpGet("{id}")]
        //public Hamper GetId(int id)
        //{
        //    Hamper hamper = _hamperService.GetSingle(h => h.HamperId == id);

        //    return hamper;
        //}

        [HttpGet("searchByCategory/{categoryId}")]
        public IEnumerable<Hamper> HampersByCategory(int categoryId)
        {
            IEnumerable<Hamper> hamCat = _hamperService.Query(h => h.CategoryId == categoryId);

            return hamCat;
        }
    }
}