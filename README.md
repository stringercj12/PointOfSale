# PointOfSale

Um sistema de PDV (Ponto de Venda) desenvolvido em .NET 10 com arquitetura em camadas. O projeto está organizado em uma API web ASP.NET Core e módulos responsáveis por aplicação, comunicação, domínio e infraestrutura.

## Visão geral

O sistema tem como foco principal a gestão de usuários e a estrutura de backend necessária para um PDV moderno. A solução está preparada para evoluir com serviços de produtos, pedidos e pagamentos, embora o foco atual seja a criação e validação de usuários.

### Camadas do projeto

- `src/PointOfSale.API`: API REST em ASP.NET Core com endpoints e pipeline de middleware.
- `src/PointOfSale.Application`: regras de negócio e serviços da aplicação, incluindo validação com FluentValidation.
- `src/PointOfSale.Communication`: modelos de requisição e resposta, assim como enums de comunicação entre camadas.
- `src/PointOfSale.Domain`: entidades de domínio e regras de negócio específicas, como `User` e `Role`.
- `src/PointOfSale.Infra`: persistência com Entity Framework Core e acesso a dados via repositório.

## Tecnologias

- .NET 10
- ASP.NET Core Web API
- Entity Framework Core
- MySQL (via provider `MySql.EntityFrameworkCore`)
- FluentValidation
- BCrypt para hash de senha
- Swagger/OpenAPI para documentação da API em desenvolvimento

## Funcionalidades atuais

- Criação de usuários via endpoint POST
- Validação de entrada para campos obrigatórios, e-mail e tamanho de strings
- Verificação de duplicidade de e-mail e nome de usuário
- Hash de senha antes da persistência
- Tratamento global de exceções customizadas

## Estrutura de endpoint

- `POST /api/user`
  - Cria um novo usuário.
  - Body esperado: `Name`, `Username`, `Email`, `Password`, `Role`, `Active`, `ProfileImage` (opcional).

> Observação: o endpoint padrão de `WeatherForecast` é mantido apenas como exemplo de scaffold.

## Configuração e execução

1. Abra o diretório do projeto:

```bash
cd c:\Workspace\csharp\PointOfSale
```

2. Configure a string de conexão MySQL em `src/PointOfSale.API/appsettings.json` ou `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=PointOfSaleDb;User=root;Password=senha;"
}
```

3. Execute a API:

```bash
dotnet run --project src/PointOfSale.API/PointOfSale.API.csproj
```

4. Acesse o Swagger em modo de desenvolvimento:

```
https://localhost:<porta>/swagger
```

## Observações

- A camada de infraestrutura já possui `UserRepository` e `UnitOfWork` implementados.
- Há referências comentadas para futuros serviços de `Product`, `Payment` e `Order`.
- O projeto está pronto para receber novas funcionalidades de PDV, como gerenciamento de produtos, vendas, carrinho e fechamento de venda.
