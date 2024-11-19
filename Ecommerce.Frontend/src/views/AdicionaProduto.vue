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
        <span>{{ product.nome }}</span>
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
        <input type="text" v-model="form.imagemUrl" placeholder="URL da Imagem" />
        <input type="number" v-model="form.quantidade" placeholder="Quantidade" required />
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
    <button @click="confirmDeleteCategory" :disabled="!selectedCategoryId" class="rounded-button">Excluir Categoria</button>

    <!-- Lista de Categorias -->
    <ul>
      <li v-for="category in categories" :key="category.id" @click="toggleSelectCategory(category.id)"
        :class="{ 'selected': category.id === selectedCategoryId }">
        <span>{{ category.nome }}</span>
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
import axios from 'axios';

export default {
  data() {
    return {
      // Dados para Gerenciamento de Produtos
      selectedProductId: null,  // Produto selecionado para edição ou exclusão
      form: {
        id: null,
        nome: "",
        descricao: "",
        preco: null,
        imagemUrl: "",
        quantidade: null,
      },
      showForm: false,  // Controle para mostrar ou ocultar o formulário
      isEditing: false,  // Define se está em modo de edição
      products: [],      // Lista de produtos (pode ser preenchida por API)
      message: "",       // Mensagem de sucesso ou erro
      messageType: "",   // Tipo da mensagem ('success' ou 'error')

      // Dados para Gerenciamento de Categorias
      selectedCategoryId: null,  // Categoria selecionada para edição ou exclusão
      categoryForm: {
        nome: "",  // Removido o id
      },
      showCategoryForm: false,  // Controle para mostrar ou ocultar o formulário de categoria
      isEditingCategory: false,  // Define se está em modo de edição de categoria
      categories: [],      // Lista de categorias (pode ser preenchida por API)
      categoryMessage: "",  // Mensagem de sucesso ou erro de categoria
      categoryMessageType: "",  // Tipo da mensagem ('success' ou 'error')
    };
  },
  methods: {
    // Métodos para Gerenciamento de Produtos
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
        const response = await axios.put(`https://localhost:7172/api/Produto/${this.form.id}`, this.form);
        const index = this.products.findIndex(p => p.id === this.form.id);
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
        this.products = this.products.filter(p => p.id !== this.selectedProductId);
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
      this.form = { id: null, nome: "", descricao: "", preco: null, imagemUrl: "", quantidade: null };
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
        if (response.data && response.data.$values) {
          this.products = response.data.$values;
        } else {
          this.products = Array.isArray(response.data) ? response.data : [];
        }
        this.setMessage("Produtos carregados com sucesso.", "success");
      } catch (error) {
        console.error("Erro ao buscar produtos:", error);
        this.setMessage("Erro ao buscar produtos. Tente novamente.", "error");
      }
    },
    toggleSelectProduct(productId) {
      if (this.selectedProductId === productId) {
        this.selectedProductId = null;
      } else {
        this.selectedProductId = productId;
      }
    },

    // Métodos para Gerenciamento de Categorias
    showAddCategoryForm() {
      this.resetCategoryForm();
      this.showCategoryForm = true;
      this.isEditingCategory = false;
    },
    async createCategory() {
      try {
        const response = await axios.post("https://localhost:7172/api/Categoria", {
          nome: this.categoryForm.nome, // Envia apenas o nome da categoria
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
        this.categories = response.data.$values || response.data;
        this.setCategoryMessage("Categorias carregadas com sucesso.", "success");
      } catch (error) {
        console.error("Erro ao buscar categorias:", error);
        this.setCategoryMessage("Erro ao buscar categorias. Tente novamente.", "error");
      }
    },
    toggleSelectCategory(categoryId) {
      if (this.selectedCategoryId === categoryId) {
        this.selectedCategoryId = null;
      } else {
        this.selectedCategoryId = categoryId;
      }
    },
    confirmDeleteCategory() {
      if (this.selectedCategoryId) {
        const confirmation = window.confirm("Tem certeza de que deseja excluir esta categoria?");
        if (confirmation) {
          this.deleteCategory();
        }
      }
    },
    async deleteCategory() {
      try {
        await axios.delete(`https://localhost:7172/api/Categoria/${this.selectedCategoryId}`);
        this.categories = this.categories.filter(c => c.id !== this.selectedCategoryId);
        this.selectedCategoryId = null;
        this.setCategoryMessage("Categoria excluída com sucesso!", "success");
      } catch (error) {
        console.error("Erro ao deletar categoria:", error);
        this.setCategoryMessage("Erro ao excluir categoria. Tente novamente.", "error");
      }
    }
  },
  created() {
    this.fetchProducts();
    this.fetchCategories();
  }
};
</script>

<style scoped>
.selected {
  font-weight: bold;
  color: green;
}

.rounded-button {
  padding: 10px;
  margin: 10px;
  border-radius: 5px;
  background-color: #28a745;
  color: white;
  border: none;
  cursor: pointer;
}

.message {
  padding: 10px;
  margin-top: 10px;
}

.success {
  background-color: #d4edda;
  color: #155724;
}

.error {
  background-color: #f8d7da;
  color: #721c24;
}

.check-icon {
  display: inline-block;
  margin-left: 10px;
  color: green;
}
</style>
