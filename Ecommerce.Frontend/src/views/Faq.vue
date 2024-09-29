<template>
  <div class="d-flex">
    <Sidebar />
    <div class="container mt-5">
      <h2 class="listagem text-center mb-4 mt-4">Listagem de FAQs</h2>

      <div class="text-center mb-5">
        <button class="btn btn-success" @click="openAddFaqModal" data-bs-toggle="modal" data-bs-target="#addFaqModal">
          Adicionar FAQ
        </button>
      </div>

      <div class="row">
        <div v-for="card in cards" :key="card.id" class="col-md-3 mb-4">
          <div class="card h-100">
            <img :src="card.imagemUrl" class="card-img-top" alt="Card image">
            <div class="card-body d-flex flex-column">
              <h5 class="card-title">{{ card.titulo }}</h5>
              <p class="card-text">{{ card.descricao }}</p>
              <div class="d-flex justify-content-between mt-auto">
                <a :href="getAbsoluteUrl(card.saibaMais)" target="_blank"
                  class="btn btn-primary btn-sm flex-grow-1 me-1">Saiba Mais</a>
                <button class="btn btn-danger btn-sm flex-grow-1 ms-1" @click="confirmDelete(card.id)"
                  data-bs-toggle="modal" data-bs-target="#confirmDeleteModal">Remover</button>
              </div>
            </div>
          </div>
        </div>
      </div>

      <nav aria-label="Page navigation">
        <ul class="pagination justify-content-center">
          <li class="page-item" :class="{ disabled: currentPage === 1 }">
            <button class="page-link" @click="changePage(currentPage - 1)">Anterior</button>
          </li>
          <li v-for="page in totalPages" :key="page" class="page-item" :class="{ active: currentPage === page }">
            <button class="page-link" @click="changePage(page)">{{ page }}</button>
          </li>
          <li class="page-item" :class="{ disabled: currentPage === totalPages }">
            <button class="page-link" @click="changePage(currentPage + 1)">Próximo</button>
          </li>
        </ul>
      </nav>

      <!-- Modal para adicionar FAQ -->
      <div class="modal fade" id="addFaqModal" tabindex="-1" aria-labelledby="addFaqModalLabel" aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="addFaqModalLabel">Adicionar FAQ</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              <div class="mb-3">
                <label for="faqTitulo" class="form-label">Título</label>
                <input type="text" v-model="newFaq.titulo" class="form-control" id="faqTitulo"
                  placeholder="Título do FAQ">
              </div>
              <div class="mb-3">
                <label for="faqDescricao" class="form-label">Descrição</label>
                <textarea v-model="newFaq.descricao" class="form-control" id="faqDescricao" rows="3"
                  placeholder="Descrição do FAQ"></textarea>
              </div>
              <div class="mb-3">
                <label for="faqImagem" class="form-label">Imagem</label>
                <input type="file" class="form-control" id="faqImagem" @change="uploadFaqImage">
              </div>
              <div class="mb-3">
                <label for="faqSaibaMais" class="form-label">Saiba Mais (URL)</label>
                <input type="text" v-model="newFaq.saibaMais" class="form-control" id="faqSaibaMais"
                  placeholder="URL Saiba Mais">
              </div>
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
              <button type="button" class="btn btn-success" @click="addNewFaq" data-bs-dismiss="modal">Salvar
                FAQ</button>
            </div>
          </div>
        </div>
      </div>

      <!-- Modal de confirmação de exclusão -->
      <div class="modal fade" id="confirmDeleteModal" tabindex="-1" aria-labelledby="confirmDeleteModalLabel"
        aria-hidden="true">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title" id="confirmDeleteModalLabel">Confirmar Exclusão</h5>
              <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
            </div>
            <div class="modal-body">
              Tem certeza de que deseja excluir este FAQ?
            </div>
            <div class="modal-footer">
              <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
              <button type="button" class="btn btn-danger" @click="handleDelete"
                data-bs-dismiss="modal">Excluir</button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted } from 'vue';
import Sidebar from '@/components/Sidebar.vue';
import { fetchFAQs, addFaq, uploadImage, deleteFaq } from '@/services/faqService';

const cards = ref([]);
const currentPage = ref(1);
const totalPages = ref(1);
const faqToDelete = ref(null);

const newFaq = ref({
  titulo: '',
  descricao: '',
  imagemUrl: '',
  saibaMais: ''
});

const loadFAQs = async (page = 1) => {
  try {
    const response = await fetchFAQs(page);
    if (response.data && response.data.faQs && Array.isArray(response.data.faQs.$values)) {
      cards.value = response.data.faQs.$values;
    } else {
      cards.value = [];
    }
    totalPages.value = response.data.totalPages || 1;
    currentPage.value = response.data.pageNumber || 1;
  } catch (error) {
    alert('Erro ao carregar FAQs.');
  }
};

const addNewFaq = async () => {
  try {
    await addFaq(newFaq.value);
    newFaq.value = { titulo: '', descricao: '', imagemUrl: '', saibaMais: '' };
    loadFAQs(currentPage.value);
  } catch (error) {
    alert('Erro ao adicionar FAQ.');
  }
};

const uploadFaqImage = async (event) => {
  const file = event.target.files[0];
  if (!file) return;

  try {
    const response = await uploadImage(file);
    newFaq.value.imagemUrl = response.data.imageUrl;
    alert('Imagem carregada com sucesso!');
  } catch (error) {
    alert('Erro ao carregar a imagem.');
  }
};

const handleDelete = async () => {
  if (faqToDelete.value !== null) {
    try {
      await deleteFaq(faqToDelete.value);
      loadFAQs(currentPage.value);
      faqToDelete.value = null;
    } catch (error) {
      alert('Erro ao excluir FAQ.');
    }
  }
};

const openAddFaqModal = () => {
  newFaq.value = { titulo: '', descricao: '', imagemUrl: '', saibaMais: '' };
};

const confirmDelete = (id) => {
  faqToDelete.value = id;
};

const changePage = (page) => {
  if (page > 0 && page <= totalPages.value) {
    loadFAQs(page);
  }
};

onMounted(() => {
  loadFAQs();
});

const getAbsoluteUrl = (url) => {
  if (!url.startsWith('http://') && !url.startsWith('https://')) {
    return `https://${url}`;
  }
  return url;
};
</script>

<style scoped>
.card-img-top {
  width: 100%;
  height: 200px;
  object-fit: cover;
}

.listagem {
  font-family: 'Roboto', sans-serif;
  font-size: 2rem;
  font-weight: bold;
  margin: 0;
}
</style>
