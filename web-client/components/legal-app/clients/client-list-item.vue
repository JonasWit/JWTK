<template>
  <v-list-item link nuxt :to="`/legal-application/clients/${clientItem.id}/c-details`"
               :key="`ci-record-${clientItem.id}`">
    <div> TEST: {{ clientItem.name }}</div>
  </v-list-item>

</template>

<script>
export default {
  name: "client-list-item",
  props: {
    clientItem: {
      type: Object,
      required: true
    }
  },
};
</script>

<style scoped>

</style>
