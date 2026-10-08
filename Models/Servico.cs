using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BarbeariaWeb.Models;

public class Servico
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do serviço é obrigatório")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(500)]
    public string Descricao { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Preco { get; set; }

    [Required]
    [Range(10, 180, ErrorMessage = "Duração deve ser entre 10 e 180 minutos")]
    public int DuracaoMinutos { get; set; } = 30;

    [StringLength(50)]
    public string Categoria { get; set; } = "Cabelo"; // Cabelo, Barba, Combo, Tratamento

    [StringLength(255)]
    public string? ImagemUrl { get; set; }

    [StringLength(100)]
    public string? IconeNome { get; set; }

    public bool Destaque { get; set; } = false;

    // Relacionamento com Agendamentos
    public ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
}
