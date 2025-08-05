
# FIAP.CloudGames 🎮

Aplicação desenvolvida como parte inicial do desafio da pós-graduação em Arquitetura de Sistemas .NET da FIAP. Plataforma fictícia de jogos eletrônicos em que usuários podem se cadastrar e adquirir jogos, muitas vezes em promoção.

## 📚 Descrição

FIAP.CloudGames é uma API REST desenvolvida com .NET 8, focada em boas práticas de arquitetura, testes e segurança. A primeira fase contempla a criação e autenticação de usuários, com controle de acesso baseado em permissões.

---

## 🧱 Tecnologias e padrões aplicados

- **.NET 8** Web API com Controllers
- **Arquitetura em camadas** (API, Application, Domain, Infrastructure, Tests)
- **DDD (Domain-Driven Design)** com uso de:
  - Entidades, Value Objects, Aggregates
  - Domain Services e Repositories
- **TDD** com **xUnit** + **Moq** para ampla cobertura de testes unitários
- **Result Pattern<T>** para tratamento de erros de forma mais elegante
- **Autenticação com JWT**
-  **Bcrypt** para hashing de senhas
- **Middlewares personalizados** para tratamento de erros e logging
-  **Swagger** para documentação dinâmica da API
-  **Entity Framework Core** ORM

---

## 🔐 Funcionalidades até o momento

- CRUD de usuários com validações robustas
- Autenticação JWT
- Controle de acesso por perfis/roles

---

## 🚀 Executar localmente

1. Clone o projeto:
   ```bash
   git clone https://github.com/betogzo/FIAPCloudGames.git
   cd FIAP.CloudGames`   
2. Configure ConnectionString e JWT Key nos arquivos de appsettings.json

3. Navegue até o projeto "API": `cd FIAP.CloudGames.API`

4. `dotnet restore`
5. `dotnet ef database update`
6. `dotnet run`

## 🧪 Executar testes

Executar `dotnet test` na raíz do diretório clonado.
