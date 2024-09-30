# Projeto Fusão
## Sobre:
Seja bem-vindo ao Projeto Fusão! Este projeto foi desenvolvido como parte da disciplina de Fábrica de Projetos da Universidade de Marília (Unimar). O principal objetivo é a criação de um sistema de e-commerce integrado a um chatbot inteligente para suporte ao cliente, além de uma plataforma de gerenciamento de FAQs.

## Implementações
### 🔑 Backend
#### Autenticação e Autorização com JWT:
> Login com tokens JWT, que permitem distinguir entre usuários e administradores.\
> Controle de acesso aos endpoints da API baseado em permissões de usuário.

#### Chatbot Integrado:
> Chatbot com a API do OpenAI que responde a perguntas frequentes, pode ser alimentado via conteúdo customizado e responde automaticamente às dúvidas dos usuários.

#### Gerenciamento de FAQs:
> CRUD completo de FAQs, onde administradores podem adicionar, remover, e editar FAQs.
> Sistema de upload de imagens para os FAQs com gerenciamento de URLs.

### 🖥️ Frontend
#### Dashboard de Administração:
> Interface completa para gerenciar FAQs, e chatbot.\
> Upload de imagens para FAQs diretamente pela interface.

#### Sistema de Login e Controle de Acesso:
> Usuários e administradores têm áreas dedicadas, com controle de permissões.\
> Redirecionamento baseado no nível de acesso (admin para /dashboard e usuários para /ecommerce).

#### Integração com Chatbot:
> Interface de chat onde o usuário pode fazer perguntas e obter respostas automáticas do chatbot.\
> Modal para administradores alimentarem o chatbot com novas respostas.

## Como rodar o Projeto

### Requisitos
- **Backend**:
  - Visual Studio 2022
  - NET Core SDK 8.0: O SDK do .NET Core é necessário para construir e executar o projeto. [Download do .NET SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
  - PostgreSQL: O banco de dados utilizado. [Download do PostgreSQL](https://www.postgresql.org/download/)

- **Frontend**:
  - Node.js 14.x ou superior [Download do Node.js](https://nodejs.org/)
  - npm 6.x ou superior

Primeiramente, você precisa instalar a ferramenta dotnet-ef globalmente para gerenciar as migrações do banco de dados no projeto. Execute o comando abaixo:

```bash
dotnet tool install --global dotnet-ef
```

Clone o repositório do projeto com o comando:
```bash
git clone https://github.com/pivetoo/ecommerce-faculdade.git
```

### Backend

1. Clonar o projeto:

2. Navegue até a pasta `Ecommerce.Backend` e abra o arquivo `Ecommerce.sln` no Visual Studio.

3. No Visual Studio, localize a pasta `Ecommerce.API`, clique com o botão direito e selecione a opção `Set as Startup Project`.

4. Navegue até o arquivo `appsettings.json` na pasta `Ecommerce.API` e configure a string de conexão para o PostgreSQL de acordo com o ambiente local.

5. Antes de rodar o projeto, é necessário aplicar as migrações para configurar o banco de dados. No terminal, navegue até a pasta Ecommerce.Repositorio e execute o comando de migração:
```bash
cd Ecommerce.Repositorio
```

Execute o comando de migração, indicando o projeto API como o projeto de inicialização:
```bash
dotnet ef database update --startup-project ../Ecommerce.API
```

### Frontend

1. Navegue até a pasta do projeto frontend:
```bash
cd ecommerce-faculdade/Ecommerce.Frontend
```
2. Instale as dependências:
```bash
npm install
```
3. Inicie o servidor de desenvolvimento:
```bash
npm run dev
```

## Desenvolvido por:
Rogério Franco Piveto Filho | 1969034\
Nicolas Basilio Golino | 1967759\
Pedro Henrique Nalom | 1968133\
Breno Silva Bernardo | 1977043\
Gilmar Junior Giroto Ferreira | 1978685\
Suelen Aleixo da Silva | 1969506

Dependências Essenciais:

