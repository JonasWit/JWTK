<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="success">mdi-file-document-edit</v-icon>
          </v-btn>
        </template>
        <span>Edytuj opis sprawy</span>
      </v-tooltip>
    </template>
    <v-form ref="editCaseForm" v-model="validation.valid">
      <v-card>
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Edytuj dane sprawy
          </v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <v-text-field v-model="form.name" label="Tytuł sprawy" required :rules="validation.name"></v-text-field>
          <v-combobox v-model="form.group" :items="getCasesGroups" required :rules="validation.group"
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
          <v-btn text color="primary" @click="saveChanges()">
            Zapisz zmianę
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>

</template>

<script>

import {mapActions, mapGetters, mapState} from "vuex";
import {lengthRule, notEmptyAndLimitedRule} from "@/data/vuetify-validations";
import {updateCase} from "@/data/endpoints/legal-app/legal-app-case-endpoints";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "edit-case-dialog",
  components: {ProgressBar},
  props: {
    caseForAction: {
      required: true,
      default: null
    }
  },
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
  async beforeMount() {
    try {
      this.form.name = this.caseForAction.name ? this.caseForAction.name : "";
      this.form.signature = this.caseForAction.signature ? this.caseForAction.signature : "";
      this.form.description = this.caseForAction.description ? this.caseForAction.description : "";
      this.form.group = this.caseForAction.group ? this.caseForAction.group : "";
    } catch (error) {
      handleError(error);
    }
  },
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
    ...mapGetters('legal-app-client-store', ['getCasesGroups'])
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getCaseDetails']),
    async saveChanges() {
      if (!this.$refs.editCaseForm.validate()) return;
      try {
        const newCase = {
          name: this.form.name,
          signature: this.form.signature,
          description: this.form.description,
          group: this.form.group,
        };
        let caseId = this.caseForAction.id;
        console.log(newCase);
        await this.$axios.$put(updateCase(caseId), newCase);
        this.$notifier.showSuccessMessage("Zmiany zostały zapisane!");
      } catch (error) {
        handleError(error);
      } finally {
        let caseId = this.caseForAction.id;
        await this.getCaseDetails({caseId});
        this.$emit('action-completed');
        this.dialog = false;
      }
    }
  }
};
</script>

<style scoped>

</style>
