<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn fab class="mx-2" v-on="{ ...tooltip, ...dialog }">
            <v-icon color="success" x-large>mdi-plus</v-icon>
          </v-btn>
        </template>
        <span>Dodaj notkę</span>
      </v-tooltip>
    </template>
    <v-form ref="addNoteForm" v-model="validation.valid">
      <v-card>
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Dodaj notatkę
          </v-toolbar-title>
        </v-toolbar>
        <v-alert elevation="5" text type="info" dismissible close-text="Zamknij">
          Placeholder na alert
        </v-alert>
        <v-card-text>
          <v-text-field v-model="form.title" label="Tytuł" required :rules="validation.title"></v-text-field>
          <v-textarea outlined v-model="form.message" label="Treść notatki" required
                      :rules="validation.message"></v-textarea>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn color="error" text @click="dialog = false">
            Anuluj
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="save">
            Zapisz
          </v-btn>

        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>

<script>
import {notEmptyAndLimitedRule, notEmptyRule} from "@/data/vuetify-validations";
import {mapActions} from "vuex";
import {createNote} from "@/data/endpoints/legal-app/legal-app-case-endpoints";

export default {
  name: "case-add-note",
  data: () => ({
    loading: false,
    dialog: false,
    form: {
      title: "",
      message: "",
    },
    validation: {
      valid: false,
      title: notEmptyRule("Tytuł notatki nie może być pusty"),
      message: notEmptyAndLimitedRule("Notatka nie może byc pusta i może zawierać maksymalnie 1000 znaków.", 4, 1000),
    },
  }),

  methods: {
    ...mapActions('legal-app-client-store', ['getNotesListForCases']),
    async save() {
      if (!this.$refs.addNoteForm.validate()) return;
      if (this.loading) return;
      this.loading = true;
      try {
        const note = {
          title: this.form.title,
          message: this.form.message,
        };
        let caseId = this.$route.params.case
        await this.$axios.$post(createNote(caseId), note);
        this.$notifier.showSuccessMessage("Notatka dodana pomyślnie!");
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        let caseId = this.$route.params.case
        await this.getNotesListForCases({caseId});
        this.dialog = false;
        this.loading = false;
      }
    },
    resetForm() {
      this.$refs.addNoteForm.reset();
      this.$refs.addNoteForm.resetValidation();
    },
  }
}
</script>

<style scoped>

</style>
