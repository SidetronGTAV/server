<template>
  <div>
    <div class="border-opacity-50 flex justify-start items-center w-full h-screen pl-5">
      <div class="h-80 w-80 card bg-base-300 rounded-box place-items-center text-white p-5">
        <div class="border-b border-b-neutral-50 w-full text-center pb-3">Character Selector</div>
        <div class="pt-3">
          <div v-for="character in characters"
               class="bg-neutral-900 p-2 rounded-lg w-72 grid grid-cols-2 items-center gap-x-5"
               @click="selectCharacter(character.id)">
            <div>{{ character.fullname }}</div>
            <div class="flex justify-end items-center">
              <button class="btn btn-sm w-10">
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
import {ref} from "vue";

const characters = ref([{fullname: 'Nico Test', id: 1}])

function selectCharacter(id: number) {

}

function joinCharacter(id: number) {

}
</script>