<template>
    <div class="purchase-page">
      <div class="product-details">
        <img :src="product.imageUrl" alt="Imagem do Produto" class="product-image" />
        <div class="details">
          <h2>{{ product.name }}</h2>
          <p>{{ product.description }}</p>
          
          <div class="color-size-dropdowns">
            <div class="dropdown">
              <button @click="toggleDropdown('color')">Cor</button>
              <div v-if="dropdownVisible.color" class="dropdown-content">
                <select v-model="selectedColor">
                  <option v-for="color in product.colors" :key="color" :value="color">
                    {{ color }}
                  </option>
                </select>
              </div>
            </div>
  
            <div class="dropdown">
              <button @click="toggleDropdown('size')">Tamanho</button>
              <div v-if="dropdownVisible.size" class="dropdown-content">
                <select v-model="selectedSize">
                  <option v-for="size in product.sizes" :key="size" :value="size">
                    {{ size }}
                  </option>
                </select>
              </div>
            </div>
          </div>
  
          <button @click="buyNow">Comprar Agora</button>
          <button @click="addToCart">Adicionar ao Carrinho</button>
        </div>
      </div>
    </div>
  </template>
  
  <script>
  import axios from "axios";
  
  export default {
    data() {
      return {
        product: {},
        selectedColor: '',
        selectedSize: '',
        dropdownVisible: {
          color: false,
          size: false,
        },
      };
    },
    methods: {
      async fetchProduct() {
        const productId = this.$route.params.id;
        try {
          const response = await axios.get(`https://localhost:7172/api/produto/${productId}`);
          this.product = response.data;
        } catch (error) {
          console.error("Erro ao carregar o produto:", error);
        }
      },
      toggleDropdown(type) {
        this.dropdownVisible[type] = !this.dropdownVisible[type];
        if (type === 'color') {
          this.dropdownVisible.size = false;
        } else {
          this.dropdownVisible.color = false;
        }
      },
      buyNow() {
        alert('Produto comprado com sucesso!');
      },
      addToCart() {
        alert('Produto adicionado ao carrinho!');
      },
    },
    mounted() {
      this.fetchProduct();
    },
  };
  </script>
  
  <style scoped>
  .purchase-page {
    display: flex;
    padding: 20px;
  }
  
  .product-details {
    display: flex;
    gap: 20px;
  }
  
  .product-image {
    width: 300px; 
  }
  
  .details {
    flex-grow: 1;
  }
  
  .color-size-dropdowns {
    display: flex;
    gap: 10px;
    margin: 10px 0;
  }
  
  .dropdown {
    position: relative;
    display: inline-block;
  }
  
  .dropdown-content {
    display: block;
    position: absolute;
    background-color: #f9f9f9;
    min-width: 160px;
    box-shadow: 0px 8px 16px 0px rgba(0, 0, 0, 0.2);
    z-index: 1;
  }
  </style>
  