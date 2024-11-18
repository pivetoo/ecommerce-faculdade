<!-- src/components/CartItem.vue -->
<template>
    <div class="cart-item">
      <img :src="item.image" alt="Imagem do Produto" class="product-image" />
      <div class="item-details">
        <h3 class="item-name">{{ item.name }}</h3>
        <p class="item-price">R$ {{ item.price.toFixed(2) }}</p>
        <select v-if="item.sizeOptions" v-model="item.selectedSize" class="size-select">
          <option v-for="size in item.sizeOptions" :key="size" :value="size">{{ size }}</option>
        </select>
      </div>
      <div class="item-quantity">
        <button @click="decreaseQuantity" class="quantity-button">−</button>
        <span class="quantity">{{ item.quantity }}</span>
        <button @click="increaseQuantity" class="quantity-button">+</button>
      </div>
      <button @click="removeItem" class="remove-button">
        <i class="fas fa-trash-alt"></i>
      </button>
    </div>
  </template>
  
  <script>
  export default {
    name: "CartItem",
    props: {
      item: Object
    },
    methods: {
      increaseQuantity() {
        this.$emit("increase-quantity");
      },
      decreaseQuantity() {
        this.$emit("decrease-quantity");
      },
      removeItem() {
        this.$emit("remove-item");
      }
    }
  };
  </script>
  
  <style scoped>
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
</style>

  