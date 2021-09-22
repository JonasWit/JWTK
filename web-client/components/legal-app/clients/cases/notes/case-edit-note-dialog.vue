<template>
  <v-dialog v-model="dialog1" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="success">mdi-file-document-edit</v-icon>
          </v-btn>
        </template>
        <span>Edytuj treść notatki</span>
      </v-tooltip>
    </template>
    <v-form ref="editNoteForm" v-model="validation.valid">
      <v-card>
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Edytuj notatkę
          </v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <v-text-field v-model="form.title" label="Tytuł" required
                        :rules="[v => !!v, v => (v && v.length <= 50) || 'Pole obowiązkowe. Dozwolona liczba znaków to 50']"></v-text-field>
          <v-textarea outlined v-model="form.message" label="Treść notatki"
                      :rules="[v => !!v, v => (v && v.length <= 1000) || 'Pole obowiązkowe. Dozwolona liczba znaków to 1000']"></v-textarea>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn color="error" text @click="dialog1 = false">
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
import {updateNote} from "@/data/endpoints/legal-app/legal-app-case-endpoints";
import {mapActions} from "vuex";
import {handleError} from "@/data/functions";

export default {
  name: "case-edit-note-dialog",
  props: {
    noteForAction: {
      required: true,
      default: null
    }
  },
  data: () => ({
    dialog1: false,
    form: {
      title: "",
      message: "",
    },
    validation: {
      valid: false,
    },
  }),
  fetch() {
    this.form.title = this.noteForAction.title;
    this.form.message = this.noteForAction.message;
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getClientsNotes']),
    async saveChanges() {
      if (!this.$refs.editNoteForm.validate()) return;
      try {
        const note = {
          title: this.form.title,
          message: this.form.message,
        };
        let caseId = this.$route.params.case
        let noteId = this.noteForAction.id;
        await this.$axios.$put(updateNote(caseId, noteId), note);
        this.$notifier.showSuccessMessage("Zmiany zostały zapisane!");
      } catch (error) {
        handleError(error);
      } finally {
        this.$emit('action-completed');
        this.$refs.editNoteForm.resetValidation();
        this.dialog1 = false;
      }
    },
    resetForm() {
      this.$refs.editNoteForm.reset();
      this.$refs.editNoteForm.resetValidation();
      this.dialog = false;
    },
  }
};
</script>

<style scoped>

</style>
