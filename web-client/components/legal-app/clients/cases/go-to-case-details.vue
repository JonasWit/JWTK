<template>
  <v-btn color="primary" dark link nuxt
         :to="`/legal-application/clients/${clientItem}/cases/${selectedCase.id}/c-details`">
    Przejdź do panelu sprawy
  </v-btn>
</template>

<script>
export default {
  name: "go-to-case-details",

  props: {
    clientItem: {
      required: true
    },
    selectedCase: {
      type: Object,
      required: true
    },
  },
}
</script>

<style scoped>

</style>
