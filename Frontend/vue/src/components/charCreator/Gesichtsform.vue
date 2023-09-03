<template>
  <div>
    <Title title="SCHRITT 2: GESICHTSFORM">
      <template #icon>
        <font-awesome-icon icon="fa-solid fa-dna"/>
      </template>
    </Title>
  </div>
  <Eyes :object="data"/>
  <Nose :object="data"/>
  <Cheek :object="data"/>
  <Lips :object="data"/>
  <MouthArea :object="data"/>
  <Neck :object="data"/>
</template>

<script setup>
import Title from '../shared/Head.vue';
import Eyes from './Eyes.vue';
import Nose from './Nose.vue';
import Cheek from './Cheek.vue';
import Lips from './Lips.vue';
import MouthArea from './MouthArea.vue';
import Neck from './Neck.vue';
import {toRefs, defineProps} from "vue";

const props = defineProps({
  data: {
    type: Object,
    required: true,
  },
});
const {data} = toRefs(props);
</script>
