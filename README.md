# 💈 Barbearia Dom Navalha - Sistema Web ASP.NET Core (.NET 10)

Aplicação web profissional e completa para barbearia desenvolvida em **ASP.NET Core MVC (.NET 10)** com **Entity Framework Core (SQLite)**, adotando o tema estético de alto padrão **Vintage Gentleman (Dark & Gold)**.

---

## 🌟 Funcionalidades do Sistema

### 1. 🏠 Página Inicial (`/`)
- **Hero Section Cinematográfica:** Apresentação da marca, chamada de ação direta para agendamento e destaques de diferenciais.
- **Diferenciais da Casa:** Sem filas (horário pontual), esterilização e higiene rigorosa, mestres visagistas e Lounge Bar com café/whisky cortesia.
- **Cardápio de Destaques:** Serviços com preços, tempo de execução e atalho para reserva direta.
- **Mestres Barbeiros:** Fotos reais, bio, avaliação em estrelas e anos de prática com botão de agendamento por profissional.
- **Depoimentos de Clientes & Avaliações:** Prova social de clientes reais.

### 2. 📖 A Barbearia / Sobre (`/Home/Sobre`)
- História e filosofia da barbearia desde 2012.
- O ritual completo da **Barboterapia & Toalha Quente**.
- Valores da marca, instrumentos de cutelaria tradicional e galeria do ambiente.

### 3. ✂️ Cardápio Completo de Serviços (`/Home/Servicos`)
- Listagem categorizada (*Cabelo, Barba, Combos, Tratamentos*).
- Filtros dinâmicos interativos em tempo real.
- Detalhes de duração e valores.

### 4. 📅 Sistema de Reserva / Agendamento Interativo (`/Agendamento`)
- **Wizard Visual em 4 Passos:**
  1. **Escolha do Serviço:** Cards clicáveis com preços e tempos.
  2. **Escolha do Barbeiro:** Cards com avatar, especialidade e estrelas.
  3. **Data & Horário:** Seletor de data com busca dinâmica via AJAX dos horários vagos (evita conflitos e sobreposição de agenda).
  4. **Dados do Cliente:** Nome completo, telefone/WhatsApp formatado automaticamente e observações.
- **Card Lateral de Resumo em Tempo Real:** Atualiza o serviço, barbeiro, data, horário e valor instantaneamente conforme o usuário clica.

### 5. 🎟️ Confirmação & Voucher Digital (`/Agendamento/Sucesso`)
- Geração de código de agendamento único (ex: `DOM-1042`).
- Resumo completo para apresentação na recepção.
- **Botão Direto para o WhatsApp:** Abre a conversa com os detalhes já digitados para salvar no celular do cliente ou da barbearia.
- **Botão de Impressão:** Comprovante formatado para impressão ou salvamento em PDF.

### 6. 🔍 Consulta de Agendamentos pelo Cliente (`/Agendamento/MinhasReservas`)
- Permite ao cliente pesquisar suas reservas a qualquer momento pelo número do telefone ou código.
- Exibe o status da reserva (*Confirmado, Pendente, Concluído, Cancelado*).

### 7. 🛡️ Painel de Gestão da Barbearia (`/Agendamento/Gerenciar`)
- Visão diária da agenda com métricas (Total do dia, confirmados, pendentes).
- Filtros por data e por status.
- Ações rápidas para confirmar, concluir ou cancelar agendamentos.

---

## 🛠️ Tecnologias Utilizadas

- **.NET 10 SDK & ASP.NET Core MVC**
- **Entity Framework Core 10 (SQLite)**
- **Bootstrap 5 & Bootstrap Icons**
- **Custom CSS Design Tokens:** Paleta *Vintage Gentleman (Dark & Gold)* com tipografias *Cinzel* e *Plus Jakarta Sans*
- **JavaScript Moderno:** Máscara de telefone brasileiro, AJAX dinâmico para cálculo de horários livres e atualização de resumo em tempo real

---

## 🚀 Como Executar o Projeto

1. Certifique-se de ter o [.NET SDK](https://dotnet.microsoft.com/) instalado (versão 10 ou compatível).
2. Na raiz do projeto, execute o comando:
   ```bash
   dotnet run --launch-profile http
   ```
3. Abra seu navegador em:
   ```
   http://localhost:5072
   ```
   *(O banco de dados SQLite `barbearia.db` e os dados iniciais de serviços e barbeiros são criados e semeados automaticamente na primeira inicialização!)*
