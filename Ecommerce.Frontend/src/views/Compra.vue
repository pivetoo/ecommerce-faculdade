<template>
  <div class="compra-page">
    <!-- Header (Navbar) -->
    <header class="header">
      <Navbar />
    </header>

    <!-- Product Details -->
    <div class="product-details" v-if="product">
      <div class="product-image">
        <img :src="product.imagemUrl" alt="Imagem do Produto" />
      </div>
      <div class="product-info">
        <h2>{{ product.nome }}</h2>
        <p>{{ product.descricao }}</p>
        <div class="price">
          <medium>
            R$ {{ product.preco.toFixed(2) }}
          </medium>
        </div>

        <!-- Campo para escolher a quantidade -->
        <div class="quantity">
          <label for="quantity">Quantidade:</label>
          <input type="number" id="quantity" v-model="quantidade" min="1" />
        </div>

        <div class="actions">
          <button @click="finalizarCompra" class="btn btn-primary">Finalizar Compra</button>
          <button @click="adicionarAoCarrinho" class="btn btn-outline-secondary">Adicionar ao Carrinho</button>
        </div>
      </div>
    </div>
    <div v-else>
      <p>Carregando informações do produto...</p>
    </div>

    <!-- Rodapé -->
    <footer class="footer">
      <div class="footer-section">
        <h4>Informações</h4>
        <p>Contato</p>
        <p>Política de Privacidade</p>
      </div>
      <div class="footer-section">
        <h4>Ajuda</h4>
        <p>FAQ</p>
        <p>Suporte</p>
      </div>
      <div class="newsletter">
        <h4>Assine nossa Newsletter</h4>
        <p>Cadastre-se para receber as últimas novidades e promoções!</p>
        <input type="email" v-model="email" placeholder="Email" required />
        <button @click="subscribe">Inscreva-se</button>

        <p v-if="errorMessage" class="error-message">{{ errorMessage }}</p>

        <!-- Card de sucesso -->
        <div v-if="showSuccessCard" class="success-card">
          <p>{{ successMessage }}</p>
          <button @click="closeCard" class="close-button">Fechar</button>
        </div>
      </div>
    </footer>
  </div>
</template>

<script>
import axios from 'axios';
import Navbar from '../components/Navbar.vue';

export default {
  data() {
    return {
      product: null,
      quantidade: 1, 
      email: '',
      errorMessage: '',
      successMessage: "Parabéns, você agora faz parte da melhor Newsletter de moda do Brasil!",
      showSuccessCard: false
    };
  },
  components: {
    Navbar
  },
  methods: {
    async fetchProduct() {
      const productId = this.$route.params.id; 
      try {
        const response = await axios.get(`https://localhost:7172/api/Produto/${productId}`);
        this.product = response.data;
      } catch (error) {
        console.error("Erro ao carregar produto:", error);
      }
    },
    finalizarCompra() {
      // Lógica para finalizar a compra
      alert(`Compra finalizada! Você comprou ${this.quantidade} unidade(s) de ${this.product.nome}.`);
    },
    adicionarAoCarrinho() {
      // Lógica para adicionar o produto ao carrinho
      alert(`Produto adicionado ao carrinho! Quantidade: ${this.quantidade}`);
    },
    subscribe() {
      if (!this.email) {
        this.errorMessage = "O email é obrigatório.";
        this.showSuccessCard = false;
      } else {
        this.errorMessage = "";
        this.showSuccessCard = true;
        this.email = "";
      }
    }
  },
  mounted() {
    this.fetchProduct(); // Chama o método para buscar o produto ao carregar a página
  }
};
</script>

<style scoped>
.compra-page {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 20px;
  min-height: 100vh;
  background-color: #f8f9fa; 
  font-family: 'Arial', sans-serif; 
}

/* Navbar */
.header {
  width: 100%;
  position: fixed;
  top: 0;
  left: 0;
  z-index: 1000;
  background-color: #ffffff; 
  border-bottom: 1px solid #ddd; 
}

/* Detalhes do produto */
.product-details {
  display: flex;
  justify-content: center;
  gap: 40px;
  margin-top: 100px; 
  width: 100%;
  max-width: 1200px;
}

.product-image img {
  width: 400px;
  height: 400px;
  object-fit: contain;
  border: 1px solid #ddd;
  border-radius: 10px;
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.1);
}

.product-info {
  max-width: 500px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.product-info h2 {
  font-size: 2.5rem;
  font-weight: bold;
  margin-bottom: 20px;
  color: #333;
}

.product-info p {
  font-size: 1.2rem;
  margin-bottom: 20px;
  color: #666;
}

.price {
  font-size: 1.8rem;
  margin-bottom: 20px;
  font-weight: bold;
  color: #e63946;
}

.quantity {
  margin-bottom: 20px;
  display: flex;
  align-items: center;
  gap: 10px;
}

.quantity input {
  padding: 8px 15px;
  font-size: 1rem;
  border: 2px solid #007bff;
  border-radius: 8px;
  width: 60px;
  text-align: center;
}

.actions button {
  padding: 12px 25px;
  font-size: 1.2rem;
  margin-top: 10px;
  border-radius: 8px;
  transition: all 0.3s ease;
}

.actions .btn-primary {
  background-color: #007bff;
  color: white;
  border: none;
  cursor: pointer;
}

.actions .btn-primary:hover {
  background-color: #0056b3;
}

.actions .btn-outline-secondary {
  background-color: transparent;
  border: 2px solid #007bff;
  color: #007bff;
  margin-left: 10px;
}

.actions .btn-outline-secondary:hover {
  background-color: #007bff;
  color: white;
  transform: scale(1.05);
}

/* Footer */
.footer {
  width: 100%; 
  margin-top: 40px;
  padding: 20px;
  background-color: #f8f9fa;
  display: flex;
  justify-content: space-between;
  gap: 30px;
}

.footer-section h4 {
  margin-bottom: 10px;
}

.newsletter {
  width: 300px;
}

.error-message {
  color: red;
  font-size: 14px;
}
</style>
