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
  </v-dialog>
</template>

<script>
import {mapMutations} from "vuex";

export default {
  name: "delete-work-record",
  props: {
    selectedFinancialRecord: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    dialog: false,
    loading: false,
  }),
  methods: {
    ...mapMutations('legal-app-client-store', ['setFinancialRecordForAction']),
    deleteFinancialRecord() {
      return this.$axios.$delete(`/api/legal-app-clients-finance/client/${this.$route.params.client}/finance-record/${this.selectedFinancialRecord.id}`)
        .then((selectedFinancialRecord) => {
          this.$notifier.showSuccessMessage("Rekord usunięty pomyślnie!");
          console.warn('selectedFinancialRecord deleted successfully', selectedFinancialRecord);
        })
        .catch((e) => {
          console.warn('delete selectedFinancialRecord error', e);
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        }).finally(() => {
          this.setFinancialRecordForAction(this.selectedFinancialRecord);
          this.dialog = false;
        });
    }


  }
}
</script>

<style scoped>

</style>
