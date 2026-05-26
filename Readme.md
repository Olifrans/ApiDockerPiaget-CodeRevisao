
# API Escola Piaget - DevOps & Cloud Computing

## ?? Descrição do Projeto

Projeto consiste em uma **Web API REST** desenvolvida em .NET 8 para gerenciamento de uma escola, contendo as entidades **Escola**, **Aluno** e **Professor** com seus relacionamentos.

O projeto foi desenvolvido como **trabalho prático** do curso de **DevOps**, com forte ênfase em containerização, automação, boas práticas de desenvolvimento e preparação para deploy em nuvem.



## ?? Objetivos Alinhados ao Curso

- **Módulo 1**: Aplicação dos conceitos básicos de DevOps, Cultura DevOps (colaboração, automação e CI/CD).
- **Módulo 2**: Utilização de Cloud Computing (IaaS) através de containers.
- **Módulo 3**: Prática com **Docker** (Aula 11), containerização e preparação para Kubernetes (Aula 12) e Pipeline CI/CD (Aula 13).
- **Módulo 4**: Aplicação de boas práticas de segurança em aplicações DevOps.



## ??? Tecnologias Utilizadas

| Tecnologia                    | Versão     | Propósito |
|------------------------------|------------|---------|
| .NET                         | 8.0        | Framework principal |
| ASP.NET Core Web API         | 8.0        | Criação da API REST |
| Entity Framework Core        | 8.0        | ORM e persistência |
| SQL Server                   | 2022       | Banco de dados |
| AutoMapper                   | -          | Mapeamento de objetos |
| FluentValidation             | -          | Validação de DTOs |
| Swagger / OpenAPI            | -          | Documentação da API |
| Docker                       | -          | Containerização |
| Docker Compose               | -          | Orquestração de containers |
| Health Checks                | -          | Monitoramento de saúde |
| CORS                         | -          | Comunicação com frontends |



## ? Funcionalidades

- CRUD completo para **Escolas**, **Alunos** e **Professores**
- Relacionamentos um-para-muitos (Escola ? Alunos/Professores)
- Validação avançada com FluentValidation
- Mapeamento automático com AutoMapper
- Tratamento global de exceções
- Health Checks personalizados
- Documentação interativa com Swagger
- Totalmente containerizado com Docker



## ??? Arquitetura do Projeto

### Estrutura de Pastas

ApiDockerPiaget/
??? Controllers/
??? Data/
??? DTOs/
??? Models/
??? Mappings/
??? Validators/
??? HealthChecks/
??? Middleware/
??? Properties/
??? appsettings.json
??? Dockerfile
??? docker-compose.yml
??? Program.cs


### Diagrama UML (Contextual)
classDiagram
    class Escola {
        +int Id
        +string Nome
        +string Endereco
        +string Cidade
        +string Telefone
    }
    class Aluno {
        +int Id
        +string Nome
        +string Email
        +DateTime DataNascimento
        +string Serie
        +int EscolaId
    }
    class Professor {
        +int Id
        +string Nome
        +string Email
        +string Disciplina
        +string Titulacao
        +int EscolaId
    }

    Escola "1" --> "N" Aluno
    Escola "1" --> "N" Professor




## ?? Como Executar o Projeto

### 1. Local (sem Docker)
dotnet restore
dotnet build
dotnet run


Acesse: `http://localhost:5254/swagger`

### 2. Com Docker (Recomendado - Aula 11)
docker-compose up --build


A API estará disponível em: `http://localhost:8080`
Health Checks:
- `http://localhost:8080/health`
- `http://localhost:8080/health/ready`



## ?? Endpoints Principais

| Método | Endpoint              | Descrição |
|--------|-----------------------|---------|
| GET    | `/api/Escolas`        | Listar todas as escolas |
| GET    | `/api/Alunos`         | Listar todos os alunos |
| GET    | `/api/Professores`    | Listar todos os professores |
| POST   | `/api/Alunos`         | Cadastrar aluno |
| PUT    | `/api/Escolas/{id}`   | Atualizar escola |



## ??? Boas Práticas de Segurança (Módulo 4)

- Validação de entrada com FluentValidation
- Tratamento global de exceções
- CORS configurado
- Health Checks para monitoramento
- Uso de DTOs para não expor entidades diretamente



## ?? DevOps & Cloud Computing Aplicados

- **Infraestrutura como Código (IaC)**: `docker-compose.yml`
- **Containerização**: Docker + Multi-stage build
- **Automação**: Preparado para CI/CD (GitHub Actions / Azure DevOps)
- **Cultura DevOps**: Separação clara de responsabilidades, colaboração entre Dev e Ops
- **Cloud Ready**: Fácil deploy em Azure, AWS ou GCP (IaaS / PaaS)


## ?? Próximos Passos (Melhorias Futuras)

- Implementação de **Kubernetes** (Aula 12)
- Pipeline completo de **CI/CD** (Aula 13)
- Deploy na nuvem (Azure App Service ou AWS ECS)
- Autenticação e Autorização (JWT)
- Logging centralizado (Serilog + Seq)
- Testes unitários e de integração




# INSERTs Completo (SQL Server)
-- =============================================
-- INSERIR ESCOLAS primeiro
-- =============================================

INSERT INTO Escolas (Nome, Endereco, Cidade, Telefone) VALUES 
('Escola Piaget', 'Rua das Flores, 123', 'São Paulo', '(11) 98765-4321'),
('Colégio Einstein', 'Av. Paulista, 1500', 'São Paulo', '(11) 3456-7890'),
('Instituto Montessori', 'Rua das Acácias, 450', 'Campinas', '(19) 98765-1234');

-- =============================================
-- INSERIR ALUNOS
-- =============================================

INSERT INTO Alunos (Nome, Email, DataNascimento, Serie, EscolaId) VALUES 
('João Silva', 'joao.silva@email.com', '2015-05-12', '6º Ano', 1),
('Maria Oliveira', 'maria.oliveira@email.com', '2014-08-25', '7º Ano', 1),
('Pedro Santos', 'pedro.santos@email.com', '2016-01-10', '5º Ano', 1),
('Ana Clara Mendes', 'ana.mendes@email.com', '2013-11-30', '8º Ano', 2),
('Lucas Ferreira', 'lucas.ferreira@email.com', '2015-03-18', '6º Ano', 2);

-- =============================================
-- INSERIR PROFESSORES
-- =============================================

INSERT INTO Professores (Nome, Email, Disciplina, Titulacao, EscolaId) VALUES 
('Prof. Carlos Almeida', 'carlos.almeida@escola.com', 'Matemática', 'Mestre', 1),
('Profª Juliana Costa', 'juliana.costa@escola.com', 'Português', 'Doutora', 1),
('Prof. Roberto Mendes', 'roberto.mendes@escola.com', 'História', 'Especialista', 1),
('Profª Fernanda Lima', 'fernanda.lima@escola.com', 'Ciências', 'Mestre', 2),
('Prof. Marcos Silva', 'marcos.silva@escola.com', 'Inglês', 'Especialista', 2);






# Verificar os dados inseridos:

-- Ver tudo
SELECT * FROM Escolas;
SELECT * FROM Alunos;
SELECT * FROM Professores;

-- Ver com relacionamentos
SELECT 
    e.Nome AS Escola,
    a.Nome AS Aluno,
    a.Serie
FROM Escolas e
LEFT JOIN Alunos a ON a.EscolaId = e.Id;

SELECT 
    e.Nome AS Escola,
    p.Nome AS Professor,
    p.Disciplina
FROM Escolas e
LEFT JOIN Professores p ON p.EscolaId = e.Id;





