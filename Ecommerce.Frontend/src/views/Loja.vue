<template>
  <div class="product-page">
    <header class="header">
      <nav class="navbar">
        <ul>
          <li>Novidades</li>
          <li>Masculino</li>
          <li>Feminino</li>
          <li>Infantil</li>
          <li>Acessórios</li>
        </ul>
      </nav>
      <div class="filters">
        <button>Categoria</button>
        <button>Preço</button>
        <button>Marca</button>
        <button>Tamanho</button>
        <button>Cor</button>
      </div>
    </header>

    <div class="product-list">
      <div class="product-card" v-for="product in products" :key="product.name">
        <img :src="product.image" :alt="product.name" />
        <h3>{{ product.name }}</h3>
        <p>{{ product.price }}</p>
      </div>
    </div>

    <footer class="footer">
      <div class="footer-section">
        <h4>Informações</h4>
        <p>Sobre Nós</p>
        <p>Contato</p>
        <p>Política de Privacidade</p>
        <p>Trocas e Devoluções</p>
      </div>
      <div class="footer-section">
        <h4>Ajuda</h4>
        <p>FAQ</p>
        <p>Suporte</p>
        <p>Rastrear Pedido</p>
        <p>Guia de Tamanhos</p>
      </div>
      <div class="newsletter">
        <h4>Assine nossa Newsletter</h4>
        <p>Cadastre-se para receber as últimas novidades e promoções!</p>
        <input type="email" placeholder="Email address" />
        <button>Subscribe</button>
      </div>
    </footer>
  </div>
</template>

<script>
export default {
  name: 'ProductPage',
  data() {
    return {
      products: [
        { name: 'Camiseta Branca', price: 'R$ 49,90', image: 'path/to/camiseta-branca.png' },
        { name: 'Calça Jeans', price: 'R$ 99,90', image: 'path/to/calca-jeans.png' },
        { name: 'Tênis Esportivo', price: 'R$ 199,90', image: 'path/to/tenis-esportivo.png' },
        { name: 'Jaqueta Couro', price: 'R$ 299,90', image: 'path/to/jaqueta-couro.png' }
      ]
    };
  }
};
</script>

<style scoped>
.product-page {
  padding: 20px;
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.header {
  text-align: center;
}

.navbar {
  justify-content: center;
}

.navbar ul {
  display: flex;
  gap: 15px;
  list-style: none;
  padding: 0;
  justify-content: center;
}

.filters {
  display: flex;
  gap: 10px;
  margin: 10px 0;
}

.filters button {
  border-radius: 20px;
  padding: 8px 16px;
  border: 1px solid #ddd;
  background-color: #f1f1f1;
  cursor: pointer;
  transition: background-color 0.3s;
}

.filters button:hover {
  background-color: #e0e0e0;
}

.product-list {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 20px;
  margin-top: 20px;
  flex: 1;
}

.product-card {
  border: 1px solid #ddd;
  border-radius: 8px;
  padding: 10px;
  text-align: center;
}

.product-card img {
  width: 100%;
  border-radius: 8px;
}

.footer {
  margin-top: 15px;
  display: flex;
  justify-content: space-between;
  background-color: #f8f8f8;
  padding: 20px;
}

.footer-section h4, .newsletter h4 {
  margin-bottom: 10px;
}

.newsletter input[type="email"] {
  padding: 8px;
  margin-top: 5px;
  margin-right: 5px;
}
</style>
