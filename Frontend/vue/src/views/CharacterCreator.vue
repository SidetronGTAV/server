<template>
  <div v-if="showCharCreator">
    <div class="absolute bottom-10 flex justify-center items-center left-2/4">
      <button
          class="p-4 border-none rounded-md bg-[#28282AFF] opacity-60 text-white mr-2 text-lg font-bold hover:opacity-100"
          @click="count--"
      >
        Zurück
      </button>
      <button
          class="p-4 border-none rounded-md bg-[#28282AFF] opacity-60 text-white mr-2 text-lg font-bold hover:opacity-100"
          @click="count++"
      >
        Weiter
      </button>
    </div>
    <div class="absolute bottom-10 right-6 flex justify-center items-center">
      <button
          @click="setCharacter"
          class="p-4 border-none rounded-md text-white bg-[#C72222FF] text-2xl font-bold hover:scale-110"
      >
        Charakter erstellen
      </button>
    </div>
    <div
        class="border rounded-lg border-black absolute left-6 lg:w-1/3 w-1/2 h-5/6 top-20 mg:top-11 lg:top-20 xl:top-24 2xl:top-32 bg-[#28282AFF] shadow-2xl max-h-max overflow-y-scroll overflow-x-hidden"
        style="transform: perspective(150vh) rotateY(30deg)"
    >
      <DNA v-show="count === 0" :data="data"/>
      <div v-show="count === 1">
        <Gesichtsform :data="data"/>
      </div>
      <div v-show="count === 2">
        <Gesichtsdetail :data="data"/>
      </div>
      <div v-show="count === 3">
        <Hairiness :data="data"/>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import DNA from "../components/charCreator/DNA.vue";
import Gesichtsform from "../components/charCreator/Gesichtsform.vue";
import Gesichtsdetail from "../components/charCreator/Gesichtsdetail.vue";
import Hairiness from "../components/charCreator/Hairiness.vue";
import {onMounted, ref, watch} from "vue";

const data = ref({
  Sex: 0,
  SkinFace: {
    ShapeFirstId: 0,
    ShapeSecondId: 0,
    ShapeThirdId: 0,
    SkinFirstId: 0,
    SkinSecondId: 0,
    SkinThirdId: 0,
    ShapeMix: 0.0,
    SkinMix: 0.0,
    ThirdMix: 0.0,
  },
  Eye: {
    EyeColor: 0,
    EyeShape: 0.0,
    EyeBrown: 0,
    EyeBrownDense: 0.0,
    EyeBrownHeight: 0.0,
    EyeBrownColorMain: 0,
    EyeBrownColorSecond: 0,
    EyeBrownOffset: 0.0,
  },
  Nose: {
    NoseWidth: 0.0,
    NoseHeight: 0.0,
    NoseLength: 0.0,
    NoseBone: 0.0,
    NoseTip: 0.0,
    NoseCurve: 0.0,
  },
  Cheek: {
    CheekHeight: 0.0,
    CheekBonesWidth: 0.0,
    CheekWidth: 0.0,
  },
  Lip: {
    LipStick: 0,
    LipStickOpacity: 0.0,
    LipWidth: 0.0,
    LipColorMain: 0,
    LipColorSecond: 0,
  },
  MouthArea: {
    ChimpWidth: 0.0,
    ChinHeight: 0.0,
    ChinLength: 0.0,
    ChinForm: 0.0,
    ChinWidth: 0.0,
    ChinTwist: 0.0,
  },
  Neck: {
    NeckWidth: 0.0,
  },
  Details: {
    Blemish: 0,
    BlemishOpacity: 0.0,
    BodyBlemish: 0,
    BodyBlemishOpacity: 0.0,
    Aging: 0,
    AgingOpacity: 0.0,
    Redness: 0,
    RednessOpacity: 0.0,
    Complexion: 0,
    ComplexionOpacity: 0.0,
    SunDamage: 0,
    SunDamageOpacity: 0.0,
    Moles: 0,
    MolesOpacity: 0.0,
    Makeup: 0,
    MakeupOpacity: 0.0,
  },
  Hairiness: {
    Hair: 0,
    HairColorMain: 0,
    HairColorSecond: 0,
    Beard: 0,
    BeardOpacity: 0.0,
    BeardColorMain: 0,
    BeardColorSecond: 0,
    ChestHair: 0,
    ChestHairOpacity: 0.0,
    ChestHairColorMain: 0,
    ChestHairColorSecond: 0,
  },
})
const count = ref(0);
const showCharCreator = ref(false);

function setCharacter() {
  if ("alt" in window) {
    alt.emit("Client:CharCreator:close");
  }
}


watch(count, (newValue) => {
  if (newValue > 3) count.value = 0;
  else if (newValue < 0) count.value = 3;
  else count.value = newValue;
})

watch(data, (newValue) => {
  if ("alt" in window) {
    alt.emit("Client:CharCreator:setProperty", count.value, newValue);
  }
}, {deep: true});

onMounted(() => {
  console.log("CharCreator.vue mounted");
  if ("alt" in window) {
    alt.on("Webview:CharCreator:handle", (state: boolean) => {
      showCharCreator.value = state;
    });
  }
})


</script>