<template>
  <div>
    <v-row justify="center">
      <v-dialog v-model="dialog" fullscreen hide-overlay transition="dialog-bottom-transition">
        <template v-slot:activator="{ on, attrs }">
          <v-btn color="primary" dark v-bind="attrs" v-on="on">
            Open Dialog
          </v-btn>
        </template>

        <v-card v-if="noteDetails">

          <v-toolbar dark color="primary">
            <v-btn icon dark @click="dialog = false">
              <v-icon>mdi-close</v-icon>
            </v-btn>
            <v-toolbar-title>{{ noteDetails.title }}</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-items>
              <v-btn dark text @click="dialog = false">
                Save
              </v-btn>
            </v-toolbar-items>
          </v-toolbar>
          <v-list three-line subheader>
            <v-subheader>{{ formatDateWithHours(noteDetails.created) }}</v-subheader>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title>Treść notatki</v-list-item-title>
                <v-list-item-subtitle>{{ noteDetails.message }}
                </v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>

          </v-list>
          <v-divider></v-divider>
          <edit-note-dialog :note-for-action="noteDetails" v-on:action-completed="editDone"/>
        </v-card>
      </v-dialog>
    </v-row>

  </div>
</template>

<script>
import {getNote} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {formatDate, formatDateWithHours} from "@/data/date-extensions";
import EditNoteDialog from "@/components/legal-app/notes/edit-note-dialog";

export default {
  name: "notes-details",
  components: {EditNoteDialog},
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
  }),
  watch: {
    dialog(visible) {
      if (visible) {
        this.getNotesDetails();
      }
    }
  },
  methods: {
    getNote(clientId, noteId) {
      return getNote(clientId, noteId);
    },

    async getNotesDetails() {
      try {
        let clientId = this.$route.params.client;
        let noteId = this.selectedNote.id;
        this.noteDetails = await this.$axios.$get(this.getNote(clientId, noteId));
        this.noteForAction = this.selectedNote.id;
        console.warn('note details fetched', this.noteDetails);
      } catch (e) {
        console.log('error fetching note details', e);
      }

    },
    async editDone() {
      await this.getNotesDetails();
    },
    formatDate(date) {
      return formatDate(date);
    },
    formatDateWithHours(date) {
      return formatDateWithHours(date);
    }

  }
};
</script>

<style scoped>

</style>
