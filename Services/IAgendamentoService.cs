using BarbeariaWeb.Models;
using BarbeariaWeb.ViewModels;

namespace BarbeariaWeb.Services;

public interface IAgendamentoService
{
    Task<List<Servico>> ObterServicosAsync();
    Task<List<Barbeiro>> ObterBarbeirosAsync();
    Task<Servico?> ObterServicoPorIdAsync(int id);
    Task<Barbeiro?> ObterBarbeiroPorIdAsync(int id);
    Task<List<string>> ObterHorariosDisponiveisAsync(int barbeiroId, DateTime data);
    Task<Agendamento> CriarAgendamentoAsync(AgendamentoViewModel model);
    Task<Agendamento?> ObterAgendamentoPorCodigoAsync(string codigo);
    Task<List<Agendamento>> BuscarAgendamentosPorClienteAsync(string termo);
    Task<List<Agendamento>> ObterTodosAgendamentosAsync(DateTime? data, string? status);
    Task<bool> AtualizarStatusAsync(int id, string novoStatus);
}
