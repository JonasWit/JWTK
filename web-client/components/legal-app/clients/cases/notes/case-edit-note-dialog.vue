<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn elevation="2" small class="mx-2" color="secondary" v-on="{ ...tooltip, ...dialog }">
            Edytuj
          </v-btn>
        </template>
        <span>Edytuj treść</span>
      </v-tooltip>
    </template>
    <v-form ref="editNoteForm">
      <v-card>
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Edytuj notatkę
          </v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <v-text-field v-model="form.title" label="Tytuł" required></v-text-field>
          <v-textarea outlined v-model="form.message" label="Treść notatki"></v-textarea>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn color="success" text @click="dialog = false">
            Anuluj
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="saveChanges()">
            Zapisz zmianę
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
    <progress-bar v-if="loader"/>
  </v-dialog>

</template>

<script>
import {updateNote} from "@/data/endpoints/legal-app/legal-app-case-endpoints";
import {mapActions} from "vuex";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "case-edit-note-dialog",
  components: {ProgressBar},
  props: {
    noteForAction: {
      required: true,
      default: null
    }
  },
  data: () => ({
    dialog: false,
    loader: false,
    form: {
      title: "",
      message: "",
    },
  }),
  fetch() {
    this.form.title = this.noteForAction.title;
    this.form.message = this.noteForAction.message;
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getClientsNotes']),
    async saveChanges() {
      this.loader = true
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
        this.dialog = false;
        this.$emit('action-completed');
        this.loader = false
      }
    }
  }
};
</script>

<style scoped>

</style>
