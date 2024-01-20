<template>
     <div v-if="showComponent">
          <div class="absolute right-0 p-5 pt-14 overflow-hidden">
               <div class="w-12 h-12 rounded-lg flex justify-center items-center bg-neutral-900">
                    <font-awesome-icon icon="fa-solid fa-bullhorn" class="text-2xl text-white" v-if="config.microphone > 3" />
                    <font-awesome-icon
                         icon="fa-solid fa-microphone"
                         class="text-2xl text-white"
                         :class="[{ 'text-white/25': config.microphone === 1 }, { 'text-white/50': config.microphone === 2 }]"
                         v-else-if="config.microphone > 0" />
                    <font-awesome-icon icon="fa-solid fa-microphone-slash" class="text-2xl text-red-400" v-else />
               </div>
               <transition>
                    <div v-if="showHunger" class="w-12 h-12 rounded-lg flex mt-2 justify-center items-center bg-neutral-900">
                         <font-awesome-icon icon="fa-solid fa-burger" class="text-2xl text-red-400" />
                    </div>
               </transition>
               <transition>
                    <div v-if="showThirst" class="w-12 h-12 rounded-lg flex mt-2 justify-center items-center bg-neutral-900">
                         <font-awesome-icon icon="glass-water" class="text-2xl text-red-400" />
                    </div>
               </transition>
          </div>
          <div>
               <HudSpeedometer />
          </div>
     </div>
</template>
<script setup lang="ts">
import { onMounted, ref, watch } from 'vue';
import HudSpeedometer from '../components/HUD/Speedometer.vue';

const showComponent = ref(true);
const config = ref({ hunger: 0, thirst: 0, microphone: 1 });
const showHunger = ref(false);
const showThirst = ref(false);

let hungerInterval: number | null = null;
let thirstInterval: number | null = null;

onMounted(() => {
     if ('alt' in window) {
          alt.on('Webview:HUD:Show', () => {
               showComponent.value = true;
          });
          alt.on('Webview:HUD:Hide', () => {
               showComponent.value = false;
          });
          alt.on('Webview:HUD:Update', (data) => {
               config.value = data;
          });
     }
});

function handleHungerInterval(hunger: boolean): void {
     if (hungerInterval && hunger) return;
     if (hunger) {
          showHunger.value = true;
          if (!!hungerInterval) clearInterval(hungerInterval);
          hungerInterval = setInterval(() => {
               showHunger.value = !showHunger.value;
          }, 15000); // 15 seconds
     } else {
          showHunger.value = false;
          if (!!hungerInterval) clearInterval(hungerInterval);
     }
}

function handleThirstInterval(thirst: boolean): void {
     if (thirstInterval && thirst) return;
     if (thirst) {
          showThirst.value = true;
          if (!!thirstInterval) clearInterval(thirstInterval);
          thirstInterval = setInterval(() => {
               showThirst.value = !showThirst.value;
          }, 15000); // 15 seconds
     } else {
          showThirst.value = false;
          if (!!thirstInterval) clearInterval(thirstInterval);
     }
}

watch(
     config,
     (newValue) => {
          handleHungerInterval(newValue.hunger <= 25);
          handleThirstInterval(newValue.thirst <= 25);
     },
     { deep: true },
);
</script>
<style scoped>
.v-enter-active,
.v-leave-active {
     -webkit-animation: roll-in-right 2s forwards;
     animation: roll-in-right 2s forwards;
}

.v-enter-from,
.v-leave-to {
     -webkit-animation: roll-in-right 2s forwards reverse;
     animation: roll-in-right 2s forwards reverse;
}

@-webkit-keyframes roll-in-right {
     0% {
          -webkit-transform: translateX(800px) rotate(540deg);
          transform: translateX(800px) rotate(540deg);
          opacity: 0;
     }
     100% {
          -webkit-transform: translateX(0) rotate(0deg);
          transform: translateX(0) rotate(0deg);
          opacity: 1;
     }
}

@keyframes roll-in-right {
     0% {
          -webkit-transform: translateX(800px) rotate(540deg);
          transform: translateX(800px) rotate(540deg);
          opacity: 0;
     }
     100% {
          -webkit-transform: translateX(0) rotate(0deg);
          transform: translateX(0) rotate(0deg);
          opacity: 1;
     }
}
</style>