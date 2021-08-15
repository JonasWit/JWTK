<template>
  <div>
    <v-row justify="center">
      <v-dialog v-model="dialog" fullscreen hide-overlay transition="dialog-bottom-transition">
        <template v-slot:activator="{ on, attrs }">
          <v-btn color="primary" dark v-bind="attrs" v-on="on">
            Otwórz
          </v-btn>
        </template>
        <v-card v-if="noteDetails">
          <v-toolbar dark color="primary">
            <v-btn icon dark @click="dialog = false">
              <v-icon>mdi-close</v-icon>
            </v-btn>
            <v-toolbar-title>{{ noteDetails.title }}</v-toolbar-title>
            <v-spacer></v-spacer>
          </v-toolbar>
          <v-alert v-if="legalAppTooltips" elevation="5" text type="info">
            Jeśli chcesz zmodyfikować treść notatki, użyj guzika "EDYTUJ". W nowym okienku będziesz mógł dokonać zmian.
            Jeśli już nie potrzebujesz tej notatki, użyj guzika "USUŃ" i potwierdź operację.
          </v-alert>
          <v-row class="mx-2 my-2 d-flex align-center">
            <v-subheader class="mx-2"><span class="font-weight-bold">Utworzono: </span>
              {{ formatDateWithHours(noteDetails.created) }}
            </v-subheader>
            <v-subheader class="mx-2">
              <span class="font-weight-bold">Utworzone przez: </span> {{ noteDetails.createdBy }}
            </v-subheader>
          </v-row>
          <v-divider></v-divider>
          <v-row class="mx-2 mb-4 my-4 d-flex align-center">
            <v-card-title>Treść notatki</v-card-title>
            <edit-note-dialog :note-for-action="noteDetails" v-on:action-completed="editDone"/>
            <delete-note-dialog :note-for-action="noteDetails" v-on:delete-completed="deleteDone"/>
            <v-checkbox v-model="checkbox" :label="labelCondition(noteDetails.public)" :value="noteDetails.public" value
                        disabled></v-checkbox>
          </v-row>
          <v-card-text>
            {{ noteDetails.message }}
          </v-card-text>
          <v-divider></v-divider>
          <v-row class="mx-2 mb-4 my-4 d-flex align-center">
            <v-card-subtitle><span class="font-weight-bold">Edytowano:</span>
              {{ formatDateWithHours(noteDetails.updated) }}
            </v-card-subtitle>
            <v-card-subtitle><span class="font-weight-bold">Edytowane przez:</span> {{ noteDetails.updatedBy }}
            </v-card-subtitle>
          </v-row>
        </v-card>
      </v-dialog>
    </v-row>

  </div>
</template>

<script>

import {formatDate, formatDateWithHours} from "@/data/date-extensions";
import EditNoteDialog from "@/components/legal-app/clients/notes/edit-note-dialog";
import DeleteNoteDialog from "@/components/legal-app/clients/notes/delete-note-dialog";
import {getNote} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {mapActions, mapState} from "vuex";

export default {
  name: "notes-details",
  components: {DeleteNoteDialog, EditNoteDialog},
  props: {
    selectedNote: {
      required: true,
      default: null
    }
  },
  data: () => ({
    dialog: false,
    noteDetails: null,
    value: 'Treść notatki...',
    noteForAction: null,
    checkbox: true
  }),
  watch: {
    dialog(visible) {
      if (visible) {
        this.getNotesDetails();
      }
    }
  },
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips'])
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getClientsNotes']),
    async getNotesDetails() {
      try {
        let clientId = this.$route.params.client;
        let noteId = this.selectedNote.id;
        this.noteDetails = await this.$axios.$get(getNote(clientId, noteId));
        this.noteForAction = this.selectedNote.id;
      } catch (error) {
        this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj ponownie");
      }
    },
    async editDone() {
      await this.getNotesDetails();
      return this.getClientsNotes(this.$route.params.client);
    },
    async deleteDone() {
      this.dialog = false;
    },
    formatDate(date) {
      return formatDate(date);
    },
    formatDateWithHours(date) {
      return formatDateWithHours(date);
    },
    labelCondition(val) {
      if (val) {
        return "Notatka publiczna";
      }
      return "Notatka prywatna";
    }
  }
};
</script>

<style scoped>

</style>
