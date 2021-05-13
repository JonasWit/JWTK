<template>
  <v-card tile>
    <v-list-item link nuxt :to="`/legal-application/clients/${clientItem.id}/c-details`"
                 :key="`ci-record-${clientItem.id}`">
      <v-list-item-title> Nazwa Klienta: {{ clientItem.name }}</v-list-item-title>
      <v-list-item-subtitle>Dodano: {{ clientItem.created }}</v-list-item-subtitle>
      <v-list-item-icon class="d-flex justify-end">
        <delete-client-dialog :selected-client="clientItem"/>
      </v-list-item-icon>
    </v-list-item>
  </v-card>
</template>

<script>

import DeleteClientDialog from "@/components/legal-app/clients/dialogs/delete-client-dialog";

export default {
  name: "client-list-item",
  components: {DeleteClientDialog},
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
