<template>
    <div class="d-flex">
        <Sidebar />
        <div class="container chatbot-container mt-5">
            <h2 class="chatbot-title">Chatbot</h2>
            <div class="chat-container">
                <div class="chat-messages">
                    <div v-for="(message, index) in messages" :key="index"
                        :class="['chat-message', message.isUser ? 'user-message' : 'bot-message']">
                        {{ message.text }}
                    </div>
                </div>
                <div class="chat-input">
                    <input type="text" v-model="newMessage" @keypress.enter="sendMessage"
                        placeholder="Digite sua mensagem" class="form-control" />
                    <button @click="sendMessage" class="btn btn-primary send-button">
                        <i class="fas fa-paper-plane"></i> Enviar
                    </button>
                </div>

                <div class="mt-4 d-flex justify-content-center">
                    <button @click="openModal" class="btn btn-secondary mr-3">Adicionar Conteudos</button>
                    <button @click="openContentManager" class="btn btn-danger">Gerenciar Conteúdos</button>
                </div>
            </div>

            <div v-if="showModal" class="modal fade show d-block" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Alimentar Chatbot</h5>
                            <button type="button" class="btn-close" @click="closeModal"></button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label for="type">Tipo</label>
                                <select v-model="selectedType" id="type" class="form-control">
                                    <option value="system">System</option>
                                    <option value="user">User</option>
                                    <option value="assistant">Assistant</option>
                                </select>
                            </div>
                            <div class="form-group mt-3">
                                <label for="message">Mensagem</label>
                                <input type="text" id="message" v-model="newFeedMessage" class="form-control"
                                    placeholder="Digite a mensagem">
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" @click="closeModal">Fechar</button>
                            <button type="button" class="btn btn-primary" @click="feedChatbot">Salvar</button>
                        </div>
                    </div>
                </div>
            </div>

            <div v-if="showContentManager" class="modal fade show d-block" tabindex="-1">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title">Gerenciar Conteúdos</h5>
                            <button type="button" class="btn-close" @click="closeContentManager"></button>
                        </div>
                        <div class="modal-body">
                            <div class="form-group">
                                <label>Conteúdos:</label>
                                <ul v-if="contents.length > 0" class="list-group">
                                    <li v-for="content in contents" :key="content.id" class="list-group-item">
                                        <input type="checkbox" v-model="selectedContents" :value="content.id">
                                        {{ content.content }} ({{ content.role }})
                                    </li>
                                </ul>
                                <p v-else>Nenhum conteúdo disponível.</p>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-danger" @click="deleteSelectedContents">Excluir
                                Selecionados</button>
                            <button type="button" class="btn btn-secondary" @click="closeContentManager">Fechar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import Sidebar from '@/components/Sidebar.vue';
import { sendMessage, feedChatbotContent, fetchChatbotContents, deleteChatbotContent } from '@/services/ChatService';

export default {
    name: 'Chatbot',
    components: {
        Sidebar,
    },
    data() {
        return {
            newMessage: '',
            messages: [],
            showModal: false,
            showContentManager: false,
            selectedType: 'system',
            newFeedMessage: '',
            contents: [],
            selectedContents: []
        };
    },
    methods: {
        async sendMessage() {
            if (this.newMessage.trim() === '') return;

            this.messages.push({ text: this.newMessage, isUser: true });

            const messageToSend = this.newMessage;
            this.newMessage = '';

            try {
                const response = await sendMessage(messageToSend);
                this.messages.push({ text: response.data.resposta, isUser: false });
            } catch (error) {
                this.messages.push({ text: 'Erro ao se comunicar com o servidor.', isUser: false });
            }
        },
        openModal() {
            this.showModal = true;
        },
        closeModal() {
            this.showModal = false;
            this.resetModalFields();
        },
        async feedChatbot() {
            if (this.newFeedMessage.trim() === '') return;

            await feedChatbotContent(this.selectedType, this.newFeedMessage);
            this.closeModal();
        },
        resetModalFields() {
            this.selectedType = 'system';
            this.newFeedMessage = '';
        },
        async openContentManager() {
            this.showContentManager = true;
            const response = await fetchChatbotContents();

            if (Array.isArray(response.data.$values)) {
                this.contents = response.data.$values;
            }
        },
        closeContentManager() {
            this.showContentManager = false;
            this.selectedContents = [];
        },
        async deleteSelectedContents() {
            if (this.selectedContents.length === 0) return;

            await Promise.all(this.selectedContents.map(async (contentId) => {
                await deleteChatbotContent(contentId);
            }));

            this.contents = this.contents.filter(content => !this.selectedContents.includes(content.id));
            this.selectedContents = [];
        }
    },
};
</script>

<style scoped>
.chatbot-container {
    max-width: 900px;
    margin: 0 auto;
}

.chatbot-title {
    font-family: 'Roboto', sans-serif;
    text-align: center;
    margin-bottom: 20px;
    font-size: 2rem;
    font-weight: bold;
}

.chat-container {
    background-color: #fff;
    border-radius: 10px;
    padding: 20px;
    box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
    max-width: 600px;
    margin: 0 auto;
}

.chat-messages {
    max-height: 400px;
    overflow-y: auto;
    padding: 10px;
    margin-bottom: 15px;
    border: 1px solid #ccc;
    border-radius: 10px;
    background-color: #f9f9f9;
}

.chat-message {
    padding: 12px 15px;
    margin-bottom: 10px;
    border-radius: 10px;
    font-size: 1rem;
}

.user-message {
    background-color: #d1e7dd;
    align-self: flex-end;
}

.bot-message {
    background-color: #f8d7da;
    align-self: flex-start;
}

.chat-input {
    display: flex;
    align-items: center;
    gap: 10px;
}

.chat-input input {
    flex-grow: 1;
    border-radius: 5px;
}

.mt-4 .btn+.btn {
    margin-left: 15px;
}

.send-button {
    padding: 8px 20px;
    border-radius: 5px;
    background-color: #007bff;
    color: white;
    display: flex;
    align-items: center;
    justify-content: center;
}

.text-center {
    text-align: center;
}

.modal {
    background-color: rgba(0, 0, 0, 0.5);
}

.modal-dialog {
    max-width: 500px;
}

.modal-content {
    background-color: white;
    border-radius: 8px;
    padding: 20px;
}
</style>