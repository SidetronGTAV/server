<template>
  <div class="pb-2 border-b-2 border-b-[#bebebe]">
    <TitleBox title="Kinn & Kiefer" subTitle="Kinn & Kiefer Anpassungen"/>
    <RangePicker
        :data="object.MouthArea"
        dataKey="ChimpWidth"
        :ArrayLength="21"
        Title="BACKENBREITE"
        TitleLeft="Schmal"
        TitleRight="Breit"
        :IsFloat="true"
        :isNegative="true"
    />
    <RangePicker
        :data="object.MouthArea"
        dataKey="ChinHeight"
        :ArrayLength="21"
        Title="KIEFERHÖHE"
        TitleLeft="Oben"
        TitleRight="Unten"
        :IsFloat="true"
        :isNegative="true"
    />
    <RangePicker
        :data="object.MouthArea"
        dataKey="ChinLength"
        :ArrayLength="21"
        Title="KINNLÄNGE"
        TitleLeft="Klein"
        TitleRight="Groß"
        :IsFloat="true"
        :isNegative="true"
    />
    <RangePicker
        :data="object.MouthArea"
        dataKey="ChinForm"
        :ArrayLength="21"
        Title="KINNSTELLUNG"
        TitleLeft="Innen"
        TitleRight="Außen"
        :IsFloat="true"
        :isNegative="true"
    />
    <RangePicker
        :data="object.MouthArea"
        dataKey="ChinWidth"
        :ArrayLength="21"
        Title="KINNBREITE"
        TitleLeft="Schmal"
        TitleRight="Breit"
        :IsFloat="true"
        :isNegative="true"
    />
    <RangePicker
        :data="object.MouthArea"
        dataKey="ChinTwist"
        :ArrayLength="21"
        Title="KINN EINKERBUNG"
        TitleLeft="Keine Kerbe"
        TitleRight="Kerbe"
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
});
const {object} = toRefs(props);
</script>
