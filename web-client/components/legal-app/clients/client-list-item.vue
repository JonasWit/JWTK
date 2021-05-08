<template>

  <!--  <nuxt-link :to="`/${}/details`">-->
  <v-list-item link nuxt :to="`/legal-application/clients/${clientItem.id}/details`"
               :key="`ci-record-${clientItem.id}`">
    <div> TEST: {{ clientItem.name }}</div>
  </v-list-item>
  <!--  </nuxt-link>-->

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
