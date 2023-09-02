import {createApp} from 'vue'
import './style.css'
// @ts-ignore
import App from './App.vue'
import {library} from '@fortawesome/fontawesome-svg-core'
import {FontAwesomeIcon} from '@fortawesome/vue-fontawesome'
import {
    faCakeCandles,
    faDna,
    faMarsStroke, faMinus,
    faPlay, faPlus,
    faUser,
    faVenus,
    faVenusMars
} from '@fortawesome/free-solid-svg-icons'

library.add(faPlay, faUser, faDna, faCakeCandles, faVenusMars, faVenus, faMarsStroke, faPlus, faMinus);

createApp(App)
    .component('font-awesome-icon', FontAwesomeIcon)
    .mount('#app')
