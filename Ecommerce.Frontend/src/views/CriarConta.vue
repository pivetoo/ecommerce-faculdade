<template>
    <div class="container">
    <div class="signup-container">
      <h2>Cadastrar Usuário</h2>
      <form @submit.prevent="handleSubmit">
        <div class="input-group">
          <label for="name">Nome:</label>
          <input type="text" id="name" v-model="name" required>
        </div>
        <div class="input-group">
          <label for="email">Email:</label>
          <input type="email" id="email" v-model="email" required>
        </div>
        <div class="input-group">
          <label for="password">Senha:</label>
          <input type="password" id="password" v-model="password" required>
        </div>
        <div class="input-group">
          <label for="confirmPassword">Confirmar Senha:</label>
          <input type="password" id="confirmPassword" v-model="confirmPassword" required>
        </div>
        <button type="submit">Cadastrar</button>
        <p class="back-to-login">
          <a href="/login">Já tem uma conta? Faça Login</a>
        </p>
      </form>
    </div>
    </div>
  </template>
  
  <script>
  export default {
    data() {
      return {
        name: '',
        email: '',
        password: '',
        confirmPassword: ''
      };
    },
    methods: {
      handleSubmit() {
        if (this.password !== this.confirmPassword) {
          console.error("As senhas não coincidem.");
          return;
        }
        // Lógica para cadastrar o usuário (substitua pela sua lógica real)
        console.log("Nome:", this.name, "Email:", this.email, "Senha:", this.password);
        // Aqui você pode fazer uma chamada para a API de cadastro
      }
    }
  }
  </script>
  
  <style scoped>
  .container {
      font-family: Arial, sans-serif;
      background-image: url("@/imagens/background.jpeg");
      background-size: cover;
      margin: 0;
      display: flex;
      justify-content: center;
      align-items: center;
      height: 100vh;
  }
    
  .signup-container {
      background-color: rgba(255, 255, 255, 0.95);
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
    
  input[type="text"],
  input[type="email"],
  input[type="password"] {
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
  