<template>
    <div>
      <Navbar />
      <div class="container">
        <!-- Seção de Introdução -->
        <div class="about-intro">
          <h1 class="title">Sobre Nós</h1>
          <p class="intro-text">
            Bem-vindo à nossa loja! Somos uma empresa dedicada a fornecer produtos de alta qualidade e um excelente serviço ao cliente. Nosso objetivo é superar suas expectativas em cada compra.
          </p>
        </div>
  
        <!-- Seção de Informações da Empresa -->
        <div class="company-info">
          <h2 class="section-title">Nossa Missão</h2>
          <p class="section-text">
            Oferecer uma experiência de compra única e satisfatória para cada cliente. Estamos sempre buscando os melhores produtos e inovação para garantir que você tenha acesso ao que há de melhor no mercado.
          </p>
          <h2 class="section-title">Nossa Visão</h2>
          <p class="section-text">
            Ser uma das principais referências de qualidade e confiança no setor de e-commerce.
          </p>
        </div>
  
        <!-- Seção de Equipe -->
        <div class="team">
          <h2 class="section-title">Conheça nossa equipe</h2>
          <div class="team-members">
            <div class="team-member" v-for="(member, index) in teamMembers" :key="index">
              <img :src="member.photo" alt="Foto do membro da equipe" class="team-photo" />
              <div class="member-details">
                <h3 class="member-name">{{ member.name }}</h3>
                <p class="member-role">{{ member.role }}</p>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import Navbar from '../components/Navbar.vue';
  
  export default {
    name: 'SobreNos',
    components: {
      Navbar,
    },
    data() {
      return {
        teamMembers: [
          { name: "Carlos Silva", role: "CEO e Fundador", photo: "https://via.placeholder.com/80" },
          { name: "Ana Souza", role: "Gerente de Operações", photo: "https://via.placeholder.com/80" },
          { name: "João Santos", role: "Desenvolvedor", photo: "https://via.placeholder.com/80" },
        ],
      };
    },
  };
  </script>
  
  <style scoped>
  .container {
    display: flex;
    flex-direction: column;
    padding: 24px;
    background-color: #f5f7fa;
    font-family: Arial, sans-serif;
  }
  
  .about-intro, .company-info, .team {
    background-color: white;
    padding: 20px;
    border-radius: 12px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    margin-bottom: 20px;
  }
  
  .title {
    font-size: 24px;
    font-weight: bold;
    color: #333;
    margin-bottom: 10px;
  }
  
  .intro-text {
    color: #555;
    font-size: 16px;
    line-height: 1.6;
  }
  
  .section-title {
    font-size: 20px;
    font-weight: 600;
    color: #333;
    margin-bottom: 10px;
  }
  
  .section-text {
    color: #555;
    font-size: 16px;
    line-height: 1.6;
  }
  
  .team-members {
    display: flex;
    flex-wrap: wrap;
    gap: 20px;
  }
  
  .team-member {
    display: flex;
    align-items: center;
    width: calc(33% - 20px);
    padding: 16px;
    background-color: #fff;
    border-radius: 12px;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    transition: transform 0.2s;
  }
  
  .team-member:hover {
    transform: scale(1.02);
  }
  
  .team-photo {
    width: 80px;
    height: 80px;
    border-radius: 50%;
    margin-right: 15px;
  }
  
  .member-details {
    flex: 1;
  }
  
  .member-name {
    font-size: 16px;
    font-weight: 600;
    color: #333;
  }
  
  .member-role {
    color: #777;
    font-size: 14px;
  }
  </style>
  