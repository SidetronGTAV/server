<template>
  <div class="pl-4 pr-4 mt-4">
    <div
        class="flex flex-wrap items-center justify-center border-l-[#C52223FF] border-l-4 mt-2 bg-[#202023FF] p-4 opacity-90 text-white">
      <div class="text-xl font-bold font-sans basis-full text-center" v-if="Title !== ''">{{ Title }}</div>
      <div class="flex items-center justify-center w-full">
        <button class="p-2 rounded-md bg-[#28282AFF] text-white mr-4 text-base font-bold hover:bg-[#242426FF]"
                @click="setValue(currentIndex - 1)">
          <font-awesome-icon icon="fa-solid fa-minus"/>
        </button>
        <div class="flex items-center flex-wrap justify-between w-full">
          <div class="text-sm font-bold font-sans text-center" v-if="TitleLeft !== ''">{{ TitleLeft }}</div>
          <div class="text-sm font-bold font-sans text-center" v-if="TitleRight !== ''">{{ TitleRight }}</div>
          <div class="basis-full flex justify-center items-center w-full" :class="split ? 'flex-wrap' : ''">
            <div
                v-for="(v, i) in Range"
                :key="i"
                class="grow h-2 mr-2 min-w-[0.25rem] mb-0.5"
                :class="i === currentIndex ? 'bg-[#C52223FF]' : 'bg-[#28282AFF]'"
                @click="setValue(i)"
            ></div>
          </div>
        </div>
        <button class="p-2 rounded-md bg-[#28282AFF] text-white ml-4 text-base font-bold hover:bg-[#242426FF]"
                @click="setValue(currentIndex + 1)">
          <font-awesome-icon icon="fa-solid fa-plus"/>
        </button>
      </div>
    </div>
  </div>
</template>

<script setup>

import {ref, toRefs, watch, defineProps} from "vue";

const props = defineProps({
  data: {
    type: Object,
    required: true,
  },
  dataKey: {
    type: String,
    required: true,
  },
  Title: {
    type: String,
    default: '',
  },
  TitleLeft: {
    type: String,
    default: '',
  },
  TitleRight: {
    type: String,
    default: '',
  },
  ArrayLength: {
    type: Number,
    required: true,
  },
  IsFloat: {
    type: Boolean,
    default: false,
  },
  split: {
    type: Boolean,
    default: false,
  },
  isNegative: {
    type: Boolean,
    default: false,
  },
});
const {data, dataKey, Title, TitleLeft, TitleRight, ArrayLength, IsFloat, split, isNegative} = toRefs(props);

const Range = ref(new Array(ArrayLength.value));
const currentIndex = ref(0);


function setValue(value) {
  value < ArrayLength.value && value >= 0 ? currentIndex.value = value : null;
}

function setIndex(newValue) {
  let value = 0;
  if (IsFloat.value) {
    value = Math.round(newValue) / 10;
    if (isNegative.value) {
      let nullValue = (ArrayLength.value - 1) / 2;
      value = value - Math.round(nullValue) / 10;
      value = parseFloat(value.toFixed(1));
    }
  } else {
    value = newValue;
  }
  data.value[dataKey.value] = value;
}

watch(currentIndex, (newValue) => {
  setIndex(newValue);
});
</script>
