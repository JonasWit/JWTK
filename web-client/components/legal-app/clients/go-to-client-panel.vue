<template>
  <v-tooltip bottom>
    <template v-slot:activator="{ on, attrs }">
      <v-btn icon link nuxt :to="`/legal-application/clients/${clientItem.id}/c-details`"
             :key="`ci-record-${clientItem.id}`" dark v-bind="attrs" v-on="on">
        <v-icon medium color="success">mdi-arrow-right-bold-box</v-icon>
      </v-btn>
    </template>
    <span>Przejdź do panelu klienta</span>
  </v-tooltip>
</template>

<script>
export default {
  name: "go-to-client-panel",
  props: {
    clientItem: {
      type: Object,
      required: true
    }
  },

}
</script>

<style scoped>

</style>
