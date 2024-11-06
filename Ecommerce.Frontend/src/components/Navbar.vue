<template>
    <div>
        <nav class="navbar">
            <ul class="nav-links">
                <li><a href="#">Início</a></li>
                <li><a href="#">Produtos</a></li>
                <li><a href="sobrenos">Sobre nós</a></li>
                <li><a href="#">Contato</a></li>
                <li><a href="#">Minha conta</a></li>
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