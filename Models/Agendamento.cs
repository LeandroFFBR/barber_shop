using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarbeariaWeb.Models;

public class Agendamento
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    public string CodigoReserva { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe seu nome completo")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter no mínimo 3 caracteres")]
    [Display(Name = "Nome Completo")]
    public string NomeCliente { get; set; } = string.Empty;

    [Required(ErrorMessage = "Informe seu WhatsApp ou Telefone")]
    [Phone(ErrorMessage = "Informe um telefone válido")]
    [StringLength(20)]
    [Display(Name = "Telefone / WhatsApp")]
    public string TelefoneCliente { get; set; } = string.Empty;

    [EmailAddress(ErrorMessage = "Informe um e-mail válido")]
    [StringLength(100)]
    [Display(Name = "E-mail (opcional)")]
    public string? EmailCliente { get; set; }

    [Required(ErrorMessage = "Selecione o serviço desejado")]
    [Display(Name = "Serviço")]
    public int ServicoId { get; set; }

    [ForeignKey(nameof(ServicoId))]
    public Servico? Servico { get; set; }

    [Required(ErrorMessage = "Selecione o barbeiro")]
    [Display(Name = "Barbeiro")]
    public int BarbeiroId { get; set; }

    [ForeignKey(nameof(BarbeiroId))]
    public Barbeiro? Barbeiro { get; set; }

    [Required(ErrorMessage = "Escolha a data e horário")]
    [Display(Name = "Data e Horário")]
    public DateTime DataHora { get; set; }

    [StringLength(300)]
    [Display(Name = "Observações ou Preferências")]
    public string? Observacoes { get; set; }

    [Required]
    [StringLength(20)]
    public string Status { get; set; } = "Pendente"; // Pendente, Confirmado, Concluído, Cancelado

    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
}
