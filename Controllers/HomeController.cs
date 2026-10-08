using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BarbeariaWeb.Models;
using BarbeariaWeb.Services;

namespace BarbeariaWeb.Controllers;

public class HomeController : Controller
{
    private readonly IAgendamentoService _agendamentoService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(IAgendamentoService agendamentoService, ILogger<HomeController> logger)
    {
        _agendamentoService = agendamentoService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var servicos = await _agendamentoService.ObterServicosAsync();
        var barbeiros = await _agendamentoService.ObterBarbeirosAsync();

        ViewBag.Servicos = servicos;
        ViewBag.Barbeiros = barbeiros;
        return View();
    }

    public async Task<IActionResult> Sobre()
    {
        var barbeiros = await _agendamentoService.ObterBarbeirosAsync();
        ViewBag.Barbeiros = barbeiros;
        return View();
    }

    public async Task<IActionResult> Servicos()
    {
        var servicos = await _agendamentoService.ObterServicosAsync();
        return View(servicos);
    }

    public IActionResult Contato()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
