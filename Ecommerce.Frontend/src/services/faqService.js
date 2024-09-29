import axios from 'axios';
const URL = "https://localhost:7172/api/FAQ";

export const fetchFAQs = (page = 1, pageSize = 8) => {
    return axios.get(URL, {
        params: {
            pageNumber: page,
            pageSize: pageSize
        }
    });
};

export const addFaq = (faqData) => {
    return axios.post(URL, faqData);
};

export const deleteFaq = (faqId) => {
    return axios.delete(`${URL}/${faqId}`);
};

export const uploadImage = (file) => {
    const formData = new FormData();
    formData.append('image', file);

    return axios.post(`${URL}/UploadImage`, formData, {
        headers: {
            'Content-Type': 'multipart/form-data'
        }
    });
};
