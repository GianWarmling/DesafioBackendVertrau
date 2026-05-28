# DESAFIO BACKEND VERTRAU

API RESTful para gerenciamento de usuários, desenvolvida com **ASP.NET Core 8**, banco de dados **Neon (PostgreSQL)** e deploy no **Render**.

## Endpoints

| Método | Rota | Descrição |
|--------|------|-----------|
| `GET` | `/usuarios` | Listar usuários (filtros e paginação) |
| `GET` | `/usuarios/{id}` | Buscar usuário por ID |
| `POST` | `/usuarios` | Cadastrar usuário |
| `PUT` | `/usuarios/{id}` | Atualizar usuário |
| `DELETE` | `/usuarios/{id}` | Remover usuário |

## Campos

| Campo | Tipo | Obrigatório |
|-------|------|-------------|
| `id` | `long` | Gerado automaticamente |
| `nome` | `string` | Sim |
| `sobrenome` | `string` | Sim |
| `email` | `string` | Sim — deve ser único |
| `genero` | `enum` | Sim — `Masculino`, `Feminino`, `Outro` |
| `dataNascimento` | `DateTime` | Não — não pode ser no futuro |

## Enum de Gênero

O campo `genero` utiliza uma enum com os seguintes valores aceitos:

- `0` — Masculino
- `1` — Feminino
- `2` — Outro

## Banco de Dados

O projeto utiliza o [Neon](https://neon.tech), um PostgreSQL serverless. Para configurar:

1. Crie um banco em [neon.tech](https://neon.tech)
2. Copie a connection string e defina na variável `ConnectionStrings__Default`
3. Rode as migrations: `dotnet ef database update`

## Validações

- E-mail deve ser único.
- Data de nascimento não pode ser no futuro.
- Gênero deve ser `Masculino`, `Feminino` ou `Outro`.

## Como rodar com Docker

```bash
docker build -t usuarios-api .
docker run -p 8080:8080 \
  -e ConnectionStrings__DefaultConnection="sua_connection_string_neon" \
  usuarios-api
```

## Variáveis de ambiente

| Variável | Descrição |
|----------|-----------|
| `ConnectionStrings__Default` | Connection string do Neon |
| `ASPNETCORE_ENVIRONMENT` | `Development` ou `Production` |

