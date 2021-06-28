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
        <v-card-text>
          <v-text-field v-model="form.title" label="Tytuł" required></v-text-field>
          <v-textarea outlined v-model="form.message" label="Treść notatki"></v-textarea>
          <v-checkbox v-model="form.public" label="Notatka publiczna" color="red darken-3"></v-checkbox>
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
import {mapActions} from "vuex";

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
      public: false,
    },
  }),
  fetch() {
    this.form.title = this.noteForAction.title;
    this.form.message = this.noteForAction.message;
    this.form.public = this.noteForAction.public;
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getClientsNotes']),
    async saveChanges() {
      try {
        const note = {
          title: this.form.title,
          message: this.form.message,
          public: this.form.public
        };
        let clientId = this.$route.params.client;
        let noteId = this.noteForAction.id;
        console.warn('notka', note)
        await this.$axios.$put(updateNote(clientId, noteId), note);

      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        await this.getClientsNotes(this.$route.params.client);
        this.dialog = false;
        this.$emit('action-completed');
      }
    }
  }
};
</script>

<style scoped>

</style>
