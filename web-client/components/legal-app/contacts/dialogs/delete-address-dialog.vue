<template>
  <v-dialog v-model="dialog" :value="selectedContact" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn class="mx-2" icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="error">mdi-delete</v-icon>
          </v-btn>
        </template>
        <span>Usuń adres</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń adres</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz wybrany adres. Odzyskanie danych będzie niemożliwe.
        Zatwierdź operację używjąc guzika 'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deleteAddress">
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
import {mapActions} from "vuex";
import {
  deleteContactPhysicalAddress
} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "delete-address-dialog",
  props: {
    selectedContact: {
      required: true,
      type: Object,
      default: null
    },
    selectedAddress: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    dialog: false,
  }),

  methods: {
    ...mapActions('legal-app-client-store', ['getContactDetailsFromFetch']),

    async deleteAddress() {
      try {
        let clientId = this.$route.params.client
        let contactId = this.selectedContact.id
        let itemId = this.selectedAddress.id
        await this.$axios.delete(deleteContactPhysicalAddress(clientId, contactId, itemId))
        this.$notifier.showSuccessMessage("Adres usunięty pomyślnie!");
      } catch (error) {
        console.error('creating contact error', error)
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        let clientId = this.$route.params.client;
        let contactId = this.selectedContact.id;
        await this.getContactDetailsFromFetch({clientId, contactId})
        this.loading = false;
        this.dialog = false;
      }

    }

  }
}
</script>

<style scoped>

</style>
