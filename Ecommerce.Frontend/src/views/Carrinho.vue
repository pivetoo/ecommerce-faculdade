<template>
  <div>
    <Navbar />
    <div class="container">
      <!-- Cart Items -->
      <div class="cart-items">
        <div v-if="!loading" v-for="(item, index) in cartItems" :key="index" class="cart-item">
          <img :src="item.image" alt="Imagem do Produto" class="product-image" />
          <div class="item-details">
            <h3 class="item-name">{{ item.name }}</h3>
            <p class="item-price">R$ {{ item.price.toFixed(2) }}</p>
            <select v-if="item.sizeOptions" v-model="item.selectedSize" class="size-select">
              <option v-for="size in item.sizeOptions" :key="size" :value="size">{{ size }}</option>
            </select>
          </div>
          <div class="item-quantity">
            <button @click="decreaseQuantity(index)" class="quantity-button">−</button>
            <span class="quantity">{{ item.quantity }}</span>
            <button @click="increaseQuantity(index)" class="quantity-button">+</button>
          </div>
          <button @click="removeItem(index)" class="remove-button">
            <i class="fas fa-trash-alt"></i>
          </button>
        </div>
        <div v-if="loading" class="loading">Carregando...</div>
      </div>

      <!-- Order Summary -->
      <div class="order-summary">
        <h2 class="summary-title">Finalizar compra</h2>
        <div class="summary-item">
          <span>Subtotal</span>
          <span>R$ {{ subtotal.toFixed(2) }}</span>
        </div>
        <div class="summary-item">
          <span>Frete</span>
          <span>R$ 20,00</span>
        </div>
        <div class="summary-item">
          <span>Imposto</span>
          <span>R$ 0,00</span>
        </div>
        <div class="summary-total">
          <span>Total</span>
          <span>R$ {{ (subtotal + 20).toFixed(2) }}</span>
        </div>
        <button class="checkout-button">Fazer pedido</button>

      </div>
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import Navbar from '../components/Navbar.vue';

export default {
  name: 'Carrinho',
  components: {
    Navbar,
  },

  data() {
    return {
      cartItems: [],
      loading: true
    };
  },
  computed: {
    subtotal() {
      return this.cartItems.reduce((total, item) => total + item.price * item.quantity, 0);
    }
  },
  methods: {
    fetchCartItems() {
      axios.get('https://localhost:7172/api/Carrinho/')
        .then(response => {
          this.cartItems = response.data;
          this.loading = false;
        })
        .catch(error => {
          console.error("Erro ao buscar itens do carrinho:", error);
          this.loading = false;
        });
    },
    increaseQuantity(index) {
      this.cartItems[index].quantity++;
    },
    decreaseQuantity(index) {
      if (this.cartItems[index].quantity > 1) {
        this.cartItems[index].quantity--;
      }
    },
    removeItem(index) {
      this.cartItems.splice(index, 1);
    }
  },
  created() {
    this.fetchCartItems();
  }
};
</script>

<style scoped>
.container {
  display: flex;
  justify-content: space-between;
  padding: 24px;
  background-color: #f5f7fa;
  font-family: Arial, sans-serif;
}

.cart-items {
  width: 65%;
  padding-right: 20px;
}

.cart-item {
  display: flex;
  align-items: center;
  padding: 16px;
  background-color: white;
  border-radius: 12px;
  margin-bottom: 15px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s;
}

.cart-item:hover {
  transform: scale(1.02);
}

.product-image {
  width: 80px;
  height: 80px;
  object-fit: cover;
  border-radius: 8px;
  margin-right: 15px;
}

.item-details {
  flex: 1;
}

.item-name {
  font-weight: 600;
  font-size: 16px;
  color: #333;
}

.item-price {
  color: #777;
  font-size: 14px;
  margin-top: 5px;
}

.size-select {
  margin-top: 10px;
  padding: 5px;
  border: 1px solid #ddd;
  border-radius: 8px;
}

.item-quantity {
  display: flex;
  align-items: center;
  margin-right: 15px;
}

.quantity-button {
  padding: 6px 12px;
  border: 1px solid #ddd;
  background-color: #f1f1f1;
  cursor: pointer;
  border-radius: 50%;
  font-weight: bold;
  transition: background-color 0.2s;
}

.quantity-button:hover {
  background-color: #e0e0e0;
}

.quantity {
  margin: 0 12px;
  font-weight: bold;
}

.remove-button {
  color: #ff4d4f;
  cursor: pointer;
  font-size: 18px;
  transition: color 0.2s;
}

.remove-button:hover {
  color: #d9363e;
}

.order-summary {
  width: 30%;
  padding: 20px;
  background-color: rgb(255, 255, 255);
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.summary-title {
  font-weight: 600;
  font-size: 18px;
  margin-bottom: 20px;
  color: #333;
}

.summary-item,
.summary-total {
  display: flex;
  justify-content: space-between;
  padding: 10px 0;
  color: #555;
}

.summary-total {
  font-size: 18px;
  font-weight: 700;
  color: #333;
  border-top: 1px solid #eee;
  margin-top: 15px;
}

.checkout-button[data-v-55bf0671] {
  width: 100%;
  padding: 12px;
  background-color: #007bff;
  color: white;
  font-weight: bold;
  text-align: center;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  transition: background-color 0.3s, transform 0.2s;
  box-shadow: 0 4px 6px rgba(0, 123, 255, 0.3);
}

.checkout-button:hover {
  background-color: #0069d9;
  transform: scale(1.03);
}

.promo-code {
  display: flex;
  margin-top: 20px;
}

.promo-input {
  flex: 1;
  padding: 10px;
  border: 1px solid #ddd;
  border-radius: 8px 0 0 8px;
}

.apply-button[data-v-55bf0671] {
  padding: 10px 20px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 0 8px 8px 0;
  cursor: pointer;
  transition: background-color 0.3s, transform 0.2s, box-shadow 0.3s;
  box-shadow: 0 2px 4px rgba(0, 123, 255, 0.2);
}
</style>