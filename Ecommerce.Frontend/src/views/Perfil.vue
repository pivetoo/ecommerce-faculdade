<template>
    <div class="d-flex layout">
        <Sidebar />

        <div class="container-fluid content-wrapper p-0">
            <div class="user-info text-center px-1 px-md-5 pt-5 pt-md-0">
                <h1 class="title fw-bold">Perfil do Usuário</h1>
                <p class="subtitle fs-5">Atualize suas informações abaixo.</p>
            </div>

            <div class="user-profile d-flex justify-content-center align-items-center w-100">
                <div class="info-section col-12 col-md-8 p-3 text-center">
                    <div class="form-group mb-4">
                        <label for="nome" class="form-label fs-5">Nome Completo</label>
                        <input type="text" v-model="usuario.nome" class="form-control border-2 rounded-0 py-3 px-4 fs-5"
                            id="nome" />
                    </div>

                    <div class="form-group mb-4">
                        <label for="telefone" class="form-label fs-5">Telefone</label>
                        <input type="text" v-model="usuario.telefone"
                            class="form-control border-2 rounded-0 py-3 px-4 fs-5" id="telefone" />
                    </div>

                    <div class="form-group mb-4">
                        <label for="email" class="form-label fs-5">Email</label>
                        <input type="email" v-model="usuario.email"
                            class="form-control border-2 rounded-0 py-3 px-4 fs-5" id="email" />
                    </div>

                    <div class="form-group mb-4">
                        <button class="btn btn-primary" @click="salvarPerfil">Salvar Alterações</button>
                    </div>
                </div>
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
                imageUrl: ''
            },
            imageSrc: null,
            editando: false,
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
.layout {
    display: flex;
}

.content-wrapper {
    flex: 1;
    padding: 0 2rem;
    background-color: var(--background-primary);
    height: 100%;
    margin-left: 250px;
}

.user-info {
    margin-bottom: 2rem;
    margin-top: 7rem;
}

.user-profile {
    display: flex;
    flex-wrap: wrap;
    margin-top: 2rem;
    min-height: 50vh;
    /* Centraliza verticalmente */
}

.photo-section {
    text-align: center;
}

.upload-area {
    width: 80%;
    height: 200px;
    border: 2px dashed #007bff;
    border-radius: 8px;
    cursor: pointer;
    background-color: #f8f9fa;
    display: flex;
    justify-content: center;
    align-items: center;
}

.upload-area p {
    margin: 0;
}

.image-preview {
    position: relative;
    width: 80%;
    height: auto;
}

.image-preview img {
    width: 100%;
    height: auto;
    object-fit: cover;
}

.delete-icon {
    position: absolute;
    top: 0;
    right: 0;
    cursor: pointer;
}

.inactive {
    opacity: 0.6;
    pointer-events: none;
}

.info-section {
    padding: 1rem;
    width: 40%;
}

.form-group {
    text-align: left;
    margin-bottom: 1.5rem;
}

@media (max-width: 768px) {
    .content-wrapper {
        padding: 1rem;
        margin-left: 0;
    }

    .photo-section,
    .info-section {
        width: 100%;
    }

    .upload-area,
    .image-preview {
        width: 100%;
    }
}
</style>
