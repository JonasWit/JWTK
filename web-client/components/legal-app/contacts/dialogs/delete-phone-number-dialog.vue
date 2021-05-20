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
      <v-card-title class="justify-center">Usuń numer telefonu email</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz wybrany adres email. Odzyskanie danych będzie niemożliwe.
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
import {mapActions} from "vuex";

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
    ...mapActions('legal-app-client-store', ['getContactDetailsFromFetch']),

    deletePhoneNumber() {
      this.$axios.$delete(`/api/legal-app-client-contacts/client/${this.$route.params.client}/contact/${this.selectedContact.id}/phone-number/${this.selectedPhoneNumber.id}`)
        .then(() => {
          this.$notifier.showSuccessMessage("Wybrany numer telefonu został usunięty pomyślnie!");
        }).catch((e) => {
        console.warn('delete email error', e);
        this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
      }).finally(() => {
        let clientId = this.$route.params.client;
        let contactId = this.selectedContact.id;
        this.getContactDetailsFromFetch({clientId, contactId})
        this.dialog = false;
      })
    }

  }

}
</script>

<style scoped>

</style>
