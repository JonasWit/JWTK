<template>

  <v-card>
    <v-card>
      <v-row class="d-flex justify-space-between mx-2">
        <div>
          <v-card-title>Wykaz czynności objętych fakturą</v-card-title>
          <v-card-subtitle class="font-italic">Invoice details</v-card-subtitle>
          <v-card-subtitle>Data: {{ getTimeStamp() }}</v-card-subtitle>
        </div>

        <div style="width: 350px">
          <v-list-item>
            <v-list-item-content>
              <v-list-item-title class="text-h6 my-1">
                {{ selectedBillingData.name }}
              </v-list-item-title>
              <v-list-item-subtitle>Adres: {{ selectedBillingData.street }} {{ selectedBillingData.address }},
                {{ selectedBillingData.postalCode }}, {{ selectedBillingData.city }}
              </v-list-item-subtitle>
              <v-list-item-subtitle>Tel.: {{ selectedBillingData.phoneNumber }}, Fax: {{
                  selectedBillingData.faxNumber
                }}
              </v-list-item-subtitle>
              <v-list-item-subtitle>NIP: {{ selectedBillingData.nip }}, REGON: {{
                  selectedBillingData.regon
                }}
              </v-list-item-subtitle>
            </v-list-item-content>
          </v-list-item>
        </div>
      </v-row>

      <v-row class="mx-2">
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
              <td>PLN {{ item.invoiceRateNet }}</td>
              <td>PLN {{ item.invoiceAmountNet }}</td>
              <td>{{ item.vat }}%</td>
              <td>PLN {{ item.invoiceVatAmount }}</td>
              <td>PLN {{ item.amount }}</td>
            </tr>
            </tbody>
          </template>
        </v-simple-table>
      </v-row>
      <v-divider class="my-6"></v-divider>
      <v-row class="d-flex justify-end">
        <div>
          <v-list-item class="my-7">
            <v-list-item-content>
              <v-list-item-title>
                Suma netto (Sum net): {{ sumNet }}
              </v-list-item-title>
              <v-list-item-title>
                Suma vat (Sum VAT): {{ sumVat }}
              </v-list-item-title>
              <v-list-item-title>
                Suma brutto (Sum gross): {{ sumGross }}
              </v-list-item-title>

            </v-list-item-content>
          </v-list-item>
        </div>
      </v-row>
    </v-card>


    <client-only>
      <vue-html2pdf :show-layout="false"
                    :float-layout="true"
                    :enable-download="true"
                    :preview-modal="false"
                    :paginate-elements-by-height="1400"
                    filename="hee hee"
                    :pdf-quality="2"
                    :manual-pagination="false"
                    pdf-format="a4"
                    pdf-orientation="landscape"
                    pdf-content-width="800px"
                    @hasStartedGeneration="hasStartedGeneration()"
                    @hasGenerated="hasGenerated($event)"
                    ref="html2Pdf">
        <section slot="pdf-content">
        </section>
      </vue-html2pdf>
    </client-only>
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
      console.warn('sumNet', totalNetValue)
    },

    sumVat() {
      const totalVatValue = this.selectedWorkRecords.reduce((acc, cur) => {
        return acc + cur.invoiceVatAmount;
      }, 0)
      console.warn('sumVat', totalVatValue)
    },

    sumGross() {
      const totalGrossValue = this.selectedWorkRecords.reduce((acc, cur) => {
        return acc + cur.amount;
      }, 0)
      console.warn('sumGross', totalGrossValue)
    }

  }
  ,
  methods: {

    formatDateForInvoice(date) {
      return formatDateForInvoice(date);
    },

    getTimeStamp() {
      return timeStamp();
    },

  }

}
</script>

<style scoped>

</style>
