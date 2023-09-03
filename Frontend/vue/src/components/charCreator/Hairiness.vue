<template>
  <div>
    <Title title="SCHRITT 4: BEHAARUNG">
      <template #icon>
        <font-awesome-icon icon="fa-solid fa-dna"/>
      </template>
    </Title>
    <div class="pb-2 border-b-2 border-b-[#bebebe]">
      <TitleBox title="Haare" subTitle="Wähle eine für deinen Charakter passende Frisur."/>
      <RangePicker :data="data.Hairiness" dataKey="Hair" :ArrayLength="76" Title="FRISUR" :split="true"/>
      <div class="mt-2 text-xl text-white text-bold text-center">Haarfarbe 1</div>
      <ColorPicker dataKey="HairColorMain" :object="data.Hairiness"/>
      <div class="mt-2 text-xl text-white text-bold text-center">Haarfarbe 2</div>
      <ColorPicker dataKey="HairColorSecond" :object="data.Hairiness"/>
    </div>
    <div class="pb-2 border-b-2 border-b-[#bebebe]">
      <TitleBox title="GESICHTSBEHAARUNG" subTitle="Wähle für deinen Charakter passende Gesichtsbehaarung."/>
      <RangePicker :data="data.Hairiness" dataKey="Beard" :ArrayLength="28" Title="BART"/>
      <RangePicker
          :data="data.Hairiness"
          dataKey="BeardOpacity"
          :ArrayLength="11"
          Title="BART SICHTBARKEIT"
          TitleLeft="nicht Sichtbar"
          TitleRight="Sichtbar"
          :IsFloat="true"
      />
      <div class="mt-2 text-xl text-white text-bold text-center">Bartfarbe 1</div>
      <ColorPicker dataKey="BeardColorMain" :object="data.Hairiness"/>
      <div class="mt-2 text-xl text-white text-bold text-center">Bartfarbe 2</div>
      <ColorPicker dataKey="BeardColorSecond" :object="data.Hairiness"/>
    </div>
    <TitleBox title="BRUSTBEHAARUNG" subTitle="Wähle für deinen Charakter passende Brustbehaarung."/>
    <RangePicker :data="data.Hairiness" dataKey="ChestHair" :ArrayLength="10" Title="BRUSTHAARE"/>
    <RangePicker
        :data="data.Hairiness"
        dataKey="ChestHairOpacity"
        :ArrayLength="11"
        Title="BRUSTHAARE SICHTBARKEIT"
        TitleLeft="nicht Sichtbar"
        TitleRight="Sichtbar"
        :IsFloat="true"
    />
    <div class="mt-2 text-xl text-white text-bold text-center">Brustfarbe 1</div>
    <ColorPicker dataKey="ChestHairColorMain" :object="data.Hairiness"/>
    <div class="mt-2 text-xl text-white text-bold text-center">Brustfarbe 2</div>
    <ColorPicker dataKey="ChestHairColorSecond" :object="data.Hairiness"/>
  </div>
</template>

<script setup>
import Title from '../shared/Head.vue';
import TitleBox from './TitleBox.vue';
import RangePicker from './RangePicker.vue';
import ColorPicker from './ColorPicker.vue';
import {toRefs, defineProps} from "vue";

const props = defineProps({
  data: {
    type: Object,
    required: true,
  },
});
const {data} = toRefs(props);
</script>
