import './assets/main.css'
import router from "@/router";
import {createApp} from 'vue'
import {createPinia} from "pinia";
import piniaPersist from 'pinia-plugin-persist'
import App from './App.vue'

const pinia = createPinia()
pinia.use(piniaPersist)

createApp(App).use(router).use(pinia).mount('#app')
