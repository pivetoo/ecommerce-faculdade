<template>
    <div class="container">
    <div class="recover-password-container">
      <h2>Recuperar Senha</h2>
      <form @submit.prevent="handleSubmit">
        <div class="input-group">
          <label for="email">Nova Senha:</label>
          <input type="email" id="email" v-model="email" required>
          <label for="email">Repetir Senha:</label>
          <input type="email" id="email" v-model="email" required>
        </div>
        <button type="submit">Recuperar Senha</button>
        <p class="back-to-login">
          <a href="/login">Voltar para Login</a>
        </p>
      </form>
    </div>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        email: ''
      };
    },
    methods: {
      handleSubmit() {
        // Lógica para enviar o link de recuperação (substitua pela sua lógica real)
        if (this.email) {
          console.log("Email:", this.email);
          // Aqui você pode fazer uma chamada para a API de recuperação de senha
        } else {
          console.error("Por favor, preencha o campo de email.");
        }
      }
    }
  }
  </script>
  
<style scoped>
  .container {
      font-family: Arial, sans-serif;
      background-image: url("../imagens/background.jpeg");
      background-size: cover;
      margin: 0;
      display: flex;
      justify-content: center;
      align-items: center;
      height: 100vh;
  }
    
  .recover-password-container {
      background-color: #fff;
      padding: 40px;
      border-radius: 8px;
      box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
      width: 300px;
      position: relative;
  }
    
  h2 {
      color: #333;
      text-align: center;
      margin-bottom: 20px;
  }
    
  .input-group {
      margin-bottom: 10px;
  }
    
  label {
      display: block;
      margin-bottom: 5px;
      color: #666;
  }
    
  input[type="email"] {
      width: 100%;
      padding: 8px;
      border: 1px solid #ccc;
      border-radius: 4px;
  }
    
  .back-to-login {
      text-align: center;
      margin-top: 10px;
  }
  
  .back-to-login a {
      color: #0077cc;
      text-decoration: none;
  }
    
  button {
      width: 100%;
      padding: 10px;
      background-color: #0077cc;
      color: white;
      border: none;
      border-radius: 4px;
      cursor: pointer;
  }
    
  button:hover {
      background-color: #005fa3;
  }
</style>
  