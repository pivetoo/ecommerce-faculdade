import { createRouter, createWebHistory } from 'vue-router';
import Login from '../views/Login.vue';
import CriarConta from '../views/CriarConta.vue';
import Homepage from '../views/Homepage.vue';
import Faq from '../views/Faq.vue';
import Perfil from '../views/Perfil.vue';
import Sobre from '../views/Sobre.vue';
import Chatbot from '../views/Chatbot.vue';
import Loja from '../views/Loja.vue';
import Notificacoes from '../views/Notificacoes.vue';
import Carrinho from '../views/Carrinho.vue';
import Compra from '../views/Compra.vue';
import Adiciona from '../views/AdicionaProduto.vue';
import SobreNos from '../views/SobreNos.vue';

const routes = [
  {
    path: '/',
    name: 'Login',
    component: Login
  },
  {
    path: "/sobrenos",
    name: "SobreNos",
    component: SobreNos
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
  },
  {
    path: "/ecommerce",
    name: "Loja",
    component: Loja
  },
  {
    path: "/notificacoes",
    name: "Notificações",
    component: Notificacoes
  },
  {
    path: "/carrinho",
    name: "Carrinho",
    component: Carrinho
  },
  {
    path: '/comprar/:id',
    name: 'Compra',
    component: Compra
  },  
  {
    path: '/adicionaProduto',
    name: "Adiciona Produto",
    component: Adiciona
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
