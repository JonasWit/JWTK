<template>
  <v-dialog v-model="dialog" :value="selectedFinancialRecord" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="error">mdi-delete</v-icon>
          </v-btn>
        </template>
        <span>Usuń zarejestrowany czas</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń zarejestrowany czas</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz wybrany rekord. Odzyskanie dostępu będzie niemożliwe.
        Zatwierdź operację używjąc guzika 'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deleteFinancialRecord">
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


import {deleteWorkRecord} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "delete-work-record",
  components: {ProgressBar},
  props: {
    selectedFinancialRecord: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    dialog: false,
    loader: false,
  }),

  methods: {

    async deleteFinancialRecord() {
      this.loader = true
      try {
        let clientId = this.$route.params.client
        let workRecordId = this.selectedFinancialRecord.id
        await this.$axios.$delete(deleteWorkRecord(clientId, workRecordId))
        console.warn('Rekord usunięty pomyślnie');
        this.$notifier.showSuccessMessage("Rekord usunięty pomyślnie!");
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
