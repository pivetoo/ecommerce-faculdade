import axios from 'axios';

const API_URL = 'http://localhost:5173/loja'; // URL do endpoint do backend

export default {
  getProdutos() {
    return axios.get(API_URL);
  },
};
