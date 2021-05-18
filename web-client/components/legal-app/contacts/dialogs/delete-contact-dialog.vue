<template>
  <v-dialog v-model="dialog" :value="selectedContact" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn class="mx-2" icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="error">mdi-delete</v-icon>
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

import {mapMutations} from "vuex";

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
    ...mapMutations('legal-app-client-store', ['setContactForAction']),
    deleteContact() {
      this.$axios.$delete(`/api/legal-app-client-contacts/client/${this.$route.params.client}/contact/${this.selectedContact.id}`)

        .then((selectedContact) => {
          this.$notifier.showSuccessMessage("Wybrany kontakt został usunięty pomyślnie!");
          console.warn(selectedContact, 'Selected Contact deleted successfully')
          Object.assign(this.$data, this.$options.data.call(this)); // total data reset (all returning to default data)
          this.$nuxt.refresh()

        }).catch((e) => {
        console.warn('delete contact error', e);
        this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");

      }).finally(() => {
        this.setContactForAction(this.selectedContact)
        this.dialog = false;
      })


    }
  }
}

</script>

<style scoped>

</style>
