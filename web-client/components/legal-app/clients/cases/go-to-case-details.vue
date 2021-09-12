<template>
  <v-tooltip bottom>
    <template v-slot:activator="{ on, attrs }">
      <v-btn color="primary" icon link nuxt
             :to="`/legal-application/clients/${clientItem}/cases/${selectedCase.id}/c-details`">
        <v-icon large color="primary">mdi-arrow-right-bold-box</v-icon>
      </v-btn>
    </template>
    <span>Przejdź do panelu sprawy</span>
  </v-tooltip>
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
