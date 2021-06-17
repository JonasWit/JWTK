<template>
  <v-card>
    <v-btn depressed color="primary" @click="generateReport">
      Generuj raport
    </v-btn>
    <v-btn depressed color="primary" @click="generateReportWithJsPdf">
      Generuj raport z jsPDF
    </v-btn>

    <v-card id="pdfTemplate" elevation="0" width="100%">
      <v-row class="d-flex justify-space-between my-4 ">
        <v-col>
          <v-card-title>Wykaz czynności objętych fakturą</v-card-title>
          <v-card-subtitle class="font-italic">Invoice details</v-card-subtitle>

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
          <v-card-title>Rozliczenie nr: {{ selectedWorkRecords.id }}</v-card-title>
          <v-card-subtitle>Data wygenerowania: {{ getTimeStamp() }}</v-card-subtitle>
        </v-col>
      </v-row>


      <v-row class="d-flex justify-space-between my-4">


        <v-col cols="4">
          <v-list-item class="d-flex justify-end">
            <v-list-item-content>
              <v-list-item-subtitle>Wygenerowano dla:</v-list-item-subtitle>
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

      </v-row>
      <v-row class="mx-7">
        <v-simple-table>
          <template v-slot:default>
            <thead>
            <tr>
              <th class="text-center">
                <span class="font-weight-bold">Data</span>
                <span class="font-italic">(Date)</span>
              </th>
              <th class="text-center">
                <span class="font-weight-bold">Prawnik</span>
                <span class="font-italic">(Lawyer name)</span>
              </th>
              <th class="text-center">
                <span class="font-weight-bold">Opis</span>
                <span class="font-italic">(Description)</span>
              </th>
              <th class="text-center">
                <span class="font-weight-bold">Godziny</span>
                <span class="font-italic">(Hours)</span>
              </th>
              <th class="text-center">
                <span class="font-weight-bold">Stawka netto</span>
                <span class="font-italic">(Rate net)</span>
              </th>
              <th class="text-center">
                <span class="font-weight-bold">Wartość netto</span>
                <span class="font-italic">(Amount net)</span>
              </th>
              <th class="text-center">
                <span class="font-weight-bold">Stawka VAT</span>
                <span class="font-italic">(VAT rate)</span>
              </th>
              <th class="text-center">
                <span class="font-weight-bold">Kwota VAT</span>
                <span class="font-italic">(VAT amount)</span>
              </th>
              <th class="text-center">
                <span class="font-weight-bold">Wartość brutto</span>
                <span class="font-italic">(Amount gross)</span>
              </th>
            </tr>
            </thead>
            <tbody>
            <tr v-for="item in selectedWorkRecords" :key="item.id" class="text-center">
              <td>{{ formatDateForInvoice(item.eventDate) }}</td>
              <td>{{ item.lawyerName }}</td>
              <td>{{ item.description }}</td>
              <td>{{ item.hours }}h {{ item.minutes }}min</td>
              <td>PLN {{ item.invoiceRateNet.toFixed(2) }}</td>
              <td>PLN {{ item.invoiceAmountNet.toFixed(2) }}</td>
              <td>{{ item.vat }}%</td>
              <td>PLN {{ item.invoiceVatAmount.toFixed(2) }}</td>
              <td>PLN {{ item.amount.toFixed(2) }}</td>
            </tr>
            </tbody>
          </template>
        </v-simple-table>
      </v-row>
      <v-row class="d-flex justify-end mx-7 my-10">
        <div>
          <v-simple-table>
            <tbody>
            <tr>
              <td class="font-weight-bold">
                Suma netto (Sum net):
              </td>
              <td class="font-weight-medium">
                {{ sumNet }}
              </td>
            </tr>
            <tr>
              <td class="font-weight-bold">
                Suma vat (Sum VAT):
              </td>
              <td class="font-weight-medium">
                {{ sumVat }}
              </td>
            </tr>
            <tr>
              <td class="font-weight-bold">
                Suma brutto (Sum gross):
              </td>
              <td class="font-weight-medium">
                {{ sumGross }}
              </td>
            </tr>
            </tbody>
          </v-simple-table>
        </div>
      </v-row>
    </v-card>


  </v-card>

</template>

<script>
import MyWorkRecordsList from "@/components/legal-app/financials/my-work-records-list";
import {timeStamp, formatDateForInvoice} from "@/data/date-extensions";


export default {
  name: "invoice-template",
  components: {MyWorkRecordsList},
  props: {
    selectedBillingData: {
      required: true,
      default: null
    },
    selectedWorkRecords: {
      required: true,
      default: null
    }
  },
  computed: {
    sumNet() {
      const totalNetValue = this.selectedWorkRecords.reduce((acc, cur) => {
        return acc + cur.invoiceAmountNet;
      }, 0)
      return totalNetValue.toFixed(2)
    },

    sumVat() {
      const totalVatValue = this.selectedWorkRecords.reduce((acc, cur) => {
        return acc + cur.invoiceVatAmount;
      }, 0)
      return totalVatValue.toFixed(2)
    },

    sumGross() {
      const totalGrossValue = this.selectedWorkRecords.reduce((acc, cur) => {
        return acc + cur.amount;
      }, 0)
      return totalGrossValue.toFixed(2)
    }
  },
  methods: {

    formatDateForInvoice(date) {
      return formatDateForInvoice(date);
    },

    getTimeStamp() {
      return timeStamp();
    },

    generateReport() {
      this.$refs.html2Pdf.generatePdf()
    },

    generateReportWithJsPdf() {
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
