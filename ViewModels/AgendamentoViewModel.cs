using System.ComponentModel.DataAnnotations;
using BarbeariaWeb.Models;

namespace BarbeariaWeb.ViewModels;

public class AgendamentoViewModel
{
    [Required(ErrorMessage = "Por favor, selecione um serviço")]
    [Display(Name = "Serviço")]
    public int ServicoId { get; set; }

    [Required(ErrorMessage = "Por favor, selecione um barbeiro")]
    [Display(Name = "Barbeiro")]
    public int BarbeiroId { get; set; }

    [Required(ErrorMessage = "Escolha a data do agendamento")]
    [DataType(DataType.Date)]
    [Display(Name = "Data")]
    public DateTime Data { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "Escolha o horário")]
    [Display(Name = "Horário")]
    public string Horario { get; set; } = string.Empty;

    [Required(ErrorMessage = "Por favor, informe seu nome")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres")]
    [Display(Name = "Seu Nome Completo")]
    public string NomeCliente { get; set; } = string.Empty;

    [Required(ErrorMessage = "Por favor, informe seu telefone ou WhatsApp")]
    [Phone(ErrorMessage = "Formato de telefone inválido")]
    [Display(Name = "WhatsApp / Telefone")]
    public string TelefoneCliente { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "E-mail inválido")]
    [Display(Name = "E-mail (para confirmação)")]
    public string? EmailCliente { get; set; }

    [StringLength(250)]
    [Display(Name = "Alguma preferência ou observação?")]
    public string? Observacoes { get; set; }

    // Listas auxiliares para renderizar opções no formulário
    public List<Servico> ServicosDisponiveis { get; set; } = new();
    public List<Barbeiro> BarbeirosDisponiveis { get; set; } = new();
    public List<string> HorariosDisponiveis { get; set; } = new();
}
