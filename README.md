# Good Hamburger — STgen technical challenge

API Rest com .NET 9 e ASP.NET Core e frontend em Blazor.
---

## Conteúdo

- [Primeiros Passos](#getting-started)
- [Estrutura do Projeto](#project-structure)
- [Decisões de Arquitetura](#architecture-decisions)
- [API Endpoints](#api-endpoints)
- [Regras de Desconto](#discount-rules)
- [Rodando os Testes](#running-the-tests)

---

## Início

### Pré requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- Git

### Instalação

```bash
git clone https://github.com/your-username/good-hamburger-api.git
cd good-hamburger-api
```

### Rodando a API

```bash
dotnet run --project src/GoodHamburger.API
```

A Api estará disponível em `http://localhost:5133`.  
E a documentação via Swagger em `http://localhost:5133/swagger`.

> Um banco de dados SQLite será criado automaticamente ao buildar o projeto, garantindo que não seja necessária nenhuma configuração de banco de dados. Para um teste pequeno e rápido assim, preferi manter simples nesse quesito.

### Rodando o Frontend

Abra uma nova janela no terminal:

```bash
dotnet run --project src/GoodHamburger.Web
```

O Frontend estará disponível em `http://localhost:5136`.

### Rodando os Testes

```bash
dotnet test
```

---

## 📁 Estrutura do Projeto
```GoodHamburguer/
├── src/
│   ├── GoodHamburger.Domain/         # Entities, business rules, interfaces
│   ├── GoodHamburger.Application/    # Use cases, DTOs, mappings
│   ├── GoodHamburger.Infrastructure/ # EF Core, repositories, SQLite
│   └── GoodHamburger.API/            # Controllers, Swagger, Program.cs
│   └── GoodHamburger.Web/            # Blazor frontend
└── tests/
└── GoodHamburger.Tests/          # xUnit tests
```
---

## 🏛️ Decisões de Arquitetura

### Clean Architecture

O projeto é estruturado em torno dos princípios de Clean Architecture, com regras restritas de dependência:
Domain ← Application ← Infrastructure ← API. Decidi seguir nessa linha pois pelo que pude pesquisar sobre a STgenetics, a empresa é bem grande e essa escolha de arquitetura possibilita que as regras de negócio fiquem isoladas, melhorando a manutenibilidade e escalabilidade.

### Inglês como base

Todo o código, commits (os quais utilizei de boas práticas de git) e documentação estão todos em inglês. Novamente, senod a STgen uma grande e internacional empresa, faz sentido que times de diferentes localidades precisem colaborar pelo menos eventualmente. Isso garante entendimento amplo por todos.

### Result ao invés de Exceptions

Operações de negócio retornam um objeto Result<T> em vez de lançar exceptions para controle de fluxo:

```csharp
public Result AddItem(MenuItem item)
{
    if (_items.Any(i => i.Type == item.Type))
        return Result.Failure("Order already contains an item of this type.");

    _items.Add(item);
    return Result.Success();
}
```

### Use Cases Instead of Fat Services

Talvez uma decisão controversa. aqui cada use case tem seu próprio arquivo, evitando arquivos gigantes.

### EF Core e SQLite

O arquivo do banco é criado automaticamente no primeiro startup via EF Core migrations. Em um cenário de produção, trocar para PostgreSQL ou SQL Server exigiria apenas a mudança da connection string e um pacote NuGet diferente

### In-Memory Menu Repository

Os itens do menu são fixos por especificação e não precisam ser persistidos.

### Blazor WebAssembly Frontend

Apesar de não ter experiência prévia com o Blazor, decidi arriscar e implementar e me diverti bastante. Me lembrou muito como o Blade do Laravel funciona, o que me trouxe certa familiaridade. Aqui, temos um backend e um frontend desacoplados, se comunicando com nossa API através de chamadas HTTP.

---

## ❌ What Was Left Out & Why

### Authentication & Authorization

Como não era requisitado nem como opcional, decidi deixar de fora qualquer tipo de autenticação para evitar complexidade.

### Pagination

Paginação não foi implementada por se tratar de um sistema pequeno e conciso, com poucos recursos e poucos dados. Em sistemas com mais informações, um sistema de paginação com certeza se faria necessário.

### Integration Tests

Decidi manter apenas os testes unitários focados nas regras de negócio.\
---

## 📡 API Endpoints

| Método | Endpoint | Descrição |
|--------|----------|-------------|
| `GET` | `/api/menu` | Retorna todos os produtos |
| `GET` | `/api/orders` | Retorna todos os pedidos |
| `GET` | `/api/orders/{id}` | Retorna um pedido específico pelo id |
| `POST` | `/api/orders` | Cria um novo pedido |
| `PUT` | `/api/orders/{id}` | Atualiza um pedido |
| `DELETE` | `/api/orders/{id}` | Exclui um pedido |

### Exemplo: Criando um pedido

```json
POST /api/orders
Content-Type: application/json

{
  "menuItemIds": [1, 4, 6]
}
```

```json
HTTP 201 Created

{
  "id": "a3f1c2d4-...",
  "createdAt": "2026-04-22T14:30:00Z",
  "items": [
    { "id": 1, "name": "CheeseBurger", "price": 5.00, "type": "Sandwich" },
    { "id": 4, "name": "French fries", "price": 2.00, "type": "Side" },
    { "id": 6, "name": "Lemon soda",   "price": 2.50, "type": "Drink" }
  ],
  "subtotal": 9.50,
  "discountPercentage": 0.20,
  "discountAmount": 1.90,
  "total": 7.60
}
```

---

## 🏷️ Regras de Descontos

| Combinação | Desconto |
|-------------|----------|
| Sandwich + Side + Drink | 20% |
| Sandwich + Drink | 15% |
| Sandwich + Side | 10% |
| Any other combination | None |

Cada pedido aceita apenas um item por tipo. Tentar adicionar dois sanduíches, dois acompanhamentos ou duas bebidas é bloqueado no frontend e também retorna um 400 Bad Request com uma mensagem de erro caso o usuário tente burlar com um request via postman, por exemplo.
---

## 🧪 Rodando os Testes

```bash
dotnet test
```

Os testes cobrem:
- Todas as combinações de regras de descontos
- Tipo de produto duplicado
- Valores corretos
- Adicionar e excluir itens de um pedido