<template>
  <div>
    <Title title="SCHRITT 1: DNA">
      <template #icon>
        <font-awesome-icon icon="fa-solid fa-dna"/>
      </template>
    </Title>
    <TextField title="DEIN NAME" :object="{ Name: '' }" dateKey="Name" subTitle="Mit diesem Namen wirst du einreisen.">
      <template #icon>
        <font-awesome-icon icon="fa-solid fa-user"/>
      </template>
    </TextField>
    <DateField
        title="DEIN GEBURTSTAG"
        :object="{ BirthDay: new Date() }"
        dateKey="BirthDay"
        subTitle="An diesem Tag ist dein Charakter geboren. Bitte präge ihn dir gut ein!"
        textType="date"
    >
      <template #icon>
        <font-awesome-icon icon="fa-solid fa-cake-candles"/>
      </template>
    </DateField>
    <TextField title="GESCHLECHT" subTitle="Wähle das Geschlecht deines Charakters aus!" textType="disabled">
      <template #icon>
        <font-awesome-icon icon="fa-solid fa-venus-mars"/>
      </template>
      <template #buttons>
        <button
            class="ml-4 p-2 rounded-md bg-[#28282AFF] text-white mr-4 text-base font-bold hover:bg-[#242426FF]"
            :class="manClass"
            @click="setSex(false)"
        >
          <font-awesome-icon icon="fa-solid fa-mars-stroke"/>
        </button>
        <button
            class="p-2 rounded-md bg-[#28282AFF] text-white mr-4 text-base font-bold hover:bg-[#242426FF]"
            :class="womanClass"
            @click="setSex(true)"
        >
          <font-awesome-icon icon="fa-solid fa-venus"/>
        </button>
      </template>
    </TextField>
    <div class="flex items-center justify-center border-t-2 border-t-[#bebebe] mt-4 mr-4 ml-4 pt-4">
      <button
          class="p-2 rounded-md bg-[#171717] text-white mr-4 text-2xl font-bold hover:bg-[#212121] shadow-2xl"
          :class="!isSkinColor ? 'border-2 border-[#C52223FF]' : 'border-none'"
          @click="isSkinColor = false"
      >
        Gesichtsgenetik
      </button>
      <button
          class="p-2 rounded-md bg-[#171717] text-white mr-4 text-2xl font-bold hover:bg-[#212121] shadow-2xl"
          :class="isSkinColor ? 'border-2 border-[#C52223FF]' : 'border-none'"
          @click="isSkinColor = true"
      >
        Hautfarbe
      </button>
    </div>
    <FaceSkin v-show="!isSkinColor" :object="data" dataKeyMum="ShapeFirstId" dataKeyDad="ShapeSecondId"
              dataKeyMix="ShapeMix"/>
    <FaceSkin v-show="isSkinColor" :object="data" dataKeyMum="SkinFirstId" dataKeyDad="SkinSecondId"
              dataKeyMix="SkinMix"/>
  </div>
</template>

<script setup>
import Title from '../shared/Head.vue';

import TextField from './TextField.vue';
import DateField from './DateField.vue';
import FaceSkin from './FaceSkin.vue';
import {ref, toRefs, watch, defineProps} from "vue";

const props = defineProps({
  data: {
    type: Object,
    required: true,
  },
});
const {data} = toRefs(props);

const manClass = ref('border-2 border-[#C52223FF]');
const womanClass = ref('border-none');
const isSkinColor = ref(false);

function setSex(bool) {
  bool ? (data.value.Sex = 1) : data.value.Sex = 0;
  manClass.value = !bool ? 'border-2 border-[#C52223FF]' : 'border-none';
  womanClass.value = bool ? 'border-2 border-[#C52223FF]' : 'border-none';
}

watch(() => data.value.Sex, (newValue) => {
  if ('alt' in window) {
    alt.emit('Client:CharCreator:setSex', newValue);
  }
});
</script>
