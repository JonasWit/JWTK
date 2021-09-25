<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn fab class="mx-2" light v-on="{ ...tooltip, ...dialog }">
            <v-icon color="success" x-large>mdi-plus</v-icon>
          </v-btn>
        </template>
        <span>Dodaj Sprawę</span>
      </v-tooltip>
    </template>
    <v-form ref="addCaseForm" v-model="validation.valid">
      <v-card>
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Dodaj Sprawę
          </v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <v-text-field v-model="form.name" label="Tytuł sprawy" required :rules="validation.name"></v-text-field>
          <v-combobox v-model="form.group" :items="getCasesGroups"
                      label="Wybierz Kategorię sprawy lub dodaj nową"></v-combobox>
          <v-text-field v-model="form.signature" label="Sygnatura sprawy" required
                        :rules="validation.signature"></v-text-field>
          <v-textarea outlined v-model="form.description" label="Opis sprawy" required
                      :rules="validation.description"></v-textarea>
          <small class="grey--text">* Dane opcjonalne</small>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn color="error" text @click="dialog = false">
            Anuluj
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="addNewCase">
            Zapisz
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>

<script>
import {lengthRule, notEmptyAndLimitedRule} from "@/data/vuetify-validations";
import {mapActions, mapGetters, mapState} from "vuex";
import {createCase} from "@/data/endpoints/legal-app/legal-app-case-endpoints";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "add-case",
  components: {ProgressBar},
  data: () => ({
    dialog: false,
    form: {
      name: "",
      signature: "",
      description: "",
      group: ""
    },
    validation: {
      valid: false,
      name: notEmptyAndLimitedRule("Tytuł sprawy nie może być pusty i liczba znaków nie może przekraczać 50", 1, 50),
      group: notEmptyAndLimitedRule("Nazwa kategorii nie może byc pusta i może zawierać maksymalnie 200 znaków.", 1, 200),
      signature: lengthRule("Liczba znaków nie może przekraczać 200", 0, 200),
      description: lengthRule("Liczba znaków nie może przekraczać 1000", 0, 1000)
    },
  }),
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
    ...mapGetters('legal-app-client-store', ['getCasesGroups'])
  },

  methods: {
    ...mapActions('legal-app-client-store', ['getClientsNotes']),
    async addNewCase() {
      if (!this.$refs.addCaseForm.validate()) return;
      try {
        const newCase = {
          name: this.form.name,
          signature: this.form.signature,
          description: this.form.description,
          group: this.form.group
        };
        let clientId = this.$route.params.client;
        await this.$axios.$post(createCase(clientId), newCase);
        this.$nuxt.refresh();
        this.$notifier.showSuccessMessage("Sprawa dodana pomyślnie!");
      } catch (error) {
        handleError(error);
      } finally {
        this.resetForm();
      }
    },
    resetForm() {
      this.$refs.addCaseForm.reset();
      this.$refs.addCaseForm.resetValidation();
      this.dialog = false;
    },
  }
};
</script>

<style scoped>

</style>
