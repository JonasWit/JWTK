<template>
  <v-dialog v-model="dialog" :value="noteForAction" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="error">mdi-delete</v-icon>
          </v-btn>
        </template>
        <span>Usuń notatkę</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń Notatkę</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz notatkę. Odzyskanie dostępu będzie niemożliwe.
        Zatwierdź operację używjąc guzika 'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deleteClientNote">
          Potwierdź
        </v-btn>
        <v-spacer/>
        <v-btn color="success" text @click="dialog = false">
          Anuluj
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>
import {deleteNote} from "@/data/endpoints/legal-app/legal-app-case-endpoints";
import {mapActions} from "vuex";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "case-delete-note-dialog",
  components: {ProgressBar},
  props: {
    noteForAction: {
      required: true,
      default: null
    }
  },
  data: () => ({
    dialog: false,
  }),
  methods: {
    ...mapActions('legal-app-client-store', ['getNotesListForCases']),
    async deleteClientNote() {
      try {
        let caseId = this.$route.params.case
        let noteId = this.noteForAction.id;
        await this.$axios.$delete(deleteNote(caseId, noteId));
      } catch (error) {
        handleError(error);
      } finally {
        let caseId = this.$route.params.case
        await this.getNotesListForCases({caseId});
        this.$nuxt.refresh()
        this.dialog = false;
      }
    }
  }
}
</script>

<style scoped>

</style>
