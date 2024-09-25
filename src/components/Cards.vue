<script setup>
import { defineProps } from 'vue';

const props = defineProps({
    id: Number,
    imagem: String,
    nome: String,
    categoria: String,
    subcategoria: String,
    descricao: String,
    valor: Number,
    quantidade_estoque: Number
});
</script>

<template>
    <div class="col-md-4">
        <div class="card mb-5 shadow-sm">
            <router-link :to="{ path: '/comprar', query: { id: props.id } }">
                <img :src="props.imagem" class="bd-placeholder-img card-img-top rounded-start" width="300" height="230" :alt="props.nome">
            </router-link>
            <div class="card-body">
                <h5 class="card-title">{{ props.nome }}</h5>
                <p class="card-text">{{ props.descricao }}</p>
                <div class="d-flex justify-content-between align-items-center">
                    <div class="btn-group">
                        <router-link :to="{ path: '/comprar', query: { id: props.id } }" class="btn btn-sm btn-outline-secondary">Comprar</router-link>
                        <router-link :to="{ path: '/comprar', query: { id: props.id } }" class="btn btn-sm btn-outline-secondary">
                            <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-cart-plus" viewBox="0 0 16 16">
                                <path d="M9 5.5a.5.5 0 0 0-1 0V7H6.5a.5.5 0 0 0 0 1H8v1.5a.5.5 0 0 0 1 0V8h1.5a.5.5 0 0 0 0-1H9z"/>
                                <path d="M.5 1a.5.5 0 0 0 0 1h1.11l.401 1.607 1.498 7.985A.5.5 0 0 0 4 12h1a2 2 0 1 0 0 4 2 2 0 0 0 0-4h7a2 2 0 1 0 0 4 2 2 0 0 0 0-4h1a.5.5 0 0 0 .491-.408l1.5-8A.5.5 0 0 0 14.5 3H2.89l-.405-1.621A.5.5 0 0 0 2 1zm3.915 10L3.102 4h10.796l-1.313 7zM6 14a1 1 0 1 1-2 0 1 1 0 0 1 2 0m7 0a1 1 0 1 1-2 0 1 1 0 0 1 2 0"/>
                            </svg>
                        </router-link>
                    </div>
                    <div class="valor_inicial">
                        <medium style="color: #FF0000; font-size: 23px; font-weight: bold">R$ {{ props.valor.toFixed(2) }}</medium>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<style scoped>
.card {
    max-width: 100%;
    margin-bottom: 1.5rem;
}

.card-body {
    padding: 1rem;
}

.card-title {
    font-size: 1.25rem;
    margin-bottom: 0.75rem;
}

.card-text {
    margin-bottom: 1.25rem;
}

.btn-group {
    margin-right: 0.5rem;
}

.valor_inicial {
    font-size: 1.5rem;
    font-weight: bold;
    color: #FF0000;
}
</style>
