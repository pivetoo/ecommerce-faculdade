<template>
  <div class="container">
    <div class="login-container">
      <h2>Seja Bem-Vindo(a)</h2>
      <p class="instrucao-login">Faça login com seu email e senha para acessar a plataforma</p>
      <form @submit.prevent="handleSubmit">
        <div class="input-group">
          <label for="email">Email:</label>
          <input type="email" id="email" v-model="email" required>
        </div>
        <div class="input-group">
          <label for="password">Senha:</label>
          <input type="password" id="password" v-model="password" required>
        </div>
        <button type="submit">Fazer Login</button>
        <p class="esqueci-senha">
          <router-link to="/recuperarSenha" class="forgot-password">Esqueci Minha Senha</router-link>
        </p>
        <p class="criar-conta">
          <router-link to="/criarConta" class="forgot-password">Não tem conta? Cadastre-se.</router-link>
        </p>
      </form>
    </div>
  </div>
</template>

### Script

```js
<script>
export default {
  data() {
    return {
      email: '',
      password: ''
    };
  },
  methods: {
    handleSubmit() {
      if (this.email && this.password) {
        console.log("Email:", this.email, "Senha:", this.password);
        // Chamada para a API de login
      } else {
        console.error("Por favor, preencha todos os campos.");
      }
    }
  }
}
</script>

<style>
    /* Estilos globais */
    html, body {
      margin: 0;
      padding: 0;
      height: 100%;
      width: 100%;
      overflow: hidden;
    }

    .container {
      font-family: Arial, sans-serif;
      background-image: url("../imagens/background.jpeg");
      background-size: cover;
      background-position: center;
      background-repeat: no-repeat;
      display: flex;
      justify-content: center;
      align-items: center;
      height: 100vh; 
      width: 100vw; 
    }

    .login-container {
      background-color: #fff;
      padding: 40px;
      border-radius: 8px;
      box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
      width: 440px;            
      max-width: 100%;         
      display: flex;
      flex-direction: column;
      justify-content: center; /* Garante que os itens internos estejam alinhados */
    }

    h2 {
      color: #333;
      text-align: center;
      margin-bottom: 20px;
    }

    .instrucao-login {
      text-align: center;
      color: #4C4C4C;
      font-size: 16px;
      font-weight: 400;
      line-height: 28px;
      margin-top: -20px;
    }

    .input-group {
      margin-bottom: 10px;
    }

    label {
      display: block;
      margin-bottom: 5px;
      color: #666;
    }

    input[type="email"],
    input[type="password"] {
      width: 100%;
      padding: 8px;
      border: 1px solid #ccc;
      border-radius: 4px;
    }

    .esqueci-senha {
      text-align: center;
      margin-top: 10px;
    }

    .esqueci-senha a {
      color: #0077cc;
      text-decoration: none;
    }

    .criar-conta {
      text-align: center;
      margin-top: 5px;
    }

    .criar-conta a {
      color: #0077cc;
      text-decoration: none;
    }

    button {
      width: 100%;
      height: 45px;
      padding: 10px;
      background-color: #0077cc;
      color: white;
      border: none;
      border-radius: 6px;
      cursor: pointer;
    }

    button:hover {
      background-color: #005fa3;
    }

</style>
