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
        <v-alert v-if="legalAppTooltips" elevation="5" text type="info">
          Każda nowa notatka jest prywatna, co oznacza, że będzie widoczna tylko dla użytkownika, który ją stworzył.
          Jeśli chcesz, aby notatka była widoczna dla innych, oznacz ją jako publiczną.
        </v-alert>
        <v-card-text>
          <v-text-field v-model="form.title" label="Tytuł" required></v-text-field>
          <v-textarea outlined v-model="form.message" label="Treść notatki"></v-textarea>
          <v-checkbox v-model="form.public" label="Notatka publiczna" color="red darken-3"></v-checkbox>
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
    loader: false,
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
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips'])
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getClientsNotes']),
    async saveChanges() {
      this.loader = true
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
        setTimeout(() => {
          this.$emit('action-completed');
          this.dialog = false;
          this.loader = false
        }, 1500)
      }
    }
  }
};
</script>

<style scoped>

</style>
