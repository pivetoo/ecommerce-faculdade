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
  - .NET Core 8.0
  - PostgreSQL

- **Frontend**:
  - Node.js 14.x ou superior
  - npm 6.x ou superior

### Backend

1. Clonar o projeto:
```bash
git clone https://github.com/pivetoo/ecommerce-faculdade.git
```

2. Abra a pasta do projeto backend `Ecommerce.Backend` após isso execute o arquivo `Ecommerce.sln`

3. Ao abrir o projeto, na pasta Ecommerce.API, clique com o botão direito e selecione a opção `Set as Startup Project`.

2. Configure a sua string de conexão postgres no arquivo appsettings.json na pasta Ecommerce.API.

3. Executar as migrações do banco de dados:
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
