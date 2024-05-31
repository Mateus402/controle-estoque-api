# Controle de Estoque - API

Esta é uma API de Controle de Estoque desenvolvida em ASP.NET Core com NHibernate, AutoMapper e PostgreSQL. A API permite a gestão eficiente de produtos com suporte a transações e verificação de duplicatas.

## Índice

1. [Tecnologias Utilizadas](#tecnologias-utilizadas)
2. [Configuração do Projeto](#configuração-do-projeto)
   - [Pré-requisitos](#pré-requisitos)
   - [Clonando o Repositório](#clonando-o-repositório)
   - [Configuração do Docker](#configuração-do-docker)
3. [Configuração do NHibernate](#configuração-do-nhibernate)
4. [Estrutura do Projeto](#estrutura-do-projeto)
5. [Endpoints da API](#endpoints-da-api)
   - [Produtos](#produtos)
6. [Como Executar](#como-executar)
7. [Autor](#autor)

## Tecnologias Utilizadas

- ASP.NET Core
- NHibernate
- AutoMapper
- PostgreSQL
- Docker

## Configuração do Projeto

### Pré-requisitos

- .NET 6.0 SDK ou superior
- Docker
- Docker Compose

### Clonando o Repositório

```bash
git clone https://github.com/seu-usuario/controle-estoque-api.git
cd controle-estoque-api
```

### Configuração do Docker

No arquivo `docker-compose.yml`:

```yaml
version: '3.7'

services:
  postgres:
    image: bitnami/postgresql:latest
    ports:
      - '5432:5432'
    environment:
      - POSTGRES_USER=docker
      - POSTGRES_PASSWORD=docker
      - POSTGRES_DB=polls
    volumes:
      - polls_pg_data:/bitnami/postgresql

  redis:
    image: bitnami/redis:latest
    environment:
      - ALLOW_EMPTY_PASSWORD=yes
    ports:
      - '6379:6379'
    volumes:
      - 'polls_redis_data:/bitnami/redis/data'

volumes:
  polls_pg_data:
  polls_redis_data:
```

Para iniciar os contêineres:

```bash
docker-compose up -d
```

## Configuração do NHibernate

### Arquivo `NHibernateHelper.cs`

```csharp
using ControleEstoqueApi.Mappings;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

public class NHibernateHelper
{
  private static ISessionFactory _sessionFactory;

  public static ISessionFactory SessionFactory
  {
    get
    {
      if (_sessionFactory == null)
      {
        InitializeSessionFactory();
      }
      return _sessionFactory;
    }
  }

  private static void InitializeSessionFactory()
  {
    _sessionFactory = Fluently.Configure()
        .Database(PostgreSQLConfiguration.Standard
            .ConnectionString(cs => cs
                .Host("localhost")
                .Port(5432)
                .Database("polls")
                .Username("docker")
                .Password("docker"))
            .ShowSql())
        .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateHelper>())
        .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
        .BuildSessionFactory();
  }

  public static NHibernate.ISession OpenSession()
  {
    return SessionFactory.OpenSession();
  }
}
```

## Estrutura do Projeto

```
ControleEstoqueApi/
│
├── Controllers/
│   ├── ProdutoController.cs
│
├── Entities/
│   ├── Produto.cs
│
├── Mappings/
│   ├── ProdutoMap.cs
│
├── Profiles/
│   ├── CadastroProfile.cs
│
├── Repositories/
│   ├── ProdutoRepository.cs
│
├── Services/
│   ├── ProdutoService.cs
│
├── Program.cs
├── Startup.cs
└── NHibernateHelper.cs
```

## Endpoints da API

### Produtos

#### Adicionar Produto

```http
POST /api/produtos
```

**Request Body:**

```json
{
  "nome": "Produto A",
  "preco": 100.0,
  "quantidade": 10
}
```

**Responses:**

- `200 OK` - Produto adicionado com sucesso.
- `400 Bad Request` - Produto já existe.
- `500 Internal Server Error` - Erro no servidor.

#### Listar Produtos

```http
GET /api/produtos
```

**Query Parameters:**

- `skip` (int) - Número de produtos a pular.
- `take` (int) - Número de produtos a retornar.

**Responses:**

- `200 OK` - Lista de produtos.
- `500 Internal Server Error` - Erro no servidor.

#### Obter Produto por ID

```http
GET /api/produtos/{id}
```

**Responses:**

- `200 OK` - Produto encontrado.
- `404 Not Found` - Produto não encontrado.
- `500 Internal Server Error` - Erro no servidor.

## Como Executar

1. Clone o repositório e navegue até o diretório do projeto.
2. Configure o Docker e inicie os contêineres.
3. Abra o projeto no Visual Studio ou VS Code.
4. Execute o projeto.

```bash
dotnet run
```

5. Acesse a documentação da API no Swagger:

```
https://localhost:7161/swagger/index.html
```

## Autor

- **Mateus Stangherlin** - [LinkedIn](https://www.linkedin.com/in/mateus-stangherlin-47a1b1230/)
