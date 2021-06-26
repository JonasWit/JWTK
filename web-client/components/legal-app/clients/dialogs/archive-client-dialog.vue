<template>
  <v-dialog v-model="dialog" :value="selectedClient" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom :disabled="tooltipVisible">
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="warning">mdi-archive-arrow-down</v-icon>
          </v-btn>
        </template>
        <span>Archiwizuj klienta</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Archiwizuj Klienta</v-card-title>
      <v-card-subtitle>Potwierdzając operację dodasz do archiwum klienta i wszystkie powiązane dane, takie jak stworzone
        notatki, przypomnienia i rozliczenia. Dostęp do danych będzie
        możliwy w Archiwum.
        Zatwierdź operację używjąc guzika 'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="archiveClient">
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
import {archiveClient} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "archive-client-dialog",
  props: {
    selectedClient: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    dialog: false,
    tooltipVisible: false,
    loading: false,
    form: {
      userId: ""
    },

  }),
  methods: {
    ...mapMutations('legal-app-client-store', ['setClientForAction']),
    async archiveClient() {
      try {
        let clientId = this.selectedClient.id
        await this.$axios.$put(archiveClient(clientId))
        this.$notifier.showSuccessMessage("Klient został pomyślnie dodany do archiwum!");
        Object.assign(this.$data, this.$options.data.call(this)); // total data reset (all returning to default data)
        this.$nuxt.refresh();

      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.setClientForAction(this.selectedClient);
        this.dialog = false;
        this.tooltipVisible = true;
      }
    }
  }
};
</script>

<style scoped>

</style>
