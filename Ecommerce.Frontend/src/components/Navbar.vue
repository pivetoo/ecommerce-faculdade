<template>
    <div>
        <nav class="navbar">
            <ul class="nav-links">
                <li><router-link to="/ecommerce">Inicio</router-link></li>   
                <li><router-link to="/sobrenos">Sobre Nós</router-link></li>
                <li><router-link to="/">Minha conta</router-link></li>
            </ul>
            <div class="nav-icons">
                <i class="fas fa-search"></i>
                <i class="fas fa-shopping-cart"></i>
                <i class="fas fa-user"></i>
            </div>
        </nav>
    </div>
</template>

<script>
import { RouterLink } from 'vue-router';

export default {
  name: "Navbar",
};
</script>

<style scoped>
.navbar {
    display: flex;
    justify-content: center;
    padding: 16px 24px;
    background-color: #ffffff;
    box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.navbar>* {
    margin: 0 12px;
}

.nav-links {
    list-style: none;
    display: flex;
    gap: 20px;
}

.nav-links li {
    font-weight: 500;
}

.nav-links a {
    text-decoration: none;
    color: #333;
}

.nav-icons {
    display: flex;
    gap: 15px;
    color: #333;
}
</style>