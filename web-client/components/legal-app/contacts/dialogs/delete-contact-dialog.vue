<template>
  <v-dialog v-model="dialog" :value="selectedContact" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn class="mx-2" medium color="error" v-on="{ ...tooltip, ...dialog }">
            Usuń kontakt
          </v-btn>
        </template>
        <span>Usuń Kontakt</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń Kontakt</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz wszystkie dane klienta. Odzyskanie dostępu będzie niemożliwe.
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

export default {
  name: "delete-contact-dialog",
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
    async deleteContact() {
      try {
        let clientId = this.$route.params.client
        let contactId = this.selectedContact.id
        await this.$axios.delete(deleteContact(clientId, contactId))
        this.$notifier.showSuccessMessage("Kontakt usunięty pomyślnie!");
      } catch (error) {
        console.error('creating contact error', error)
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.$nuxt.refresh()
        this.loading = false;
        this.dialog = false;
      }

    }
  }
}

</script>

<style>

</style>
