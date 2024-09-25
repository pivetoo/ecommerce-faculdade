import './assets/style.css'
import './assets/comprar.css'
import './assets/carrinhos.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'

const app = createApp(App)

app.use(router)

app.mount('#app')
