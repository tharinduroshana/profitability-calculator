import './assets/main.css'
import router from "@/router";
import {createApp} from 'vue'
import {createPinia} from "pinia";
import piniaPersist from 'pinia-plugin-persist'
import App from './App.vue'
import {createI18n} from 'vue-i18n'
import {messages} from "@/utils/languages";

const i18n = new createI18n({
    locale: "EN",
    messages: messages
})

const pinia = createPinia()
pinia.use(piniaPersist)

createApp(App).use(router).use(pinia).use(i18n).mount('#app')
