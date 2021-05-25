<template>
  <v-card tile>
    <v-row>
      <v-col class="mx-2">
        <v-list class="d-flex justify-space-between">
          <v-list-item-content>
            <v-list-item-subtitle> Nazwa: {{ clientItem.title }}</v-list-item-subtitle>
            <v-list-item-subtitle> Imię i nazwisko: {{ clientItem.name }} {{
                clientItem.surname
              }}
            </v-list-item-subtitle>
          </v-list-item-content>
        </v-list>
      </v-col>
      <v-col class="mx-2">
        <v-list class="d-flex justify-space-between">
          <v-list-item-content>
            <v-list-item-subtitle>Komentarz: {{ clientItem.comment }}</v-list-item-subtitle>
            <v-list-item-subtitle>Dodano: {{ clientItem.created }}</v-list-item-subtitle>
          </v-list-item-content>
        </v-list>
      </v-col>
      <v-col class="mx-2">
        <v-list class="d-flex justify-space-between">
          <delete-client-dialog :selected-client="clientItem"/>
          <archive-client-dialog :selected-client="clientItem"/>
          <edit-client-name-dialog :selected-client="clientItem"/>
          <go-to-client-panel :client-item="clientItem"/>
        </v-list>
      </v-col>
    </v-row>
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
