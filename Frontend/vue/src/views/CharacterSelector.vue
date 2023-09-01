<template>
  <div v-if="showCharacter">
    <div class="border-opacity-50 flex justify-start items-center w-full h-screen pl-5">
      <div class="h-80 w-80 card bg-base-300 rounded-box place-items-center text-white p-5">
        <div class="border-b border-b-neutral-50 w-full text-center pb-3">Character Selector</div>
        <div class="max-h-60 overflow-y-scroll">
          <div v-for="character in characters"
               class="bg-base-100/30 hover:bg-base-100/20 p-2 rounded-lg w-72 grid grid-cols-2 items-center gap-x-5 my-3"
               @click="selectCharacter(character.id)">
            <div>{{ character.fullname }}</div>
            <div class="flex justify-end items-center">
              <button class="btn btn-sm w-10" @click="joinCharacter(character.id)">
                <font-awesome-icon icon="fa-solid fa-play"/>
              </button>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import {onMounted, ref} from "vue";

const characters = ref<{ fullname: string, id: number }[]>([{fullname: 'Nico Test', id: 1}, {
  fullname: 'Nico Test',
  id: 1
}, {
  fullname: 'Nico Test',
  id: 1
}, {fullname: 'Nico Test', id: 1}, {fullname: 'Nico Test', id: 1}])
const showCharacter = ref(false)

function selectCharacter(id: number) {
  if ('alt' in window)
    alt.emit("Client:Character:SelectCharacter", id)
}

function joinCharacter(id: number) {
  if ('alt' in window)
    alt.emit("Client:Character:JoinCharacter", id)
}

onMounted(() => {
  if ('alt' in window) {
    alt.on("Webview:Character:OpenSelector", (show: boolean, clientCharacters: { fullname: string, id: number }[]) => {
      characters.value = clientCharacters;
      showCharacter.value = show;
    })
  }
})
</script>