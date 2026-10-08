using Microsoft.EntityFrameworkCore;
using BarbeariaWeb.Models;

namespace BarbeariaWeb.Data;

public class BarbeariaDbContext : DbContext
{
    public BarbeariaDbContext(DbContextOptions<BarbeariaDbContext> options) : base(options)
    {
    }

    public DbSet<Servico> Servicos => Set<Servico>();
    public DbSet<Barbeiro> Barbeiros => Set<Barbeiro>();
    public DbSet<Agendamento> Agendamentos => Set<Agendamento>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração de índices para performance e unicidade
        modelBuilder.Entity<Agendamento>()
            .HasIndex(a => a.CodigoReserva)
            .IsUnique();

        modelBuilder.Entity<Agendamento>()
            .HasIndex(a => a.DataHora);

        modelBuilder.Entity<Agendamento>()
            .HasOne(a => a.Servico)
            .WithMany(s => s.Agendamentos)
            .HasForeignKey(a => a.ServicoId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Agendamento>()
            .HasOne(a => a.Barbeiro)
            .WithMany(b => b.Agendamentos)
            .HasForeignKey(a => a.BarbeiroId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
