<template>
  <div class="register-background">
    <div class="register-container">
      <div class="register-card">
        <h2>Criar Conta</h2>
        <p><b>Preencha os campos abaixo para criar uma nova conta.</b></p>
        <form @submit.prevent="handleRegister">
          <div class="form-group" :class="{ 'has-error': nomeErro }">
            <label for="nome">Nome:</label>
            <input type="text" id="nome" v-model="nome" required>
            <span v-if="nomeErro" class="error-message">Nome é obrigatório</span>
          </div>
          <div class="form-group" :class="{ 'has-error': emailErro }">
            <label for="email">Email:</label>
            <input type="email" id="email" v-model="email" required>
            <span v-if="emailErro" class="error-message">Email inválido</span>
          </div>
          <div class="form-group" :class="{ 'has-error': senhaErro }">
            <label for="senha">Senha:</label>
            <input type="password" id="senha" v-model="senha" required>
            <span v-if="senhaErro" class="error-message">A senha deve ter no mínimo 6 caracteres</span>
          </div>
          <div class="form-group" :class="{ 'has-error': confirmarSenhaErro }">
            <label for="confirmarSenha">Confirmar Senha:</label>
            <input type="password" id="confirmarSenha" v-model="confirmarSenha" required>
            <span v-if="confirmarSenhaErro" class="error-message">As senhas não coincidem</span>
          </div>
          <button type="submit" :disabled="isSubmitting">Criar Conta</button>
        </form>
        <p class="ja-tem-conta">Já tem uma conta?</p>
        <router-link to="/" class="login-link">Faça login aqui</router-link>
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue';
import { useRouter } from 'vue-router';
import { register } from '@/services/authService';

export default {
  setup() {
    const nome = ref('');
    const email = ref('');
    const senha = ref('');
    const confirmarSenha = ref('');
    const isSubmitting = ref(false);
    const nomeErro = ref(false);
    const emailErro = ref(false);
    const senhaErro = ref(false);
    const confirmarSenhaErro = ref(false);
    const router = useRouter();

    const validarFormulario = () => {
      let valid = true;
      nomeErro.value = !nome.value;
      emailErro.value = !/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email.value);
      senhaErro.value = senha.value.length < 6;
      confirmarSenhaErro.value = senha.value !== confirmarSenha.value;

      return valid && !nomeErro.value && !emailErro.value && !senhaErro.value && !confirmarSenhaErro.value;
    };

    const handleRegister = async () => {
      if (!validarFormulario()) return;

      try {
        isSubmitting.value = true;
        await register(nome.value, email.value, senha.value);

        alert('Conta criada com sucesso!');
        router.push('/');
      } catch (error) {
        alert('Erro ao criar a conta. Tente novamente.');
      } finally {
        isSubmitting.value = false;
      }
    };

    return {
      nome,
      email,
      senha,
      confirmarSenha,
      isSubmitting,
      nomeErro,
      emailErro,
      senhaErro,
      confirmarSenhaErro,
      handleRegister
    };
  }
};
</script>

<style scoped>
.register-background {
  background-image: url('../assets/background.jpeg');
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
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.15);
  width: 100%;
  max-width: 500px;
  text-align: center;
  height: auto;
}

.form-group {
  margin-bottom: 15px;
  text-align: left;
}

.has-error input {
  border-color: #ff4d4d;
}

.error-message {
  color: #ff4d4d;
  font-size: 0.75rem;
  margin-top: 4px;
  display: block;
  text-align: left;
}

input[type="text"],
input[type="email"],
input[type="password"] {
  width: 100%;
  padding: 8px;
  border: 1px solid #ccc;
  border-radius: 4px;
}

button[type="submit"] {
  width: 100%;
  padding: 10px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  transition: background-color 0.3s;
}

button[type="submit"]:hover {
  background-color: #0056b3;
}

button[disabled] {
  background-color: #aaa;
  cursor: not-allowed;
}

.ja-tem-conta {
  margin-top: 10px;
}

.login-link {
  color: #0077cc;
  text-decoration: none;
  margin-top: 8px;
  display: block;
}

.login-link:hover {
  text-decoration: underline;
}
</style>