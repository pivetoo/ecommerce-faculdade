<template>
  <div class="loja">
    <nav class="navbar">
      <ul>
        <li><router-link to="/novidades">Novidades</router-link></li>
        <li class="icon"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16" fill="none">
          <path d="M4 6L8 10L12 6" stroke="#281D1B" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
        </svg></li>
        <li><router-link to="/masculino">Masculino</router-link></li>
        <li class="icon"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16" fill="none">
          <path d="M4 6L8 10L12 6" stroke="#281D1B" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
        </svg></li>
        <li><router-link to="/feminino">Feminino</router-link></li>
        <li class="icon"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16" fill="none">
          <path d="M4 6L8 10L12 6" stroke="#281D1B" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
        </svg></li>
        <li><router-link to="/infantil">Infantil</router-link></li>
        <li class="icon"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16" fill="none">
          <path d="M4 6L8 10L12 6" stroke="#281D1B" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
        </svg></li>
        <li><router-link to="/acessorios">Acessórios</router-link></li>
        <li class="icon"><svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" viewBox="0 0 16 16" fill="none">
          <path d="M4 6L8 10L12 6" stroke="#281D1B" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
        </svg></li>
      </ul>
      <div class="extra-icons">
        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
          <path d="M21.0004 21L16.6504 16.65M19 11C19 15.4183 15.4183 19 11 19C6.58172 19 3 15.4183 3 11C3 6.58172 6.58172 3 11 3C15.4183 3 19 6.58172 19 11Z" stroke="#281D1B" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
          <path d="M20 21V19C20 17.9391 19.5786 16.9217 18.8284 16.1716C18.0783 15.4214 17.0609 15 16 15H8C6.93913 15 5.92172 15.4214 5.17157 16.1716C4.42143 16.9217 4 17.9391 4 19V21M16 7C16 9.20914 14.2091 11 12 11C9.79086 11 8 9.20914 8 7C8 4.79086 9.79086 3 12 3C14.2091 3 16 4.79086 16 7Z" stroke="#281D1B" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
        <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none">
          <path d="M3 6L6 2H18L21 6M3 6V20C3 20.5304 3.21071 21.0391 3.58579 21.4142C3.96086 21.7893 4.46957 22 5 22H19C19.5304 22 20.0391 21.7893 20.4142 21.4142C20.7893 21.0391 21 20.5304 21 20V6M3 6H21M16 10C16 11.0609 15.5786 12.0783 14.8284 12.8284C14.0783 13.5786 13.0609 14 12 14C10.9391 14 9.92172 13.5786 9.17157 12.8284C8.42143 12.0783 8 11.0609 8 10" stroke="#281D1B" stroke-width="1.5" stroke-linecap="round" stroke-linejoin="round"/>
        </svg>
      </div>
    </nav>
    <h1>Produtos Disponíveis</h1>
    <div v-if="loading">Carregando produtos...</div>
    <div v-if="error">{{ error }}</div>
    <div v-if="produtos.length" class="produtos-list">
      <div v-for="produto in produtos" :key="produto.id" class="produto-item">
        <h2>{{ produto.nome }}</h2>
        <p>{{ produto.descricao }}</p>
        <p>Preço: R$ {{ produto.preco }}</p>
      </div>
    </div>
  </div>
</template>

<script>
import produtoService from '@/services/produtoService'; 

export default {
  name: 'Loja',
  data() {
    return {
      produtos: [],
      loading: true,
      error: null,
    };
  },
  async created() {
    try {
      const response = await produtoService.getProdutos();
      this.produtos = response.data;
    } catch (err) {
      this.error = 'Erro ao carregar os produtos';
    } finally {
      this.loading = false;
    }
  },
};
</script>

<style scoped>
.navbar {
  background-color: white;
  padding: 10px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  display: flex;
  justify-content: center; 
  align-items: center; 
  height: 60px; 
}

.navbar ul {
  list-style-type: none;
  display: flex;
  padding: 0;
  margin: 0;
}

.navbar li {
  margin: 0 10px; 
  display: flex; 
  align-items: center; 
}

.navbar a {
  text-decoration: none;
  color: #281D1B;
  font-weight: bold;
  font-size: 15px;
}

.icon {
  margin: 0 5px; 
}

.extra-icons {
  display: flex;
  margin-left: auto; 
}

.extra-icons svg {
  width: 24px; 
  height: 24px; 
  margin: 0 10px; 
}

.produtos-list {
  display: flex;
  flex-wrap: wrap;
}

.produto-item {
  border: 1px solid #ccc;
  padding: 16px;
  margin: 8px;
  width: 200px;
  text-align: center;
}
</style>
