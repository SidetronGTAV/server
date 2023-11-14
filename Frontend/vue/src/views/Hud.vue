<template>
     <div v-if="showComponent">
          <div class="absolute right-0 p-5">
               <div class="border w-12 h-12 rounded-lg bg-neutral-800"></div>
               <div class="border w-12 h-12 rounded-lg mt-2 flex justify-center items-center bg-neutral-800">
                    <font-awesome-icon icon="fa-solid fa-bullhorn" class="text-2xl text-white" v-if="config.microphone > 3" />
                    <font-awesome-icon
                         icon="fa-solid fa-microphone"
                         class="text-2xl text-white"
                         :class="[{ 'text-white/25': config.microphone === 1 }, { 'text-white/50': config.microphone === 2 }]"
                         v-else-if="config.microphone > 0" />
                    <font-awesome-icon icon="fa-solid fa-microphone-slash" class="text-2xl text-red-400" v-else />
               </div>
          </div>
     </div>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue';

const showComponent = ref(true);
const config = ref({ hunger: 0, thirst: 0, microphone: 0 });

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
</script>