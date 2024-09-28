<template>
    <div class="d-flex">
      <Sidebar />
      <div class="faq-container p-4 flex-grow-1">
        <h1 class="page-title">Perguntas Frequentes (FAQ)</h1>
  
        <div class="faq-list">
          <div class="faq-item" v-for="(faq, index) in faqs" :key="index">
            <h3 class="faq-question" @click="toggleFAQ(index)">
              {{ faq.question }}
              <span class="arrow" :class="{ 'open': faq.open }">▶</span>
            </h3>
            <p v-if="faq.open" class="faq-answer">{{ faq.answer }}</p>
          </div>
        </div>
      </div>
    </div>
    <ChatbotWidget />
  </template>
  
  <script>
  import Sidebar from '@/components/Sidebar.vue';
  import ChatbotWidget from '@/components/ChatbotWidget.vue';
  import { ref } from 'vue';
  
  export default {
    name: 'Faq',
    components: {
      Sidebar,
      ChatbotWidget,
    },
    setup() {
      const faqs = ref([
        {
          question: 'Como posso criar uma conta?',
          answer: 'Para criar uma conta, clique no botão "Criar Conta" na página inicial e preencha os campos necessários.',
          open: false,
        },
        {
          question: 'Esqueci minha senha, o que devo fazer?',
          answer: 'Clique em "Esqueci minha senha" na página de login e siga as instruções para redefinir sua senha.',
          open: false,
        },
        {
          question: 'Como entro em contato com o suporte?',
          answer: 'Você pode entrar em contato com o suporte por meio do nosso chatbot ou enviando um e-mail para suporte@exemplo.com.',
          open: false,
        },
        // Adicione mais perguntas e respostas conforme necessário
      ]);
  
      const toggleFAQ = (index) => {
        faqs.value[index].open = !faqs.value[index].open;
      };
  
      return {
        faqs,
        toggleFAQ,
      };
    },
  };
  </script>
  
  <style scoped>
  .faq-container {
    width: 100%;
    background-color: #f9f9f9;
    padding: 20px;
    border-radius: 8px;
    box-shadow: 0 4px 10px rgba(0, 0, 0, 0.15);
  }
  
  .page-title {
    font-size: 1.8rem;
    margin-bottom: 20px;
  }
  
  .faq-list {
    max-width: 800px;
    margin: 0 auto;
  }
  
  .faq-item {
    margin-bottom: 20px;
  }
  
  .faq-question {
    font-size: 1.2rem;
    cursor: pointer;
    display: flex;
    justify-content: space-between;
    align-items: center;
    background-color: #fff;
    padding: 10px;
    border-radius: 4px;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  }
  
  .faq-answer {
    margin-top: 10px;
    font-size: 1rem;
    padding: 10px;
    background-color: #fff;
    border-radius: 4px;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
  }
  
  .arrow {
    transition: transform 0.3s ease;
  }
  
  .arrow.open {
    transform: rotate(90deg);
  }
  </style>
  