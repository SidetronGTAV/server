<template>
  <div class="pb-2 border-b-2 border-b-[#bebebe]">
    <TitleBox title="Nase" subTitle="An der Nase eines Mannes..."/>
    <RangePicker
        :data="object.Nose"
        dataKey="NoseWidth"
        :ArrayLength="21"
        Title="NASENBREITE"
        TitleLeft="Schmal"
        TitleRight="Breit"
        :IsFloat="true"
        :isNegative="true"
    />
    <RangePicker
        :data="object.Nose"
        dataKey="NoseHeight"
        :ArrayLength="21"
        Title="NASENHÖHE"
        TitleLeft="Oben"
        TitleRight="Unten"
        :IsFloat="true"
        :isNegative="true"
    />
    <RangePicker
        :data="object.Nose"
        dataKey="NoseLength"
        :ArrayLength="21"
        Title="NASENLÄNGE"
        TitleLeft="Lang"
        TitleRight="Kurz"
        :IsFloat="true"
        :isNegative="true"
    />
    <RangePicker
        :data="object.Nose"
        dataKey="NoseBone"
        :ArrayLength="21"
        Title="NASENBEIN"
        TitleLeft="Rund"
        TitleRight="Hohl"
        :IsFloat="true"
        :isNegative="true"
    />
    <RangePicker
        :data="object.Nose"
        dataKey="NoseTip"
        :ArrayLength="21"
        Title="NASENSPITZE"
        TitleLeft="Oben"
        TitleRight="Unten"
        :IsFloat="true"
        :isNegative="true"
    />
    <RangePicker
        :data="object.Nose"
        dataKey="NoseCurve"
        :ArrayLength="21"
        Title="NASENKRÜMMUNG"
        TitleLeft="Links"
        TitleRight="Rechts"
        :IsFloat="true"
        :isNegative="true"
    />
  </div>
</template>

<script setup>
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
