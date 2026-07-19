# Sistema de Controle de Gastos Residenciais

<div align="center">
  <img src="https://img.shields.io/badge/status-em%20desenvolvimento-F59E0B?style=for-the-badge" alt="Status: Em desenvolvimento">
  <img src="https://img.shields.io/badge/C%23-.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="C# e .NET">
  <img src="https://img.shields.io/badge/React-TypeScript-3178C6?style=for-the-badge&logo=react&logoColor=white" alt="React e TypeScript">
  <img src="https://img.shields.io/badge/PostgreSQL-Database-4169E1?style=for-the-badge&logo=postgresql&logoColor=white" alt="PostgreSQL">
</div>

Sistema web para controle de gastos residenciais, permitindo o cadastro de pessoas e transações financeiras, além da consulta de receitas, despesas e saldos individuais e gerais. A aplicação também contará com autenticação de usuários por meio de contas associadas às pessoas cadastradas.

> **Versão atual:** `v0.0.1`<br>
> **Estágio:** Em desenvolvimento

## Arquitetura

O backend segue uma arquitetura em camadas, separando as responsabilidades relacionadas às requisições HTTP, regras de negócio, acesso aos dados e persistência. O Entity Framework Core realiza o mapeamento das entidades e a comunicação com o PostgreSQL.

### Domínios

| Categoria    | Componentes          | Papel no sistema                                               |
| :----------- | :------------------- | :------------------------------------------------------------- |
| **Core**     | Pessoas e transações | Concentra o cadastro e o controle dos gastos residenciais      |
| **Suporte**  | Consulta de totais   | Calcula receitas, despesas, saldos individuais e totais gerais |
| **Genérico** | Conta e autenticação | Fornece acesso autenticado às funcionalidades da aplicação     |

### Documentação visual

Os diagramas abaixo registram os requisitos e a modelagem inicial do sistema.

<div align="center">
  <img src="./docs/diagrama-casos-de-uso-v0.0.1.png" width="49%" alt="Diagrama de casos de uso">
  <img src="./docs/diagrama-de-classes-v0.0.1.png" width="49%" alt="Diagrama de classes">
</div>

<div align="center">
  <img src="./docs/modelagem-banco-v0.0.1.png" width="49%" alt="Modelagem do banco de dados">
  <img src="./docs/arquitetura-aplicacao-v0.0.1.png" width="49%" alt="Arquitetura da aplicação">
</div>

## Portas e serviços

### Serviços e persistência

| Status do serviço | Status do banco | Serviço        | Porta da aplicação | Persistência | Porta local do banco | Database  |
| :---------------: | :-------------: | :------------- | :-----------------: | :----------- | :------------------: | :-------- |
|        🔴         |       🔴        | Backend .NET   |          —          | PostgreSQL   |        `5432`        | A definir |
|        🔴         |        —        | Frontend React |       `5173`        | —            |          —           | —         |

> As portas e configurações poderão ser atualizadas conforme a evolução da aplicação.

## Stack tecnológica

### Backend

- **C#:** linguagem utilizada no desenvolvimento do backend.
- **.NET:** plataforma utilizada para construção e execução da aplicação.
- **ASP.NET Core:** base para criação das APIs e processamento das requisições HTTP.
- **Entity Framework Core:** realiza o acesso, mapeamento e persistência dos dados.
- **PostgreSQL:** banco de dados relacional utilizado pela aplicação.

### Frontend

- **React:** biblioteca utilizada para construção da interface web.
- **TypeScript:** adiciona tipagem estática ao desenvolvimento do frontend.

### Infraestrutura

- **PostgreSQL:** banco de dados utilizado para persistência das pessoas, contas e transações.

## Como executar localmente

### Pré-requisitos

- .NET SDK;
- Node.js;
- PostgreSQL;
- Git.

### 1. Clone o repositório

```bash
git clone https://github.com/RaphaelMun1z/csharp-dotnet-sistema-controle-gastos-residenciais.git
````

Acesse o diretório:

```bash
cd csharp-dotnet-sistema-controle-gastos-residenciais
```

### 2. Configure o banco de dados

Configure a conexão com o PostgreSQL no arquivo `appsettings.json` de acordo com o ambiente local.

### 3. Inicie o backend

Acesse o diretório do projeto .NET e execute:

```bash
dotnet run
```

### 4. Inicie o frontend

Acesse o diretório da aplicação React, instale as dependências e execute:

```bash
npm install
npm run dev
```

### 5. Acesse a aplicação

Abra no navegador o endereço disponibilizado pelo servidor de desenvolvimento do frontend.

## Endpoints de apoio

| Recurso     | URL                     |
| :---------- | :---------------------- |
| Backend API | A definir               |
| Frontend    | `http://localhost:5173` |

## Relato de bugs

Encontrou um comportamento inesperado? [Crie uma issue no repositório](https://github.com/RaphaelMun1z/csharp-dotnet-sistema-controle-gastos-residenciais/issues/new) com uma descrição objetiva, os passos para reproduzir, o resultado esperado e, quando possível, logs ou capturas de tela. Antes de abrir uma nova issue, verifique se o problema já foi relatado.

## Interface da aplicação

<div align="center">
  <img src="./preview-frontend/preview1.png" width="48%" alt="Interface da aplicação">
  &nbsp;&nbsp;
  <img src="./preview-frontend/preview2.png" width="48%" alt="Interface da aplicação">
</div>

<br>

<div align="center">
  <img src="./preview-frontend/preview3.png" width="48%" alt="Interface da aplicação">
  &nbsp;&nbsp;
  <img src="./preview-frontend/preview4.png" width="48%" alt="Interface da aplicação">
</div>
