# 🎮 FIAP.CloudGames

Aplicação desenvolvida como parte do **desafio da pós-graduação em Arquitetura de Sistemas .NET** da **FIAP**.

Plataforma fictícia de jogos eletrônicos, onde usuários podem se cadastrar, realizar autenticação e adquirir jogos (em fases futuras). Esta aplicação segue padrões modernos de arquitetura, testes, segurança, **integração contínua**, **entrega contínua** e **deploy em nuvem**.

---

## 📚 Descrição

FIAP.CloudGames é uma **API REST** desenvolvida com **.NET 8**, utilizando os melhores padrões e práticas da engenharia de software moderna.

---

## 🧱 Tecnologias e Padrões Aplicados

- ✅ .NET 8 - Web API com Controllers
- ✅ Arquitetura em Camadas (API, Application, Domain, Infrastructure, Tests)
- ✅ **DDD** (Domain-Driven Design):
  - Entidades, Value Objects, Aggregates
  - Domain Services e Repositórios
- ✅ **TDD** com **xUnit** + **Moq**
- ✅ **Result Pattern<T>** para retorno e tratamento de erros
- ✅ **Autenticação JWT** com controle de acesso por perfis
- ✅ Hash de senhas com **BCrypt**
- ✅ **Middlewares personalizados** para logging e erros
- ✅ **Entity Framework Core** com SQL Server
- ✅ **Swagger** para documentação dinâmica
- ✅ **Docker** com build e execução via containers
- ✅ **Deploy automático via Azure App Service (Docker Container)**
- ✅ **CI/CD com GitHub Actions**
- ✅ **Banco de Dados em Nuvem (Azure SQL Server)**

---

## 🛠️ Funcionalidades

- 🔐 CRUD de usuários com validações robustas
- 🔐 Login com autenticação JWT
- 🔐 Autorização por perfis
- 🧪 Testes unitários com alta cobertura
- ☁️ Deploy e execução 100% funcional na Azure

---

## 🐳 Containerização

A aplicação é empacotada em um container Docker, permitindo portabilidade e escalabilidade:

```bash
docker build -t betogzo/fiapcloudgames .
docker run -p 5000:80 betogzo/fiapcloudgames
```

- A imagem é publicada no Docker Hub: [betogzo/fiapcloudgames](https://hub.docker.com/r/betogzo/fiapcloudgames)
- Executada na Azure via App Service (Linux + Docker Container)

---

## ☁️ Deploy Automatizado (CI/CD)

O pipeline de CI/CD foi implementado com **GitHub Actions**, em dois arquivos distintos:

### 🔹 CI - Integração Contínua
- Executa build e testes a cada `push` ou `pull request`
- Garante que o código esteja íntegro antes de qualquer deploy

### 🔸 CD - Entrega Contínua
- Após o `push` na `main`, o CD:
  - Faz o build da imagem Docker
  - Publica a imagem no Docker Hub
  - A Azure detecta a nova imagem e realiza o deploy automático

> Todas as secrets (como connection strings e credenciais do Docker) estão protegidas no GitHub Secrets.

---

## 📊 Monitoramento

A aplicação conta com **monitoramento ativo via [New Relic](https://newrelic.com/)**:

- Monitoramento de disponibilidade
- Coleta de métricas de uso e desempenho
- Logs e rastreamento de erros
- Agente instalado no container da aplicação

---

## 🚀 Executar localmente

### Pré-requisitos
- .NET 8 SDK
- Docker (opcional)

### Passos

```bash
git clone https://github.com/betogzo/FIAPCloudGames.git
cd FIAPCloudGames
```

1. Configure o arquivo `appsettings.Development.json` com:
   - `"ConnectionStrings:DefaultConnection"` para sua instância local do SQL Server
   - `"Jwt:Key"` com uma chave segura

2. Crie e aplique o banco de dados:
```bash
cd FIAP.CloudGames.API
dotnet ef database update
dotnet run
```

> Acesse o Swagger em: [http://localhost:5000/swagger](http://localhost:5000/swagger)

---

## 🧪 Executar Testes

Na raiz do projeto:

```bash
dotnet test
```

Todos os testes são **unitários e independentes do banco de dados**.

---

## 📦 Publicação de Imagens (Docker Hub)

A imagem é automaticamente enviada para:

🔗 [https://hub.docker.com/r/betogzo/fiapcloudgames](https://hub.docker.com/r/betogzo/fiapcloudgames)

---

## ☁️ Aplicação Online

A aplicação está publicada e acessível via:

🔗 [fiapcloudgamesapi-f0dhh6enfmasgpdh.centralus-01.azurewebsites.net/swagger](fiapcloudgamesapi-f0dhh6enfmasgpdh.centralus-01.azurewebsites.net/swagger)

---

## 📜 Licença

MIT License
