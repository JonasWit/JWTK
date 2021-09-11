<template>
  <v-dialog v-model="dialog1" :value="selectedContact" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn class="mx-2" icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="error">mdi-delete</v-icon>
          </v-btn>
        </template>
        <span>Usuń adres email</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń adres email</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz wybrany adres email. Odzyskanie danych będzie niemożliwe.
        Zatwierdź operację używjąc guzika 'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deleteEmail">
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

import {deleteContactEmail} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "delete-email-dialog",
  components: {ProgressBar},
  props: {
    selectedContact: {
      required: true,
      type: Object,
      default: null
    },
    selectedEmail: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    dialog1: false,
  }),

  methods: {
    async deleteEmail() {
      try {
        let clientId = this.$route.params.client
        let contactId = this.selectedContact.id
        let itemId = this.selectedEmail.id
        await this.$axios.delete(deleteContactEmail(clientId, contactId, itemId))
        this.$notifier.showSuccessMessage("Adres email usunięty pomyślnie!");
      } catch (error) {
        handleError(error);
      } finally {
        this.$emit('action-completed');
        this.dialog1 = false;
      }
    }
  }
}
</script>

<style scoped>

</style>
