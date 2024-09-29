<template>
  <div class="d-flex">
    <Sidebar />

    <div class="content p-4 flex-grow-1">
      <h1 class="welcome-message">Olá, {{ nome }} 👋</h1>
      <p class="welcome-submessage">É bom vê-lo(a) novamente.</p>

      <br>
      <div id="carouselExampleIndicators" class="carousel slide mb-4" data-bs-ride="carousel">
        <div class="carousel-indicators">
          <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="0" class="active"
            aria-current="true" aria-label="Slide 1"></button>
          <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="1"
            aria-label="Slide 2"></button>
          <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="2"
            aria-label="Slide 3"></button>
        </div>
        <div class="carousel-inner">
          <div class="carousel-item active">
            <img src="@/assets/carroussel1.jpg" class="d-block w-100" alt="Slide 1">
          </div>
          <div class="carousel-item">
            <img src="@/assets/carroussel2.jpg" class="d-block w-100" alt="Slide 2">
          </div>
          <div class="carousel-item">
            <img src="@/assets/carroussel3.jpg" class="d-block w-100" alt="Slide 3">
          </div>
        </div>
        <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators"
          data-bs-slide="prev">
          <span class="carousel-control-prev-icon" aria-hidden="true"></span>
          <span class="visually-hidden">Previous</span>
        </button>
        <button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators"
          data-bs-slide="next">
          <span class="carousel-control-next-icon" aria-hidden="true"></span>
          <span class="visually-hidden">Next</span>
        </button>
      </div>

      <div class="row">
        <div class="col-md-4">
          <div class="card mb-4 shadow-sm">
            <div class="card-body">
              <h5 class="card-title">Total de Conteúdos Cadastrados</h5>
              <p class="card-text display-4">184</p>
              <div class="growth-info">
                <i class="material-icons trending-icon">trending_up</i>
                <span class="growth-percentage">8.5%</span> Desde o mês passado
              </div>
            </div>
          </div>
        </div>

        <div class="col-md-4">
          <div class="card mb-4 shadow-sm">
            <div class="card-body">
              <h5 class="card-title">Total de FAQs Cadastrados</h5>
              <p class="card-text display-4">184</p>
              <div class="growth-info">
                <i class="material-icons trending-icon">trending_up</i>
                <span class="growth-percentage">8.5%</span> Desde o mês passado
              </div>
            </div>
          </div>
        </div>

        <div class="col-md-4">
          <div class="card mb-4 shadow-sm">
            <div class="card-body">
              <h5 class="card-title">Total de Usuários Ativos</h5>
              <p class="card-text display-4">75</p>
              <div class="growth-info">
                <i class="material-icons trending-icon">trending_up</i>
                <span class="growth-percentage">5%</span> Desde o mês passado
              </div>
            </div>
          </div>
        </div>
      </div>

      <div class="chart-card mb-4 shadow-sm">
        <div class="card-body">
          <h5 class="card-title">Suportes via Chatbot</h5>
          <div class="chart-container">
          </div>
          <div class="d-flex justify-content-between mt-3">
            <span class="text-muted">Novos Suportes</span>
            <span class="text-muted">Repetidos</span>
          </div>
          <div class="d-flex justify-content-between">
            <span class="display-4">1420</span>
            <span class="display-4">25</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import Sidebar from '@/components/Sidebar.vue';

export default {
  name: "HomePage",
  components: {
    Sidebar,
  },
  data() {
    return {
      nome: '',
    };
  },
  mounted() {
    const token = localStorage.getItem('token');
    if (token) {
      const decodedToken = this.parseJwt(token);
      this.nome = decodedToken?.unique_name || 'Usuário';
    }
  },
  methods: {
    parseJwt(token) {
      try {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const jsonPayload = decodeURIComponent(
          atob(base64)
            .split('')
            .map(function (c) {
              return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
            })
            .join('')
        );
        return JSON.parse(jsonPayload);
      } catch (e) {
        return null;
      }
    },
  },
};
</script>

<style scoped>
.welcome-message {
  font-family: 'Roboto', sans-serif;
  font-size: 2rem;
  font-weight: bold;
  margin: 0;
}

.welcome-submessage {
  font-family: 'Roboto', sans-serif;
  font-size: 1.2rem;
  font-weight: 300;
  color: #6c757d;
  margin-top: 0.5rem;
}

.carousel-item img {
  object-fit: cover;
  height: 350px;
  border-radius: 20px;
}

.card-title {
  font-size: 1.25rem;
  font-weight: 500;
}

.card-text {
  font-size: 2rem;
  font-weight: bold;
}

.shadow-sm {
  box-shadow: 0 .125rem .25rem rgba(0, 0, 0, .075) !important;
}

.growth-info {
  display: flex;
  align-items: center;
  font-size: 1rem;
  color: #6c757d;
}

.growth-percentage {
  color: #28a745;
  font-weight: bold;
  margin-right: 0.3rem;
}

.trending-icon {
  color: #28a745;
  margin-right: 0.3rem;
  font-size: 1.5rem;
  display: flex;
  align-items: center;
}

.chart-card {
  width: 520px;
  margin: 0 auto;
  border-radius: 10px;
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
  background-color: #fff;
  border: 1px solid #eaeaea;
  padding: 20px;
}

.chart-container {
  height: 180px;
  width: 180px;
  margin: 0 auto;
}

.chart-card .d-flex {
  justify-content: space-between;
  margin-top: 15px;
}

.chart-card .display-4 {
  font-size: 1.5rem;
  font-weight: 600;
}
</style>
