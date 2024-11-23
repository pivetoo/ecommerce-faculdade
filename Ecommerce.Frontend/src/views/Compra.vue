<template>
  <div class="compra-page">
    <header class="header">
      <Navbar />
    </header>

    <div class="product-details">
      <div class="product-image">
        <img :src="product.imagemUrl" alt="Imagem do Produto" />
      </div>
      <div class="product-info">
        <h2>{{ product.nome }}</h2>
        <p>{{ product.descricao }}</p>
        <div class="price">
          <medium style="color: #FF0000; font-size: 30px; font-weight: bold">
            R$ {{ product.preco.toFixed(2) }}
          </medium>
        </div>
        <div class="actions">
          <button @click="finalizarCompra" class="btn btn-primary">Finalizar Compra</button>
          <button @click="adicionarAoCarrinho" class="btn btn-outline-secondary">Adicionar ao Carrinho</button>
        </div>
      </div>
    </div>

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
        <input type="email" v-model="email" placeholder="Email" required />
        <button @click="subscribe">Inscreva-se</button>
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
      const productId = this.$route.params.id; // Captura o ID da URL
      try {
        const response = await axios.get(`https://localhost:7172/api/Produto/${productId}`);
        this.product = response.data;
      } catch (error) {
        console.error("Erro ao carregar produto:", error);
      }
    },
    finalizarCompra() {
      // Lógica para finalizar a compra
      alert('Compra finalizada!');
    },
    adicionarAoCarrinho() {
      // Lógica para adicionar o produto ao carrinho
      alert('Produto adicionado ao carrinho!');
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
    this.fetchProduct();
  }
};
</script>

<style scoped>
.compra-page {
  display: flex;
  flex-direction: column;
  align-items: center;
  padding: 20px;
}

.product-details {
  display: flex;
  justify-content: center;
  gap: 40px;
  margin-top: 40px;
}

.product-image img {
  width: 400px;
  height: 400px;
  object-fit: contain;
  border: 1px solid #ddd;
  border-radius: 8px;
}

.product-info {
  max-width: 500px;
  display: flex;
  flex-direction: column;
}

.product-info h2 {
  font-size: 2rem;
  font-weight: bold;
  margin-bottom: 20px;
}

.product-info p {
  font-size: 1.2rem;
  margin-bottom: 20px;
}

.price {
  font-size: 1.5rem;
  margin-bottom: 20px;
}

.actions button {
  padding: 10px 20px;
  font-size: 1.2rem;
  margin-top: 10px;
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
  border: 1px solid #007bff;
  color: #007bff;
  margin-left: 10px;
}

.actions .btn-outline-secondary:hover {
  background-color: #007bff;
  color: white;
}

.footer {
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
