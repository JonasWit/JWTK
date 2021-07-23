<template>
  <v-dialog v-model="dialog" :value="selectedContact" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn class="mx-2" icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="error">mdi-delete</v-icon>
          </v-btn>
        </template>
        <span>Usuń numer telefonu</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń numer telefonu</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz wybrany numer telefonu. Odzyskanie danych będzie niemożliwe.
        Zatwierdź operację używjąc guzika 'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deletePhoneNumber">
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
import {deleteContactPhoneNumber} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "delete-phone-number-dialog",

  props: {
    selectedContact: {
      required: true,
      type: Object,
      default: null
    },
    selectedPhoneNumber: {
      required: true,
      type: Object,
      default: null
    }
  },

  data: () => ({
    dialog: false,
  }),
  methods: {
    async deletePhoneNumber() {
      try {
        let clientId = this.$route.params.client
        let contactId = this.selectedContact.id
        let itemId = this.selectedPhoneNumber.id
        await this.$axios.delete(deleteContactPhoneNumber(clientId, contactId, itemId))
        this.$notifier.showSuccessMessage("Numer usunięty pomyślnie!");
      } catch (error) {
        console.error('creating contact error', error)
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.$emit('action-completed');
        this.loading = false;
        this.dialog = false;
      }
    }

  }

}
</script>

<style scoped>

</style>
