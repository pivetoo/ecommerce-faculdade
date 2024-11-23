<template>
  <div class="product-manager">
    <h2>Gerenciador de Produtos</h2>
    <!-- Botões de Ação para Produtos -->
    <button @click="showAddForm" class="rounded-button">Adicionar Produto</button>
    <button @click="showUpdateForm" :disabled="!selectedProductId" class="rounded-button">Editar Produto</button>
    <button @click="confirmDeleteProduct" :disabled="!selectedProductId" class="rounded-button">Excluir Produto</button>

    <!-- Lista de Produtos -->
    <ul>
      <li v-for="product in products" :key="product.id" @click="toggleSelectProduct(product.id)"
        :class="{ 'selected': product.id === selectedProductId }">
        <div class="product-item">
          <h3>{{ product.nome }}</h3>
          <div class="product-info">
            <span>ID: {{ product.id }} | Preço: {{ product.preco | currency }} | Estoque: {{ product.estoque }}</span>
          </div>
        </div>
        <div v-if="product.id === selectedProductId" class="check-icon">✔</div>
      </li>
    </ul>


    <!-- Formulário de Adição/Alteração de Produto -->
    <div v-if="showForm" class="form-container">
      <h2>{{ isEditing ? "Editar Produto" : "Adicionar Produto" }}</h2>
      <form @submit.prevent="isEditing ? updateProduct() : createProduct()">
        <input type="text" v-model="form.nome" placeholder="Nome do Produto" required />
        <input type="text" v-model="form.descricao" placeholder="Descrição" required />
        <input type="number" v-model="form.preco" placeholder="Preço" required step="0.01" />
        <input type="number" v-model="form.estoque" placeholder="Estoque" required />
        <input type="number" v-model="form.quantidade" placeholder="Quantidade" required />
        <input type="text" v-model="form.imagemUrl" placeholder="URL da Imagem" required />
        <select v-model="form.categoriaId" required>
          <option value="" disabled>Escolha uma categoria</option>
          <option v-for="category in categories" :key="category.id" :value="category.id">
            {{ category.nome }}
          </option>
        </select>
        <button type="submit">{{ isEditing ? "Salvar alterações" : "Adicionar Produto" }}</button>
        <button @click="cancelAction" type="button">Cancelar</button>
      </form>
    </div>

    <!-- Mensagem de Sucesso ou Erro -->
    <div v-if="message" :class="messageType" class="message">
      {{ message }}
    </div>
  </div>

  <div class="category-manager">
    <h2>Gerenciador de Categorias</h2>
    <!-- Botões de Ação para Categorias -->
    <button @click="showAddCategoryForm" class="rounded-button">Adicionar Categoria</button>
    <button @click="confirmDeleteCategory" :disabled="!selectedCategoryId" class="rounded-button">Excluir
      Categoria</button>

    <!-- Lista de Categorias -->
    <ul>
      <li v-for="category in categories" :key="category.id" @click="toggleSelectCategory(category.id)"
        :class="{ 'selected': category.id === selectedCategoryId }">
        <div class="category-item">
          <h3>{{ category.nome }}</h3>
          <div class="category-info">
            <span>ID: {{ category.id }}</span>
          </div>
        </div>
        <div v-if="category.id === selectedCategoryId" class="check-icon">✔</div>
      </li>
    </ul>

    <!-- Formulário de Adição de Categoria -->
    <div v-if="showCategoryForm" class="form-container">
      <h2>{{ isEditingCategory ? "Editar Categoria" : "Adicionar Categoria" }}</h2>
      <form @submit.prevent="isEditingCategory ? updateCategory() : createCategory()">
        <input type="text" v-model="categoryForm.nome" placeholder="Nome da Categoria" required />
        <button type="submit">{{ isEditingCategory ? "Salvar alterações" : "Adicionar Categoria" }}</button>
        <button @click="cancelCategoryAction" type="button">Cancelar</button>
      </form>
    </div>

    <!-- Mensagem de Sucesso ou Erro -->
    <div v-if="categoryMessage" :class="categoryMessageType" class="message">
      {{ categoryMessage }}
    </div>
  </div>
</template>

<script>
import axios from "axios";

export default {
  data() {
    return {
      // Dados para Gerenciamento de Produtos
      selectedProductId: null,
      form: {
        id: null,
        nome: "",
        descricao: "",
        preco: null,
        quantidade: null,
        estoque: null,
        imagemUrl: "",
        categoriaId: null,
      },
      showForm: false,
      isEditing: false,
      products: [],
      message: "",
      messageType: "",

      // Dados para Gerenciamento de Categorias
      selectedCategoryId: null,
      categoryForm: {
        nome: "",
      },
      showCategoryForm: false,
      isEditingCategory: false,
      categories: [],
      categoryMessage: "",
      categoryMessageType: "",
    };
  },
  methods: {
    // Métodos para Produtos
    showAddForm() {
      this.resetForm();
      this.showForm = true;
      this.isEditing = false;
    },
    showUpdateForm() {
      if (this.selectedProductId) {
        const product = this.products.find((p) => p.id === this.selectedProductId);
        this.form = { ...product };
        this.showForm = true;
        this.isEditing = true;
      }
    },
    async createProduct() {
      try {
        const response = await axios.post("https://localhost:7172/api/Produto", this.form);
        this.products.push(response.data);
        this.setMessage("Produto adicionado com sucesso!", "success");
        this.cancelAction();
      } catch (error) {
        console.error("Erro ao criar produto:", error);
        this.setMessage("Erro ao criar produto. Tente novamente.", "error");
      }
    },
    async updateProduct() {
      try {
        const response = await axios.put(
          `https://localhost:7172/api/Produto/${this.form.id}`,
          this.form
        );
        const index = this.products.findIndex((p) => p.id === this.form.id);
        this.products[index] = response.data;
        this.setMessage("Produto atualizado com sucesso!", "success");
        this.cancelAction();
      } catch (error) {
        console.error("Erro ao atualizar produto:", error);
        this.setMessage("Erro ao atualizar produto. Tente novamente.", "error");
      }
    },
    confirmDeleteProduct() {
      if (this.selectedProductId) {
        const confirmation = window.confirm("Tem certeza de que deseja excluir este produto?");
        if (confirmation) {
          this.deleteProduct();
        }
      }
    },
    async deleteProduct() {
      try {
        await axios.delete(`https://localhost:7172/api/Produto/${this.selectedProductId}`);
        this.products = this.products.filter((p) => p.id !== this.selectedProductId);
        this.selectedProductId = null;
        this.setMessage("Produto excluído com sucesso!", "success");
      } catch (error) {
        console.error("Erro ao deletar produto:", error);
        this.setMessage("Erro ao excluir produto. Tente novamente.", "error");
      }
    },
    cancelAction() {
      this.resetForm();
      this.showForm = false;
    },
    resetForm() {
      this.form = { id: null, nome: "", descricao: "", preco: null, quantidade: null, estoque: null, imagemUrl: "", categoriaId: null };
      this.isEditing = false;
    },
    setMessage(message, type) {
      this.message = message;
      this.messageType = type;
      setTimeout(() => {
        this.message = "";
      }, 3000);
    },
    async fetchProducts() {
      try {
        const response = await axios.get("https://localhost:7172/api/Produto");
        this.products = Array.isArray(response.data) ? response.data : response.data.$values || [];
        this.setMessage("Produtos carregados com sucesso.", "success");
      } catch (error) {
        console.error("Erro ao buscar produtos:", error);
        this.setMessage("Erro ao buscar produtos. Tente novamente.", "error");
      }
    },
    toggleSelectProduct(productId) {
      this.selectedProductId = this.selectedProductId === productId ? null : productId;
    },

    // Métodos para Categorias
    showAddCategoryForm() {
      this.resetCategoryForm();
      this.showCategoryForm = true;
      this.isEditingCategory = false;
    },
    async createCategory() {
      try {
        const response = await axios.post("https://localhost:7172/api/Categoria", {
          nome: this.categoryForm.nome,
        });
        this.categories.push(response.data);
        this.setCategoryMessage("Categoria adicionada com sucesso!", "success");
        this.cancelCategoryAction();
      } catch (error) {
        console.error("Erro ao criar categoria:", error);
        this.setCategoryMessage("Erro ao criar categoria. Tente novamente.", "error");
      }
    },
    cancelCategoryAction() {
      this.resetCategoryForm();
      this.showCategoryForm = false;
    },
    resetCategoryForm() {
      this.categoryForm = { nome: "" };
      this.isEditingCategory = false;
    },
    setCategoryMessage(message, type) {
      this.categoryMessage = message;
      this.categoryMessageType = type;
      setTimeout(() => {
        this.categoryMessage = "";
      }, 3000);
    },
    async fetchCategories() {
      try {
        const response = await axios.get("https://localhost:7172/api/Categoria");
        this.categories = Array.isArray(response.data)
          ? response.data
          : response.data.$values || [];
        this.setCategoryMessage("Categorias carregadas com sucesso.", "success");
      } catch (error) {
        console.error("Erro ao buscar categorias:", error);
        this.setCategoryMessage("Erro ao buscar categorias. Tente novamente.", "error");
      }
    },
    toggleSelectCategory(categoryId) {
      this.selectedCategoryId = this.selectedCategoryId === categoryId ? null : categoryId;
    },
  },
  mounted() {
    this.fetchProducts();
    this.fetchCategories();
  },
  filters: {
    currency(value) {
      return new Intl.NumberFormat("pt-BR", {
        style: "currency",
        currency: "BRL",
      }).format(value);
    },
  },
};
</script>

<style scoped>
/* Estilo Global */
body {
  font-family: 'Roboto', sans-serif;
  background-color: #121212;
  color: #fff;
  margin: 0;
  padding: 0;
  display: flex;
  flex-direction: column;
  align-items: center;
}

h2 {
  font-size: 2rem;
  margin-bottom: 20px;
  color: #f8f9fa;
  text-align: center;
}

/* Botões */
.rounded-button {
  padding: 12px 20px;
  margin: 10px;
  border-radius: 50px;
  background: linear-gradient(45deg, #6a11cb, #2575fc);
  color: white;
  font-weight: bold;
  border: none;
  cursor: pointer;
  transition: transform 0.2s, box-shadow 0.3s;
}

.rounded-button:hover {
  transform: scale(1.05);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
}

.rounded-button:disabled {
  background: #888;
  cursor: not-allowed;
}

button:focus {
  outline: none;
}

/* Formulários */
.form-container {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  display: flex;
  flex-direction: column;
  justify-content: center;
  align-items: center; 
  background: rgb(48, 53, 63);
  padding: 20px;
  border-radius: 10px;
  width: 100%;
  max-width: 600px; 
  box-shadow: 0 4px 10px rgba(0, 0, 0, 0.3); 
  z-index: 9999; 
}

h2 {
  color: #2196F3;
}

input,
select {
  width: 100%;
  padding: 12px;
  margin: 10px 0;
  border: 1px solid #444;
  border-radius: 8px;
  background: #2c2f36;
  color: #f8f9fa;
  font-size: 1rem;
  box-sizing: border-box;
  transition: background 0.3s, border 0.3s;
}

input:focus,
select:focus {
  background: #444;
  border: 1px solid #2575fc;
  outline: none;
}

button[type="submit"] {
  background: linear-gradient(45deg, #28a745, #2196F3);
  border: none;
  padding: 12px;
  border-radius: 50px;
  color: white;
  font-weight: bold;
  width: 100%;
  cursor: pointer;
  transition: background 0.3s, box-shadow 0.3s;
}

button[type="submit"]:hover {
  background: linear-gradient(45deg, #2196F3, #28a745);
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
}

button[type="button"] {
  background: #d9534f;
  border: none;
  padding: 12px;
  border-radius: 50px;
  color: white;
  font-weight: bold;
  width: 100%;
  cursor: pointer;
  transition: background 0.3s, box-shadow 0.3s;
}

button[type="button"]:hover {
  background: #c9302c;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
}

.message {
  padding: 15px;
  border-radius: 8px;
  margin-top: 20px;
  max-width: 500px;
  text-align: center;
  font-weight: bold;
  margin: 0 auto; 
  display: flex; 
  justify-content: center; 
  align-items: center; 
  min-height: 50px; 
}

/* Mensagem de Sucesso */
.success {
  background-color: #28a745;
  color: white;
}

/* Mensagem de Erro */
.error {
  background-color: #dc3545;
  color: white;
}

/* Itens de Produtos e Categorias */
.product-item, .category-item {
  padding: 15px;
  border-radius: 10px;
  margin-bottom: 10px;
  cursor: pointer;
  transition: background 0.3s, transform 0.2s;
}

.product-info, .category-info {
  margin-top: 5px;
  font-size: 0.9rem;
  color: #000000;
}

li:hover {
  background-color: #2195f327;
}

li.selected {
  background: rgba(41, 127, 185, 0.342);
}

/* Ícones de seleção */
.check-icon {
  display: inline-block;
  margin-left: 10px;
  color: #28a745;
}
/* Outros Estilos */
.container {
  width: 100%;
  display: flex;
  justify-content: center;
  flex-direction: column;
  align-items: center;
}
</style>
