<template>
  <div class="pagina-pagamento">
    <header class="cabecalho">
      <div>
        <h1>Finalizar Compra</h1>
        <button class="botao-voltar" v-if="pagamentoFinalizado" @click="voltar">Voltar</button>
      </div>
    </header>

    <div class="info-envio" v-if="!pagamentoFinalizado" >
      <h2>Informações de Envio</h2>
      <form @submit.prevent="proximoPasso">
        <div>
          <label for="nomeCompleto">Nome Completo:</label><br>
          <input type="text" v-model="infoEnvio.nomeCompleto" placeholder="Nome" required maxlength="20"/>
        </div>

        <div>
          <label for="endereco">Endereço:</label><br>
          <input type="text" v-model="infoEnvio.endereco" placeholder="Avenida/Rua..."required maxlength="20"/>
        </div>

        <div>
          <label for="cidade">Cidade:</label><br>
          <input type="text" v-model="infoEnvio.cidade" placeholder="Cidade"required maxlength="20"/>
        </div>

        <div>
  <label for="cep">CEP:</label><br>
  <input type="number" v-model="infoEnvio.cep" placeholder="CEP"required @input="validarCep"
  />
</div>

        <button type="submit" class="botao-pagamento">Próximo</button>
      </form>
    </div>

    <div class="opcoes-pagamento" v-if="pagamentoFinalizado">
      <h2>Escolha o Método de Pagamento</h2>
      <div class="botoes-pagamento">
        <button @click="selecionarMetodoPagamento('cartao')" class="botao-pagamento">Pagar com Cartão</button>
        <button @click="selecionarMetodoPagamento('pix')" class="botao-pagamento">Finalizar com Pix</button>
        <button @click="selecionarMetodoPagamento('applepay')" class="botao-pagamento">Pagar com Apple Pay</button>
      </div>
    </div>

    <div class="info-pagamento" v-if="metodoSelecionado && pagamentoFinalizado" :class="{ 'com-borda': metodoSelecionado }">
      <div v-if="metodoSelecionado === 'cartao'">
        <h2>Pagamento via Cartão</h2>
        <form @submit.prevent="processarPagamento">
          <div>
            <label for="nomeCartao">Nome no Cartão:</label><br>
            <input type="text" v-model="infoPagamento.nome" placeholder="Títular do Cartão"required  maxlength="20"/>
          </div>

          <div>
            <label for="numeroCartao">Número do Cartão:</label><br>
            <input type="number" v-model="infoPagamento.numeroCartao" placeholder="N° Cartão"required @input="validarNumeroCartao"/>
          </div>

          <div>
            <label for="dataValidade">Data de Validade (MM/AA):</label><br>
            <input type="text" v-model="infoPagamento.dataValidade" placeholder="MM/AA" @input="formatarData" required maxlength="5" />
          </div>
          <div>
            <label for="cvv">CVV:</label>
            <br>
            <input type="number" v-model="infoPagamento.cvv" placeholder="CVV" required @input="validarCVV"/>
          </div>

          <button type="submit" class="botao-pagamento">Finalizar Pagamento</button>
        </form>
      </div>

      <div v-if="metodoSelecionado === 'pix'">
        <h2>Pagamento via Pix</h2>
        <p>Use o QR Code abaixo para efetuar o pagamento:</p>
        <div class="qr-code">
          <img src="/src/assets/qrcode.png" alt="QR Code" />
        </div>
      </div>

      <div v-if="metodoSelecionado === 'applepay'">
        <h2>Pagamento via Apple Pay</h2>
        <p>Toque no botão abaixo para pagar com Apple Pay:</p>
        <button class="botao-pagamento">Pagar com Apple Pay</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      infoEnvio: {
        nomeCompleto: '',
        endereco: '',
        cidade: '',
        cep: '',
      },
      infoPagamento: {
        nome: '',
        numeroCartao: '',
        dataValidade: '',
        cvv: '',
      },
      metodoSelecionado: null,
      pagamentoFinalizado: false,
    };
  },
  methods: {
    proximoPasso() {
      this.pagamentoFinalizado = true;
    },
    selecionarMetodoPagamento(metodo) {
      this.metodoSelecionado = metodo;
    },
    processarPagamento() {
      alert('Pagamento realizado com sucesso!');
      this.resetarFormulario();
    },
    voltar() {
      this.pagamentoFinalizado = false; 
      this.metodoSelecionado = null; 
    },
    resetarFormulario() {
      this.infoEnvio = {
        nomeCompleto: '',
        endereco: '',
        cidade: '',
        cep: '',
      };
      this.infoPagamento = {
        nome: '',
        numeroCartao: '',
        dataValidade: '',
        cvv: '',
      };
      this.metodoSelecionado = null;
      this.pagamentoFinalizado = false;
    },
    validarCep(){
    if (this.infoEnvio.cep >= 99999999) {
      this.infoEnvio.cep = this.infoEnvio; 
      }
    },
    validarNumeroCartao() {
    if (this.infoPagamento.numeroCartao >= 9999999999999999) {
      this.infoPagamento.numeroCartao = this.infoPagamento; 
    }
  },
    validarCVV() {
    if (this.infoPagamento.cvv >= 999) {
      this.infoPagamento.cvv = this.infoPagamento; 
      }
    },
  }
};


</script>

<style scoped>
html,
body {
  height: 100%;
  margin: 0;
  font-family: 'Arial', sans-serif;
}

.pagina-pagamento {
  display: flex;
  flex-direction: column;
  justify-content: flex-start;
  align-items: center;
  height: auto;
  min-height: 100vh;
  padding: 40px;
  background-color: #fff;
}

.cabecalho {
  align-self: flex-start;
  margin-bottom: 30px;
}

.botao-voltar{
  padding: 4px 0;
  background-color: #000;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  width: 50px;
  transition: background-color 0.3s;
  white-space: nowrap;
  margin: 5px;
}

h1 {
  font-size: 28px;
  color: #333;
}


.info-envio,
.info-pagamento {
  display: flex;
  flex-direction: column;
  justify-content: center;
  margin-top: 20px;
  width: 100vh;
  max-width: 450px;
  padding: 20px;
  background-color: #ebebeb;
  border: 2px solid #000;
  border-radius: 5px;
}

.opcoes-pagamento {
  display: flex;
  flex-direction: column;
  align-items: center;
  margin-top: 0px;
}

.botoes-pagamento {
  display: flex;
  justify-content: space-between;
  width: 100%;
}

.com-borda {
  border: 2px solid #000;
  border-radius: 5px;
}

.info-envio form,
.info-pagamento form {
  display: flex;
  flex-direction: column;
  
}

.info-envio div,
.info-pagamento div {
  margin-bottom: 15px;
}

input {
  padding: 10px;
  border: 1px solid #ccc;
  border-radius: 5px;
  font-size: 14px;
  width: 80%;
}

.botao-pagamento {
  padding: 12px 0;
  background-color: #000;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 16px;
  width: 200px;
  transition: background-color 0.3s;
  white-space: nowrap;
  margin: 5px;  
}

.botao-pagamento:hover {
  background-color: #444;
}

.qr-code {
  margin-top: 20px;
  text-align: center;
}
</style>
