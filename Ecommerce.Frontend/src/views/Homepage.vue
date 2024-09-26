<template>
    <div class="container-fluid dashboard">
      <!-- Seção de cabeçalho com saudação e banner -->
      <section class="dashboard-header row mb-4">
        <div class="col-12 greeting mb-3">
          <h2>Olá, {{ userName }} 👋</h2>
          <p>É bom vê-lo(a) novamente.</p>
        </div>
        <div class="col-12 banner">
          <img src="@/imagens/background.jpeg" alt="Banner" class="img-fluid rounded">
        </div>
      </section>
  
      <!-- Seção de cards com métricas -->
      <section class="dashboard-cards row">
        <div class="col-md-4 mb-4">
          <div class="card shadow-sm">
            <div class="card-body">
              <h5 class="card-title">Suportes via Chatbot</h5>
              <p class="card-text display-4">1420</p>
              <small class="text-muted">25 Repetidos</small>
            </div>
          </div>
        </div>
  
        <div class="col-md-4 mb-4">
          <div class="card shadow-sm">
            <div class="card-body">
              <h5 class="card-title">Total de Conteúdos Cadastrados</h5>
              <p class="card-text display-4">184</p>
              <small class="text-muted">8.5% Desde o mês passado</small>
            </div>
          </div>
        </div>
  
        <div class="col-md-4 mb-4">
          <div class="card shadow-sm">
            <div class="card-body">
              <h5 class="card-title">Total de Conteúdos Cadastrados</h5>
              <p class="card-text display-4">184</p>
              <small class="text-muted">8.5% Desde o mês passado</small>
            </div>
          </div>
        </div>
      </section>
    </div>
  </template>
  
  <script setup>
  // Declaração da variável para o nome do usuário
  const userName = 'Usuário'; // Altere para valor dinâmico se necessário
  </script>
  
  <style scoped>
  .dashboard {
    padding: 20px;
  }
  
  .dashboard-header .greeting h2 {
    font-size: 2rem;
  }
  
  .dashboard-header .greeting p {
    font-size: 1.2rem;
    color: #666;
  }
  
  .banner img {
    width: 100%;
    border-radius: 8px;
  }
  
  .dashboard-cards {
    display: flex;
    gap: 1.5rem;
  }
  
  /* Estilização dos cards */
  .card {
    text-align: center;
  }
  
  .card-title {
    font-size: 1.25rem;
    margin-bottom: 0.75rem;
  }
  
  .card-text {
    font-size: 2.5rem;
    font-weight: bold;
  }
  
  .text-muted {
    font-size: 0.875rem;
  }
  </style>
  