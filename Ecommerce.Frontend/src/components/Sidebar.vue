<template>
  <div class="sidebar bg-light">
    <div class="sidebar-logo d-flex justify-content-center p-3">
      <img src="@/assets/logo.png" alt="Logo do Projeto" class="logo-img">
    </div>

    <nav class="nav flex-column align-items-center mt-3">
      <router-link to="/dashboard" class="nav-link py-2">
        <span class="material-symbols-outlined">grid_view</span>
      </router-link>
      <router-link to="/faq" class="nav-link py-2">
        <span class="material-symbols-outlined">help</span>
      </router-link>
      <router-link to="/chat-bot" class="nav-link py-2">
        <span class="material-symbols-outlined">smart_toy</span>
      </router-link>
    </nav>

    <div class="sidebar-footer mt-auto p-3 d-flex flex-column align-items-center">
      <router-link to="/notificacoes" class="nav-link py-2">
        <span class="material-symbols-outlined">notifications</span>
        <span class="notification-dot"></span>
      </router-link>
      <router-link to="/perfil" class="nav-link py-2">
        <span class="material-symbols-outlined">person</span>
      </router-link>
      <router-link to="/sobre" class="nav-link py-2">
        <span class="material-symbols-outlined">info</span>
      </router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: "Sidebar",
};
</script>

<style scoped>
.sidebar {
  width: 80px;
  height: 100vh;
  position: fixed;
  top: 0;
  left: 0;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  box-shadow: 3px 0 8px rgba(0, 0, 0, 0.1);
  overflow: hidden;
  z-index: 1000; 
  background-color: #f8f9fa; 
}


.nav {
  flex-grow: 1; 
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: flex-start;
}

.sidebar-footer {
  margin-top: auto; 
  display: flex;
  flex-direction: column;
  align-items: center;
  padding-bottom: 1rem;
}

.logo-img {
  width: 45px;
  height: 45px;
  object-fit: contain;
}

.nav-link {
  color: #666;
  font-size: 1.8rem;
  display: flex;
  justify-content: center;
  position: relative;
}

.nav-link:hover {
  color: #007bff;
}

.notification-dot {
  position: absolute;
  top: 10px;
  right: 15px;
  width: 8px;
  height: 8px;
  background-color: orange;
  border-radius: 50%;
}
</style>
