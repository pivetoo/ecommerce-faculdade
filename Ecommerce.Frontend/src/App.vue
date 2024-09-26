<script setup>
import { useRouter, RouterView } from 'vue-router';

</script>

<template>
    <<router-link to="/login">Login teste</router-link>>
    <<router-link to="/recuperarSenha">Recuperar teste</router-link>>
    <<router-link to="/"> Home teste</router-link>>
    <<router-link to="/criarConta"> CriarConta teste</router-link>>
    <RouterView />

</template>

<style scoped>

</style>
