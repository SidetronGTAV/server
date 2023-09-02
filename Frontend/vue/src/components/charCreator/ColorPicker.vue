<template>
  <div>
    <div class="flex items-center justify-center flex-wrap mt-2">
      <div
          v-for="(color, v) in colorPicker"
          class="w-8 h-8 border m-1"
          :class="v === colorPickIndex ? `border-[#C52223] ${color}` : `${color}`"
          @click="colorPickIndex = v"
      ></div>
    </div>
  </div>
</template>

<script setup lang="ts">
import {ref, toRefs, watch, defineProps} from "vue";

const props = defineProps({
  object: {
    type: Object,
    required: true,
  },
  dataKey: {
    type: String,
    required: true,
  },
})
const {object, dataKey} = toRefs(props)


const colorPickIndex = ref(0)
const colorPicker = ref([
  'bg-[#000000]',
  'bg-[#342d27]',
  'bg-[#60483B]',
  'bg-[#71442d]',
  'bg-[#72361c]',
  'bg-[#9a4825]',
  'bg-[#a05430]',
  'bg-[#9b5c37]',
  'bg-[#b77f54]',
  'bg-[#b78860]',
  'bg-[#ad8053]',
  'bg-[#cda56d]',
  'bg-[#e0c18a]',
  'bg-[#dcb069]',
  'bg-[#e0c18a]',
  'bg-[#eac37d]',
  'bg-[#f2d496]',
  'bg-[#bb8659]',
  'bg-[#6b3622]',
  'bg-[#9b3824]',
  'bg-[#7f1310]',
  'bg-[#a2271c]',
  'bg-[#a11f18]',
  'bg-[#c5341d]',
  'bg-[#d64421]',
  'bg-[#c85f34]',
  'bg-[#d04e24]',
  'bg-[#977e6a]',
  'bg-[#ad927e]',
  'bg-[#d7c0ab]',
  'bg-[#e4ccb7]',
  'bg-[#795363]',
  'bg-[#8b5770]',
  'bg-[#b04c6c]',
  'bg-[#fa70dd]',
  'bg-[#fd66a4]',
  'bg-[#f9a0af]',
  'bg-[#21ab9c]',
  'bg-[#088089]',
  'bg-[#085485]',
  'bg-[#609c52]',
  'bg-[#1d5b39]',
  'bg-[#103230]',
  'bg-[#103230]',
  'bg-[#8eab18]',
  'bg-[#579e21]',
  'bg-[#e0ae4b]',
  'bg-[#f7c90f]',
  'bg-[#f29b18]',
  'bg-[#fc7d0e]',
  'bg-[#fc7a24]',
  'bg-[#f66d1b]',
  'bg-[#ef5225]',
  'bg-[#df290f]',
  'bg-[#bc0f10]',
  'bg-[#751615]',
  'bg-[#1f1412]',
  'bg-[#3b241b]',
  'bg-[#4b291b]',
  'bg-[#47271b]',
  'bg-[#613b29]',
  'bg-[#2f1f19]',
  'bg-[#211b18]',
  'bg-[#dfb179]',
  'bg-[#c89a69]',
])

watch(colorPickIndex, (newValue) => {
  object.value[dataKey.value] = newValue;
})
</script>