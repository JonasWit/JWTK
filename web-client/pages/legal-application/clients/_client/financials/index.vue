<template>
  <layout>
    <template v-slot:content>
      <v-card>
        <v-tabs v-model="tab" background-color="indigo accent-4" centered dark icons-and-text>
          <v-tabs-slider></v-tabs-slider>
          <v-tab href="#tab-1">
            Twoje dane
            <v-icon>mdi-account-box-multiple</v-icon>
          </v-tab>
          <v-tab href="#tab-2">
            Moje rozliczenia
            <v-icon>mdi-clipboard-text-search</v-icon>
          </v-tab>
          <v-tab href="#tab-4">
            Generuj raport
            <v-icon>mdi-file-chart</v-icon>
          </v-tab>
        </v-tabs>
        <v-tabs-items v-model="tab">
          <v-tab-item :value="'tab-1'">
            <v-card flat>
              <v-alert v-if="legalAppTooltips" elevation="5" text type="info">Kliknij guzik "DODAJ NOWE DANE",
                aby dodać dane dotyczące Twojej działalności - take jak nazwa, adres, numer NIP itd. Te dane będą
                widoczne na raporcie rozliczeniowym. W tym miejscu możesz również edytować lub usunąć wcześniej
                uzupełnione dane.
              </v-alert>
              <add-billing-details/>
              <billing-details-list/>
            </v-card>
          </v-tab-item>
          <v-tab-item :value="'tab-2'">
            <v-card flat>
              <v-alert v-if="legalAppTooltips" elevation="5" text type="info">Kliknij guzik "DODAJ ROZLICZENIE", aby
                dodać nowy rekord.
                Wybierz datę początkową i
                końcową, a następnie użyj guzika 'Wyszukaj', aby uzyskać dostęp do wybranych rozliczeń. W tym miejscu
                możesz usunąć lub edytować dodane rozliczenia.
              </v-alert>
              <my-work-date-picker/>

            </v-card>
          </v-tab-item>
          <v-tab-item :value="'tab-4'">
            <generate-invoice/>
          </v-tab-item>
        </v-tabs-items>
      </v-card>

    </template>
  </layout>
</template>

<script>
import Layout from "../../../../../components/legal-app/layout";
import {formatDate} from "@/data/date-extensions";
import InvoiceTemplate from "@/components/legal-app/financials/invoice-template";
import AddNewWorkRecord from "@/components/legal-app/financials/dialogs/add-new-work-record";
import GenerateInvoice from "@/components/legal-app/financials/generate-invoice";
import AddBillingDetails from "@/components/legal-app/financials/dialogs/add-billing-details";
import BillingDetailsList from "@/components/legal-app/financials/billing-details-list";
import {mapState} from "vuex";
import {handleError} from "@/data/functions";
import MyWorkDatePicker from "@/components/legal-app/financials/my-work-date-picker";

export default {
  name: "index",
  components: {
    MyWorkDatePicker,
    BillingDetailsList,
    AddBillingDetails,
    GenerateInvoice,
    AddNewWorkRecord,
    InvoiceTemplate,
    Layout,
  },
  middleware: ['legal-app-permission', 'user', 'authenticated'],
  data: () => ({
      financialRecords: [],
      loading: false,
      dateFrom: null,
      dateTo: null,
      modalFrom: false,
      modalTo: false,
      alert: false,
      tab: null,
    }
  ),
  fetch() {
    return this.searchFinancialRecords();
  },
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
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
        let clientId = this.$route.params.client
        let apiQuery = `/api/legal-app-clients-work/client/${clientId}/work-records${this.query}`;
        this.financialRecords = await this.$axios.$get(apiQuery);
      } catch (error) {
        handleError(error)
        this.$notifier.showErrorMessage(error.response.data);
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
