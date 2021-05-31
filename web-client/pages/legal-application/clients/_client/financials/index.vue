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
        <v-col cols="12" md="5">
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
        <v-col cols="12" md="5">
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
      </v-row>
      <v-card v-for="item in financialRecords" :key="item.id">
        <v-row>
          <v-col class="mx-2">
            <v-list class="d-flex justify-space-between">
              <v-list-item-content>
                <v-list-item-subtitle> Nazwa: {{ item.name }}</v-list-item-subtitle>
                <v-list-item-subtitle> Data zdarzenia: {{ formatDate(item.eventDate) }}</v-list-item-subtitle>
                <v-list-item-subtitle> Created: {{ formatDate(item.created) }}</v-list-item-subtitle>
                <v-list-item-subtitle> Created by: {{ item.createdBy }}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list>
          </v-col>
          <v-col class="mx-2">
            <v-list class="d-flex justify-space-between">
              <v-list-item-content>
                <v-list-item-subtitle class="hidden-sm-and-down">Amount: {{ item.amount }}</v-list-item-subtitle>
                <v-list-item-subtitle class="hidden-sm-and-down">VAT: {{ item.vat }}</v-list-item-subtitle>
                <v-list-item-subtitle class="hidden-sm-and-down">Rate: {{ item.rate }}</v-list-item-subtitle>
                <v-list-item-subtitle class="hidden-sm-and-down">Hours: {{ item.hours }}</v-list-item-subtitle>
                <v-list-item-subtitle class="hidden-sm-and-down">Minutes: {{ item.minutes }}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list>
          </v-col>
          <v-col class="mx-2">
            <v-list class="d-flex justify-space-between">

              <delete-work-record :selected-financial-record="item"/>

            </v-list>
          </v-col>

        </v-row>
      </v-card>
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
      dateFrom: new Date().toISOString().substr(0, 10),
      dateTo: null,
      modalFrom: false,
      modalTo: false,
    }
  ),


  async fetch() {
    return this.searchFinancialRecords();
  },

  computed: {

    query() {
      let convertedDate = new Date(this.dateTo)
      convertedDate.setDate(convertedDate.getDate() + 1)
      convertedDate = convertedDate.toISOString().substr(0, 10)
      let fromDate = this.dateFrom
      let toDate = convertedDate
      return `?from=${fromDate}&to=${toDate}`;
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


  },
}
</script>

<style scoped>

</style>
