<template>
  <v-container class="my-7">
    <v-stepper v-model="e1" vertical>
      <v-stepper-header>
        <v-stepper-step :complete="e1 > 1" step="1" editable>
          Zakres dat
        </v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="2" editable>
          Twoje dane
        </v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="3" editable>
          Faktura
        </v-stepper-step>
        <v-divider></v-divider>
        <v-stepper-step step="4" editable>
          Generuj raport
        </v-stepper-step>
      </v-stepper-header>
      <v-stepper-items>
        <v-stepper-content step="1">
          <v-card class="mb-12" elevation="0">
            <v-alert elevation="5" text type="info" dismissible close-text="Zamknij">Wybierz datę początkową i końcową,
              a następnie użyj guzika 'Wyszukaj', aby uzyskać dostęp
              do wybranych rozliczeń.
            </v-alert>
            <generate-report-date-picker/>
            <v-select v-model="selectedWorkRecords" :items="sortedFinancialRecords" item-text="name"
                      :menu-props="{ maxHeight: '400' }"
                      label="Wybierz rozliczenia"
                      persistent-hint multiple
                      return-object class="mx-4">
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
          </v-card>
          <v-btn color="primary" @click="e1 = 2">
            Przejdź dalej
          </v-btn>
        </v-stepper-content>
        <v-stepper-content step="2">
          <v-alert elevation="5" text type="info" dismissible close-text="Zamknij">Wybierz Twoje dane. Zostaną one
            dodane do dokumentu. Jeśli jeszcze nie dodałeś danych
            rozliczeniowych wejdź w zakładkę 'TWOJE DANE'.
          </v-alert>
          <v-card class="mb-12 pa-4" elevation="0">
            <v-select v-model="selectedBillingData" :items="billingDataList" item-text="name"
                      :menu-props="{ maxHeight: '400' }"
                      label="Wybierz Twoje dane rozliczeniowe" persistent-hint
                      return-object></v-select>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title class="text-h6 my-1">
                  {{ selectedBillingData.name }}
                </v-list-item-title>
                <v-list-item-subtitle>Adres: {{ selectedBillingData.street }} {{ selectedBillingData.address }},
                  {{ selectedBillingData.postalCode }}, {{ selectedBillingData.city }}
                </v-list-item-subtitle>
                <v-list-item-subtitle>Tel.: {{ selectedBillingData.phoneNumber }}, Fax:
                  {{ selectedBillingData.faxNumber }}
                </v-list-item-subtitle>
                <v-list-item-subtitle>NIP: {{ selectedBillingData.nip }}, REGON: {{ selectedBillingData.regon }}
                </v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-card>
          <v-btn color="primary" @click="e1 = 3">
            Przejdź dalej
          </v-btn>
          <v-btn color="primary" @click="e1 = 1">
            Wróć
          </v-btn>
        </v-stepper-content>
        <v-stepper-content step="3">
          <v-card class="mb-12" elevation="0">
            <v-alert elevation="5" text type="info" close-text="Zamknij" class="mb-12">
              Uzupełnij poniższe dane, jeśli rozliczenie jest powiązane z wystawioną fakturą VAT.
            </v-alert>
            <v-dialog v-model="dialog" max-width="500px">
              <template #activator="{ on: dialog }" v-slot:activator="{ on }">
                <v-tooltip bottom>
                  <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
                    <v-btn depressed color="primary" v-on="{ ...tooltip, ...dialog }">
                      Dodaj nr i datę faktury
                    </v-btn>
                  </template>
                  <span>Dodaj nr i datę faktury</span>
                </v-tooltip>
              </template>
              <v-form ref="addInvoiceDetails">
                <v-card>
                  <v-card-text>
                    <v-text-field v-model="form.invoiceNumber" label="Dodaj nr faktury"
                                  required></v-text-field>
                    <v-text-field v-model="form.invoiceDate" label="Dodaj datę faktury"
                                  required></v-text-field>
                  </v-card-text>
                  <v-divider></v-divider>
                  <v-card-actions>
                    <v-spacer></v-spacer>
                    <v-btn text color="primary" @click="handleSubmit">
                      Dodaj
                    </v-btn>
                  </v-card-actions>
                </v-card>
              </v-form>
            </v-dialog>
            <v-list-item>
              <v-list-item-content>
                <v-list-item-subtitle>Numer faktury: {{ invoiceDetails.number }}
                </v-list-item-subtitle>
                <v-list-item-subtitle>Data faktury: {{
                    invoiceDetails.date
                  }}
                </v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-card>
          <v-btn color="primary" @click="e1 = 4">
            Przejdź dalej
          </v-btn>
          <v-btn color="primary" @click="e1 = 2">
            Wróć
          </v-btn>
        </v-stepper-content>
        <v-stepper-content step="4">
          <v-card class="mb-12" elevation="0">
            <invoice-template :selected-billing-data="selectedBillingData"
                              :selected-work-records="selectedWorkRecords"
                              :invoice-details="invoiceDetails"/>
          </v-card>
          <v-btn color="primary" @click="e1 = 3">
            Wróć
          </v-btn>
        </v-stepper-content>
      </v-stepper-items>
    </v-stepper>
    <progress-bar v-if="loader"/>
  </v-container>
</template>
<script>
import AddBillingDetails from "@/components/legal-app/financials/dialogs/add-billing-details";
import InvoiceTemplate from "@/components/legal-app/financials/invoice-template";
import {mapActions, mapGetters, mapMutations} from "vuex";
import MyWorkDatePicker from "@/components/legal-app/financials/my-work-date-picker";
import GenerateReportDatePicker from "@/components/legal-app/financials/dialogs/generate-report-date-picker";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "generate-invoice",
  components: {ProgressBar, GenerateReportDatePicker, MyWorkDatePicker, InvoiceTemplate, AddBillingDetails},
  data: () => ({
    dialog: false,
    loader: false,
    selectedBillingData: [],
    selectedWorkRecords: [],
    form: {
      invoiceNumber: '',
      invoiceDate: ''
    },
    invoiceDetails: [],
    e1: 1,
  }),
  async fetch() {
    this.loader = true
    try {
      await this.getBillingDataFromFetch()
    } catch (error) {
      handleError(error);
    } finally {
      this.loader = false
    }

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
    handleSubmit() {
      this.loader = true
      try {
        const details = {
          number: this.form.invoiceNumber,
          date: this.form.invoiceDate
        }
        return this.invoiceDetails = details
        this.$notifier.showSuccessMessage("Dodano pomyślnie!");
      } catch (error) {
        handleError(error);
      } finally {
        setTimeout(() => {
          this.loader = false;
          this.dialog = false;
        }, 1500)
      }
    }
  },
}
</script>

<style scoped>


</style>
