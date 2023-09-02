<template>
    <div class="flex items-center bg-[#242426FF] p-4 opacity-90 text-3xl font-bold rounded-t-lg !overflow-y-hidden">
        <div style="color: white" class="pr-2"><slot name="icon" /></div>
        <div class="text-white">{{ props.title }}</div>
    </div>
</template>

<script setup lang="ts">
const props = defineProps(['title']);
</script>
