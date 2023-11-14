<template>
     <div class="flex overflow-hidden">
          <CharacterSelector />
          <CharacterCreator />
          <DeadScreen />
          <Hud />
     </div>
</template>
<script setup lang="ts">
import CharacterSelector from './views/CharacterSelector.vue';
import CharacterCreator from './views/CharacterCreator.vue';
import DeadScreen from './views/DeadScreen.vue';
import Hud from './views/Hud.vue';
</script>