import { createRouter, createWebHistory } from 'vue-router';
import Login from '../views/Login.vue';
import CriarConta from '../views/CriarConta.vue';
import Homepage from '../views/Homepage.vue';
import Faq from '../views/Faq.vue';
import Perfil from '../views/Perfil.vue';
import Sobre from '../views/Sobre.vue';
import Chatbot from '../views/Chatbot.vue';

const routes = [
  {
    path: '/',
    name: 'Login',
    component: Login
  },
  {
    path: "/criar-conta",
    name: "CriarConta",
    component: CriarConta
  },
  {
    path: "/dashboard",
    name: "Dashboard",
    component: Homepage
  },
  {
    path: "/faq",
    name: "Faq",
    component: Faq
  },
  {
    path: "/perfil",
    name: "Perfil",
    component: Perfil
  },
  {
    path: "/sobre",
    name: "Sobre",
    component: Sobre
  },
  {
    path: "/chat-bot",
    name: "Chatbot",
    component: Chatbot
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;