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

    <!-- Listagem de produtos -->
    <div class="product-list w-100 px-5">
      <div class="product-card" v-for="product in filteredProducts" :key="product.id">
        <router-link :to="{ path: '/comprar/' + product.id }" class="product-link">
          <img :src="product.imagemUrl" class="product-image" :alt="product.nome" />
          <div class="product-info">
            <h3 class="product-title">{{ product.nome }}</h3>
            <p class="product-description">{{ product.descricao }}</p>
            <div class="product-footer">
              <span class="product-price">R$ {{ product.preco.toFixed(2) }}</span>
              <router-link :to="{ path: '/comprar/' + product.id }" class="btn-buy">Comprar</router-link>
              <router-link :to="{ path: '/comprar/' + product.id }" class="btn-add-cart">
                <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-cart-plus" viewBox="0 0 16 16">
                  <path d="M9 5.5a.5.5 0 0 0-1 0V7H6.5a.5.5 0 0 0 0 1H8v1.5a.5.5 0 0 0 1 0V8h1.5a.5.5 0 0 0 0-1H9z" />
                  <path d="M.5 1a.5.5 0 0 0 0 1h1.11l.401 1.607 1.498 7.985A.5.5 0 0 0 4 12h1a2 2 0 1 0 0 4 2 2 0 0 0 0-4h7a2 2 0 1 0 0 4 2 2 0 0 0 0-4h1a.5.5 0 0 0 .491-.408l1.5-8A.5.5 0 0 0 14.5 3H2.89l-.405-1.621A.5.5 0 0 0 2 1zm3.915 10L3.102 4h10.796l-1.313 7zM6 14a1 1 0 1 1-2 0 1 1 0 0 1 2 0m7 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0" />
                </svg>
              </router-link>
            </div>
          </div>
        </router-link>
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
      selectedCategory: "",
      email: "",
      successMessage: "Parabéns, você agora faz parte da melhor Newsletter de moda do Brasil!",
      showSuccessCard: false,
      errorMessage: "",
      priceInputsVisible: false,
      priceRange: [1, 1500],
      categoryDropdownVisible: false,
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
/* Estilo Geral da Página */
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
  margin-left: 5%;
}

.filters button {
  border-radius: 30px;
  padding: 10px 20px;
  border: 1px solid #ddd;
  background-color: #f5f5f5;
  color: #333;
  font-size: 1rem;
  cursor: pointer;
  transition: background-color 0.3s, transform 0.2s ease;
}

.filters button:hover {
  background-color: #e0e0e0;
  transform: translateY(-2px); 
}

.filters .dropdown-content {
  display: none;
  position: absolute;
  top: 40px;
  left: 0;
  background-color: #fff;
  border-radius: 8px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  padding: 15px;
  z-index: 10;
  width: 200px;
}

.filters .dropdown:hover .dropdown-content {
  display: block;
}

.filters .price-inputs {
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.filters .price-inputs input {
  padding: 10px;
  font-size: 1rem;
  border: 1px solid #ddd;
  border-radius: 8px;
  transition: border-color 0.3s ease;
}

.filters .price-inputs input:focus {
  border-color: #28a745;
}

.filters select {
  width: 100%;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 8px;
  background-color: #fff;
  font-size: 1rem;
  color: #333;
  transition: border-color 0.3s ease;
}

.filters select:focus {
  border-color: #007bff;
}

/* Listagem de Produtos */
.product-list {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr)); 
  gap: 2rem;
  margin: 2rem auto;
  padding: 0 1rem;
  justify-content: center; 
  gap: 1rem;
}

/* Cartões de Produtos */
.product-card {
  background-color: #fff;
  border: 1px solid #eaeaea;
  border-radius: 8px;
  overflow: hidden;
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  display: flex;
  flex-direction: column;
  text-align: center;
  width: 100%; 
  max-width: 250px; 
  margin: 0 auto; 
}

.product-card:hover {
  transform: scale(1.02);
  box-shadow: 0 8px 20px rgba(0, 0, 0, 0.1);
}

/* Imagens nos Cartões */
.product-image {
  width: 100%;
  height: 200px; 
  object-fit: cover; 
  object-position: center;
}

/* Informações dos Produtos */
.product-info {
  padding: 1rem;
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
  text-decoration: none;
}

.product-title {
  font-size: 1.2rem;
  font-weight: bold;
  text-decoration: none;
}

.product-description {
  font-size: 0.9rem;
  color: #666;
  text-decoration: none;
  white-space: nowrap;
}

.product-footer {
  margin-top: auto;
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-top: 1rem;
}

.product-card a {
  text-decoration: none;
}

.product-title a {
  text-decoration: none;
}

.product-description a {
  text-decoration: none;
}

/* Preço */
.product-price {
  font-size: 1rem;
  color: #28a745;
  font-weight: bold;
  text-decoration: none;
}

/* Botões */
.btn-buy,
.btn-add-cart {
  background-color: #007bff;
  color: #fff;
  padding: 0.5rem 1rem;
  border-radius: 5px;
  text-decoration: none;
  font-size: 0.9rem;
  transition: background-color 0.3s ease;
}

.btn-buy:hover,
.btn-add-cart:hover {
  background-color: #0056b3;
}

/* Responsividade */
@media (max-width: 900px) {
  .product-list {
    grid-template-columns: repeat(auto-fit, minmax(200px, 1fr)); 
    gap: 5rem;
  }
}

@media (max-width: 600px) {
  .product-list {
    grid-template-columns: 1fr; 
    gap: 5rem;
  }

  .product-card {
    max-width: 100%; 
  }
}

/* Limitar a 4 itens por linha em telas grandes */
@media (min-width: 1200px) {
  .product-list {
    grid-template-columns: repeat(4, 1fr); 
    gap: 5rem; 
  }
}

/* Newsletter */
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

/* Sucesso */
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

/* Footer */
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

</style>
