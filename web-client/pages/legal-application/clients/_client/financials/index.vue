<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="my-3">
        <v-toolbar-title class="mr-3">
          Financials
        </v-toolbar-title>
        <add-new-work-record/>
      </v-toolbar>
      <v-row class="d-flex align-center my-3 ">
        <v-col cols="12" md="4">
          <v-dialog ref="dialogFrom" v-model="modalFrom" :return-value.sync="dateFrom" persistent width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field v-model="dateFrom" label="Wybierz datę początkową" prepend-icon="mdi-calendar" readonly
                            v-bind="attrs"
                            v-on="on"></v-text-field>
            </template>
            <v-date-picker v-model="dateFrom" scrollable>
              <v-spacer></v-spacer>
              <v-btn text color="primary" @click="modalFrom = false">
                Cancel
              </v-btn>
              <v-btn text color="primary" @click="$refs.dialogFrom.save(dateFrom)">
                OK
              </v-btn>
            </v-date-picker>
          </v-dialog>
        </v-col>
        <v-col cols="12" md="4">
          <v-dialog ref="dialogTo" v-model="modalTo" :return-value.sync="dateTo" persistent
                    width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field v-model="dateTo" label="Wybierz datę końcową" prepend-icon="mdi-calendar" readonly
                            v-bind="attrs"
                            v-on="on"></v-text-field>
            </template>
            <v-date-picker v-model="dateTo" scrollable>
              <v-spacer></v-spacer>
              <v-btn text color="primary" @click="modalTo = false">
                Cancel
              </v-btn>
              <v-btn text color="primary" @click="$refs.dialogTo.save(dateTo)">
                OK
              </v-btn>
            </v-date-picker>
          </v-dialog>
        </v-col>
        <v-col cols="12" md="2">
          <v-btn depressed color="primary" @click="searchFinancialRecords">
            Wyszukaj
          </v-btn>
        </v-col>
        <v-col cols="12" md="2">
          <v-btn depressed color="primary" @click="clearResults">
            Wyczyść
          </v-btn>
        </v-col>
        <v-col cols="12" md="2">
          <v-btn depressed color="primary" @click="downloadFile">
            Generuj raport
          </v-btn>
        </v-col>
      </v-row>
      <v-card v-for="item in financialRecords" :key="item.id" id="invoice">
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
                  <v-list-item-subtitle>Hours: {{ item.hours }}</v-list-item-subtitle>
                  <v-list-item-subtitle>Minutes: {{ item.minutes }}</v-list-item-subtitle>
                  <v-list-item-subtitle>Rate: {{ item.rate }}</v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-list>
          </v-col>
          <v-col>
            <v-list class="d-flex justify-space-between">
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-subtitle>Rate: {{ item.rate }}</v-list-item-subtitle>
                  <v-list-item-subtitle>VAT: {{ item.vat }}</v-list-item-subtitle>
                  <v-list-item-subtitle>Amount: {{ item.amount }}</v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-list>
          </v-col>

          <v-col>
            <v-list class="d-flex justify-md-end justify-sm-space-between">
              <v-list-item>
                <delete-work-record :selected-financial-record="item"/>
                <edit-work-record :selected-financial-record="item"/>
              </v-list-item>
            </v-list>
          </v-col>
        </v-row>
      </v-card>

      <button-to-go-up/>
      <v-alert v-model="alert" border="left" close-text="Zamknij" type="error" outlined dismissible>
        Proszę wybrać poprawny zakres dat. Data początkowa nie może być większa od daty końcowej."
      </v-alert>
    </template>

  </layout>
</template>

<script>
import Layout from "../../../../../components/legal-app/layout";
import {formatDate} from "@/data/date-extensions";
import ButtonToGoUp from "../../../../../components/legal-app/button-to-go-up";
import AddNewWorkRecord from "../../../../../components/legal-app/financials/dialogs/add-new-work-record";
import DeleteWorkRecord from "../../../../../components/legal-app/financials/dialogs/delete-work-record";
import EditWorkRecord from "../../../../../components/legal-app/financials/dialogs/edit-work-record";
import html2pdf from "html2pdf.js";

export default {
  name: "index",
  components: {EditWorkRecord, DeleteWorkRecord, AddNewWorkRecord, ButtonToGoUp, Layout},
  middleware: ['legal-app-permission', 'client', 'authenticated'],
  data: () => ({
      financialRecords: [],
      loading: false,
      dateFrom: null,
      dateTo: null,
      modalFrom: false,
      modalTo: false,
      alert: false,
    }
  ),


  async fetch() {
    return this.searchFinancialRecords();
  },

  computed: {

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
    async searchFinancialRecords() {
      if (this.loading) return;
      this.loading = true;
      console.warn('handle logs fired', this.query);

      try {
        let financialRecords = await this.$axios.$get(`/api/legal-app-clients-finance/client/${this.$route.params.client}/finance-records${this.query}`)
        console.log('financialRecords', financialRecords);
        this.financialRecords = financialRecords
      } catch (e) {
        console.warn('Error during financial records fetch', e);
      } finally {
        this.loading = false;
      }
    },
    formatDate(date) {
      return formatDate(date);
    },
    clearResults() {
      Object.assign(this.$data, this.$options.data.call(this)); // total data reset (all returning to default data)
    },

    downloadFile() {
      const element = document.getElementById('invoice');
      html2pdf().from(element).save('my_document.pdf');

    },


  },
}
</script>

<style scoped>

</style>
