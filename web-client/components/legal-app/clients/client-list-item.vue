<template>
  <v-card tile>
    <v-list-item>
      <v-list-item-title> Nazwa: {{ clientItem.title }}</v-list-item-title>
      <v-list-item-title> Imię i nazwisko: {{ clientItem.name }} {{ clientItem.surname }}</v-list-item-title>
      <v-list-item-subtitle>Komentarz: {{ clientItem.comment }}</v-list-item-subtitle>
      <v-list-item-subtitle>Dodano: {{ clientItem.created }}</v-list-item-subtitle>
      <v-list-item-icon class="mx-2">
        <delete-client-dialog :selected-client="clientItem"/>
      </v-list-item-icon>
      <v-list-item-icon class="mx-2">
        <archive-client-dialog :selected-client="clientItem"/>
      </v-list-item-icon>
      <v-list-item-icon class="mx-2">
        <edit-client-name-dialog :selected-client="clientItem"/>
      </v-list-item-icon>
      <v-list-item-icon class="ml-3">
        <go-to-client-panel :client-item="clientItem"/>
      </v-list-item-icon>

    </v-list-item>
  </v-card>
</template>

<script>

import DeleteClientDialog from "@/components/legal-app/clients/dialogs/delete-client-dialog";
import ArchiveClientDialog from "@/components/legal-app/clients/dialogs/archive-client-dialog";
import GoToClientPanel from "@/components/legal-app/clients/go-to-client-panel";
import EditClientNameDialog from "@/components/legal-app/clients/dialogs/edit-client-name-dialog";


export default {
  name: "client-list-item",
  components: {EditClientNameDialog, GoToClientPanel, ArchiveClientDialog, DeleteClientDialog},
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
