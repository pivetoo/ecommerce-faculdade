import { createRouter, createWebHistory } from 'vue-router'
import Home from '@/views/Home.vue'
import Carrinho from '@/views/Carrinho.vue'
import Comprar from '@/views/Comprar.vue'
import Adicionar from '@/views/AdicionarCarrinho.vue'
import CriarConta from '@/components/CriarConta.vue'
import Login from '@/components/Login.vue'
import Gerenciar from '@/views/GerenciarProdutos.vue'
import EditarProduto from '@/components/EditarProduto.vue'
import GerenciarContas from '@/views/GerenciarContas.vue'

const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/carrinho',
    name: 'Carrinho',
    component: Carrinho
  },
  {
    path: '/comprar',
    name: 'Comprar',
    component: Comprar
  },
  {
    path: '/adicionar',
    name: 'Adicionar',
    component: Adicionar
  }
  ,
  {
    path: '/criar',
    name: 'CriarConta',
    component: CriarConta
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/gerenciar',
    name: 'Gerenciar',
    component: Gerenciar
  },
  {
    path: '/editar',
    name: 'EditarProduto',
    component: EditarProduto
  },
  {
    path: '/gerenciarContas',
    name: 'GerenciarContas',
    component: GerenciarContas
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

export default router;
