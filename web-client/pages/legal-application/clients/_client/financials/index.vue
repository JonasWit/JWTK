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
            Dodaj nowe rozliczenie
            <v-icon>mdi-clock</v-icon>
          </v-tab>
          <v-tab href="#tab-3">
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
              <v-alert elevation="5" text type="info" dismissible close-text="Zamknij">Kliknij guzik "DODAJ NOWE DANE",
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
              <v-alert elevation="5" text type="info" dismissible close-text="Zamknij">Kliknij guzik "ZAREJESTRUJ NOWE
                ROZLICZENIE", aby dodać nowy rekord. Wszystkie Twoje rozliczenia będą widoczne w zakładce "MOJE
                ROZLICZENIA".
              </v-alert>
              <add-new-work-record/>
            </v-card>
          </v-tab-item>
          <v-tab-item :value="'tab-3'">
            <v-card flat>
              <v-alert elevation="5" text type="info" dismissible close-text="Zamknij">Wybierz datę początkową i
                końcową, a następnie użyj guzika 'Wyszukaj', aby uzyskać dostęp do wybranych rozliczeń. W tym miejscu
                możesz usunąć lub edytować dodane rozliczenia.
              </v-alert>
              <my-work-records-search/>
            </v-card>
          </v-tab-item>
          <v-tab-item :value="'tab-4'">
            <generate-invoice/>
          </v-tab-item>
        </v-tabs-items>
      </v-card>
      <button-to-go-up/>
    </template>
  </layout>
</template>

<script>
import Layout from "../../../../../components/legal-app/layout";
import {formatDate} from "@/data/date-extensions";
import ButtonToGoUp from "../../../../../components/legal-app/button-to-go-up";
import InvoiceTemplate from "@/components/legal-app/financials/invoice-template";
import AddNewWorkRecord from "@/components/legal-app/financials/dialogs/add-new-work-record";
import GenerateInvoice from "@/components/legal-app/financials/generate-invoice";
import MyWorkRecordsSearch from "@/components/legal-app/financials/my-work-records-search";
import AddBillingDetails from "@/components/legal-app/financials/dialogs/add-billing-details";
import BillingDetailsList from "@/components/legal-app/financials/billing-details-list";
import MyWorkRecordsList from "~/components/legal-app/financials/my-work-records-list";
import {mapActions} from "vuex";

export default {
  name: "index",
  components: {
    MyWorkRecordsList,
    BillingDetailsList,
    AddBillingDetails,
    MyWorkRecordsSearch,
    GenerateInvoice,
    AddNewWorkRecord,
    InvoiceTemplate,
    ButtonToGoUp,
    Layout,
  },
  middleware: ['legal-app-permission', 'client-admin', 'authenticated'],
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
    ...mapActions('legal-app-client-store', ['getAllWorkRecordsOnFetch']),
    async searchFinancialRecords() {
      if (this.loading) return;
      this.loading = true;
      console.warn('handle logs fired', this.query);

      try {
        let apiQuery = `/api/legal-app-clients-work/client/${this.$route.params.client}/work-records${this.query}`;
        console.warn('call from API', apiQuery)
        let financialRecords = await this.$axios.$get(apiQuery);
        console.log('financialRecords', financialRecords);
        this.financialRecords = financialRecords
      } catch (e) {
        console.warn('Error during financial records fetch', e);
      } finally {
        let clientId = this.$route.params.client
        await this.getAllWorkRecordsOnFetch({clientId});
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
