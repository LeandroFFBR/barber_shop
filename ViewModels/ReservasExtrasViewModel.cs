using BarbeariaWeb.Models;

namespace BarbeariaWeb.ViewModels;

public class ConfirmacaoViewModel
{
    public Agendamento Agendamento { get; set; } = null!;
    public string WhatsAppMensagemUrl { get; set; } = string.Empty;
}

public class GerenciarReservasViewModel
{
    public DateTime? DataFiltro { get; set; }
    public string? StatusFiltro { get; set; }
    public List<Agendamento> Agendamentos { get; set; } = new();
    public int TotalPendentes { get; set; }
    public int TotalConfirmados { get; set; }
    public int TotalHoje { get; set; }
}

public class ConsultaReservaViewModel
{
    public string TermoBusca { get; set; } = string.Empty;
    public List<Agendamento>? Resultados { get; set; }
    public bool JaBuscou { get; set; } = false;
}
