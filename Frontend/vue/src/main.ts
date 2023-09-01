import { createApp } from 'vue'
import './style.css'
// @ts-ignore
import App from './App.vue'
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { faPlay } from '@fortawesome/free-solid-svg-icons'

library.add(faPlay);

createApp(App)
    .component('font-awesome-icon', FontAwesomeIcon)
    .mount('#app')
