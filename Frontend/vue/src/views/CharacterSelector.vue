<template>
  <div v-if="showCharacter">
    <div class="border-opacity-50 flex justify-start items-center w-full h-screen pl-5">
      <div class="h-80 w-80 card bg-base-300 rounded-box place-items-center text-white p-5">
        <div class="border-b border-b-neutral-50 w-full text-center pb-3">Character Selector</div>
        <div class="max-h-60 overflow-y-scroll">
          <div v-for="character in characters"
               class="bg-base-100/30 p-2 rounded-lg w-72 grid grid-cols-2 items-center gap-x-5 my-3">
            <div @click="changeCharacter(character.Id)">{{ character.Fullname }}</div>
            <div class="flex justify-end items-center">
              <button class="btn btn-sm w-10" @click="selectCharacter(character.Id)">
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

const characters = ref<Character[]>([]);
const showCharacter = ref(false)

function changeCharacter(id: number) {
  if ('alt' in window)
    alt.emit("Client:Character:ChangeCharacter", id)
}

function selectCharacter(id: number) {
  if ('alt' in window)
    alt.emit("Client:Character:SelectCharacter", id)
}

onMounted(() => {
  if ('alt' in window) {
    alt.on("Webview:Character:OpenSelector", (show: boolean, clientCharacters: Character[]) => {
      characters.value = clientCharacters;
      showCharacter.value = show;
    })
  }
})
</script>