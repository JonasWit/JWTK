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
    <progress-bar v-if="loader"/>
  </v-dialog>
</template>

<script>
import {deleteContactPhoneNumber} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "delete-phone-number-dialog",
  components: {ProgressBar},
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
    loader: false
  }),
  methods: {
    async deletePhoneNumber() {
      this.loader = true
      try {
        let clientId = this.$route.params.client
        let contactId = this.selectedContact.id
        let itemId = this.selectedPhoneNumber.id
        await this.$axios.delete(deleteContactPhoneNumber(clientId, contactId, itemId))
        this.$notifier.showSuccessMessage("Numer usunięty pomyślnie!");
      } catch (error) {
        handleError(error);
      } finally {
        setTimeout(() => {
          this.$emit('action-completed');
          this.dialog = false;
          this.loader = false;
        }, 1500)
      }
    }

  }

}
</script>

<style scoped>

</style>
