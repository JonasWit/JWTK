<template>
  <div>
    <template>
      <div class="text-center">
        <v-overlay color="grey lighten-5">
          <v-progress-circular
            size="70"
            width="7"
            color="light-blue darken-4"
            indeterminate
          ></v-progress-circular>
        </v-overlay>
      </div>
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

}
</script>

<style scoped>

</style>
