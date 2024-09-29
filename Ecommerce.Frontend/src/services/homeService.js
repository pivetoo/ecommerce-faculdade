import axios from 'axios';

const URL = "https://localhost:7172/api";

export const getTotalFAQs = async () => {
  try {
    const response = await axios.get(`${URL}/FAQ/count`);
    return response.data.total;
  } catch (error) {
    console.error("Erro ao obter o total de FAQs:", error);
    throw error;
  }
};

export const getTotalConteudos = async () => {
  try {
    const response = await axios.get(`${URL}/chatbotcontent/count`);
    return response.data.total;
  } catch (error) {
    console.error("Erro ao obter o total de Conteúdos:", error);
    throw error;
  }
};

export const getTotalUsuariosAtivos = async () => {
  try {
    const response = await axios.get(`${URL}/usuario/count`);
    return response.data.total;
  } catch (error) {
    console.error("Erro ao obter o total de Usuários Ativos:", error);
    throw error;
  }
};