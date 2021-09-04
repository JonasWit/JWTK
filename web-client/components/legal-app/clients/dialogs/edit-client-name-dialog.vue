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
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Edytuj nazwę klienta
          </v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <v-text-field v-model="client.name" :rules="validation.name" label="Edytuj nazwę Klienta"
                        required></v-text-field>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn color="error" text @click="dialog = false">
            Anuluj
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="saveClientNameChange()">
            Zapisz zmianę
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
    <progress-bar v-if="loader"/>
  </v-dialog>
</template>

<script>
import {mapMutations} from "vuex";
import {notEmptyAndLimitedRule} from "@/data/vuetify-validations";
import {updateClient} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "edit-client-name-dialog",
  components: {ProgressBar},
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
    loader: false,
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
      if (this.loader) return;
      this.loader = true;
      const payload = {
        name: this.client.name,
      };
      try {
        let clientId = this.selectedClient.id
        await this.$axios.$put(updateClient(clientId), payload)
        this.$notifier.showSuccessMessage("Nazwa klienta updatowana pomyślnie!");
      } catch (error) {
        handleError(error);
      } finally {
        setTimeout(() => {
          this.setClientForAction(this.selectedClient);
          this.dialog = false;
          this.loader = false
        }, 1500)
      }
    },
  }
};
</script>

<style scoped>

</style>
