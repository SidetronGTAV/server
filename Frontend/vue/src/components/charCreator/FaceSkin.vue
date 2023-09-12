<template>
  <div>
    <RangePicker :data="object.SkinFace" :dataKey="dataKeyMix" :ArrayLength="10" TitleLeft="Mutter" TitleRight="Vater"
                 :IsFloat="true"></RangePicker>
    <div class="p-4 m-4 border-2 border-[#bebebe]">
      <div class="flex items-center justify-center mt-2">
        <button
            class="p-2 rounded-md bg-[#171717] text-white mr-4 text-2xl font-bold hover:bg-[#212121] shadow-2xl"
            :class="mumClass"
            @click="setParent(false)"
        >
          Mutter
        </button>
        <button
            class="p-2 rounded-md bg-[#171717] text-white mr-4 text-2xl font-bold hover:bg-[#212121] shadow-2xl"
            :class="dadClass"
            @click="setParent(true)"
        >
          Vater
        </button>
      </div>
      <div v-show="!parent" class="grid grid-cols-4 gap-5 place-content-center p-4">
        <div
            v-for="(v, i) in mumArray"
            :key="i"
            class="bg-[#202023FF] p-1 rounded-md shadow-2xl"
            :class="i === object.SkinFace[dataKeyMum] ? 'border-2 border-[#C52223FF]' : 'border-none'"
            @click="object.SkinFace[dataKeyMum] = i"
        >
          <img :src="`./img/charCreator/female/${v}.png`" :alt="v"/>
          <div class="text-white text-xl font-bold font-sans text-center">{{ v }}</div>
        </div>
      </div>
      <div v-show="parent" class="grid grid-cols-4 gap-5 place-content-center p-4">
        <div
            v-for="(v, i) in dadArray"
            :key="i"
            class="bg-[#202023FF] p-1 rounded-md shadow-2xl"
            :class="i === object.SkinFace[dataKeyDad] ? 'border-2 border-[#C52223FF]' : 'border-none'"
            @click="object.SkinFace[dataKeyDad] = i"
        >
          <img :src="`./img/charCreator/male/${v}.png`" :alt="v"/>
          <div class="text-white text-xl font-bold font-sans text-center">{{ v }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import RangePicker from './RangePicker.vue';
import {toRefs, defineProps, ref} from "vue";

const props = defineProps({
  object: Object,
  dataKeyMum: String,
  dataKeyDad: String,
  dataKeyMix: String,
});
const {object, dataKeyMum, dataKeyDad, dataKeyMix} = toRefs(props);

const dadClass = ref('border-none');
const mumClass = ref('border-2 border-[#C52223FF]');
const parent = ref(false);
const dadArray = ref([
  'Benjamin',
  'Daniel',
  'Joshua',
  'Noah',
  'Andrew',
  'Juan',
  'Alex',
  'Isaac',
  'Evan',
  'Ethan',
  'Vincent',
  'Angel',
  'Diego',
  'Adrian',
  'Gabriel',
  'Michael',
  'Santiago',
  'Kevin',
  'Louis',
  'Samuel',
  'Anthony',
  'Claude',
  'Niko',
  'John',
]);
const mumArray = ref([
  'Hannah',
  'Audrey',
  'Jasmine',
  'Giselle',
  'Amelia',
  'Isabella',
  'Zoe',
  'Ava',
  'Camilla',
  'Violet',
  'Sophia',
  'Evelyn',
  'Nicole',
  'Ashley',
  'Grace',
  'Brianna',
  'Natalie',
  'Olivia',
  'Elizabeth',
  'Charlotte',
  'Emma',
  'Misty',
]);

function setParent(bool) {
  parent.value = bool;
  mumClass.value = !bool ? 'border-2 border-[#C52223FF]' : 'border-none';
  dadClass.value = bool ? 'border-2 border-[#C52223FF]' : 'border-none';
}
</script>
