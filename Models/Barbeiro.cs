using System.ComponentModel.DataAnnotations;

namespace BarbeariaWeb.Models;

public class Barbeiro
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do barbeiro é obrigatório")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Titulo { get; set; } = "Mestre Barbeiro"; // ex: Especialista em Degradê, Mestre Navalha

    [Required]
    [StringLength(500)]
    public string Bio { get; set; } = string.Empty;

    [StringLength(255)]
    public string FotoUrl { get; set; } = string.Empty;

    public int ExperienciaAnos { get; set; } = 5;

    [Range(1.0, 5.0)]
    public double Avaliacao { get; set; } = 4.9;

    public int TotalAtendimentos { get; set; } = 1200;

    public bool Ativo { get; set; } = true;

    // Relacionamento com Agendamentos
    public ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
}
