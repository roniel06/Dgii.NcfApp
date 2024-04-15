using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dgii.NcfApp.Models;
using Dgii.NcfApp.Services;
using System.ComponentModel.DataAnnotations;

namespace Dgii.NcfApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ISoapDgiiService _soapDgiiService;

    public HomeController(ILogger<HomeController> logger, ISoapDgiiService soapDgiiService)
    {
        _logger = logger;
        _soapDgiiService = soapDgiiService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Index([FromForm] string rnc, [FromForm] string ncf)
    {

        var ncfResult = await _soapDgiiService.GetNcfAsync(rnc, ncf);
        if (ncfResult is not null)
        {
            ViewBag.NcfResult = ncfResult;
            var rncResult = await _soapDgiiService.GetRNC(rnc);
            ViewBag.Rnc = rncResult;
            return View();
        }
        ViewBag.Error = "No se encontro nigun registo con los datos proporcionados.";
        return View();

    }

    public IActionResult Placas()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Placas([FromForm] string rnc, [FromForm] string placa)
    {
        var result = await _soapDgiiService.GetPlacaResult(rnc, placa);
        if(result is not null)
        {
            ViewBag.PlacaResult = result;
            return View();
        }
        ViewBag.Error = "No se encontro nigun registo con los datos proporcionados.";

        return View();

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

