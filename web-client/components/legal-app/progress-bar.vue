<template>
  <v-progress-circular
    size="70"
    width="7"
    color="light-blue darken-4"
    indeterminate
  ></v-progress-circular>
</template>
<script>

export default {
  name: "progress-bar",
  props: {
    loading: Boolean,
    color: String,
  },

}
</script>

<style scoped>

</style>
