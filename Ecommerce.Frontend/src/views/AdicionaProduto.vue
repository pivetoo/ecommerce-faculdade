<template>
  <div class="product-manager">
    <h2>Gerenciador de Produtos</h2>
    <!-- Botões de Ação -->
    <button @click="showAddForm" class="rounded-button">Adicionar Produto</button>
    <button @click="showUpdateForm" :disabled="!selectedProductId" class="rounded-button">Editar Produto</button>
    <button @click="deleteProduct" :disabled="!selectedProductId" class="rounded-button">Excluir Produto</button>

    <!-- Formulário de Adição/Alteração de Produto -->
    <div v-if="showForm" class="form-container">
      <h2>{{ isEditing ? "Editar Produto" : "Adicionar Produto" }}</h2>
      <form @submit.prevent="isEditing ? updateProduct() : createProduct">
        <input type="text" v-model="form.nome" placeholder="Nome do Produto" required />
        <input type="text" v-model="form.descricao" placeholder="Descrição" required />
        <input type="number" v-model="form.preco" placeholder="Preço" required step="0.01"/>
        <input type="text" v-model="form.imagemUrl" placeholder="URL da Imagem" />
        <input type="number" v-model="form.quantidade" placeholder="Quantidade" required />
        <select v-model="form.categoriaId">
          <option value="">Selecione a Categoria</option>
          <option v-for="category in categories" :key="category.id" :value="category.id">{{ category.nome }}</option>
        </select>
        <button type="submit">{{ isEditing ? "Salvar alterações" : "Adicionar Produto" }}</button>
        <button @click="cancelAction" type="button">Cancelar</button>
      </form>
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      selectedProductId: null,  // Produto selecionado para edição ou exclusão
      form: {
        id: null,
        nome: "",
        descricao: "",
        preco: null,
        imagemUrl: "",
        quantidade: null,
        categoriaId: "",
      },
      showForm: false,  // Controle para mostrar ou ocultar o formulário
      isEditing: false,  // Define se está em modo de edição
      categories: [],    // Categoria de produtos (pode ser preenchida por API)
      products: [],      // Lista de produtos (pode ser preenchida por API)
    };
  },
  methods: {
    // Mostra o formulário para adicionar um novo produto
    showAddForm() {
      this.resetForm();
      this.showForm = true;
      this.isEditing = false;
    },

    // Mostra o formulário para editar um produto existente
    showUpdateForm() {
      if (this.selectedProductId) {
        const product = this.products.find(p => p.id === this.selectedProductId);
        this.form = { ...product };
        this.showForm = true;
        this.isEditing = true;
      }
    },

    // Cria um novo produto via API
    async createProduct() {
      try {
        const response = await axios.post("https://localhost:7172/api/Produto", this.form);
        this.products.push(response.data);  // Adiciona o produto à lista
        this.cancelAction();
      } catch (error) {
        console.error("Erro ao criar produto:", error);
      }
    },

    // Atualiza um produto existente via API
    async updateProduct() {
      try {
        const response = await axios.put(`https://localhost:7172/api/Produto/${this.form.id}`, this.form);
        const index = this.products.findIndex(p => p.id === this.form.id);
        this.products[index] = response.data;  // Atualiza a lista de produtos
        this.cancelAction();
      } catch (error) {
        console.error("Erro ao atualizar produto:", error);
      }
    },

    // Deleta um produto via API
    async deleteProduct() {
      if (this.selectedProductId) {
        try {
          await axios.delete(`https://localhost:7172/api/Produto/${this.selectedProductId}`);
          this.products = this.products.filter(p => p.id !== this.selectedProductId); // Remove o produto da lista
          this.selectedProductId = null;
        } catch (error) {
          console.error("Erro ao deletar produto:", error);
        }
      }
    },

    // Cancela a ação (oculta o formulário)
    cancelAction() {
      this.resetForm();
      this.showForm = false;
    },

    // Reseta os dados do formulário
    resetForm() {
      this.form = { id: null, nome: "", descricao: "", preco: null, categoriaId: "" };
      this.isEditing = false;
    },

    // Busca categorias disponíveis
    async fetchCategories() {
      try {
        const response = await axios.get("https://localhost:7172/api/Categoria");
        this.categories = response.data;
      } catch (error) {
        console.error("Erro ao buscar categorias:", error);
      }
    },

    // Busca produtos para preencher a lista (se necessário)
    async fetchProducts() {
      try {
        const response = await axios.get("https://localhost:7172/api/Produto");
        this.products = response.data;
      } catch (error) {
        console.error("Erro ao buscar produtos:", error);
      }
    },
  },

  mounted() {
    this.fetchCategories();  // Busca as categorias quando o componente for montado
    this.fetchProducts();    // Busca os produtos quando o componente for montado
  },
};
</script>

<style scoped>
/* Estilos para centralizar o formulário */
.form-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  background-color: #f9f9f9;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.2);
}

.rounded-button {
  background-color: #4CAF50;
  color: white;
  border: none;
  padding: 10px 20px;
  margin: 10px;
  border-radius: 5px;
  cursor: pointer;
}

.rounded-button:disabled {
  background-color: #ccc;
  cursor: not-allowed;
}

form input,
form select {
  display: block;
  margin: 10px 0;
  padding: 8px;
  width: 100%;
  max-width: 300px;
}

form button {
  background-color: #2196F3;
  color: white;
  padding: 10px 20px;
  border: none;
  cursor: pointer;
  margin: 10px 0;
}

form button[type="button"] {
  background-color: #f44336;
}
</style>
