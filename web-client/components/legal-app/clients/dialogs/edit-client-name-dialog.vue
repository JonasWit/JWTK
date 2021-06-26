<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="success">mdi-file-document-edit</v-icon>
          </v-btn>
        </template>
        <span>Edytuj nazwę klienta</span>
      </v-tooltip>
    </template>
    <v-form ref="editClientNameForm">
      <v-card>
        <v-card-text>
          <v-text-field v-model="client.name" :rules="validation.name" label="Edytuj nazwę Klienta"
                        required></v-text-field>
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
import {notEmptyAndLimitedRule} from "@/data/vuetify-validations";
import {updateClient} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

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
    validation: {
      valid: false,
      name: notEmptyAndLimitedRule("Nazwa jest wymagana. Dozwolona liczba znaków pomiędzy 4, a 50", 4, 50),
    },
    loading: false,
  }),
  fetch() {
    this.client = this.selectedClient;
  },
  methods: {
    ...mapMutations('legal-app-client-store', ['setClientForAction']),
    async saveClientNameChange() {
      if (!this.$refs.editClientNameForm.validate()) return;
      if (this.loading) return;
      this.loading = true;
      const payload = {
        name: this.client.name,
      };
      try {
        let clientId = this.selectedClient.id
        await this.$axios.$put(updateClient(clientId), payload)
        this.$notifier.showSuccessMessage("Nazwa klienta updatowana pomyślnie!");
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.setClientForAction(this.selectedClient);
        this.dialog = false;
      }
    },
  }
};
</script>

<style scoped>

</style>
