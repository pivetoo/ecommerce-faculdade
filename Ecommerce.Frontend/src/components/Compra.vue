<template>
  <div class="compra-page">
    <!-- Header (Navbar) -->
    <header class="header">
      <Navbar />
    </header>

    <!-- Product Details -->
    <div class="product-details" v-if="product">
      <div class="product-image">
        <img :src="product.imagemUrl" alt="Imagem do Produto" class="product-image-img" />
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

    <!-- Produtos Recomendados -->
    <div class="recommended-products" v-if="recommendedProducts.length > 0">
      <h3>Produtos Recomendados</h3>
      <div class="product-list">
        <div class="product-card" v-for="recommendedProduct in recommendedProducts" :key="recommendedProduct.id">
          <img :src="recommendedProduct.imagemUrl" alt="Imagem do Produto" class="product-image-img" />
          <h4>{{ recommendedProduct.nome }}</h4>
          <p>{{ recommendedProduct.descricao }}</p>
          <div class="price">
            R$ {{ recommendedProduct.preco.toFixed(2) }}
          </div>
          <button @click="adicionarAoCarrinho(recommendedProduct)" class="btn btn-outline-secondary">
            Adicionar ao Carrinho
          </button>
          <button @click="showModal(recommendedProduct)" class="btn btn-outline-primary">
            Ver Detalhes
          </button>
        </div>
      </div>
    </div>

    <!-- Modal de Produto -->
    <div v-if="showProductModal" class="product-modal">
      <div class="modal-content">
        <span class="close" @click="closeModal">&times;</span>
        <div class="product-image">
          <img :src="selectedProduct.imagemUrl" alt="Imagem do Produto" class="product-image-img" />
        </div>
        <div class="product-info">
          <div class="product-detail">
            <strong>Nome: </strong>{{ selectedProduct.nome }}
          </div>
          <div class="product-detail">
            <strong>Preço: </strong>R$ {{ selectedProduct.preco.toFixed(2) }}
          </div>
          <div class="product-detail">
            <strong>Descrição: </strong>{{ selectedProduct.descricao }}
          </div>
        </div>
      </div>
    </div>

    <!-- Veja Outros Produtos da Loja -->
    <div class="other-products" v-if="otherProducts.length > 0">
      <h3>Veja Outros Produtos da Loja</h3>
      <div class="product-list">
        <div class="product-card" v-for="otherProduct in otherProducts" :key="otherProduct.id">
          <img :src="otherProduct.imagemUrl" alt="Imagem do Produto" class="product-image-img" />
          <h4>{{ otherProduct.nome }}</h4>
          <div class="price">
            R$ {{ otherProduct.preco.toFixed(2) }}
          </div>
          <button @click="redirectToHome" class="btn btn-outline-secondary">
            Ver Mais Produtos
          </button>
        </div>
      </div>
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
      </div>
    </footer>
  </div>
</template>

<script>
import axios from "axios";
import Navbar from "../components/Navbar.vue";

export default {
  data() {
    return {
      product: null,
      quantidade: 1,
      email: "",
      errorMessage: "",
      showSuccessCard: false,
      recommendedProducts: [],
      otherProducts: [],
      allProducts: [],
      showProductModal: false,
      selectedProduct: null,
    };
  },
  components: {
    Navbar,
  },
  methods: {
    async fetchProduct() {
      const productId = this.$route.params.id;
      try {
        const response = await axios.get(`https://localhost:7172/api/Produto/${productId}`);
        this.product = response.data;
        await this.fetchAllProducts();
      } catch (error) {
        console.error("Erro ao carregar produto:", error);
      }
    },
    async fetchAllProducts() {
      try {
        const response = await axios.get("https://localhost:7172/api/Produto");
        if (response.data.$values && Array.isArray(response.data.$values)) {
          this.allProducts = response.data.$values;
          this.filterRecommendedProducts();
          this.filterOtherProducts();
        } else {
          console.error("A resposta da API não contém produtos válidos:", response.data);
        }
      } catch (error) {
        console.error("Erro ao carregar todos os produtos:", error);
      }
    },
    filterRecommendedProducts() {
      // Filtrar produtos da mesma categoria e com preço maior que o produto principal
      const filteredProducts = this.allProducts.filter(
        (product) =>
          product.categoriaId === this.product.categoriaId && product.preco > this.product.preco
      );

      // Embaralhar os produtos filtrados aleatoriamente
      const shuffledProducts = filteredProducts.sort(() => Math.random() - 0.5);

      // Selecionar os dois primeiros produtos embaralhados
      this.recommendedProducts = shuffledProducts.slice(0, 2);
    },

    filterOtherProducts() {
      this.otherProducts = this.allProducts
        .filter((product) => product.id !== this.product.id)
        .slice(0, 5);
    },
    finalizarCompra() {
      alert(`Compra finalizada! Você comprou ${this.quantidade} unidade(s) de ${this.product.nome}.`);
    },
    adicionarAoCarrinho(product = this.product) {
      alert(`Produto adicionado ao carrinho! Quantidade: ${this.quantidade}. Produto: ${product.nome}`);
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
    },
    closeModal() {
      this.showProductModal = false;
      this.selectedProduct = null;
    },
    showModal(product) {
      this.selectedProduct = product;
      this.showProductModal = true;
    },

    redirectToHome() {
      this.$router.push({ name: 'Loja' }); 
    },
  },
  mounted() {
    this.fetchProduct();
  },
};
</script>

<style scoped>
/* Imagem do Produto Principal */
.product-image img {
  width: 100%;
  height: 400px;
  object-fit: cover;
  border-radius: 15px;
  box-shadow: 0 6px 18px rgba(0, 0, 0, 0.1);
}

/* Produto Principal */
.product-details {
  display: flex;
  margin: 40px auto;
  padding: 30px;
  background-color: #ffffff;
  border-radius: 15px;
  box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
  max-width: 1200px;
  gap: 30px;
  align-items: center;
  flex-wrap: wrap;
}

/* Informações do Produto */
.product-info {
  flex: 1;
  display: flex;
  flex-direction: column;
  gap: 20px;
  text-align: left;
}

.product-info h2 {
  font-size: 2.2rem;
  color: #333;
  font-weight: 600;
  margin-bottom: 10px;
}

.product-info p {
  font-size: 1.1rem;
  color: #666;
  line-height: 1.6;
  margin-bottom: 20px;
}

.price {
  font-size: 1.6rem;
  color: #28a745;
  font-weight: bold;
  margin-bottom: 20px;
}

/* Produtos Recomendados */
.recommended-products {
  margin: 40px auto;
  padding: 30px;
  background-color: #f9f9f9;
  border-radius: 15px;
  box-shadow: 0 6px 15px rgba(0, 0, 0, 0.1);
  max-width: 1200px;
}

.recommended-products h3 {
  text-align: center;
  font-size: 2rem;
  margin-bottom: 20px;
}

.recommended-products .product-list {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(280px, 1fr));
  gap: 25px;
}

.recommended-products .product-card {
  background-color: white;
  padding: 20px;
  border-radius: 12px;
  box-shadow: 0 4px 15px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s;
}

.recommended-products .product-card:hover {
  transform: translateY(-8px);
}

.recommended-products .product-card img {
  width: 100%;
  height: 180px;
  object-fit: cover;
  border-radius: 10px;
}

.recommended-products .product-card h4 {
  font-size: 1.1rem;
  margin-top: 15px;
  color: #333;
}

.recommended-products .product-card .price {
  font-size: 1.2rem;
  color: #28a745;
  margin-bottom: 15px;
}

/* Modal de Produto */
.product-modal {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}

.modal-content {
  background: white;
  padding: 40px;
  border-radius: 15px;
  max-width: 700px;
  width: 100%;
  text-align: center;
}

.close {
  position: absolute;
  top: 10px;
  right: 10px;
  font-size: 2rem;
  cursor: pointer;
  color: #007bff;
}

/* Produtos da Loja */
.other-products {
  margin: 40px auto;
  padding: 30px;
  background-color: #f9f9f9;
  border-radius: 15px;
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
  max-width: 1200px;
  overflow-x: auto;
}

.other-products .product-list {
  display: flex;
  gap: 20px;
  justify-content: space-between;
}

.other-products .product-card {
  min-width: 180px;
  background-color: white;
  padding: 15px;
  border-radius: 10px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  transition: transform 0.3s;
}

.other-products .product-card:hover {
  transform: translateY(-5px);
}

.other-products .product-card img {
  width: 100%;
  height: 150px;
  object-fit: cover;
  border-radius: 8px;
}

.other-products .product-card h4 {
  font-size: 1.1rem;
  margin: 10px 0;
}

.other-products .product-card .price {
  font-size: 1.1rem;
  color: #28a745;
}

/* Rodapé */
.footer {
  margin-top: 60px;
  padding: 30px;
  background-color: #f8f9fa;
  display: flex;
  justify-content: space-between;
  gap: 50px;
}

.footer-section h4 {
  font-size: 1.3rem;
  margin-bottom: 10px;
  color: #333;
}

.newsletter {
  width: 350px;
  display: flex;
  flex-direction: column;
}

.newsletter input {
  padding: 12px;
  font-size: 1rem;
  border-radius: 8px;
  border: 1px solid #ddd;
  margin-bottom: 15px;
}

.newsletter button {
  padding: 12px;
  background-color: #28a745;
  color: white;
  border-radius: 8px;
  font-size: 1rem;
  transition: background-color 0.3s;
}

.newsletter button:hover {
  background-color: #218838;
}

.error-message {
  color: red;
  font-size: 14px;
  margin-top: 10px;
}

/* Responsividade */
@media (max-width: 1024px) {
  /* Ajuste no layout para telas menores */
  .product-details {
    flex-direction: column;
    gap: 20px;
  }

  .product-info {
    flex: none;
    text-align: center;
  }

  .product-image img {
    height: 300px;
  }

  .recommended-products .product-list {
    grid-template-columns: repeat(auto-fill, minmax(250px, 1fr));
  }

  .footer {
    flex-direction: column;
    align-items: center;
  }

  .newsletter {
    width: 100%;
  }
}

@media (max-width: 768px) {
  .product-details {
    padding: 15px;
  }

  .product-info h2 {
    font-size: 1.8rem;
  }

  .product-info p {
    font-size: 1rem;
  }

  .product-info .price {
    font-size: 1.4rem;
  }

  .actions button {
    font-size: 1rem;
  }

  .recommended-products h3 {
    font-size: 1.5rem;
  }

  .recommended-products .product-list {
    grid-template-columns: repeat(auto-fill, minmax(200px, 1fr));
  }

  .footer {
    padding: 20px;
  }
}

@media (max-width: 480px) {
  .product-details {
    padding: 10px;
  }

  .product-image img {
    height: 250px;
  }

  .product-info h2 {
    font-size: 1.6rem;
  }

  .product-info p {
    font-size: 0.9rem;
  }

  .price {
    font-size: 1.2rem;
  }

  .actions button {
    font-size: 0.9rem;
  }

  .recommended-products h3 {
    font-size: 1.2rem;
  }

  .recommended-products .product-list {
    grid-template-columns: repeat(auto-fill, minmax(150px, 1fr));
  }

  .footer {
    padding: 10px;
  }

  .footer-section h4 {
    font-size: 1.1rem;
  }

  .newsletter input {
    font-size: 0.9rem;
  }

  .newsletter button {
    font-size: 0.9rem;
  }
}
</style>
