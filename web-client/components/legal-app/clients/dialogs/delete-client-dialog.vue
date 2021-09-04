<template>
  <v-dialog v-model="dialog" :value="selectedClient" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="error">mdi-delete</v-icon>
          </v-btn>
        </template>
        <span>Usuń klienta</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń Klienta</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz wszystkie dane klienta i wszystkie powiązane dane, takie jak
        stworzone
        notatki, przypomnienia i rozliczenia. Odzyskanie dostępu będzie niemożliwe.
        Zatwierdź operację używjąc guzika 'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deleteClient">
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
import {mapMutations} from "vuex";
import {deleteClient} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "delete-client-dialog",
  components: {ProgressBar},
  props: {
    selectedClient: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    dialog: false,
    loader: false,
    form: {
      userId: ""
    },

  }),
  methods: {
    ...mapMutations('legal-app-client-store', ['setClientForAction']),
    async deleteClient() {
      this.loader = true
      try {
        let clientId = this.selectedClient.id
        await this.$axios.$delete(deleteClient(clientId))
        this.$notifier.showSuccessMessage("Klient usunięty pomyślnie!");
      } catch (error) {
        handleError(error);
      } finally {
        setTimeout(() => {
          this.setClientForAction(this.selectedClient);
          this.dialog = false;
          this.loader = false
        }, 1500)
      }
    }
  }
};
</script>

<style scoped>

</style>
