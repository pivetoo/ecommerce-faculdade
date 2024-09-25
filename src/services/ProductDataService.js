import axios from 'axios';

const apiClient = axios.create({
    baseURL: 'http://localhost:5057/api',
    headers: {
        'Content-Type': 'application/json'
    }
});

export default {
    getAll() {
        return apiClient.get('/Produto/exibirProdutos');
    },
    get(id) {
        return apiClient.get(`/Produto/exibirProduto/${id}`);
    },
    create(data) {
        return apiClient.post('/Produto/adicionarProduto', data);
    },
    update(id, data) {
        return apiClient.put(`/Produto/updateProduto/${id}`, data);
    },
    delete(id) {
        return apiClient.delete(`/Produto/deleteProduto/${id}`);
    }
};
