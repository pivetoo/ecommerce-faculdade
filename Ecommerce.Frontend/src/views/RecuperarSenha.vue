<template>
  <div class="login-background">
    <div class="login-container">
      <div class="login-card">
        <h2>Recuperar senha</h2>
        <form @submit.prevent="handleLogin">
          <div class="form-group">
            <label for="email">Nova senha</label>
            <input type="email" id="email" v-model="email" required>
          </div>
          <div class="form-group">
            <label for="password">Repetir senha</label>
            <input type="password" id="password" v-model="password" required>
          </div>
          <button type="submit">Recuperar</button>
        </form>
        <p class="back-to-login">
          <a href="/login">Voltar para Login</a>
        </p>

      </div>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue';
import { useRouter } from 'vue-router';

export default {
  setup() {
    const email = ref('');
    const password = ref('');
    const router = useRouter();

    const handleLogin = async () => {
      try {
        console.log("Email:", email.value, "Senha:", password.value);
        localStorage.setItem('token', 'fake-jwt-token');
        router.push('/');
      } catch (error) {
        console.error('Login falhou:', error);
      }
    };

    return {
      email,
      password,
      handleLogin
    };
  }
};
</script>

<style scoped>
/* Background image applied */
.login-background {
  background-image: url('../imagens/background.jpeg');
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  position: fixed; /* Isso garante que o fundo cubra toda a viewport */
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

