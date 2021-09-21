<template>
  <div>
    <v-row class="d-flex align-center justify-space-between my-5 mx-5">

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


      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn color="primary" icon v-on="{ ...tooltip }" @click="searchFinancialRecords">
            <v-icon>mdi-clipboard-text-search</v-icon>
          </v-btn>
        </template>
        <span>Wyszukaj rozliczenia</span>
      </v-tooltip>
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn color="amber" icon v-on="{ ...tooltip }" @click="resetAll">
            <v-icon>mdi-close-circle</v-icon>
          </v-btn>
        </template>
        <span>Wyczyść rezultaty</span>
      </v-tooltip>

      <v-alert width="100%" v-model="alert" border="left" close-text="Zamknij" type="error" outlined dismissible>
        Proszę wybrać poprawny zakres dat. Data początkowa nie może być większa od daty końcowej."
      </v-alert>

    </v-row>
  </div>

</template>

<script>
import {mapActions, mapGetters, mapMutations, mapState} from "vuex";
import {formatDate} from "@/data/date-extensions";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "generate-report-date-picker",
  components: {ProgressBar},
  data: () => ({

      loader: false,
      dateFrom: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
      dateTo: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
      modalFrom: false,
      modalTo: false,
      alert: false,

    }
  ),
  async fetch() {
    this.loader = true
    try {
      await this.searchFinancialRecords();
    } catch (error) {
      handleError(error);
    } finally {
      this.loader = false
    }

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
      this.loader = true;
      try {
        let clientId = this.$route.params.client;
        let query = this.query;
        await this.getFinancialRecordsFromFetch({clientId, query});
      } catch (error) {
        handleError(error);
      } finally {
        this.loader = false;
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
