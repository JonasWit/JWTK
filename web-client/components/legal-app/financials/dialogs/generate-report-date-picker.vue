<template>
  <div>
    <v-row class="d-flex align-center my-3 mx-4">
      <v-col cols="12" md="3">
        <v-dialog ref="dialogFrom" v-model="modalFrom" :return-value.sync="dateFrom" persistent width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field v-model="dateFrom" label="Wybierz datę początkową" prepend-icon="mdi-calendar" readonly
                          v-bind="attrs"
                          v-on="on"></v-text-field>
          </template>
          <v-date-picker v-model="dateFrom" scrollable @change="$refs.dialogFrom.save(dateFrom)" locale="pl">
            <v-btn text color="error" @click="modalFrom = false">
              Anuluj
            </v-btn>
            <v-spacer></v-spacer>
          </v-date-picker>
        </v-dialog>
      </v-col>
      <v-col cols="12" md="3">
        <v-dialog ref="dialogTo" v-model="modalTo" :return-value.sync="dateTo" persistent
                  width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field v-model="dateTo" label="Wybierz datę końcową" prepend-icon="mdi-calendar" readonly
                          v-bind="attrs"
                          v-on="on"></v-text-field>
          </template>
          <v-date-picker v-model="dateTo" scrollable @change="$refs.dialogTo.save(dateTo)" locale="pl">
            <v-btn text color="error" @click="modalTo = false">
              Anuluj
            </v-btn>
            <v-spacer></v-spacer>
          </v-date-picker>
        </v-dialog>
      </v-col>
      <v-col cols="12" md="2">
        <v-btn depressed color="primary" @click="searchFinancialRecords">
          Wyszukaj
        </v-btn>
      </v-col>
      <v-col cols="12" md="2">
        <v-btn depressed color="primary" @click="resetAll">
          Wyczyść
        </v-btn>
      </v-col>

      <v-alert width="100%" v-model="alert" border="left" close-text="Zamknij" type="error" outlined dismissible>
        Proszę wybrać poprawny zakres dat. Data początkowa nie może być większa od daty końcowej."
      </v-alert>

    </v-row>

  </div>

</template>

<script>
import {mapActions, mapGetters, mapMutations} from "vuex";
import {formatDate} from "@/data/date-extensions";
import ProgressBar from "@/components/legal-app/progress-bar";

export default {
  name: "generate-report-date-picker",
  components: {ProgressBar},
  data: () => ({

      loading: false,
      dateFrom: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
      dateTo: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
      modalFrom: false,
      modalTo: false,
      alert: false,

    }
  ),
  async fetch() {
    return this.searchFinancialRecords();
  },

  computed: {
    ...mapGetters('legal-app-client-store', ['workRecordsList']),
    query() {
      let convertedDateTo = new Date(this.dateTo);
      convertedDateTo.setDate(convertedDateTo.getDate() + 1);
      convertedDateTo = convertedDateTo.toISOString().substr(0, 10);

      let convertedDateFrom = new Date(this.dateFrom);
      convertedDateFrom = convertedDateFrom.toISOString().substr(0, 10);

      let fromDate = convertedDateFrom;
      let toDate = convertedDateTo;

      if (fromDate < toDate) {
        return `?from=${fromDate}&to=${toDate}`;
      } else {
        this.alert = true;
      }
    },

  },

  methods: {
    ...mapMutations('legal-app-client-store', ['updateFinancialRecordsFromFetch', 'reset']),
    ...mapActions('legal-app-client-store', ['getFinancialRecordsFromFetch']),

    async searchFinancialRecords() {
      if (this.loading) return;
      this.progressBar = true;
      this.loading = true;
      console.warn('handle logs fired', this.query);
      try {
        let clientId = this.$route.params.client;
        let query = this.query;
        await this.getFinancialRecordsFromFetch({clientId, query});
      } catch (error) {
        console.error(error)
        this.$notifier.showErrorMessage(error);
      } finally {
        this.loading = false;
        this.progressBar = false
      }
    },
    resetAll() {
      Object.assign(this.$data, this.$options.data.call(this));
      this.reset()
    },
    formatDate(date) {
      return formatDate(date);
    },
  },
}
</script>

<style scoped>

</style>
