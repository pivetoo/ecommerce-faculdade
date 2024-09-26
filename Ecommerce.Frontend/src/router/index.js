import { createRouter, createWebHistory } from 'vue-router';
import Login from '../views/Login.vue';
import RecuperarSenha from  '../views/RecuperarSenha.vue';
import Home from '../views/Home.vue';
import CriarConta from '../views/CriarConta.vue';

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: "/recuperarSenha",
    name: "RecuperarSenha",
    component: RecuperarSenha
  },
  {
    path: "/criarConta",
    name: "CriarConta",
    component: CriarConta
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;


