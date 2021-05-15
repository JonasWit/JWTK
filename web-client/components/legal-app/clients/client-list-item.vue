<template>
  <v-card tile>
    <v-list-item>
      <v-list-item-title> Nazwa Klienta: {{ clientItem.name }}</v-list-item-title>
      <v-list-item-subtitle>Dodano: {{ clientItem.created }}</v-list-item-subtitle>
      <v-list-item-icon class="d-flex justify-end">
        <delete-client-dialog :selected-client="clientItem"/>
      </v-list-item-icon>
      <v-list-item-icon class="d-flex justify-end">
        <archive-client-dialog :selected-client="clientItem"/>
      </v-list-item-icon>
      <v-list-item-icon class="d-flex justify-end">
        <go-to-client-panel :client-item="clientItem"/>
      </v-list-item-icon>
    </v-list-item>
  </v-card>
</template>

<script>

import DeleteClientDialog from "@/components/legal-app/clients/dialogs/delete-client-dialog";
import ArchiveClientDialog from "@/components/legal-app/clients/dialogs/archive-client-dialog";
import GoToClientPanel from "@/components/legal-app/clients/go-to-client-panel";


export default {
  name: "client-list-item",
  components: {GoToClientPanel, ArchiveClientDialog, DeleteClientDialog},
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
