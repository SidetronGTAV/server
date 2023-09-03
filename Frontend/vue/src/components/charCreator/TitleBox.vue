<template>
  <div class="pl-4 pr-4 mt-4">
    <div class="flex items-center border-l-[#C52223FF] border-l-4 mt-2 bg-[#202023FF] p-4 opacity-90 text-white z-50">
      <div style="color: #ffffff" class="pr-4">
        <slot name="icon"/>
      </div>
      <div class="w-full">
        <div class="flex items-center flex-wrap justify-center">
          <div class="text-xl font-bold font-sans text-center">{{ title }}</div>
          <div class="text-sm font-medium font-sans text-gray-200 basis-full text-center">{{ subTitle }}</div>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup>
import {toRefs, defineProps} from "vue";

const props = defineProps({
  title: String,
  subTitle: String,
});
const {title, subTitle} = toRefs(props);
</script>
