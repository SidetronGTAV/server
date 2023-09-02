<template>
  <div>
    <TitleBox title="Nacken" subTitle="Auf dein Nacken!"/>
    <RangePicker :data="object.Neck" dataKey="NeckWidth" :ArrayLength="21" Title="NACKENBREITE" TitleLeft="Schmal"
                 TitleRight="Breit" :IsFloat="true"/>
  </div>
</template>

<script setup lang="ts">
import TitleBox from './TitleBox.vue';
import RangePicker from './RangePicker.vue';
import {toRefs, defineProps} from "vue";

const props = defineProps({
  object: {
    type: Object,
    required: true,
  },
});
const {object} = toRefs(props);
</script>
