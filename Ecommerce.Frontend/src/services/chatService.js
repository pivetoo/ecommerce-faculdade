import axios from 'axios';

const BASE_URL = 'https://localhost:7172/api';

export const sendMessage = (message) => {
    return axios.post(`${BASE_URL}/Chatbot/pergunta`, {
        pergunta: message,
    });
};

export const feedChatbotContent = (type, message) => {
    return axios.post(`${BASE_URL}/ChatbotContent`, {
        role: type,
        content: message,
    });
};

export const fetchChatbotContents = () => {
    return axios.get(`${BASE_URL}/ChatbotContent`);
};

export const deleteChatbotContent = (contentId) => {
    return axios.delete(`${BASE_URL}/ChatbotContent/${contentId}`);
};