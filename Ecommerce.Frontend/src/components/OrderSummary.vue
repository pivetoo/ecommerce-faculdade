<!-- src/components/OrderSummary.vue -->
<template>
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
  </template>
  
  <script>
  export default {
    name: "OrderSummary",
    props: {
      subtotal: Number
    }
  };
  </script>
  
  <style scoped>
.order-summary {
  width: 100%;
  max-width: 300px;
  padding: 20px;
  background-color: #fff;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.summary-title {
  font-weight: 600;
  font-size: 18px;
  margin-bottom: 20px;
  color: #333;
}

.summary-item {
  display: flex;
  justify-content: space-between;
  padding: 10px 0;
  color: #555;
}

.summary-total {
  display: flex;
  justify-content: space-between;
  padding: 15px 0;
  font-size: 18px;
  font-weight: 700;
  color: #333;
  border-top: 1px solid #eee;
  margin-top: 15px;
}

.checkout-button {
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
</style>

  