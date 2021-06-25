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
  </v-dialog>
</template>

<script>
import {deleteNote} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {mapActions} from "vuex";

export default {
  name: "delete-note-dialog",
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
    ...mapActions('legal-app-client-store', ['getClientsNotes']),
    async deleteClientNote() {
      try {
        let clientId = this.$route.params.client;
        let noteId = this.noteForAction.id;
        await this.$axios.$delete(deleteNote(clientId, noteId));

      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        await this.getClientsNotes(this.$route.params.client);
        this.dialog = false;
        this.$emit('delete-completed');
      }
    }
  }
}
</script>

<style scoped>

</style>
