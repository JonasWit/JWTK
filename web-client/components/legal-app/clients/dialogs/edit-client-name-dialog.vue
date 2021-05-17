<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn class="mx-3" icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="success">mdi-file-document-edit</v-icon>
          </v-btn>
        </template>
        <span>Edytuj nazwę klienta</span>
      </v-tooltip>
    </template>
    <v-form ref="editClientNameForm">
      <v-card>
        <v-card-text>
          <v-text-field v-model="client.name" label="Edytuj nazwę Klienta"
                        required>
          </v-text-field>
          <small class="grey--text">* Hint text here</small>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn text color="primary" @click="saveClientNameChange()">
            Zapisz zmianę
          </v-btn>
          <v-btn color="success" text @click="dialog = false">
            Anuluj
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>

<script>
import {mapMutations} from "vuex";

export default {
  name: "edit-client-name-dialog",
  props: {
    selectedClient: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    client: null,
    dialog: false,
    form: {
      name: "",

    },

  }),
  fetch() {
    this.client = this.selectedClient;
  },
  methods: {
    ...mapMutations('legal-app-client-store', ['setClientForAction']),
    saveClientNameChange() {

      const payload = {
        name: this.client.name,
      }

      this.$axios.$put(`/api/legal-app-clients/update/${this.selectedClient.id}`, payload)
        .then((selectedClient) => {
          this.$notifier.showSuccessMessage("Nazwa klienta updatowana pomyślnie!");
          console.warn(selectedClient, 'Client name updated successfully')

        })
        .catch((e) => {
          console.warn('client name change error', e);
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        }).finally(() => {
        this.setClientForAction(this.selectedClient)
        this.dialog = false;
      });

    },

  }
}
</script>

<style scoped>

</style>
