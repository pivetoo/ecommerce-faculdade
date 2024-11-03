<template>
  <div class="product-page">
    <header class="header">
      <!-- Nav Bar -->
      <nav class="navbar">
        <ul>
          <li>Novidades</li>
          <li>Masculino</li>
          <li>Feminino</li>
          <li>Infantil</li>
          <li>Acessórios</li>
          <div class="dropdown">
            <button @click="toggleProductManager">Gerenciador de Produto</button>
            <div v-if="productManagerVisible" class="dropdown-content">
              <button @click="showAddForm">Adicionar Produto</button>
              <button @click="showUpdateForm" :disabled="!selectedProductId">Atualizar Produto</button>
              <button @click="deleteProduct" :disabled="!selectedProductId">Excluir Produto</button>
            </div>
          </div>
        </ul>
      </nav>

      <!-- Filtros -->
      <div class="filters">

        <div class="dropdown">
          <button @click="toggleDropdown('category')">Categoria</button>
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
          <button @click="toggleDropdown('brand')">Marca</button>
          <div v-if="dropdownVisible.brand" class="dropdown-content">
            <select v-model="selectedBrand" @change="filterProducts">
              <option value="">Selecionar Marca</option>
              <option v-for="brand in uniqueBrands" :key="brand" :value="brand">
                {{ brand }}
              </option>
              <option v-if="uniqueBrands.length === 0">Sem opções disponíveis</option>
            </select>
          </div>
        </div>

        <div class="dropdown">
          <button @click="toggleDropdown('size')">Tamanho</button>
          <div v-if="dropdownVisible.size" class="dropdown-content">
            <select v-model="selectedSize" @change="filterProducts">
              <option value="">Selecionar Tamanho</option>
              <option v-for="size in uniqueSizes" :key="size" :value="size">
                {{ size }}
              </option>
              <option v-if="uniqueSizes.length === 0">Sem opções disponíveis</option>
            </select>
          </div>
        </div>

        <div class="dropdown">
          <button @click="toggleDropdown('color')">Cor</button>
          <div v-if="dropdownVisible.color" class="dropdown-content">
            <select v-model="selectedColor" @change="filterProducts">
              <option value="">Selecionar Cor</option>
              <option v-for="color in uniqueColors" :key="color" :value="color">
                {{ color }}
              </option>
              <option v-if="uniqueColors.length === 0">Sem opções disponíveis</option>
            </select>
          </div>
        </div>

        <div class="dropdown">
          <button @click="togglePriceInputs">Preço</button>
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

    <!-- Formulário do Gerenciador de Produtos -->
    <div class="product-manager">
      <div v-if="showForm" class="product-form">
        <h3>{{ isEditing ? "Atualizar Produto" : "Adicionar Produto" }}</h3>
        <form @submit.prevent="isEditing ? updateProduct() : createProduct()">
          <input v-model="form.nome" placeholder="Nome" required />
          <input v-model="form.descricao" placeholder="Descrição" required />
          <input v-model.number="form.preco" placeholder="Preço" required type="number" />
          <input v-model="form.imagemUrl" placeholder="Imagem URL" required />
          <input v-model.number="form.quantidade" placeholder="Quantidade" required type="number" />
          <input v-model="form.categoriaId" placeholder="Categoria ID" required />
          <input v-model="form.cor" placeholder="Cor" required />
          <input v-model="form.tamanho" placeholder="Tamanho" required />
          <button type="submit">{{ isEditing ? "Atualizar" : "Adicionar" }}</button>
        </form>

        <button @click="cancelAction">Cancelar</button>
      </div>
    </div>

    <!-- Listagem de produtos da loja -->

    <div>
      <h3>Lista de Produtos</h3>
      <ul>
        <li v-for="product in filteredProducts" :key="product.id">
          <input type="radio" :value="product.id" v-model="selectedProductId" />
          <div>
            <strong>Nome:</strong> {{ product.name }}<br />
            <strong>Descrição:</strong> {{ product.description }}<br />
            <strong>Preço:</strong> {{ product.price }}<br />
            <strong>Imagem:</strong> <img :src="product.imageURL" alt="Imagem do Produto"
              style="width: 100px; height: auto;" /><br />
            <strong>Quantidade:</strong> {{ product.quantity }}<br />
          </div>
        </li>
      </ul>
    </div>

    <!-- Rodapé -->
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
        const response = await axios.get("https://localhost:7172/api/produto");
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
    subscribe() {
      if (!this.email) {
        this.errorMessage = "O email é obrigatório."; // Define mensagem de erro
        this.showSuccessCard = false; // Certifica-se de que o card de sucesso está oculto
      } else {
        this.errorMessage = ""; // Limpa a mensagem de erro
        this.showSuccessCard = true; // Exibe o card de sucesso
        this.email = ""; // Limpa o campo de email
      }
    },
    closeCard() {
      this.showSuccessCard = false; // Fecha o card
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

    getSizes() {
      this.sizes = [...new Set(this.products.map(product => product.size))];
    },

    getColors() {
      this.colors = [...new Set(this.products.map(product => product.color))];
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

    showAddForm() {
      this.resetForm();
      this.showForm = true;
      this.isEditing = false;
    },

    showUpdateForm() {
      if (this.selectedProductId) {
        const product = this.products.find(p => p.id === this.selectedProductId);
        this.form = { ...product };
        this.showForm = true;
        this.isEditing = true;
      }
    },

    async createProduct() {
      try {
        const response = await axios.post("https://localhost:7172/api/produto", this.form);
        this.products.push(response.data);
        this.cancelAction();
        this.filterProducts();
      } catch (error) {
        console.error("Erro ao criar produto:", error);
      }
    },

    async updateProduct() {
      try {
        const response = await axios.put(`https://localhost:7172/api/produto/${this.form.id}`, this.form);
        const index = this.products.findIndex(p => p.id === this.form.id);
        this.products[index] = response.data;
        this.cancelAction();
        this.filterProducts();
      } catch (error) {
        console.error("Erro ao atualizar produto:", error);
      }
    },

    async deleteProduct() {
      if (this.selectedProductId) {
        try {
          await axios.delete(`https://localhost:7172/api/produto/${this.selectedProductId}`);
          this.products = this.products.filter(p => p.id !== this.selectedProductId);
          this.selectedProductId = null;
          this.filterProducts();
        } catch (error) {
          console.error("Erro ao deletar produto:", error);
        }
      }
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
  /* Cor de fundo verde claro */
  border: 1px solid #d6e9c6;
  /* Borda verde */
  color: #3c763d;
  /* Cor do texto verde escuro */
  padding: 15px;
  border-radius: 5px;
  margin-top: 10px;
  display: flex;
  flex-direction: column;
  /* Coloca os itens em coluna */
  align-items: flex-start;
  /* Alinha os itens à esquerda */
}

.close-button {
  background-color: transparent;
  border: none;
  color: red;
  /* Cor do texto do botão "Fechar" */
  cursor: pointer;
  margin-top: 10px;
  /* Adiciona espaço entre a mensagem e o botão */
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
