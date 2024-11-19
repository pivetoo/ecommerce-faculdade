<template>
  <div class="product-page h-100 w-100 p-0">
    <header class="header">
      <Navbar />
      <!-- Filtros -->
      <div class="filters">
        <div class="dropdown">
          <button @click="togglePriceInputs">Preço<span class="dropdown-arrow"></span></button>
          <div v-if="priceInputsVisible" class="dropdown-content">
            <div class="price-inputs">
              <input type="number" v-model="priceRange[0]" @change="updatePriceRange" placeholder="Min" />
              <input type="number" v-model="priceRange[1]" @change="updatePriceRange" placeholder="Max" />
            </div>
            <button @click="filterProducts">Aplicar Filtros</button>
          </div>
        </div>

        <!-- Filtro de Categorias -->
        <div class="dropdown">
          <button @click="toggleCategoryDropdown">Categoria<span class="dropdown-arrow"></span></button>
          <div v-if="categoryDropdownVisible" class="dropdown-content">
            <select v-model="selectedCategory" @change="filterProducts">
              <option value="">Todas as Categorias</option>
              <option v-for="category in categories" :key="category.id" :value="category.id">{{ category.nome }}</option>
            </select>
          </div>
        </div>
      </div>
    </header>

    <!-- Listagem de produtos da loja -->
    <div class="product-list w-100 px-5">
      <div class="col-12 p-3" v-for="product in filteredProducts" :key="product.id">
        <div class="card shadow-sm">
          <!-- Link para página de compra -->
          <router-link :to="{ path: '/comprar/' + product.id }">
            <img :src="product.imagemUrl" class="bd-placeholder-img card-img-top rounded-start" width="300" height="230" :alt="product.nome" />
          </router-link>

          <div class="card-body">
            <h5 class="card-title">{{ product.nome }}</h5>
            <p class="card-text">{{ product.descricao }}</p>
            <div class="d-flex justify-content-between align-items-center">
              <div class="btn-group">
                <!-- Botão Comprar, linkando para a página de compra -->
                <router-link :to="{ path: '/comprar/' + product.id }" class="btn btn-sm btn-outline-secondary">
                  Comprar
                </router-link>
                <!-- Botão Adicionar ao Carrinho -->
                <router-link :to="{ path: '/comprar/' + product.id }" class="btn btn-sm btn-outline-secondary">
                  <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-cart-plus" viewBox="0 0 16 16">
                    <path d="M9 5.5a.5.5 0 0 0-1 0V7H6.5a.5.5 0 0 0 0 1H8v1.5a.5.5 0 0 0 1 0V8h1.5a.5.5 0 0 0 0-1H9z" />
                    <path d="M.5 1a.5.5 0 0 0 0 1h1.11l.401 1.607 1.498 7.985A.5.5 0 0 0 4 12h1a2 2 0 1 0 0 4 2 2 0 0 0 0-4h7a2 2 0 1 0 0 4 2 2 0 0 0 0-4h1a.5.5 0 0 0 .491-.408l1.5-8A.5.5 0 0 0 14.5 3H2.89l-.405-1.621A.5.5 0 0 0 2 1zm3.915 10L3.102 4h10.796l-1.313 7zM6 14a1 1 0 1 1-2 0 1 1 0 0 1 2 0m7 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0" />
                  </svg>
                </router-link>
              </div>
              <div class="valor_inicial">
                <medium style="color: #FF0000; font-size: 23px; font-weight: bold">
                  R$ {{ product.preco.toFixed(2) }}
                </medium>
              </div>
            </div>
          </div>
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
import axios from "axios";
import Navbar from '../components/Navbar.vue';

export default {
  data() {
    return {
      products: [],
      filteredProducts: [],
      categories: [],
      selectedCategory: "", // Variável para armazenar a categoria selecionada
      email: "",
      successMessage: "Parabéns, você agora faz parte da melhor Newsletter de moda do Brasil!",
      showSuccessCard: false,
      errorMessage: "",
      priceInputsVisible: false,
      priceRange: [1, 1500],
      categoryDropdownVisible: false, // Para controlar a visibilidade do dropdown de categorias
    };
  },
  components: {
    Navbar
  },

  methods: {
    async fetchProducts() {
      try {
        const response = await axios.get("https://localhost:7172/api/Produto");
        this.products = response.data.$values || [];
        this.filteredProducts = this.products;

        // Busca as categorias
        const categoryResponse = await axios.get("https://localhost:7172/api/Categoria");
        this.categories = categoryResponse.data.$values || [];
      } catch (error) {
        console.error("Erro ao carregar produtos ou categorias:", error);
      }
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
    closeCard() {
      this.showSuccessCard = false;
    },

    filterProducts() {
      this.filteredProducts = this.products.filter(product => {
        const matchesPrice = product.preco >= this.priceRange[0] && product.preco <= this.priceRange[1];
        const matchesCategory = this.selectedCategory ? product.categoriaId === this.selectedCategory : true;
        return matchesPrice && matchesCategory;
      });
    },

    togglePriceInputs() {
      this.priceInputsVisible = !this.priceInputsVisible;
    },

    toggleCategoryDropdown() {
      this.categoryDropdownVisible = !this.categoryDropdownVisible;
    },

    updatePriceRange() {
      if (this.priceRange[1] <= this.priceRange[0]) {
        this.priceRange[1] = this.priceRange[0] + 1;
      }
      this.filterProducts();
    }
  },
  mounted() {
    this.fetchProducts();
  }
};
</script>

<style scoped>
/* Estilos relevantes para a página de produtos */

.product-page {
  padding: 20px;
  min-height: 100vh;
  display: flex;
  flex-direction: column;
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

.success-card {
  background-color: #dff0d8;
  border: 1px solid #d6e9c6;
  color: #3c763d;
  padding: 15px;
  border-radius: 5px;
  margin-top: 10px;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}

.dropdown-arrow {
  display: inline-block;
  width: 0;
  height: 0;
  margin-left: 5px;
  border-left: 5px solid transparent;
  border-right: 5px solid transparent;
  border-top: 5px solid black;
  vertical-align: middle;
}

.product-list {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 20px;
  margin-top: 20px;
  flex: 1;
}

@media (max-width: 900px) {
  .product-list {
    grid-template-columns: repeat(2, 1fr);
  }
}

@media (max-width: 600px) {
  .product-list {
    grid-template-columns: 1fr;
  }
}

.card {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  min-height: 380px;
  border: 1px solid #ddd;
  border-radius: 8px;
  padding: 20px;
  width: 100%;
  /* Garantir que os cartões ocupem toda a largura disponível */
  height: auto;
}

.card img {
  width: 100%;
  border-radius: 8px;
}

.card-body {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  height: 100%;
}

.card-title {
  font-size: 1.25rem;
  font-weight: bold;
}

.card-text {
  font-size: 1rem;
  color: #555;
  margin-bottom: 15px;
}

.valor_inicial {
  font-size: 1.2rem;
}

.btn-group {
  display: flex;
  gap: 10px;
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
