<template>
  <div v-if="dead" class="min-w-screen min-h-screen w-screen h-screen bg-black transition-opacity"
       :class="transition ? 'opacity-0' : 'opacity-100'" style="transition-duration: 20000ms !important;"/>
</template>

<script setup lang="ts">
import {ref} from "vue";

const dead = ref(false);
const transition = ref(false);

if ('alt' in window) {
  alt.on("Webview:DeadScreen:State", async (state: boolean) => {
    if (!state) {
      transition.value = true;
      setTimeout(() => {
        transition.value = false;
        dead.value = state;
      }, 20000);
    } else {
      dead.value = state;
      transition.value = true;
      setTimeout(() => {
        transition.value = false;
      }, 500);
    }
  })
}
</script>
