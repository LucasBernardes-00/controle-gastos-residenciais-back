# 💰 API de controle financeiro doméstico
Esta é uma API de controle financeiro desenvolvida em .NET 9, estruturada em multicamadas (Domain, Infra e Web) para garantir escalabilidade e fácil manutenção. O sistema permite o gerenciamento de pessoas, categorias e transações com validações de regras de negócio.

## 🚀 Tecnologias Utilizadas
Runtime: .NET 9

Banco de Dados: MySQL

ORM: Entity Framework Core (com Fluent API para mapeamento)

Arquitetura: Clean Architecture (Domain, Infra, Web)

Documentação: Scalar

## 🏗️ Estrutura do Projeto
Domain: Contém as entidades principais (Person, Category, Transaction), Enums e os DTOs de resposta como o PersonBalance.

Infra: Responsável pela persistência de dados, contém o AppDbContext, os mapeamentos (Maps) e a implementação dos Repositórios.

Web (API): Camada de entrada com Controllers, UseCases e configurações de Injeção de Dependência.

## 🛠️ Como Executar o Projeto
Pré-requisitos
- SDK do .NET 9 instalado.
- Instância do MySQL rodando.

Passo a Passo

1. Clone o repositório:
```bash
git clone https://github.com/SEU_USUARIO/SEU_REPOSITORIO.git
cd SEU_REPOSITORIO
```

2. Configure o Banco de Dados:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=finance_db;Uid=root;Pwd=SUA_SENHA;"
}
```

3. Rode as Migrations:
```bash
dotnet ef database update -p Infra -s WebApplication1
```

4. Execute a aplicação:
```bash
Inicie a aplicação:
```
A API estará disponível em https://localhost:7001 (ou na porta configurada).

## 📊 Endpoints Principais
GET /api/Person: Lista todas as pessoas.

GET /api/Dashboard/Balances: Retorna o saldo consolidado (Income, Expense, Balance) por pessoa ou categoria.

POST /api/Transaction: Registra uma nova movimentação (Valida se menores de idade estão tentando registrar receitas).

DELETE /api/Person/{id}: Remove uma pessoa e todas as suas transações vinculadas (Cascade Delete).

## 📝 Regras de Negócio Implementadas
Validação de Idade: Menores de 18 anos não podem registrar transações do tipo RECEITA.

Cálculo de Saldo: O saldo é calculado dinamicamente no banco de dados via SQL (SUM CASE) para maior performance.

Integridade: Categorias sem movimentação são filtradas nos relatórios de dashboard.
