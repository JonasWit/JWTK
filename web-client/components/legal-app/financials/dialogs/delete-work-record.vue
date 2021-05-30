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
import {mapActions, mapMutations} from "vuex";

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
  computed: {
    query() {

      let fromDate = `2020-12-02`
      let toDate = `2021-05-31`
      return `?from=${fromDate}&to=${toDate}`;


    },
  },
  methods: {
    ...mapMutations('legal-app-client-store', ['setFinancialRecordForAction']),
    ...mapActions('legal-app-client-store', ['getFinancialRecordsFromFetch']),
    deleteFinancialRecord() {
      console.warn('Rekord do usunięcia', this.selectedFinancialRecord)

      return this.$axios.$delete(`/api/legal-app-clients-finance/client/${this.$route.params.client}/finance-record/${this.selectedFinancialRecord.id}`)
        .then(() => {
          this.$notifier.showSuccessMessage("Rekord usunięty pomyślnie!");
          console.warn('selectedFinancialRecord deleted successfully', this.selectedFinancialRecord);
        })
        .catch((e) => {
          console.warn('delete selectedFinancialRecord error', e);
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        }).finally(() => {
          let clientId = this.$route.params.client;
          let query = this.query;
          this.getFinancialRecordsFromFetch({clientId, query})
          this.dialog = false;
        });
    }


  }
}
</script>

<style scoped>

</style>
