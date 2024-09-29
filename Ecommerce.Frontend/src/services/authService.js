import axios from 'axios';
const URL = "https://localhost:7172/api";

export const login = (email, senha) => {
  return axios.post(`${URL}/Login`, { email, senha });
};

export const register = (nome, email, senha) => {
  return axios.post(`${URL}/Usuario`, { nome, email, senha });
};