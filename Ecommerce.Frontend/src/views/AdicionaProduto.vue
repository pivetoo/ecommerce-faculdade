<template>
  <div class="product-manager">
    <h2>Gerenciador de Produtos</h2>
    <!-- Botões de Ação -->
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
      },
      showForm: false,  // Controle para mostrar ou ocultar o formulário
      isEditing: false,  // Define se está em modo de edição
      products: [],      // Lista de produtos (pode ser preenchida por API)
      message: "",       // Mensagem de sucesso ou erro
      messageType: "",   // Tipo da mensagem ('success' ou 'error')
    };
  },
  methods: {
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
    // Atualiza um produto existente via API
    async updateProduct() {
      try {
        const response = await axios.put(`https://localhost:7172/api/Produto/${this.form.id}`, this.form);
        const index = this.products.findIndex(p => p.id === this.form.id);
        this.products[index] = response.data;  // Atualiza a lista de produtos
        this.setMessage("Produto atualizado com sucesso!", "success");
        this.cancelAction();
      } catch (error) {
        // Adicionando um log detalhado do erro para depuração
        console.error("Erro ao atualizar produto:", error);
        if (error.response) {
          // Resposta da API com status de erro
          console.error("Erro na resposta:", error.response.data);
          console.error("Status:", error.response.status);
          this.setMessage(`Erro ao atualizar produto: ${error.response.data.message || error.response.statusText}`, "error");
        } else if (error.request) {
          // Requisição enviada, mas não houve resposta
          console.error("Erro na requisição:", error.request);
          this.setMessage("Erro ao conectar com o servidor. Tente novamente.", "error");
        } else {
          // Outro erro
          console.error("Erro inesperado:", error.message);
          this.setMessage("Erro inesperado ao atualizar produto. Tente novamente.", "error");
        }
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
  },
  mounted() {
    this.fetchProducts();
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
  width: 90%;
  max-width: 400px;
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

.message {
  padding: 10px;
  border-radius: 5px;
  margin-top: 20px;
  text-align: center;
}

.success {
  background-color: #4CAF50;
  color: white;
}

.error {
  background-color: #f44336;
  color: white;
}

/* Estilos para a lista de produtos */
ul {
  list-style-type: none;
  padding: 0;
  margin: 0;
}

li {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 10px;
  margin: 5px 0;
  border: 2px solid #ccc;
  border-radius: 5px;
  cursor: pointer;
  transition: all 0.3s;
}

li.selected {
  border-color: #4CAF50;
  background-color: #e0ffe0;
}

.check-icon {
  background-color: #4CAF50;
  color: white;
  width: 20px;
  height: 20px;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 14px;
}
</style>
