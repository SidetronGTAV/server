<template>
  <div class="pb-2 border-b-2 border-b-[#bebebe]">
    <TitleBox title="Augen" subTitle="Zur Abwechslung mal wirklich Auge machen!"></TitleBox>
    <RangePicker :data="object.Eye" dataKey="EyeColor" :ArrayLength="30" Title="AUGENFARBE"/>
    <RangePicker
        :data="object.Eye"
        dataKey="EyeShape"
        :isNegative="true"
        :ArrayLength="21"
        Title="AUGENÖFFNUNG"
        TitleLeft="Offen"
        TitleRight="Geschlossen"
        :IsFloat="true"
    />
    <RangePicker :data="object.Eye" dataKey="EyeBrown" :ArrayLength="35" Title="AUGENBRAUEN"/>
    <RangePicker
        :data="object.Eye"
        dataKey="EyeBrownDense"
        :ArrayLength="11"
        Title="AUGENBRAUEN DICHTE"
        TitleLeft="Undicht"
        TitleRight="Dicht"
        :IsFloat="true"
    />
    <RangePicker
        :data="object.Eye"
        dataKey="EyeBrownHeight"
        :ArrayLength="21"
        Title="AUGENBRAUEN HÖHE"
        TitleLeft="Oben"
        TitleRight="Unten"
        :IsFloat="true"
        :isNegative="true"
    />
    <RangePicker
        :data="object.Eye"
        dataKey="EyeBrownOffset"
        :ArrayLength="21"
        Title="AUGENBRAUEN Versatz"
        TitleLeft="Innen"
        TitleRight="Außen"
        :IsFloat="true"
        :isNegative="true"
    />
    <div class="text-center text-white">Augenbrauen 1</div>
    <ColorPicker dataKey="EyeBrownColorMain" :object="object.Eye"/>
    <div class="text-center text-white">Augenbrauen 2</div>
    <ColorPicker dataKey="EyeBrownColorSecond" :object="object.Eye"/>
  </div>
</template>

<script setup lang="ts">
import RangePicker from './RangePicker.vue';
import TitleBox from './TitleBox.vue';
import ColorPicker from './ColorPicker.vue';
import {toRefs, defineProps} from "vue";

const props = defineProps({
  object: {
    type: Object,
    required: true,
  },
});
const {object} = toRefs(props);
</script>
