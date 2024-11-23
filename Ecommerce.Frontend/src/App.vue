<script setup>
import { RouterView } from 'vue-router'

</script>

<template>

  <RouterView />
  
</template>

<style scoped>

@media (max-width: 1024px) {

  
}
</style>

<script>
import Loja from './views/Loja.vue';

export default {
  name: 'App',
  components: {
    Loja
  }
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
}
</style>