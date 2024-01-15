<template>
     <div v-if="showComponent" class="absolute bottom-0 right-0 p-5">
          <div class="w-96 h-12 rounded-lg grid grid-cols-3 gap-x-2 text-white bg-neutral-900">
               <div class="flex justify-center items-center">
                    <font-awesome-icon icon="fa-solid fa-gauge" class="pr-2" />
                    <div>{{ speed }} Km/h</div>
               </div>
               <div class="flex justify-center items-center">
                    <font-awesome-icon icon="fa-solid fa-gas-pump" class="pr-2" />
                    <div>{{ fuel }} l</div>
               </div>
               <div class="flex justify-center items-center">
                    <font-awesome-icon icon="fa-solid fa-road" class="pr-2" />
                    <div>{{ distance }} km</div>
               </div>
          </div>
     </div>
</template>
<script setup lang="ts">
import { onMounted, ref } from 'vue';

const showComponent = ref(false);
const speed = ref<number>(0);
const fuel = ref<number>(0);
const distance = ref<number>(0);

onMounted(() => {
     if ('alt' in window) {
          alt.on('Webview:Speedometer:Show', () => {
               showComponent.value = true;
          });
          alt.on('Webview:Speedometer:Hide', () => {
               showComponent.value = false;
          });
          alt.on('Webview:Speedometer:Update', (cSpeed, cFuel, cDistance) => {
               speed.value = cSpeed;
               fuel.value = cFuel;
               distance.value = cDistance;
          });
     }
});

</script>