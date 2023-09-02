<template>
  <div>
    <Title title="SCHRITT 3: GESICHTSDETAILS">
      <template #icon>
        <font-awesome-icon icon="fa-solid fa-dna"/>
      </template>
    </Title>
    <div class="pb-2 border-b-2 border-b-[#bebebe]">
      <TitleBox title="Details" subTitle="Passe die Gesichtsdetails deines Charakters an!"/>
      <RangePicker :data="data.Details" dataKey="Blemish" :ArrayLength="25" Title="SCHÖNHEITSFEHLER" TitleLeft="Schmal"
                   TitleRight="Breit"/>
      <RangePicker
          :data="data.Details"
          dataKey="BlemishOpacity"
          :ArrayLength="11"
          Title="SCHÖNHEITSFEHLER SICHTBARKEIT"
          TitleLeft="nicht Sichtbar"
          TitleRight="Sichtbar"
          :IsFloat="true"
      />
      <RangePicker :data="data.Details" dataKey="BodyBlemish" :ArrayLength="25" Title="SCHÖNHEITSFEHLER KÖRPER"
                   TitleLeft="Schmal" TitleRight="Breit"/>
      <RangePicker
          :data="data.Details"
          dataKey="BodyBlemishOpacity"
          :ArrayLength="11"
          Title="SCHÖNHEITSFEHLER KÖRPER SICHTBARKEIT"
          TitleLeft="nicht Sichtbar"
          TitleRight="Sichtbar"
          :IsFloat="true"
      />
      <RangePicker :data="data.Details" dataKey="Aging" :ArrayLength="16" Title="ALTERUNG" TitleLeft="Schmal"
                   TitleRight="Breit"/>
      <RangePicker
          :data="data.Details"
          dataKey="AgingOpacity"
          :ArrayLength="11"
          Title="ALTERUNG SICHTBARKEIT"
          TitleLeft="nicht Sichtbar"
          TitleRight="Sichtbar"
          :IsFloat="true"
      />
      <RangePicker :data="data.Details" dataKey="Redness" :ArrayLength="23" Title="RÖTUNGEN" TitleLeft="Schmal"
                   TitleRight="Breit"/>
      <RangePicker
          :data="data.Details"
          dataKey="RednessOpacity"
          :ArrayLength="11"
          Title="RÖTUNGEN SICHTBARKEIT"
          TitleLeft="nicht Sichtbar"
          TitleRight="Sichtbar"
          :IsFloat="true"
      />
      <RangePicker :data="data.Details" dataKey="Complexion" :ArrayLength="23" Title="TEINT" TitleLeft="Schmal"
                   TitleRight="Breit"/>
      <RangePicker
          :data="data.Details"
          dataKey="ComplexionOpacity"
          :ArrayLength="11"
          Title="TEINT SICHTBARKEIT"
          TitleLeft="nicht Sichtbar"
          TitleRight="Sichtbar"
          :IsFloat="true"
      />
      <RangePicker :data="data.Details" dataKey="SunDamage" :ArrayLength="23" Title="SONNENSCHÄDEN" TitleLeft="Schmal"
                   TitleRight="Breit"/>
      <RangePicker
          :data="data.Details"
          dataKey="SunDamageOpacity"
          :ArrayLength="11"
          Title="SONNENSCHÄDEN SICHTBARKEIT"
          TitleLeft="nicht Sichtbar"
          TitleRight="Sichtbar"
          :IsFloat="true"
      />
      <RangePicker
          :data="data.Details"
          dataKey="Moles"
          :ArrayLength="23"
          Title="MUTTERMALE/SOMMERSPROSSEN"
          TitleLeft="Schmal"
          TitleRight="Breit"
          :IsFloat="false"
      />
      <RangePicker
          :data="data.Details"
          dataKey="MolesOpacity"
          :ArrayLength="11"
          Title="MUTTERMALE/SOMMERSPROSSEN SICHTBARKEIT"
          TitleLeft="nicht Sichtbar"
          TitleRight="Sichtbar"
          :IsFloat="true"
      />
    </div>
    <div class="pb-2 border-b-2 border-b-[#bebebe]">
      <TitleBox title="MakeUp" subTitle="Schminke deinen Charakter!"/>
      <RangePicker
          :data="data.Details"
          dataKey="Makeup"
          :ArrayLength="74"
          Title="MAKEUP"
          TitleLeft="Schmal"
          TitleRight="Breit"
          :IsFloat="false"
          :split="true"
      />
      <RangePicker
          :data="data.Details"
          dataKey="MakeupOpacity"
          :ArrayLength="11"
          Title="MAKEUP SICHTBARKEIT"
          TitleLeft="nicht Sichtbar"
          TitleRight="Sichtbar"
          :IsFloat="true"
      />
    </div>
  </div>
</template>

<script setup lang="ts">
import Title from '../shared/Head.vue';
import TitleBox from './TitleBox.vue';
import RangePicker from './RangePicker.vue';
import {toRefs, defineProps} from "vue";

const props = defineProps({
  data: {
    type: Object,
    required: true,
  },
});
const {data} = toRefs(props);
</script>
