<template>
    <div class="d-flex">
        <Sidebar />
        <div class="container chatbot-container mt-5">
            <h2 class="chatbot-title">Carrinho</h2>
        </div>
    </div>
</template>

<script>
import Sidebar from '@/components/Sidebar.vue';

export default {
  name: "Carrinho",
  components: {
    Sidebar
  }
}
</script>