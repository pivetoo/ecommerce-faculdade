<template>
  <div class="product-page">
    <header class="header">
      <Navbar />
      <!-- Filtros -->
      <div class="filters">
        <div class="dropdown">
          <button @click="toggleDropdown('category')">Categoria<span class="dropdown-arrow"></span></button>
          <div v-if="dropdownVisible.category" class="dropdown-content">
            <select v-model="selectedCategory" @change="filterProducts">
              <option value="">Selecionar Categoria</option>
              <option v-for="category in uniqueCategories" :key="category" :value="category">
                {{ category }}
              </option>
              <option v-if="uniqueCategories.length === 0">Sem opções disponíveis</option>
            </select>
          </div>
        </div>

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
      </div>
    </header>

    <!-- Listagem de produtos da loja -->
    <div>
      <h3>Lista de Produtos</h3>
      <ul>
        <li v-for="product in filteredProducts" :key="product.id">
          <div @click="goToPurchasePage(product.id)" class="product-card">
            <strong>Nome:</strong> {{ product.name }}<br />
            <strong>Descrição:</strong> {{ product.description }}<br />
            <strong>Preço:</strong> {{ product.price }}<br />
            <strong>Imagem:</strong>
            <img :src="product.imageURL" alt="Imagem do Produto" style="width: 100px; height: auto;" /><br />
            <strong>Quantidade:</strong> {{ product.quantity }}<br />
          </div>
        </li>
      </ul>
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
;
export default {
  data() {
    return {
      products: [],
      filteredProducts: [],
      selectedProductId: null,
      form: {
        id: null,
        nome: "",
        descricao: "",
        preco: null,
        imagemUrl: "",
        quantidade: null,
        categoriaId: "",
        cor: "",
        tamanho: "",
      },
      email: "",
      successMessage: "Parabéns, você agora faz parte da melhor Newsletter de moda do Brasil!",
      showSuccessCard: false,
      errorMessage: "",
      showForm: false,
      productManagerVisible: false,
      isEditing: false,
      dropdownVisible: false,
      priceInputsVisible: false,
      categories: [],
      brands: [],
      sizes: [],
      colors: [],
      selectedCategory: "",
      selectedBrand: "",
      selectedSize: "",
      selectedColor: "",
      priceRange: [1, 1500],
    };
  },
  components: {
    Navbar
  },
  computed: {
    uniqueCategories() {
      return [...new Set(this.products.map(product => product.category))];
    },
    uniqueBrands() {
      return [...new Set(this.products.map(product => product.brand))];
    },
    uniqueSizes() {
      return [...new Set(this.products.map(product => product.size))];
    },
    uniqueColors() {
      return [...new Set(this.products.map(product => product.color))];
    },
  },
  methods: {
    async fetchProducts() {
      try {
        const response = await axios.get("https://localhost:7172/api/Produto");
        this.products = response.data;
        this.filteredProducts = this.products;
        this.getCategories();
        this.getBrands();
        this.getSizes();
        this.getColors();
      } catch (error) {
        console.error("Erro ao carregar produtos:", error);
      }
    },
    subscribe() {
      this.showSuccessCard = true;
      this.email = "";
    },
    goToPurchasePage(productId) {
      this.$router.push({ path: `/product/${productId}` });
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
    toggleProductManager() {
      this.productManagerVisible = !this.productManagerVisible;
      console.log('Product Manager Visible:', this.productManagerVisible);
    },

    filterProducts() {
      this.filteredProducts = this.products.filter(product => {
        const matchesCategory = this.selectedCategory ? product.category === this.selectedCategory : true;
        const matchesBrand = this.selectedBrand ? product.brand === this.selectedBrand : true;
        const matchesSize = this.selectedSize ? product.size === this.selectedSize : true;
        const matchesColor = this.selectedColor ? product.color === this.selectedColor : true;
        const matchesPrice = product.price >= this.priceRange[0] && product.price <= this.priceRange[1];

        return matchesCategory && matchesBrand && matchesSize && matchesColor && matchesPrice;
      });
    },

    getCategories() {
      this.categories = [...new Set(this.products.map(product => product.category))];
    },

    getBrands() {
      this.brands = [...new Set(this.products.map(product => product.brand))];
    },

    toggleDropdown() {
      this.dropdownVisible = !this.dropdownVisible;
    },

    togglePriceInputs() {
      this.priceInputsVisible = !this.priceInputsVisible;
    },

    updatePriceRange() {
      if (this.priceRange[1] <= this.priceRange[0]) {
        this.priceRange[1] = this.priceRange[0] + 1;
      }
      this.filterProducts();
    },

    cancelAction() {
      this.resetForm();
      this.showForm = false;
    },

    resetForm() {
      this.form = { id: null, name: "", description: "", price: null, category: "" };
      this.isEditing = false;
    },
  },
  mounted() {
    this.fetchProducts();
  },
};
</script>


<style scoped>
.product-page {
  padding: 20px;
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.rounded-button {
  border-radius: 20px;
  padding: 8px 16px;
  border: 1px solid #ddd;
  background-color: #f1f1f1;
  cursor: pointer;
  transition: background-color 0.3s;
}

.navbar {
  background-color: #f1f1f1;
  padding: 10px 20px;
  box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
}

.navbar ul {
  display: flex;
  gap: 15px;
  list-style: none;
  padding: 0;
  justify-content: center;
  margin: 0;
}

.navbar li {
  color: white;
}


.rounded-button:hover {
  background-color: #e0e0e0;
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


.close-button {
  background-color: transparent;
  border: none;
  color: red;
  cursor: pointer;
  margin-top: 10px;
}

.dropdown {
  position: relative;
  display: inline-block;
}


.dropdown-content {
  display: none;
  position: absolute;
  background-color: #f9f9f9;
  min-width: 160px;
  box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
  z-index: 1;
}

.dropdown-content input[type="text"],
.dropdown-content select {
  width: 100%;
  padding: 8px;
  margin: 4px 0;
  border: 1px solid #ddd;
  border-radius: 4px;
}

.dropdown-content button {
  width: 100%;
  background-color: #4CAF50;
  color: white;
  padding: 8px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

.dropdown-content button:hover {
  background-color: #45a049;
}

.dropdown:hover .dropdown-content {
  display: block;
}

.price-inputs {
  display: flex;
  gap: 10px;
}

.price-inputs input {
  width: 80px;
  padding: 8px;
  border: 1px solid #ddd;
  border-radius: 4px;
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

.product-card {
  cursor: pointer;
  padding: 10px;
  border: 1px solid #ccc;
  margin: 10px 0;
  transition: background-color 0.3s;
}

.product-card:hover {
  background-color: #f0f0f0;
}

.footer {
  margin-top: 15px;
  display: flex;
  justify-content: space-between;
  background-color: #f8f8f8;
  padding: 20px;
}

.footer-section h4,
.newsletter h4 {
  margin-bottom: 10px;
}

.newsletter input[type="email"] {
  padding: 8px;
  margin-top: 5px;
  margin-right: 5px;
}
</style>