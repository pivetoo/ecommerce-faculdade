<template>
  <div class="login-container">
    <h2>Seja Bem-Vindo(a)</h2>
    <form @submit.prevent="handleSubmit">
      <div class="input-group">
        <label for="email">Email:</label>
        <input type="email" id="email" v-model="email" required>
      </div>
      <div class="input-group">
        <label for="password">Senha:</label>
        <input type="password" id="password" v-model="password" required>
      </div>
      <a href="#" class="forgot-password">Esqueci Minha Senha</a>
      <button type="submit">Fazer Login</button>
    </form>
  </div>
</template>

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
      // Fazer a lógica para o login (substitua pela sua lógica real)
      if (this.email && this.password) {
        console.log("Email:", this.email, "Senha:", this.password);
        // Aqui você pode fazer uma chamada para a API de login
      } else {
        console.error("Por favor, preencha todos os campos.");
      }
    }
  }
}
</script>

<style scoped>
body {
    font-family: Arial, sans-serif;
    background-image: url("@/imagens/background.jpeg");
    background-size: cover;
    margin: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    height: 100vh;
}
  
.login-container {
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
  
input[type="email"],
input[type="password"] {
    width: 100%;
    padding: 8px;
    border: 1px solid #ccc;
    border-radius: 4px;
}
  
.forgot-password {
    position: absolute;
    right: 10px;
    bottom: 10px;
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
