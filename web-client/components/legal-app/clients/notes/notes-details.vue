<template>
  <div>
    <v-row justify="center">
      <v-dialog v-model="dialog" fullscreen hide-overlay>
        <template #activator="{ on: dialog }" v-slot:activator="{ on }">
          <v-tooltip bottom>
            <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
              <v-btn icon v-on="{ ...tooltip, ...dialog }">
                <v-icon large color="primary">mdi-arrow-right-bold-box</v-icon>
              </v-btn>
            </template>
            <span>Przejdź do szczegółów</span>
          </v-tooltip>
        </template>
        <v-card v-if="noteDetails">
          <v-toolbar dark color="primary">
            <v-btn icon dark @click="dialog = false">
              <v-icon>mdi-close</v-icon>
            </v-btn>
            <v-toolbar-title>Tytuł: {{ noteDetails.title }}</v-toolbar-title>
            <v-spacer></v-spacer>
          </v-toolbar>
          <v-alert v-if="legalAppTooltips" elevation="5" text type="info">
            Jeśli chcesz zmodyfikować treść notatki, użyj ikonki "EDYTUJ". W nowym okienku będziesz mógł dokonać zmian.
            Jeśli już nie potrzebujesz tej notatki, użyj ikonki "USUŃ" i potwierdź operację.
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
            <v-card-subtitle>Status: {{ labelCondition(noteDetails.public) }}</v-card-subtitle>
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
        <options-floating-icon/>
        <progress-bar v-if="loader"/>
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
import RemindersFloatingIcon from "@/components/legal-app/reminders-floating-icon";
import OptionsFloatingIcon from "@/components/legal-app/options-floating-icon";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "notes-details",
  components: {ProgressBar, OptionsFloatingIcon, RemindersFloatingIcon, DeleteNoteDialog, EditNoteDialog},
  props: {
    selectedNote: {
      required: true,
      default: null
    }
  },
  data: () => ({
    dialog: false,
    loader: false,
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
      this.loader = true
      try {
        let clientId = this.$route.params.client;
        let noteId = this.selectedNote.id;
        this.noteDetails = await this.$axios.$get(getNote(clientId, noteId));
        this.noteForAction = this.selectedNote.id;
      } catch (error) {
        handleError(error);
      } finally {
        this.loader = false
      }
    },
    async editDone() {
      this.loader = true
      try {
        await this.getNotesDetails();
        await this.getClientsNotes(this.$route.params.client);
      } catch (error) {
        handleError(error);
      } finally {
        this.loader = false
      }

    },
    deleteDone() {
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
