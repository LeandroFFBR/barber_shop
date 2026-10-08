using System.Web;
using Microsoft.AspNetCore.Mvc;
using BarbeariaWeb.Models;
using BarbeariaWeb.Services;
using BarbeariaWeb.ViewModels;

namespace BarbeariaWeb.Controllers;

public class AgendamentoController : Controller
{
    private readonly IAgendamentoService _service;
    private readonly ILogger<AgendamentoController> _logger;

    public AgendamentoController(IAgendamentoService service, ILogger<AgendamentoController> logger)
    {
        _service = service;
        _logger = logger;
    }

    // GET: /Agendamento ou /Agendamento/Reservar
    [HttpGet]
    public async Task<IActionResult> Index(int? servicoId, int? barbeiroId)
    {
        var servicos = await _service.ObterServicosAsync();
        var barbeiros = await _service.ObterBarbeirosAsync();

        var selectedServicoId = servicoId ?? (servicos.FirstOrDefault()?.Id ?? 0);
        var selectedBarbeiroId = barbeiroId ?? (barbeiros.FirstOrDefault()?.Id ?? 0);
        var dataHoje = DateTime.Today;

        var horariosDisponiveis = await _service.ObterHorariosDisponiveisAsync(selectedBarbeiroId, dataHoje);

        var model = new AgendamentoViewModel
        {
            ServicoId = selectedServicoId,
            BarbeiroId = selectedBarbeiroId,
            Data = dataHoje,
            ServicosDisponiveis = servicos,
            BarbeirosDisponiveis = barbeiros,
            HorariosDisponiveis = horariosDisponiveis
        };

        return View(model);
    }

    // GET: Endpoint AJAX para buscar horários dinamicamente ao mudar data ou barbeiro
    [HttpGet]
    public async Task<IActionResult> ObterHorarios(int barbeiroId, string data)
    {
        if (DateTime.TryParse(data, out var dataParsed))
        {
            var horarios = await _service.ObterHorariosDisponiveisAsync(barbeiroId, dataParsed);
            return Json(new { sucesso = true, horarios });
        }

        return Json(new { sucesso = false, mensagem = "Data inválida." });
    }

    // POST: /Agendamento
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(AgendamentoViewModel model)
    {
        // Se a data for anterior a hoje, adiciona erro de modelo
        if (model.Data.Date < DateTime.Today)
        {
            ModelState.AddModelError(nameof(model.Data), "Não é possível agendar para uma data retroativa.");
        }

        if (!ModelState.IsValid)
        {
            // Recarrega opções para o formulário
            model.ServicosDisponiveis = await _service.ObterServicosAsync();
            model.BarbeirosDisponiveis = await _service.ObterBarbeirosAsync();
            model.HorariosDisponiveis = await _service.ObterHorariosDisponiveisAsync(model.BarbeiroId, model.Data);
            return View(model);
        }

        try
        {
            var agendamento = await _service.CriarAgendamentoAsync(model);
            return RedirectToAction(nameof(Sucesso), new { codigo = agendamento.CodigoReserva });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao registrar agendamento");
            ModelState.AddModelError("", "Ocorreu um erro ao salvar o agendamento. Por favor, tente novamente.");
            model.ServicosDisponiveis = await _service.ObterServicosAsync();
            model.BarbeirosDisponiveis = await _service.ObterBarbeirosAsync();
            model.HorariosDisponiveis = await _service.ObterHorariosDisponiveisAsync(model.BarbeiroId, model.Data);
            return View(model);
        }
    }

    // GET: /Agendamento/Sucesso?codigo=DOM-1234
    [HttpGet]
    public async Task<IActionResult> Sucesso(string codigo)
    {
        if (string.IsNullOrWhiteSpace(codigo))
        {
            return RedirectToAction(nameof(Index));
        }

        var agendamento = await _service.ObterAgendamentoPorCodigoAsync(codigo);
        if (agendamento == null)
        {
            return NotFound("Agendamento não encontrado.");
        }

        // Gera link do WhatsApp com mensagem pré-formatada para salvar / confirmar
        var textoZap = $"Olá! Acabei de fazer um agendamento na Barbearia Dom Navalha.\n" +
                       $"Código: *{agendamento.CodigoReserva}*\n" +
                       $"Cliente: *{agendamento.NomeCliente}*\n" +
                       $"Serviço: *{agendamento.Servico?.Nome}*\n" +
                       $"Barbeiro: *{agendamento.Barbeiro?.Nome}*\n" +
                       $"Data e Horário: *{agendamento.DataHora:dd/MM/yyyy às HH:mm}*\n" +
                       $"Valor: *{agendamento.Servico?.Preco.ToString("C")}*";

        var urlZap = $"https://wa.me/5511999998888?text={HttpUtility.UrlEncode(textoZap)}";

        var viewModel = new ConfirmacaoViewModel
        {
            Agendamento = agendamento,
            WhatsAppMensagemUrl = urlZap
        };

        return View(viewModel);
    }

    // GET: /Agendamento/MinhasReservas
    [HttpGet]
    public async Task<IActionResult> MinhasReservas(string? termo)
    {
        var model = new ConsultaReservaViewModel
        {
            TermoBusca = termo ?? string.Empty
        };

        if (!string.IsNullOrWhiteSpace(termo))
        {
            model.Resultados = await _service.BuscarAgendamentosPorClienteAsync(termo);
            model.JaBuscou = true;
        }

        return View(model);
    }

    // GET: /Agendamento/Gerenciar (Painel de Gestão da Barbearia)
    [HttpGet]
    public async Task<IActionResult> Gerenciar(DateTime? data, string? status)
    {
        var dataConsulta = data ?? DateTime.Today;
        var agendamentos = await _service.ObterTodosAgendamentosAsync(dataConsulta, status);

        var todosHoje = await _service.ObterTodosAgendamentosAsync(DateTime.Today, null);

        var viewModel = new GerenciarReservasViewModel
        {
            DataFiltro = dataConsulta,
            StatusFiltro = status ?? "Todos",
            Agendamentos = agendamentos,
            TotalHoje = todosHoje.Count,
            TotalPendentes = todosHoje.Count(a => a.Status == "Pendente"),
            TotalConfirmados = todosHoje.Count(a => a.Status == "Confirmado")
        };

        return View(viewModel);
    }

    // POST: /Agendamento/AtualizarStatus
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AtualizarStatus(int id, string novoStatus, DateTime? dataFiltro, string? statusFiltro)
    {
        await _service.AtualizarStatusAsync(id, novoStatus);
        TempData["MensagemSucesso"] = $"Status da reserva atualizado para '{novoStatus}' com sucesso!";
        return RedirectToAction(nameof(Gerenciar), new { data = dataFiltro?.ToString("yyyy-MM-dd"), status = statusFiltro });
    }
}
