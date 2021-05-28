<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="my-3">
        <v-toolbar-title class="mr-3">
          Financials
        </v-toolbar-title>
        <add-new-work-record/>
      </v-toolbar>
      <v-col>
        <v-dialog ref="dialog" v-model="modal" :return-value.sync="dates" persistent width="290px">
          <template v-slot:activator="{ on, attrs }">
            <v-text-field v-model="dateRangeText" label="Wybierz zakres dat" prepend-icon="mdi-calendar" readonly
                          v-bind="attrs"
                          v-on="on"></v-text-field>

          </template>
          <v-date-picker v-model="dates" range scrollable>
            <v-spacer></v-spacer>
            <v-btn text color="primary" @click="modal = false">
              Anuluj
            </v-btn>
            <v-btn text color="primary" @click="saveDates">
              Zapisz
            </v-btn>
          </v-date-picker>
        </v-dialog>
      </v-col>

      <v-expansion-panels focusable>
        <v-expansion-panel v-for="item in financialRecords" :key="item.id">
          <v-expansion-panel-header>
            <v-row>
              <v-col>
                <v-col> Nazwa: {{ item.name }}</v-col>
                <v-col> Created: {{ formatDate(item.created) }}</v-col>
                <v-col> Created by: {{ item.createdBy }}</v-col>
              </v-col>
              <v-col>
                <v-col class="hidden-sm-and-down">Amount: {{ item.amount }}</v-col>
                <v-col class="hidden-sm-and-down">Rate: {{ item.rate }}</v-col>
                <v-col class="hidden-sm-and-down">Hours: {{ item.hours }}</v-col>
                <v-col class="hidden-sm-and-down">Minutes: {{ item.minutes }}</v-col>
              </v-col>
            </v-row>
          </v-expansion-panel-header>
          <v-expansion-panel-content>
            <delete-work-record :selected-financial-record="item"/>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>

      <button-to-go-up/>
    </template>
  </layout>
</template>

<script>
import Layout from "../../../../../components/legal-app/layout";
import {formatDate} from "@/data/date-extensions";
import ButtonToGoUp from "../../../../../components/legal-app/button-to-go-up";
import AddNewWorkRecord from "../../../../../components/legal-app/financials/dialogs/add-new-work-record";
import DeleteWorkRecord from "../../../../../components/legal-app/financials/dialogs/delete-work-record";


export default {
  name: "index",
  components: {DeleteWorkRecord, AddNewWorkRecord, ButtonToGoUp, Layout},
  middleware: ['legal-app-permission', 'client', 'authenticated'],

  data: () => ({
      financialRecords: [],
      loading: false,
      dates: [],
      fromDate: "",
      toDate: "",
      modal: false,
      searchConditionsProvided: false,
    }
  ),

  async fetch() {
    return this.handleLogs();

  },
  watch: {
    dates(dates) {
      if (dates.length === 0) {
        this.fromDate = new Date();
        this.toDate = new Date();
      }
      if (dates.length === 1) {
        this.fromDate = dates[0];
        this.toDate = dates[0];
      }
      if (dates.length === 2) {
        let fromDate = dates[0].replace(/-/g, "");
        let toDate = dates[1].replace(/-/g, "");

        let firstDateInt = parseInt(fromDate);
        let secondDateInt = parseInt(toDate);

        if (firstDateInt > secondDateInt) {
          this.fromDate = dates[1];
          this.toDate = dates[0];
        } else {
          this.fromDate = dates[0];
          this.toDate = dates[1];
        }
      }

      console.log('fromDate', this.fromDate);
      console.log('toDate', this.toDate);
    }
  },

  computed: {

    dateRangeText() {
      return this.dates.join(' - ')

    },

    query() {

      let fromDate = `2020-12-02`
      let toDate = `2021-05-31`
      return `?from=${fromDate}&to=${toDate}`;


    },
    todayDate() {
      return new Date().toISOString().substr(0, 10);
    },

  }
  ,

  methods: {
    handleLogs() {
      if (this.loading) return;
      this.loading = true;

      console.warn('handle logs fired', this.query);
      return this.$axios.$get(`/api/legal-app-clients-finance/client/${this.$route.params.client}/finance-records${this.query}`)
        .then(financialRecords => {
          console.log('financialRecords', financialRecords);
          this.financialRecords = financialRecords
        })
        .finally(() => this.loading = false);

    },
    formatDate(date) {
      return formatDate(date);
    },

    saveDates(dates) {
      // this.handleLogs();
      // this.loading = false;
      // console.warn('dates saved', this.dates)
      // console.warn('filtered records', this.financialRecords)


    }

  }
  ,
}
</script>

<style scoped>

</style>
