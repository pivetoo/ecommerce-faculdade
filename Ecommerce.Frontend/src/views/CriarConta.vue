<template>
  <div class="register-background">
    <div class="register-container">
      <div class="register-card">
        <h2>Criar Conta</h2>
        <p>Preencha os campos abaixo para criar uma nova conta.</p>
        <form @submit.prevent="handleRegister">
          <div class="form-group">
            <label for="nome">Nome:</label>
            <input type="text" id="nome" v-model="nome" required>
          </div>
          <div class="form-group">
            <label for="email">Email:</label>
            <input type="email" id="email" v-model="email" required>
          </div>
          <div class="form-group">
            <label for="senha">Senha:</label>
            <input type="password" id="senha" v-model="senha" required>
          </div>
          <div class="form-group">
            <label for="confirmarSenha">Confirmar Senha:</label>
            <input type="password" id="confirmarSenha" v-model="confirmarSenha" required>
          </div>
          <button type="submit">Criar Conta</button>
        </form>
        <p class="ja-tem-conta">Já tem uma conta?</p>
        <router-link to="/login" class="login-link">Faça login aqui</router-link>
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
    const nome = ref('');
    const email = ref('');
    const senha = ref('');
    const confirmarSenha = ref('');
    const router = useRouter();

    const handleRegister = async () => {
    try {
      if (senha.value !== confirmarSenha.value) {
        alert('As senhas não coincidem');
        return;
      }

      const response = await axios.post('https://localhost:7172/api/Usuario', {
        nome: nome.value,
        email: email.value,
        senha: senha.value
      });

      alert('Conta criada com sucesso!');
      router.push('/login');
    } catch (error) {
      if (error.response) {
        console.error('Registro falhou:', error.response.data);
      } else {
        console.error('Erro desconhecido:', error.message);
      }
    }
  };
    return {
      nome,
      email,
      senha,
      confirmarSenha,
      handleRegister
    };
  }
};
</script>

<style scoped>
.register-background {
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

.register-container {
  display: flex;
  justify-content: center;
  align-items: center;    
  width: 100%;
}

.register-card {
  background-color: white;
  padding: 50px;
  border-radius: 10px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  width: 60%;
  max-width: 500px;
}

.register-card h2 {
  text-align: center;
  font-weight: bold;
  font-size: 1.75rem;
  font-family: Arial, Helvetica, sans-serif;
}

.register-card p {
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

input[type="text"],
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

.ja-tem-conta {
  text-align: center;
  margin-top: 10px;
}

.login-link {
  display: block;
  text-align: center;
  color: #0077cc;
  text-decoration: none;
  margin-top: 10px;
}

.login-link:hover {
  text-decoration: underline;
}
</style>
