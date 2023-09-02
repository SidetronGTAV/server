<template>
  <div class="pl-4 pr-4 mt-4">
    <div class="flex items-center border-l-[#C52223FF] border-l-4 mt-2 bg-[#202023FF] p-4 opacity-90 text-white">
      <div style="color: #ffffff" class="pr-4">
        <slot name="icon"/>
      </div>
      <div class="flex items-center flex-wrap justify-center">
        <div class="text-xl font-bold font-sans" :class="textType !== 'disabled' ? 'pr-4' : ''">{{ title }}</div>
        <input class="bg-[#28282a] border-b-white border-b input" v-model="object[dateKey]" :type="textType"
               v-if="textType !== 'disabled'"/>
        <div class="text-sm font-medium font-sans text-gray-200 basis-full text-center">{{ subTitle }}</div>
        <div class="mt-1">
          <slot v-if="textType === 'disabled'" name="buttons"/>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import {toRefs, defineProps} from "vue";

const props = defineProps({
  textType: {
    type: String,
    default: 'text',
  },
  object: Object,
  title: String,
  subTitle: String,
  dateKey: String,
});
const {object, textType, title, subTitle, dateKey} = toRefs(props);
</script>
