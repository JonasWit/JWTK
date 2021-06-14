<template>
  <div>
    <my-work-date-picker/>
    <v-container class="my-7">
      <v-row align="center">
        <v-col class="d-flex" cols="12" sm="6">
          <v-select v-model="selectedBillingData" :items="billingDataList" item-text="name"
                    :menu-props="{ maxHeight: '400' }"
                    label="Wybierz dane rozliczeniowe" hint="Informacje będą widoczne na rozliczeniu" persistent-hint
                    return-object></v-select>
        </v-col>
        <v-col class="d-flex" cols="12" sm="6">
          <v-select v-model="selectedWorkRecords" :items="sortedFinancialRecords" item-text="name"
                    :menu-props="{ maxHeight: '400' }"
                    label="Wybierz rozliczenia" hint="Proszę wybrać zakres dat, aby zobaczyć dostępne rozliczenia"
                    persistent-hint multiple
                    return-object></v-select>
        </v-col>
      </v-row>
    </v-container>


    <v-row class="d-flex justify-space-around my-7 ">

      <!--      <v-btn depressed color="primary" @click="generateReportView">-->
      <!--        Generuj podgląd raportu-->
      <!--      </v-btn>-->

      <v-btn depressed color="primary" @click="generateReport">
        Generuj raport
      </v-btn>
    </v-row>
    <v-divider></v-divider>
    <v-card>
      <v-card-title></v-card-title>
    </v-card>

    <invoice-template :selected-billing-data="selectedBillingData" :selected-work-records="selectedWorkRecords"/>

  </div>
</template>

<script>
import AddBillingDetails from "@/components/legal-app/financials/dialogs/add-billing-details";
import InvoiceTemplate from "@/components/legal-app/financials/invoice-template";
import MyWorkRecordsList from "@/components/legal-app/financials/my-work-records-list";
import {mapActions, mapGetters, mapMutations} from "vuex";
import MyWorkDatePicker from "@/components/legal-app/financials/my-work-date-picker";

export default {
  name: "generate-invoice",
  components: {MyWorkDatePicker, MyWorkRecordsList, InvoiceTemplate, AddBillingDetails},
  data: () => ({
    selectedBillingData: [],
    selectedWorkRecords: [],


  }),
  async fetch() {
    await this.getBillingDataFromFetch()
    console.warn('billing data list -- fetch from store completed', this.billingDataList);


  },

  watch: {},


  computed: {
    ...mapGetters('legal-app-client-store', ['billingDataList', 'workRecordsList', 'sortedFinancialRecords']),


  },
  methods: {
    ...
      mapActions('legal-app-client-store', ['getBillingDataFromFetch']),
    ...
      mapMutations('legal-app-client-store', ['updateBillingDataFromFetch']),

    generateReport() {
      this.$refs.html2Pdf.generatePdf()
    }
    ,


  }
  ,


}
</script>

<style scoped>

</style>
