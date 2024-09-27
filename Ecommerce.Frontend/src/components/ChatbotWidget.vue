<template>
  <div>
    <div class="chatbot-widget" v-if="isOpen">
      <div class="chatbot-header">
        <h5>Chatbot</h5>
        <button @click="toggleChatbot" class="close-btn">X</button>
      </div>
      <div class="chatbot-body">
        <p>Olá, como posso ajudar?</p>
      </div>
    </div>

    <div class="chatbot-toggle-btn" @click="toggleChatbot">
      <i class="material-icons">chat</i>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      isOpen: false,
    };
  },
  methods: {
    toggleChatbot() {
      this.isOpen = !this.isOpen;
    },
  },
};
</script>

<style scoped>
.chatbot-toggle-btn {
  position: fixed;
  bottom: 20px;
  right: 20px;
  background-color: #007bff;
  color: white;
  border-radius: 50%;
  width: 50px;
  height: 50px;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
}

.chatbot-widget {
  position: fixed;
  bottom: 80px;
  right: 20px;
  width: 300px;
  height: 400px;
  background-color: white;
  border-radius: 10px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  display: flex;
  flex-direction: column;
}

.chatbot-header {
  background-color: #007bff;
  color: white;
  padding: 10px;
  border-radius: 10px 10px 0 0;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.chatbot-body {
  padding: 10px;
  overflow-y: auto;
}

.close-btn {
  background: none;
  border: none;
  color: white;
  font-size: 1.2rem;
  cursor: pointer;
}
</style>
