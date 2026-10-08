using BarbeariaWeb.Data;
using BarbeariaWeb.Models;
using BarbeariaWeb.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace BarbeariaWeb.Services;

public class AgendamentoService : IAgendamentoService
{
    private readonly BarbeariaDbContext _context;

    public AgendamentoService(BarbeariaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Servico>> ObterServicosAsync()
    {
        return await _context.Servicos
            .OrderByDescending(s => s.Destaque)
            .ThenBy(s => s.Preco)
            .ToListAsync();
    }

    public async Task<List<Barbeiro>> ObterBarbeirosAsync()
    {
        return await _context.Barbeiros
            .Where(b => b.Ativo)
            .OrderByDescending(b => b.ExperienciaAnos)
            .ToListAsync();
    }

    public async Task<Servico?> ObterServicoPorIdAsync(int id)
    {
        return await _context.Servicos.FindAsync(id);
    }

    public async Task<Barbeiro?> ObterBarbeiroPorIdAsync(int id)
    {
        return await _context.Barbeiros.FindAsync(id);
    }

    public async Task<List<string>> ObterHorariosDisponiveisAsync(int barbeiroId, DateTime data)
    {
        // Horários de funcionamento: 09:00 às 19:30 com intervalos de 45 minutos
        var slotsPadrao = new List<string>
        {
            "09:00", "09:45", "10:30", "11:15", "12:00",
            "13:30", "14:15", "15:00", "15:45", "16:30",
            "17:15", "18:00", "18:45", "19:30"
        };

        var dataInicio = data.Date;
        var dataFim = data.Date.AddDays(1);

        // Agendamentos já marcados para o barbeiro no dia (exceto cancelados)
        var agendados = await _context.Agendamentos
            .Where(a => a.BarbeiroId == barbeiroId 
                     && a.DataHora >= dataInicio 
                     && a.DataHora < dataFim
                     && a.Status != "Cancelado")
            .Select(a => a.DataHora.ToString("HH:mm"))
            .ToListAsync();

        // Se for hoje, remove horários que já passaram
        if (data.Date == DateTime.Today)
        {
            var horaAtual = DateTime.Now.TimeOfDay;
            slotsPadrao = slotsPadrao.Where(slot =>
            {
                if (TimeSpan.TryParse(slot, out var ts))
                {
                    return ts > horaAtual.Add(TimeSpan.FromMinutes(20)); // margem de 20 min
                }
                return true;
            }).ToList();
        }

        // Filtra os que já estão ocupados
        var disponiveis = slotsPadrao.Except(agendados).ToList();
        return disponiveis;
    }

    public async Task<Agendamento> CriarAgendamentoAsync(AgendamentoViewModel model)
    {
        // Parse da data e horário
        var partesHora = model.Horario.Split(':');
        var hora = int.Parse(partesHora[0]);
        var minuto = int.Parse(partesHora[1]);

        var dataHoraFinal = model.Data.Date.AddHours(hora).AddMinutes(minuto);

        // Gera código de reserva único
        var codigo = $"DOM-{Random.Shared.Next(1000, 9999)}";
        while (await _context.Agendamentos.AnyAsync(a => a.CodigoReserva == codigo))
        {
            codigo = $"DOM-{Random.Shared.Next(1000, 9999)}";
        }

        var agendamento = new Agendamento
        {
            CodigoReserva = codigo,
            NomeCliente = model.NomeCliente.Trim(),
            TelefoneCliente = model.TelefoneCliente.Trim(),
            EmailCliente = string.IsNullOrWhiteSpace(model.EmailCliente) ? null : model.EmailCliente.Trim(),
            ServicoId = model.ServicoId,
            BarbeiroId = model.BarbeiroId,
            DataHora = dataHoraFinal,
            Observacoes = model.Observacoes,
            Status = "Confirmado",
            DataCriacao = DateTime.UtcNow
        };

        _context.Agendamentos.Add(agendamento);
        await _context.SaveChangesAsync();

        // Carrega as propriedades de navegação para a tela de confirmação
        await _context.Entry(agendamento).Reference(a => a.Servico).LoadAsync();
        await _context.Entry(agendamento).Reference(a => a.Barbeiro).LoadAsync();

        return agendamento;
    }

    public async Task<Agendamento?> ObterAgendamentoPorCodigoAsync(string codigo)
    {
        return await _context.Agendamentos
            .Include(a => a.Servico)
            .Include(a => a.Barbeiro)
            .FirstOrDefaultAsync(a => a.CodigoReserva.ToUpper() == codigo.ToUpper().Trim());
    }

    public async Task<List<Agendamento>> BuscarAgendamentosPorClienteAsync(string termo)
    {
        if (string.IsNullOrWhiteSpace(termo))
            return new List<Agendamento>();

        var busca = termo.Trim().ToLower();

        return await _context.Agendamentos
            .Include(a => a.Servico)
            .Include(a => a.Barbeiro)
            .Where(a => a.CodigoReserva.ToLower().Contains(busca) 
                     || a.TelefoneCliente.Contains(busca)
                     || a.NomeCliente.ToLower().Contains(busca))
            .OrderByDescending(a => a.DataHora)
            .ToListAsync();
    }

    public async Task<List<Agendamento>> ObterTodosAgendamentosAsync(DateTime? data, string? status)
    {
        var query = _context.Agendamentos
            .Include(a => a.Servico)
            .Include(a => a.Barbeiro)
            .AsQueryable();

        if (data.HasValue)
        {
            var inicio = data.Value.Date;
            var fim = data.Value.Date.AddDays(1);
            query = query.Where(a => a.DataHora >= inicio && a.DataHora < fim);
        }

        if (!string.IsNullOrWhiteSpace(status) && status != "Todos")
        {
            query = query.Where(a => a.Status == status);
        }

        return await query.OrderByDescending(a => a.DataHora).ToListAsync();
    }

    public async Task<bool> AtualizarStatusAsync(int id, string novoStatus)
    {
        var agendamento = await _context.Agendamentos.FindAsync(id);
        if (agendamento == null) return false;

        agendamento.Status = novoStatus;
        await _context.SaveChangesAsync();
        return true;
    }
}
