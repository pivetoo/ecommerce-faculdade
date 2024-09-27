import { createRouter, createWebHistory } from 'vue-router';
import Login from '../views/Login.vue';
import CriarConta from '../views/CriarConta.vue';
import Homepage from '../views/Homepage.vue';
import ChatbotContent from '../views/ChatbotContent.vue';

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
    path: "/chat-content",
    name: "ChatbotContent",
    component: ChatbotContent
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;


