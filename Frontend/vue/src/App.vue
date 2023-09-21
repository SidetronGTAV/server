<template>
  <div class="flex overflow-hidden">
    <CharacterSelector/>
    <CharacterCreator/>
    <DeadScreen/>
  </div>
</template>
<script setup lang="ts">
import CharacterSelector from "./views/CharacterSelector.vue";
import CharacterCreator from "./views/CharacterCreator.vue";
import DeadScreen from "./views/DeadScreen.vue";
</script>
