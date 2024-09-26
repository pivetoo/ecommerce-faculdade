<template>
  <div class="login-background">
    <div class="login-container">
      <div class="login-card">
        <h2>Seja Bem-Vindo(a)</h2>
        <p>Faça login com seu email e senha para acessar a plataforma.</p>
        <form @submit.prevent="handleLogin">
          <div class="form-group">
            <label for="email">Email:</label>
            <input type="email" id="email" v-model="email" required>
          </div>
          <div class="form-group">
            <label for="senha">Senha:</label>
            <input type="password" id="senha" v-model="senha" required>
          </div>
          <button type="submit">Login</button>
        </form>
        <p class="esqueci-senha" />
        <router-link to="/recuperarSenha" class="forgot-senha">Esqueci Minha Senha</router-link>
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import axios from 'axios';

export default {
  setup() {
    const email = ref('');
    const senha = ref('');
    const router = useRouter();

    const handleLogin = async () => {
      try {
        const response = await axios.post('https://localhost:7172/api/Login', {
          email: email.value,
          senha: senha.value
        });

        const { token, isAdmin } = response.data;

        localStorage.setItem('token', token);

        if (isAdmin) {
          router.push('/dashboard');
        } else {
          router.push('/ecommerce');
        }
      } catch (error) {
        console.error('Login failed:', error);
      }
    };

    return {
      email,
      senha,
      handleLogin
    };
  }
};
</script>

<style scoped>

.h2 {
  font-weight: bold;
  font-size: 1.75rem;
  font-family: Arial, sans-serif;
}

.login-background {
  background-image: url('../imagens/background.jpeg');
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  width: 100vw;
  height: 100vh;
  display: flex;
  justify-content: center;
  align-items: center;
}
.login-container {
  display: flex;
  justify-content: center;
  align-items: center;    
  width: 100%;
}

.login-card {
  background-color: white;
  padding: 50px;
  border-radius: 10px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  width: 60%;
  max-width: 500px;
}

.login-card h2 {
  text-align: center;
  font-weight: bold;
  font-size: 1.75rem;
  font-family: Arial, Helvetica, sans-serif;
}

.login-card p {
  text-align: center;
  font-size: 1rem;
  font-family: Arial, Helvetica, sans-serif;
}

.form-group {
  margin-bottom: 30px;
}

label {
  display: block;
  margin-bottom: 8px;
  font-weight: bold;
}

input[type="email"],
input[type="password"] {
  width: 100%;
  padding: 12px;
  border: 1px solid #ccc;
  border-radius: 5px;
  box-sizing: border-box;
}

button[type="submit"] {
  width: 100%;
  padding: 12px;
  background-color: #007bff; 
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  transition: background-color 0.3s;
}

button[type="submit"]:hover {
  background-color: #0056b3;
}

.esqueci-senha {
  text-align: center;
  margin-top: 10px;
}
  
.esqueci-senha a {
  color: #0077cc;
  text-decoration: none;
}
</style>
