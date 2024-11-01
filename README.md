# Thunders.Tecnologia.Api

## Visão Geral
Thunders.Tecnologia.Api é um projeto de CRUD em .NET 8 que segue a arquitetura limpa (Clean Architecture) e o padrão CQRS utilizando o MediatR. Este projeto utiliza um único banco de dados com a tabela `Lista de Tarefas`, além de incluir testes de unidade para garantir a qualidade e as melhores práticas do código.

### Tecnologias e Ferramentas Utilizadas
- .NET 8
- Docker & Docker Compose
- Clean Architecture
- CQRS com MediatR
- Swagger para documentação de API
- xUnit, FluentAssertions e Moq para testes
- Integração contínua e entrega contínua (CI/CD) com GitHub Actions
- Controle de versão com Git

## Endpoints Disponíveis

### Person Endpoints

| Método | Endpoint               | Descrição                          |
|--------|-------------------------|------------------------------------|
| GET    | `/person/get/{id}`      | Obtém uma pessoa pelo ID          |
| GET    | `/person/get/all`       | Obtém todas as pessoas            |
| POST   | `/person/add`           | Adiciona uma nova pessoa          |
| PUT    | `/person/update/{id}`   | Atualiza uma pessoa existente     |
| DELETE | `/person/delete/{id}`   | Remove uma pessoa pelo ID         |

### Task Endpoints

| Método | Endpoint                     | Descrição                               |
|--------|-------------------------------|-----------------------------------------|
| GET    | `/task/get/{id}`              | Obtém uma tarefa pelo ID                |
| GET    | `/task/get/all/{idPerson}`    | Obtém todas as tarefas de uma pessoa    |
| POST   | `/task/add`                   | Adiciona uma nova tarefa                |
| PUT    | `/task/update/{id}`           | Atualiza uma tarefa existente           |
| DELETE | `/task/delete/{id}`           | Remove uma tarefa pelo ID               |

## Estrutura do Projeto

A estrutura do projeto segue a organização em camadas para manter a separação de responsabilidades:

- **Thunders.Tecnologia.Api**: Contém os controladores de API e a configuração de endpoints.
- **Thunders.Tecnologia.Application**: Contém os serviços e casos de uso da aplicação, utilizando MediatR para consultas e comandos.
- **Thunders.Tecnologia.Domain**: Define as entidades, interfaces e lógica de domínio.
- **Thunders.Tecnologia.Infrastructure**: Implementa os repositórios e o acesso a dados.

## Configuração de Injeção de Dependência

A configuração da injeção de dependência utiliza extensões personalizadas para registrar todos os serviços e repositórios necessários, seguindo as melhores práticas de organização.

## Documentação da API com Swagger

A API está documentada com o Swagger para facilitar o entendimento e a interação com os endpoints. A documentação pode ser acessada na rota `/swagger` após iniciar a aplicação.

## Testes de Unidade

Os testes foram implementados utilizando **xUnit** e **FluentAssertions** para garantir a qualidade do código e cobrir os casos de uso dos serviços e repositórios.

## Requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started)

## Configuração do Banco de Dados

A aplicação utiliza o PostgreSQL como banco de dados. A configuração é feita através do arquivo `docker-compose.yml`, onde o banco de dados é configurado com um volume persistente.

## Execução

Para executar a aplicação, utilize o Docker Compose. No terminal, navegue até o diretório do projeto e execute:

```docker
docker-compose up --build
```

### Docker Compose
```docker
services:
  api:
    build:
      context: .
      dockerfile: Thunders.Tecnologia.Api/Dockerfile
    ports:
      - "5000:5000"      
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ConnectionStrings__DefaultConnection=Host=postgresdb;Database=thundersdb;Username=postgres;Password=yourpassword
    depends_on:
      - postgresdb

  postgresdb:
    image: postgres:latest
    environment:
      POSTGRES_USER: postgres
      POSTGRES_PASSWORD: yourpassword
      POSTGRES_DB: thundersdb
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

volumes:
  postgres_data:
```

### Dockerfile
```docker
# Use the official .NET 8 SDK as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000
#EXPOSE 443

# Install OpenSSL if it's not already installed in the image
RUN apt-get update && apt-get install -y openssl

# Generate a self-signed certificate with OpenSSL
RUN mkdir /https && \
    openssl req -x509 -nodes -days 365 \
    -subj "/CN=localhost" \
    -addext "subjectAltName=DNS:localhost" \
    -newkey rsa:2048 -keyout /https/localhost.key -out /https/localhost.crt && \
    openssl pkcs12 -export -out /https/localhost.pfx -inkey /https/localhost.key -in /https/localhost.crt -password pass:yourpassword

# Set environment variables to configure HTTPS with the generated certificate
#ENV ASPNETCORE_URLS=https://+:443;http://+:5000
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/https/localhost.pfx
ENV ASPNETCORE_Kestrel__Certificates__Default__Password=yourpassword

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy .csproj and restore as distinct layers
COPY ["Thunders.Tecnologia.Api/Thunders.Tecnologia.Api.csproj", "Thunders.Tecnologia.Api/"]
COPY ["Thunders.Tecnologia.Application/Thunders.Tecnologia.Application.csproj", "Thunders.Tecnologia.Application/"]
COPY ["Thunders.Tecnologia.Domain/Thunders.Tecnologia.Domain.csproj", "Thunders.Tecnologia.Domain/"]
COPY ["Thunders.Tecnologia.Infrastructure/Thunders.Tecnologia.Infrastructure.csproj", "Thunders.Tecnologia.Infrastructure/"]

# Restore dependencies
RUN dotnet restore "Thunders.Tecnologia.Api/Thunders.Tecnologia.Api.csproj"

# Copy all files and build the app
COPY . .
WORKDIR "/src/Thunders.Tecnologia.Api"
RUN dotnet build "Thunders.Tecnologia.Api.csproj" -c Release -o /app/build

# Publish the app
FROM build AS publish
RUN dotnet publish "Thunders.Tecnologia.Api.csproj" -c Release -o /app/publish

# Final stage: runtime environment
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Set entry point
ENTRYPOINT ["dotnet", "Thunders.Tecnologia.Api.dll"]
```

A aplicação estará disponível em `http://localhost:5000`.

## Explicação dos Endpoints da API (Exemplo: Pessoa)

A seguir estão os principais endpoints disponíveis na API:

### Criar Pessoa

- **POST** `/person/add`
- **Body**: `{"name": "Nome", "email": "email@exemplo.com", "dateOfBirth": "2000-01-01"}`
- **Response**: `201 Created` com o ID da pessoa criada.

### Obter Pessoa por ID

- **GET** `/person/get/{id:guid}`
- **Response**: `200 OK` com os dados da pessoa ou `404 Not Found` se não encontrada.

### Obter Todas as Pessoas

- **GET** `/person/get/all`
- **Response**: `200 OK` com a lista de todas as pessoas.

### Atualizar Pessoa

- **PUT** `/person/update/{id:guid}`
- **Body**: `{"id": "{id}", "name": "Novo Nome", "email": "novoemail@exemplo.com", "dateOfBirth": "2000-01-01"}`
- **Response**: `202 Accepted` se a atualização for bem-sucedida ou `404 Not Found` se a pessoa não for encontrada.

### Deletar Pessoa

- **DELETE** `/person/delete/{id:guid}`
- **Response**: `202 Accepted` se a deleção for bem-sucedida ou `404 Not Found` se a pessoa não for encontrada.

## Implementação

### Handlers

Os handlers são responsáveis por processar os comandos e consultas. Por exemplo, o `CreatePersonCommandHandler` manipula a criação de uma nova pessoa:

```csharp
public class CreatePersonCommandHandler : IRequestHandler<CreatePersonCommand, Guid>
{
    private readonly IMapper _mapper;
    private readonly IPersonService _service;

    public CreatePersonCommandHandler(IMapper mapper, IPersonService service)
    {
        _mapper = mapper;
        _service = service;
    }

    public async Task<Guid> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var create = _mapper.Map<PersonDto>(request);
        var id = await _service.AddAsync(create);
        return id;
    }
}
```

### Repositórios

Os repositórios são responsáveis pela interação com o banco de dados. O `PersonRepository` é um exemplo de repositório que lida com as operações CRUD para a entidade `Person`:

```csharp
public class PersonRepository : IPersonRepository
{
    private readonly AppDbContext _context;

    public PersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Person?> GetByIdAsync(Guid id) { ... }
    public async Task<IEnumerable<Person>> GetAllAsync() { ... }
    public async Task AddAsync(Person person) { ... }
    public async Task UpdateAsync(Person person) { ... }
    public async Task DeleteAsync(Guid id) { ... }
}
```

### Serviços

Os serviços contêm a lógica de negócios e utilizam os repositórios para acessar os dados. O `PersonService` é um exemplo de serviço que gerencia operações relacionadas a pessoas:

```csharp
public class PersonService : IPersonService
{
    private readonly ILogger<PersonService> _logger;
    private readonly IMapper _mapper;
    private readonly IPersonRepository _peopleRepository;

    public PersonService(IPersonRepository peopleRepository, IMapper mapper, ILogger<PersonService> logger)
    {
        _peopleRepository = peopleRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IEnumerable<PersonDto>> GetAllAsync() { ... }
    public async Task<PersonDto?> GetByIdAsync(Guid id) { ... }
    public async Task<Guid> AddAsync(PersonDto personDto) { ... }
    public async Task<bool> UpdateAsync(PersonDto personDto) { ... }
    public async Task<bool> DeleteAsync(Guid id) { ... }
}
```

### Logging

O logging é implementado através da injeção de dependência do `ILogger` nos serviços e handlers. Isso permite registrar informações sobre operações, como adições, atualizações e exclusões de pessoas.

## Licença

Este projeto está licenciado sob a MIT License. Veja o arquivo LICENSE para mais detalhes.