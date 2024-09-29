<template>
    <div class="d-flex">
        <Sidebar />
        <div class="perfil p-4 flex-grow-1">
            <h1 class="page-title">Perfil</h1>
        </div>
    </div>
</template>

<script>
import Sidebar from '@/components/Sidebar.vue';

export default {
    name: 'Perfil',
    components: {
        Sidebar,
    },
};
</script>