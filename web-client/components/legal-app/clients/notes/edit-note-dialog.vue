<template>
  <v-dialog v-model="dialog" max-width="500px">
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
        <v-alert v-if="legalAppTooltips" elevation="5" text type="info">
          Każda nowa notatka jest prywatna, co oznacza, że będzie widoczna tylko dla użytkownika, który ją stworzył.
          Jeśli chcesz, aby notatka była widoczna dla innych, oznacz ją jako publiczną.
        </v-alert>
        <v-card-text>
          <v-text-field v-model="form.title" label="Tytuł" required
                        :rules="[v => !!v, v => (v && v.length <= 50) || 'Pole obowiązkowe. Dozwolona liczba znaków to 50']"></v-text-field>
          <v-textarea outlined v-model="form.message" label="Treść notatki"
                      :rules="[v => !!v, v => (v && v.length <= 1000) || 'Pole obowiązkowe. Dozwolona liczba znaków to 1000']"></v-textarea>
          <v-checkbox v-model="form.public" label="Notatka publiczna" color="red darken-3"></v-checkbox>
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
import {updateNote} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {mapActions, mapState} from "vuex";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "edit-note-dialog",
  components: {ProgressBar},
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
    validation: {
      valid: false,
    },
  }),
  fetch() {
    this.form.title = this.noteForAction.title;
    this.form.message = this.noteForAction.message;
    this.form.public = this.noteForAction.public;
  },
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips'])
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getClientsNotes']),
    async saveChanges() {
      if (!this.$refs.editNoteForm.validate()) return;
      try {
        const note = {
          title: this.form.title,
          message: this.form.message,
          public: this.form.public
        };
        let clientId = this.$route.params.client;
        let noteId = this.noteForAction.id;
        await this.$axios.$put(updateNote(clientId, noteId), note);
        this.$notifier.showSuccessMessage("Zmiany zostały zapisane!");
      } catch (error) {
        handleError(error);
      } finally {
        this.$emit('action-completed');
        this.dialog = false;

      }
    }
  }
};
</script>

<style scoped>

</style>
