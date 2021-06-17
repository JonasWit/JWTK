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
                    return-object>
            <template v-slot:prepend-item>
              <v-list-item ripple @click="toggle">
                <v-list-item-action>
                  <v-icon :color="selectedWorkRecords.length > 0 ? 'indigo darken-4' : ''">
                    {{ icon }}
                  </v-icon>
                </v-list-item-action>
                <v-list-item-content>
                  <v-list-item-title>
                    Wybierz wszystko
                  </v-list-item-title>
                </v-list-item-content>
              </v-list-item>
            </template>
          </v-select>
        </v-col>
      </v-row>
    </v-container>


    <v-row class="d-flex justify-space-around my-7 ">

      <!--      <v-btn depressed color="primary" @click="generateReportView">-->
      <!--        Generuj podgląd raportu-->
      <!--      </v-btn>-->


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

  computed: {
    ...mapGetters('legal-app-client-store', ['billingDataList', 'workRecordsList', 'sortedFinancialRecords']),

    selectAllRecords() {
      return this.selectedWorkRecords.length === this.sortedFinancialRecords.length
    },
    selectSomeRecords() {
      return this.selectedWorkRecords.length > 0 && !this.selectAllRecords
    },


    icon() {
      if (this.selectAllRecords) return 'mdi-close-box'
      if (this.selectSomeRecords) return 'mdi-minus-box'
      return 'mdi-checkbox-blank-outline'
    },
  },


  methods: {
    ...mapActions('legal-app-client-store', ['getBillingDataFromFetch']),
    ...mapMutations('legal-app-client-store', ['updateBillingDataFromFetch']),


    toggle() {
      this.$nextTick(() => {
        if (this.selectAllRecords) {
          this.selectedWorkRecords = []
        } else {
          this.selectedWorkRecords = this.sortedFinancialRecords.slice()
        }
      })
    },

  },

}
</script>

<style scoped>

</style>
