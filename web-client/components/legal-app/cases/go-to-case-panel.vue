<template>
  <v-tooltip bottom>
    <template v-slot:activator="{ on, attrs }">
      <v-btn icon link nuxt :to="`/legal-application/clients/${clientItem}/cases/${selectedCase.id}/c-details`"
             :key="`ci-record-${selectedCase.id}`" dark v-bind="attrs" v-on="on">
        <v-icon medium color="success">mdi-arrow-right-bold-box</v-icon>
      </v-btn>
    </template>
    <span>Przejdź do panelu sprawy</span>
  </v-tooltip>
</template>

<script>
export default {
  name: "go-to-case-panel",
  props: {
    selectedCase: {
      type: Object,
      required: true
    },
    clientItem: {
      required: true
    }
  },
}
</script>

<style scoped>

</style>
