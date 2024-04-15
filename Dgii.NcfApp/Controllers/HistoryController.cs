using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dgii.NcfApp.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dgii.NcfApp.Controllers
{
    public class HistoryController : Controller
    {

        private readonly IDgiiHistoryService _service;
        public HistoryController(IDgiiHistoryService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _service.GetNcfHistory();
            if (result.Any())
            {
                ViewBag.History = result;
            }
            return View();
        }

        //public async Task<IActionResult> PlacasHistory()
        //{
        //    var result = await _service.GetPlacasHistory();
        //    if (result.Any())
        //    {
        //        ViewBag.PlacasHistory = result;
        //    }
        //    return View();
        //}
    }
}

