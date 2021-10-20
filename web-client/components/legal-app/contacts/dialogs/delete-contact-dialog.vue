<template>
  <v-dialog v-model="dialog" :value="selectedContact" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="error">mdi-delete</v-icon>
          </v-btn>
        </template>
        <span>Usuń Kontakt</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń Kontakt</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz wybrane dane kontaktowe. Odzyskanie dostępu będzie niemożliwe.
        Zatwierdź operację używjąc guzika 'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deleteContact">
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

import {deleteContact} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {mapActions} from "vuex";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "delete-contact-dialog",
  components: {ProgressBar},
  props: {
    selectedContact: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    dialog: false,
  }),
  methods: {
    ...mapActions('legal-app-client-store', ['getContactsList']),
    async deleteContact() {
      try {
        let clientId = this.$route.params.client;
        let contactId = this.selectedContact.id;
        await this.$axios.delete(deleteContact(clientId, contactId));
        this.$notifier.showSuccessMessage("Kontakt usunięty pomyślnie!");
      } catch (error) {
        handleError(error);
      } finally {
        this.$nuxt.refresh();
        this.dialog = false;
      }
    }
  }
};

</script>

<style>

</style>
