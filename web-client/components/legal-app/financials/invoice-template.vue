<template>
  <div>
    <v-alert elevation="5" text type="info" dismissible close-text="Zamknij">Poniżej znajduje się podgląd rozliczenia.
      Sprawdź dane i kliknij 'GENERUJ ROZLICZENIE'. Jeśli
      chcesz dokonać zmian, cofnij się do poprzednich kroków.
    </v-alert>
    <v-divider></v-divider>
    <v-card id="pdfTemplate" elevation="0">
      <v-row class="d-flex justify-space-between my-4 ">
        <v-col>
          <v-card-title>Wykaz czynności objętych fakturą</v-card-title>
          <v-card-subtitle class="font-italic">Invoice details</v-card-subtitle>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-subtitle>Numer faktury <span
                class="font-italic">(Invoice number): </span>{{ invoiceDetails.number }}
              </v-list-item-subtitle>
              <v-list-item-subtitle>Data faktury <span class="font-italic">(Invoice date): </span>{{
                  invoiceDetails.date
                }}
              </v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
        </v-col>
        <v-col>
          <v-list-item>
            <v-list-item-content>
              <v-list-item-subtitle>Wygenerowano przez:</v-list-item-subtitle>
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
        </v-col>
        <v-col>
          <v-card-title>Data wygenerowania: {{ getTimeStamp() }}</v-card-title>
        </v-col>
      </v-row>
      <v-row class="d-flex justify-space-between my-4">
        <v-col cols="4">
          <v-list-item class="d-flex justify-end">
            <v-list-item-content>
              <v-list-item-subtitle>Wygenerowano dla:</v-list-item-subtitle>
              <v-list-item-title class="text-h6 my-1">
                {{ clientData.name }}
              </v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </v-col>
      </v-row>
      <v-row>
        <v-simple-table>
          <template v-slot:default>
            <thead>
            <tr>
              <th class="text-left">
                <span class="font-weight-bold">Data</span>
                <span class="font-italic">(Date)</span>
              </th>
              <th class="text-left">
                <span class="font-weight-bold">Prawnik</span>
                <span class="font-italic">(Lawyer name)</span>
              </th>
              <th class="text-left">
                <span class="font-weight-bold">Opis</span>
                <span class="font-italic">(Description)</span>
              </th>
              <th class="text-left">
                <span class="font-weight-bold">Godziny</span>
                <span class="font-italic">(Hours)</span>
              </th>
              <th class="text-left">
                <span class="font-weight-bold">Stawka netto</span>
                <span class="font-italic">(Rate net)</span>
              </th>
              <th class="text-left">
                <span class="font-weight-bold">Wartość netto</span>
                <span class="font-italic">(Amount net)</span>
              </th>
              <th class="text-left">
                <span class="font-weight-bold">Stawka VAT</span>
                <span class="font-italic">(VAT rate)</span>
              </th>
              <th class="text-left">
                <span class="font-weight-bold">Kwota VAT</span>
                <span class="font-italic">(VAT amount)</span>
              </th>
              <th class="text-left">
                <span class="font-weight-bold">Wartość brutto</span>
                <span class="font-italic">(Amount gross)</span>
              </th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="item in selectedWorkRecords" :key="item.id">
              <td>{{ formatDateForInvoice(item.eventDate) }}</td>
              <td>{{ item.lawyerName }}</td>
              <td>{{ item.description }}</td>
              <td>{{ item.hours }}godz. {{ item.minutes }}min.</td>
              <td>PLN {{ item.invoiceRateNet.toLocaleString('pl') }}</td>
              <td>PLN {{ item.invoiceAmountNet.toLocaleString('pl') }}</td>
              <td>{{ item.vat }}%</td>
              <td>PLN {{ item.invoiceVatAmount.toLocaleString('pl') }}</td>
              <td>PLN {{ item.amount.toLocaleString('pl') }}</td>
            </tr>
            </tbody>
          </template>
        </v-simple-table>
      </v-row>
      <v-row class="d-flex justify-end mx-7 my-10">
        <v-col cols="8">
        </v-col>
        <v-col cols="4">
          <v-simple-table>
            <tbody>
            <tr>
              <td class="font-weight-bold">
                Suma netto (Sum net):
              </td>
              <td class="font-weight-medium">
                PLN {{ sumNet }}
              </td>
            </tr>
            <tr>
              <td class="font-weight-bold">
                Suma vat (Sum VAT):
              </td>
              <td class="font-weight-medium">
                PLN {{ sumVat }}
              </td>
            </tr>
            <tr>
              <td class="font-weight-bold">
                Suma brutto (Sum gross):
              </td>
              <td class="font-weight-medium">
                PLN {{ sumGross }}
              </td>
            </tr>
            </tbody>
          </v-simple-table>
        </v-col>
      </v-row>
    </v-card>

    <v-alert elevation="5" text type="warning" color="orange" dark dismissible close-text="Zamknij">
      Po naciśnięciu przycisku 'GENERUJ RAPORT' pojawi się 'Podgląd wydruku'. Jeśli chcesz zapisać rozliczenie w formie
      pdf w oknie podgląd wybierz opcję 'Zapisz jako pdf'. Jeśli chcesz wydrukować rozliczenie postępuj zgodnie z
      instrukcją drukowania. Pamiętaj, że jakośc wydruku zależy od indywidualnych ustawień.
      Rekomendujemy wybór poziomego układu strony w oknie wydruku.
    </v-alert>
    <v-btn color="error" block @click="generateReport">
      Generuj rozliczenie
    </v-btn>
    <progress-bar v-if="loader"/>
  </div>

</template>

<script>
import {timeStamp, formatDateForInvoice} from "@/data/date-extensions";
import {mapActions, mapGetters, mapMutations} from "vuex";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";


export default {
  name: "invoice-template",
  components: {ProgressBar},
  props: {
    selectedBillingData: {
      required: true,
      default: null
    },
    selectedWorkRecords: {
      required: true,
      default: null
    },
    invoiceDetails: {
      required: true,
      default: null
    }
  },
  data: () => ({
    loader: true
  }),

  async fetch() {
    this.loader = true
    try {
      let clientId = this.$route.params.client;
      await this.getClientData({clientId})
    } catch (error) {
      handleError(error);
    } finally {
      this.loader = false
    }

  },
  computed: {
    ...mapGetters('legal-app-client-store', ['clientData']),
    sumNet() {
      const totalNetValue = this.selectedWorkRecords.reduce((acc, cur) => {
        return acc + cur.invoiceAmountNet;
      }, 0)
      return totalNetValue.toLocaleString('pl')
    },

    sumVat() {
      const totalVatValue = this.selectedWorkRecords.reduce((acc, cur) => {
        return acc + cur.invoiceVatAmount;
      }, 0)
      return totalVatValue.toLocaleString('pl')
    },

    sumGross() {
      const totalGrossValue = this.selectedWorkRecords.reduce((acc, cur) => {
        return acc + cur.amount;
      }, 0)
      return totalGrossValue.toLocaleString('pl')
    }
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getClientData']),
    ...mapMutations('legal-app-client-store', ['updateClientDataFromFetch']),

    formatDateForInvoice(date) {
      return formatDateForInvoice(date);
    },

    getTimeStamp() {
      return timeStamp();
    },

    generateReport() {
      try {
        const printContents = document.getElementById('pdfTemplate').innerHTML;
        const originalContents = document.body.innerHTML;
        document.body.innerHTML = printContents;
        window.print();
        document.body.innerHTML = originalContents;
      } catch (e) {
        this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        console.warn('Print error', e)
      } finally {
        document.location.reload()
      }
    },
  }
}
</script>

<style scoped>


</style>
