using BarbeariaWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace BarbeariaWeb.Data;

public static class DbInitializer
{
    public static async Task InitializeAsync(BarbeariaDbContext context)
    {
        // Garante que o banco de dados e as tabelas estejam criados
        await context.Database.EnsureCreatedAsync();

        // Se já tiver serviços, não precisa semear
        if (await context.Servicos.AnyAsync())
        {
            return;
        }

        // 1. Cadastrar Barbeiros
        var barbeiros = new List<Barbeiro>
        {
            new Barbeiro
            {
                Nome = "Vicente Rossi",
                Titulo = "Mestre Barbeiro & Fundador",
                Bio = "Mais de 12 anos de cutelaria tradicional. Especialista no autêntico corte à tesoura, alinhamento clássico e navalha de lâmina aberta.",
                FotoUrl = "https://images.unsplash.com/photo-1622286342621-4bd786c2447c?auto=format&fit=crop&w=800&q=80",
                ExperienciaAnos = 12,
                Avaliacao = 4.98,
                TotalAtendimentos = 3850,
                Ativo = true
            },
            new Barbeiro
            {
                Nome = "Eduardo \"Fade\" Moreira",
                Titulo = "Especialista em Fade & Freestyle",
                Bio = "Referência em degradês milimétricos (Low, Mid, High Fade, Taper Fade). Visagismo moderno que valoriza o formato do rosto.",
                FotoUrl = "https://images.unsplash.com/photo-1599566150163-29194dcaad36?auto=format&fit=crop&w=800&q=80",
                ExperienciaAnos = 7,
                Avaliacao = 4.95,
                TotalAtendimentos = 2400,
                Ativo = true
            },
            new Barbeiro
            {
                Nome = "Arthur Albuquerque",
                Titulo = "Especialista em Barboterapia & Visagismo",
                Bio = "Especialista no ritual da barba: óleos essenciais, massagem relaxante, toalha quente aromatizada e contorno cirúrgico na navalha.",
                FotoUrl = "https://images.unsplash.com/photo-1507003211169-0a1dd7228f2d?auto=format&fit=crop&w=800&q=80",
                ExperienciaAnos = 9,
                Avaliacao = 5.0,
                TotalAtendimentos = 3100,
                Ativo = true
            }
        };

        await context.Barbeiros.AddRangeAsync(barbeiros);
        await context.SaveChangesAsync();

        // 2. Cadastrar Serviços
        var servicos = new List<Servico>
        {
            new Servico
            {
                Nome = "Corte Clássico Gentleman",
                Descricao = "Corte tradicional à tesoura e máquina, finalizado com lavagem especial, massagem capilar e penteado clássico com pomada matte.",
                Preco = 60.00m,
                DuracaoMinutos = 40,
                Categoria = "Cabelo",
                ImagemUrl = "https://images.unsplash.com/photo-1503951914875-452162b0f3f1?auto=format&fit=crop&w=800&q=80",
                IconeNome = "scissors",
                Destaque = true
            },
            new Servico
            {
                Nome = "Degradê / Fade Navalhado",
                Descricao = "Transição suave milimétrica com navalhete, acabamento dos contornos com lâmina e aplicação de loção pós-barba refrescante.",
                Preco = 65.00m,
                DuracaoMinutos = 45,
                Categoria = "Cabelo",
                ImagemUrl = "https://images.unsplash.com/photo-1585747860715-2ba37e788b70?auto=format&fit=crop&w=800&q=80",
                IconeNome = "zap",
                Destaque = true
            },
            new Servico
            {
                Nome = "Barboterapia & Toalha Quente",
                Descricao = "Ritual completo com toalha a vapor aromatizada com eucalipto, esfoliação facial, massagem com óleos nobres e navalha de precisão.",
                Preco = 50.00m,
                DuracaoMinutos = 35,
                Categoria = "Barba",
                ImagemUrl = "https://images.unsplash.com/photo-1621605815971-fbc98d665033?auto=format&fit=crop&w=800&q=80",
                IconeNome = "feather",
                Destaque = true
            },
            new Servico
            {
                Nome = "Combo Imperial (Cabelo + Barba)",
                Descricao = "A experiência definitiva da barbearia: Corte de cabelo completo + Barboterapia com toalha quente + Bebida cortesia da nossa adega.",
                Preco = 100.00m,
                DuracaoMinutos = 70,
                Categoria = "Combos",
                ImagemUrl = "https://images.unsplash.com/photo-1512690459411-b9245aed614b?auto=format&fit=crop&w=800&q=80",
                IconeNome = "crown",
                Destaque = true
            },
            new Servico
            {
                Nome = "Acabamento & Pezinho",
                Descricao = "Alinhamento das linhas da nuca, costeletas e testa com navalha afiada. Ideal para manutenção entre cortes.",
                Preco = 30.00m,
                DuracaoMinutos = 20,
                Categoria = "Cabelo",
                ImagemUrl = "https://images.unsplash.com/photo-1622286342621-4bd786c2447c?auto=format&fit=crop&w=800&q=80",
                IconeNome = "check-circle",
                Destaque = false
            },
            new Servico
            {
                Nome = "Pigmentação & Camuflagem",
                Descricao = "Alinhamento visual de fios grisalhos e falhas da barba ou cabelo com pigmento de aspecto 100% natural.",
                Preco = 45.00m,
                DuracaoMinutos = 30,
                Categoria = "Tratamentos",
                ImagemUrl = "https://images.unsplash.com/photo-1599566150163-29194dcaad36?auto=format&fit=crop&w=800&q=80",
                IconeNome = "brush",
                Destaque = false
            },
            new Servico
            {
                Nome = "Hidratação Profunda & Detox",
                Descricao = "Tratamento reconstrutor dos fios com ativos botânicos e ozonioterapia para purificação do couro cabeludo.",
                Preco = 55.00m,
                DuracaoMinutos = 30,
                Categoria = "Tratamentos",
                ImagemUrl = "https://images.unsplash.com/photo-1503951914875-452162b0f3f1?auto=format&fit=crop&w=800&q=80",
                IconeNome = "droplet",
                Destaque = false
            }
        };

        await context.Servicos.AddRangeAsync(servicos);
        await context.SaveChangesAsync();

        // 3. Cadastrar Agendamentos de Demonstração (para o painel já conter dados reais)
        var hoje = DateTime.Today;
        var vicente = barbeiros[0];
        var eduardo = barbeiros[1];
        var corteClasico = servicos[0];
        var combo = servicos[3];

        var agendamentosDemo = new List<Agendamento>
        {
            new Agendamento
            {
                CodigoReserva = "DOM-1042",
                NomeCliente = "Lucas Mendes",
                TelefoneCliente = "(11) 98765-4321",
                EmailCliente = "lucas.mendes@email.com",
                ServicoId = corteClasico.Id,
                BarbeiroId = vicente.Id,
                DataHora = hoje.AddHours(14),
                Observacoes = "Prefere corte bem tesourado no topo.",
                Status = "Confirmado",
                DataCriacao = DateTime.UtcNow.AddDays(-1)
            },
            new Agendamento
            {
                CodigoReserva = "DOM-1088",
                NomeCliente = "Gabriel Siqueira",
                TelefoneCliente = "(11) 99812-7744",
                EmailCliente = "gabriel.siqueira@email.com",
                ServicoId = combo.Id,
                BarbeiroId = eduardo.Id,
                DataHora = hoje.AddHours(16).AddMinutes(30),
                Observacoes = "Fade médio no pente 1 e barba quadrada.",
                Status = "Confirmado",
                DataCriacao = DateTime.UtcNow.AddHours(-5)
            }
        };

        await context.Agendamentos.AddRangeAsync(agendamentosDemo);
        await context.SaveChangesAsync();
    }
}
