<template>
  <div class="pb-2 border-b-2 border-b-[#bebebe]">
    <TitleBox title="Lippen" subTitle="Lippen Anpassungen"/>
    <RangePicker :data="object.Lip" dataKey="LipStick" :ArrayLength="11" Title="LIPPENSTIFT"/>
    <RangePicker
        :data="object.Lip"
        dataKey="LipStickOpacity"
        :ArrayLength="11"
        Title="LIPPENSTIFT SICHTBARKEIT"
        TitleLeft="Nicht sichtbar"
        TitleRight="Sichtbar"
        :IsFloat="true"
    />
    <RangePicker
        :data="object.Lip"
        dataKey="LipWidth"
        :ArrayLength="21"
        Title="LIPPENBREITE"
        TitleLeft="Breit"
        TitleRight="Schmal"
        :IsFloat="true"
        :isNegative="true"
    />
    <div class="mt-2 text-xl text-white text-bold text-center">Lippenstift Farbe 1</div>
    <ColorPicker dataKey="LipColorMain" :object="object.Lip"/>
    <div class="mt-2 text-xl text-white text-bold text-center">Lippenstift Farbe 2</div>
    <ColorPicker dataKey="LipColorSecond" :object="object.Lip"/>
  </div>
</template>

<script setup lang="ts">
import TitleBox from './TitleBox.vue';
import ColorPicker from './ColorPicker.vue';
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
