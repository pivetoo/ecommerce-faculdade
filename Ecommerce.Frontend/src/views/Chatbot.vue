<template>
    <div class="d-flex">
        <Sidebar />
        <div class="chatbot p-4 flex-grow-1">
            <h1 class="page-title">Chatbot</h1>
        </div>
    </div>
</template>

<script>
import Sidebar from '@/components/Sidebar.vue';

export default {
    name: 'Chatbot',
    components: {
        Sidebar,
    },
};
</script>