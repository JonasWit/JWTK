<template>
  <v-dialog v-model="dialog" :value="selectedClient" persistent width="500">
    <template v-slot:activator="{ on, attrs }">
      <v-btn class="mx-2" icon v-bind="attrs" v-on="on">
        <v-icon medium color="warning">mdi-delete</v-icon>
      </v-btn>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń Klienta</v-card-title>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deleteClient">
          Delete
        </v-btn>
        <v-spacer/>

        <v-btn color="success" text @click="dialog = false">
          Cancel
        </v-btn>

      </v-card-actions>
    </v-card>
    <snackbar/>
  </v-dialog>
</template>

<script>
import Snackbar from "@/components/snackbar";

export default {
  name: "delete-client-dialog",
  components: {Snackbar},
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
    deleteClient(selectedClient) {
      this.$axios.$delete(`/api/legal-app-clients/delete/${this.selectedClient.id}`)
        .then((selectedClient) => {
          this.$notifier.showSuccessMessage("Klient usunięty pomyślnie!");
          console.warn(selectedClient, 'Dane usunietego klienta')

        })
        .catch((e) => {
          console.warn('delete client error', e);
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        }).finally(() => {
        // this.$bus.$emit('action-confirmed');
        this.dialog = false;
      });


    }
  }
}
</script>

<style scoped>

</style>
