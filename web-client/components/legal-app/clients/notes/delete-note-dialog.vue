<template>
  <v-dialog v-model="dialog" :value="noteForAction" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn elevation="2" small class="mx-2" color="error" v-on="{ ...tooltip, ...dialog }">
            Usuń
          </v-btn>
        </template>
        <span>Usuń klienta</span>
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
    <progress-bar v-if="loader"/>
  </v-dialog>
</template>

<script>
import {deleteNote} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {mapActions} from "vuex";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "delete-note-dialog",
  components: {ProgressBar},
  props: {
    noteForAction: {
      required: true,
      default: null
    }
  },

  data: () => ({
    dialog: false,
    loader: false
  }),

  methods: {
    ...mapActions('legal-app-client-store', ['getClientsNotes']),
    async deleteClientNote() {
      this.loader = true
      try {
        let clientId = this.$route.params.client;
        let noteId = this.noteForAction.id;
        await this.$axios.$delete(deleteNote(clientId, noteId));

      } catch (error) {
        handleError(error);
      } finally {
        setTimeout(() => {
          let clientId = this.$route.params.client;
          this.getClientsNotes(clientId);
          this.dialog = false;
          this.$emit('delete-completed');
          this.loader = false
        }, 1500)
      }
    }
  }
}
</script>

<style scoped>

</style>
