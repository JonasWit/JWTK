<template>
  <v-dialog v-model="dialog" :value="selectedClient" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="error">mdi-delete</v-icon>
          </v-btn>
        </template>
        <span>Usuń klienta</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń Klienta</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz wszystkie dane klienta. Odzyskanie dostępu będzie niemożliwe.
        Zatwierdź operację używjąc guzika 'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deleteClient">
          Potwierdź
        </v-btn>
        <v-spacer/>

        <v-btn color="success" text @click="dialog = false">
          Anuluj
        </v-btn>

      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {mapMutations} from "vuex";

export default {
  name: "delete-client-dialog",
  props: {
    selectedClient: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    dialog: false,
    loading: false,
    form: {
      userId: ""
    },

  }),
  methods: {
    ...mapMutations('legal-app-client-store', ['setClientForAction']),
    deleteClient() {
      this.$axios.$delete(`/api/legal-app-clients/delete/${this.selectedClient.id}`)
        .then((selectedClient) => {
          this.$notifier.showSuccessMessage("Klient usunięty pomyślnie!");
          console.warn(selectedClient, 'Client deleted successfully');

        })
        .catch((e) => {
          console.warn('delete client error', e);
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        }).finally(() => {
        this.setClientForAction(this.selectedClient);
        this.dialog = false;
      });


    }
  }
};
</script>

<style scoped>

</style>
