
# Thunders.Tecnologia API

Este projeto é uma API RESTful para gerenciar pessoas, implementado em .NET 8 com Docker. Utiliza o padrão de arquitetura limpa (Clean Architecture) e CQRS (Command Query Responsibility Segregation) com MediatR. O banco de dados utilizado é o PostgreSQL.

## Estrutura do Projeto

O projeto está dividido nas seguintes camadas:

- **Api**: Contém os controladores da API.
- **Domain**: Define as entidades e regras de negócio.
- **Application**: Contém os comandos, consultas e DTOs (Data Transfer Objects).
- **Infrastructure**: Implementa o contexto do banco de dados e repositórios.

## Tecnologias Utilizadas

- **.NET 8**
- **Docker**
- **PostgreSQL**
- **MediatR**
- **Serilog** para logging
- **FluentValidation** para validação de dados

## Instruções para Execução

### Pré-requisitos

- Docker e Docker Compose instalados na máquina.

### Executando o Projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu_usuario/thunders-tech.git
   cd thunders-tech
   ```

2. Certifique-se de que o Docker está rodando.

3. Execute o Docker Compose para iniciar os serviços:
   ```bash
   docker-compose up --build
   ```

4. Acesse a API em `http://localhost:5000` ou `https://localhost:443`.

5. A documentação do Swagger pode ser acessada em `http://localhost:5000/swagger`.

### Executando Migrations

Para aplicar as migrations automáticas no banco de dados PostgreSQL, basta executar o projeto. As migrations serão aplicadas ao iniciar a aplicação.

## Estrutura de Dados

### PersonDto

```csharp
public class PersonDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required DateTime DateOfBirth { get; set; }
}
```

## Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou pull requests.

## Licença

Este projeto é licenciado sob a MIT License.