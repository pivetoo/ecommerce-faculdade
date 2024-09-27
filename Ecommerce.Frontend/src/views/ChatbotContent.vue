<template>
    <div class="d-flex">
        <Sidebar />
        <div class="content p-4 flex-grow-1">
            <h1 class="page-title">Chatbot Content</h1>
            <div class="chatbot-container">
                <p>Bem-vindo ao Chatbot! Como podemos ajudar você hoje?</p>
                <div class="chat-interface">
                    <input type="text" class="chat-input" placeholder="Digite sua pergunta aqui..." />
                    <button class="send-btn">Enviar</button>
                </div>
            </div>
        </div>
    </div>
    <ChatbotWidget />
</template>

<script>
import Sidebar from '@/components/Sidebar.vue';
import ChatbotWidget from '@/components/ChatbotWidget.vue';

export default {
    name: 'ChatbotContent',
    components: {
        Sidebar,
        ChatbotWidget
    },
};
</script>

<style scoped>
.content {
    padding: 20px;
    margin-left: 80px;
    transition: margin-left 0.3s ease;
}

.page-title {
    font-size: 2rem;
    font-weight: bold;
    margin-bottom: 20px;
    font-family: 'Roboto', sans-serif;
}

.chatbot-container {
    background-color: #f8f9fa;
    padding: 20px;
    border-radius: 10px;
    box-shadow: 0px 2px 8px rgba(0, 0, 0, 0.1);
}

.chat-interface {
    display: flex;
    align-items: center;
    margin-top: 20px;
}

.chat-input {
    flex-grow: 1;
    padding: 10px;
    border-radius: 5px;
    border: 1px solid #ccc;
    margin-right: 10px;
}

.send-btn {
    padding: 10px 20px;
    background-color: #007bff;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
}

.send-btn:hover {
    background-color: #0056b3;
}
</style>