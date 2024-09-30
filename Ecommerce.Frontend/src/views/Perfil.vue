<template>
    <div class="perfil-container">
      <!-- Sidebar -->
      <Sidebar />
  
      <!-- Perfil -->
      <div class="perfil-content">
        <h1>Perfil do Usuário</h1>
  
        <!-- Imagem de Perfil -->
        <div class="perfil-imagem">
          <img :src="imageSrc || '/default-profile.png'" alt="Imagem do Usuário" />
        </div>
  
        <!-- Informações do Usuário -->
        <div class="perfil-info">
          <div class="form-group">
            <label for="nome">Nome</label>
            <input type="text" id="nome" v-model="usuario.nome" disabled />
          </div>
  
          <div class="form-group">
            <label for="email">Email</label>
            <input 
              type="email" 
              id="email" 
              v-model="usuario.email" 
              @blur="validarEmail" 
            />
            <span v-if="emailErro" class="erro">{{ emailErro }}</span>
          </div>
  
          <div class="form-group">
            <label for="telefone">Telefone</label>
            <input 
              type="tel" 
              id="telefone" 
              v-model="usuario.telefone" 
              @blur="validarTelefone" 
            />
            <span v-if="telefoneErro" class="erro">{{ telefoneErro }}</span>
          </div>
  
          <!-- Novo campo de Endereço -->
          <div class="form-group">
            <label for="endereco">Endereço</label>
            <input 
              type="text" 
              id="endereco" 
              v-model="usuario.endereco" 
              placeholder="Digite seu endereço completo" 
            />
          </div>
  
          <button @click="salvarPerfil">Salvar</button>
        </div>
      </div>
    </div>
</template>
  
<script>
  import axios from 'axios';
  import Sidebar from '@/components/Sidebar.vue';
  
  export default {
    name: "Perfil",
    components: {
      Sidebar,
    },
    data() {
      return {
        usuario: {
          id: null,
          nome: '',
          telefone: '',
          email: '',
          endereco: '', 
          imageUrl: ''
        },
        imageSrc: null,
        emailErro: null, // Para armazenar a mensagem de erro de email
        telefoneErro: null, // Para armazenar a mensagem de erro de telefone
      };
    },
    methods: {
      parseJwt(token) {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        const jsonPayload = decodeURIComponent(
          atob(base64)
            .split('')
            .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
            .join('')
        );
        return JSON.parse(jsonPayload);
      },
  
      // Validação de email
      validarEmail() {
        const regexEmail = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
        if (!regexEmail.test(this.usuario.email)) {
          this.emailErro = "Por favor, insira um email válido.";
        } else {
          this.emailErro = null;
        }
      },
  
      // Validação de telefone
      validarTelefone() {
        const regexTelefone = /^(?:\(?\d{2}\)?[\s-]?)?(?:\d{4,5}[\s-]?\d{4})$/;
        if (!regexTelefone.test(this.usuario.telefone)) {
          this.telefoneErro = "Por favor, insira um telefone válido (com DDD e 10 ou 11 dígitos).";
        } else {
          this.telefoneErro = null;
        }
      },
  
      async carregarUsuario() {
        try {
          const token = localStorage.getItem('token');
          if (token) {
            const decodedToken = this.parseJwt(token);
            const userId = decodedToken['nameid'];
  
            const response = await axios.get(`https://localhost:7172/api/Usuario`, { params: { id: userId } });
            this.usuario = response.data;
            this.imageSrc = this.usuario.imageUrl;
          }
        } catch (error) {
          console.error('Erro ao carregar usuário:', error);
        }
      },
  
      async salvarPerfil() {
        this.validarEmail(); // Valida o email antes de salvar
        this.validarTelefone(); // Valida o telefone antes de salvar
        if (this.emailErro || this.telefoneErro) {
          alert("Erro: Verifique os campos antes de salvar.");
          return;
        }
  
        try {
          const token = localStorage.getItem('token');
          if (token) {
            const decodedToken = this.parseJwt(token);
            const userId = decodedToken['nameid'];
  
            await axios.put(`https://localhost:7172/api/Usuario/${userId}`, this.usuario, {
              headers: {
                Authorization: `Bearer ${token}`,
              }
            });
            alert('Perfil atualizado com sucesso!');
          }
        } catch (error) {
          console.error('Erro ao salvar perfil:', error);
          alert('Erro ao atualizar perfil. Tente novamente.');
        }
      }
    },
    mounted() {
      this.carregarUsuario();
    }
  };
</script>
  
<style scoped>
  .perfil-container {
    display: flex;
    justify-content: center;
    align-items: center;
    min-height: 100vh;
    width: 100vw;
    background-color: #f0f0f0;
  }
  
  .perfil-content {
    display: flex;
    flex-direction: column;
    align-items: center;
    background-color: #ffffff;
    padding: 60px; 
    border-radius: 10px;
    box-shadow: 0px 4px 10px rgba(0, 0, 0, 0.1);
    width: 90vw; 
    max-width: 1500px; 
    min-height: 90vh; 
  }
  
  .perfil-imagem {
    margin-bottom: 40px; 
  }
  
  .perfil-imagem img {
    width: 250px; 
    height: 250px; 
    border-radius: 50%;
    object-fit: cover;
  }
  
  .perfil-info {
    width: 100%;
    max-width: 800px; 
    display: flex;
    flex-direction: column;
    align-items: center;
  }
  
  .form-group {
    margin-bottom: 25px; 
    width: 100%;
  }
  
  .form-group label {
    font-size: 1.2rem; 
    display: block;
    font-weight: bold;
    margin-bottom: 10px;
    text-align: left;
  }
  
  .form-group input {
    font-size: 1.2rem; 
    width: 100%;
    padding: 15px; 
    box-sizing: border-box;
    border-radius: 5px;
    border: 1px solid #ccc;
  }
  
  button {
    font-size: 1.2rem; 
    padding: 15px 30px; 
    background-color: #4CAF50;
    color: white;
    border: none;
    border-radius: 5px;
    cursor: pointer;
    transition: background-color 0.3s ease;
  }
  
  button:hover {
    background-color: #45a049;
  }
  
  .erro {
    color: red;
    font-size: 1rem; 
    margin-top: 5px;
  }
</style>
  
