<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn class="mx-2 option-btn" x-large v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="success">mdi-file-document-edit</v-icon>
          </v-btn>
        </template>
        <span>Edytuj treść</span>
      </v-tooltip>
    </template>
    <v-form ref="editNoteForm">
      <v-card>
        <v-card-text>
          <v-text-field v-model="form.title" label="Tytuł" required></v-text-field>
          <v-textarea outlined v-model="form.message" label="Treść notatki"></v-textarea>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn text color="primary" @click="saveChanges()">
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
import {updateNote} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "edit-note-dialog",
  props: {
    noteForAction: {
      required: true,
      default: null
    }
  },
  data: () => ({
    dialog: false,
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
    updateNote(clientId, noteId) {
      return updateNote(clientId, noteId);
    },
    async saveChanges() {
      try {
        const note = {
          title: this.form.title,
          message: this.form.message,
        };

        let clientId = this.$route.params.client;
        let noteId = this.noteForAction.id;

        await this.$axios.$put(updateNote(clientId, noteId), note);

      } catch (e) {
        console.error(e);
      } finally {
        this.dialog = false;
        this.$emit('action-completed');
      }
    }
  }
};
</script>

<style scoped>

</style>
