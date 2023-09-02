<template>
  <div class="pb-2 border-b-2 border-b-[#bebebe]">
    <TitleBox title="Wangenknochen" subTitle="Passe deine Wangenknochen an."/>
    <RangePicker
        :data="object.Cheek"
        dataKey="CheekHeight"
        :ArrayLength="21"
        Title="HÖHE"
        TitleLeft="Hoch"
        TitleRight="Tief"
        :IsFloat="true"
        :isNegative="true"
    />
    <RangePicker
        :data="object.Cheek"
        dataKey="CheekBonesWidth"
        :ArrayLength="21"
        Title="KNOCHENBREITE"
        TitleLeft="Schmal"
        TitleRight="Breit"
        :IsFloat="true"
        :isNegative="true"
    />
    <RangePicker
        :data="object.Cheek"
        dataKey="CheekWidth"
        :ArrayLength="21"
        Title="WANGENBREITE"
        TitleLeft="Breit"
        TitleRight="Schmal"
        :IsFloat="true"
        :isNegative="true"
    />
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
})
const {object} = toRefs(props);

</script>
