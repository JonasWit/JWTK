<template>
  <div>
    <template>
      <v-progress-linear
        :active="loading"
        :color="color"
        :indeterminate="true"
        class="ma-0"
        height="4"
        style="top: -2px;"
      >Loading...
      </v-progress-linear>
    </template>
  </div>


</template>

<script>
export default {
  name: "progress-bar",
  props: {
    loading: Boolean,
    color: String,
  },
  data: () => ({}
  ),
}
</script>

<style scoped>

</style>
