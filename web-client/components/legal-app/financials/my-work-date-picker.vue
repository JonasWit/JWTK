<template>
  <div>
    <v-row class="d-flex align-center justify-space-between my-5 mx-5">
      <add-new-work-record v-on:action-completed="actionDone"/>
      <v-divider vertical class="mx-4"></v-divider>
      <v-dialog ref="dialogFrom" v-model="modalFrom" :return-value.sync="dateFrom" persistent width="290px">
        <template v-slot:activator="{ on, attrs }">
          <v-text-field v-model="dateFrom" label="Wybierz datę początkową" prepend-icon="mdi-calendar" readonly
                        v-bind="attrs" v-on="on"></v-text-field>
        </template>
        <v-date-picker v-model="dateFrom" scrollable @change="$refs.dialogFrom.save(dateFrom)" locale="pl">
          <v-btn text color="error" @click="modalFrom = false">
            Anuluj
          </v-btn>
          <v-spacer></v-spacer>
        </v-date-picker>
      </v-dialog>
      <v-dialog ref="dialogTo" v-model="modalTo" :return-value.sync="dateTo" persistent width="290px">
        <template v-slot:activator="{ on, attrs }">
          <v-text-field v-model="dateTo" label="Wybierz datę końcową" prepend-icon="mdi-calendar" readonly
                        v-bind="attrs" v-on="on"></v-text-field>
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
    <v-toolbar color="primary" dark v-if="workRecordsList.length > 0">
      <v-toolbar-title>Lista rozliczeń</v-toolbar-title>
    </v-toolbar>
    <v-card v-for="item in workRecordsList" :key="item.id">
      <v-row class="d-flex justify-space-between">
        <v-col>
          <v-list class="d-flex justify-space-between">
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title> Nazwa: {{ item.name }}</v-list-item-title>
                <v-list-item-title> Data zdarzenia: {{ formatDate(item.eventDate) }}</v-list-item-title>
                <v-list-item-subtitle> Created: {{ formatDate(item.created) }}</v-list-item-subtitle>
                <v-list-item-subtitle> Created by: {{ item.createdBy }}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </v-col>
        <v-col>
          <v-list class="d-flex justify-space-between">
            <v-list-item>
              <v-list-item-content>
                <p>Godziny: {{ item.hours }} godz.</p>
                <p>Minuty: {{ item.minutes }} min.</p>
                <p>Stawka godzinowa brutto: {{ item.rate.toLocaleString('pl') }}PLN</p>
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </v-col>
        <v-col>
          <v-list class="d-flex justify-space-between">
            <v-list-item>
              <v-list-item-content class="text--primary">
                <p>Stawka VAT: {{ item.vat }}%</p>
                <p>Kwota VAT: {{ item.invoiceVatAmount.toLocaleString('pl') }}PLN</p>
                <p>Kwota brutto: {{ item.amount.toLocaleString('pl') }}PLN</p>
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </v-col>
        <v-col>
          <v-list class="d-flex justify-md-end justify-sm-space-between">
            <v-list-item>
              <delete-work-record :selected-financial-record="item" v-on:action-completed="actionDone"/>
              <edit-work-record :selected-financial-record="item" v-on:action-completed="actionDone"/>
            </v-list-item>
          </v-list>
        </v-col>
      </v-row>
    </v-card>
  </div>
</template>

<script>
import {mapActions, mapGetters, mapMutations} from "vuex";
import AddNewWorkRecord from "~/components/legal-app/financials/dialogs/add-new-work-record";
import {formatDate} from "@/data/date-extensions";
import DeleteWorkRecord from "@/components/legal-app/financials/dialogs/delete-work-record";
import EditWorkRecord from "@/components/legal-app/financials/dialogs/edit-work-record";
import {handleError} from "@/data/functions";

export default {
  name: "my-work-date-picker",
  components: {AddNewWorkRecord, EditWorkRecord, DeleteWorkRecord},
  data: () => ({
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
      try {
        let clientId = this.$route.params.client;
        let query = this.query;
        await this.getFinancialRecordsFromFetch({clientId, query});
      } catch (error) {
        handleError(error);
      }
    },
    resetAll() {
      Object.assign(this.$data, this.$options.data.call(this));
      this.reset();
    },
    formatDate(date) {
      return formatDate(date);
    },
    async actionDone() {
      try {
        return this.searchFinancialRecords();
      } catch (error) {
        handleError(error);
      }
    }
  },
};
</script>

<style scoped>

</style>
