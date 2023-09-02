<template>
<button class="mx-px text-xxs h-auto w-20 py-1 bg-hg1 border-2 border-c1 text-t1" style="transform: skew(-16deg);">{{aktion}}</button>
</template>

<script>
export default {
  name: 'xsTButton',
  props: {
    aktion: String
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}



</style>
