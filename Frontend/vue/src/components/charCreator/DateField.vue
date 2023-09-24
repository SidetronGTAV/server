<template>
     <div class="pl-4 pr-4 mt-4">
          <div class="flex items-center border-l-[#C52223FF] border-l-4 mt-2 bg-[#202023FF] p-4 opacity-90 text-white">
               <div style="color: #ffffff" class="pr-4">
                    <slot name="icon" />
               </div>
               <div class="flex items-center flex-wrap justify-center">
                    <div class="pr-4 text-lg font-bold font-sans">{{ title }}</div>
                    <VueDatePicker
                         class="z-50"
                         v-model="object[dateKey]"
                         :teleport="true"
                         locale="de"
                         :dark="true"
                         :format-locale="de"
                         format="dd.MM.yyyy"
                         :enable-time-picker="false"
                         cancelText="Abbrechen"
                         selectText="Auswählen" />
                    <div class="text-sm font-medium font-sans text-gray-200 basis-full text-center">
                         {{ subTitle }}
                    </div>
               </div>
          </div>
     </div>
</template>

<script setup>
import { defineProps, toRefs } from 'vue';
import VueDatePicker from '@vuepic/vue-datepicker';
import { de } from 'date-fns/locale';
import '@vuepic/vue-datepicker/dist/main.css';

const props = defineProps({
     title: String,
     subTitle: String,
     dateKey: String,
     object: Object,
});
const { title, subTitle, dateKey, object } = toRefs(props);
</script>