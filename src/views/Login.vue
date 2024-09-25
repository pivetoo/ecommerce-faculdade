<template>
  <div class="login-container">
    <h2>Seja Bem-Vindo(a)</h2>
    <form @submit.prevent="submitForm">
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

<style scoped src="@/assets/login.css"></style>
