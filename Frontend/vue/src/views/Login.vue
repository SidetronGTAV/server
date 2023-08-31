<template>
  <div v-if="inLogin">
    <div class="flex justify-center items-center h-screen w-full border-opacity-50">
      <div class="grid grid-cols-2 card bg-base-300 rounded-box place-items-center w-[50vw] h-60">
        <div class=""></div>
        <div class="text-center">
          <div class="text-2xl text-white pb-7">Login</div>
          <button class="btn btn-wide flex justify-around" @click="startDiscordAuth">
            <img src="../assets/discord.svg" class="h-5" alt="discord Logo"/>
            Login with Discord
          </button>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import {ref} from "vue";

const inLogin = ref(false);

function startDiscordAuth() {
  if ('alt' in window) {
    alt.emit('Client:Login:HandleLogin')
  }
}

alt.on('Webview:Login:OpenLogin', () => inLogin.value = true)
</script>