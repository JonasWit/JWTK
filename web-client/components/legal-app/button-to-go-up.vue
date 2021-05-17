<template>

  <v-btn fab large dark absolute bottom right color="primary" style="position:fixed;" class="ma-9" @click="scrollUp">
    <v-icon>mdi-chevron-up-box</v-icon>
  </v-btn>

</template>

<script>
export default {
  name: "button-to-go-up",
  methods: {
    scrollUp() {
      window.scrollTo({
        top: 0,
        left: 0,
        behaviour: 'smooth'
      })
    }
  }
}
</script>

<style scoped>

</style>
