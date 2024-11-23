<!-- src/views/Carrinho.vue -->
<template>
  <div>
    <Navbar />
    <div class="container">
      <!-- Cart Items -->
      <div class="cart-items">
        <div v-if="!loading">
          <CartItem
            v-for="(item, index) in cartItems"
            :key="index"
            :item="item"
            @increase-quantity="increaseQuantity(index)"
            @decrease-quantity="decreaseQuantity(index)"
            @remove-item="removeItem(index)"
          />
        </div>
        <div v-if="loading" class="loading">Carregando...</div>
      </div>

      <!-- Order Summary -->
      <OrderSummary :subtotal="subtotal" />
    </div>
  </div>
</template>

<script>
import axios from 'axios';
import Navbar from '../components/Navbar.vue';
import CartItem from '../components/Cartitem.vue';
import OrderSummary from '../components/OrderSummary.vue';

export default {
  name: 'Carrinho',
  components: {
    Navbar,
    CartItem,
    OrderSummary
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
/* Estilo do contêiner principal */
.container {
  display: flex;
  justify-content: space-between;
  padding: 24px;
  background-color: #f5f7fa;
  font-family: Arial, sans-serif;
}

/* Contêiner para os itens do carrinho */
.cart-items {
  width: 65%;
  padding-right: 20px;
}

/* Estilos de carregamento para exibir enquanto os dados estão sendo buscados */
.loading {
  font-size: 16px;
  color: #777;
  text-align: center;
  padding: 20px;
}

/* Espaço dedicado ao resumo do pedido */
.order-summary-container {
  width: 30%;
  display: flex;
  flex-direction: column;
  align-items: flex-start;
}
</style>
