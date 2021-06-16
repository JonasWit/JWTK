<template>
  <v-card>
    <v-card>
      <v-row class="d-flex">
        <v-col cols="6">
          <v-card-title>Wykaz czynności objętych fakturą</v-card-title>
          <v-card-subtitle class="font-italic">Invoice details</v-card-subtitle>
        </v-col>
        <v-col cols="6">
          <v-card-subtitle>Data: {{ getTimeStamp() }}</v-card-subtitle>

        </v-col>
      </v-row>

      <v-row>
        <v-col cols="6">
          <v-card-title>
            Nazwa: {{ selectedBillingData.name }}
          </v-card-title>
          <v-card-text>Adres: {{ selectedBillingData.street }} {{ selectedBillingData.address }},
            {{ selectedBillingData.postalCode }}, {{ selectedBillingData.city }}
          </v-card-text>
          <v-card-text>Tel.: {{ selectedBillingData.phoneNumber }}, Fax: {{
              selectedBillingData.faxNumber
            }}
          </v-card-text>
          <v-card-text>NIP: {{ selectedBillingData.nip }}, REGON: {{ selectedBillingData.regon }}</v-card-text>
        </v-col>
        <v-col cols="6">

        </v-col>
      </v-row>

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
              <span class="font-italic">(Layer name)</span>
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
              <span class="font-weight-bold">VAT</span>
              <span class="font-italic">(VAT)</span>
            </th>
            <th class="text-center">
              <span class="font-weight-bold">Kwota VAT</span>
              <span class="font-italic">(VAT amount)</span>
            </th>
            <th class="text-center">
              <span class="font-weight-bold">Amount net</span>
              <span class="font-italic">(Amount net)</span>
            </th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="item in selectedWorkRecords" :key="item.id" class="text-center">
            <td>{{ formatDateForInvoice(item.eventDate) }}</td>
            <td>{{ item.lawyerName }}</td>
            <td>{{ item.description }}</td>
            <td>{{ item.hours }}h {{ item.minutes }}min</td>
            <td>PLN{{ item.invoiceRateNet }}</td>
            <td>{{ item.invoiceDecimalVat }}</td>
            <td>PLN {{ item.invoiceVatAmount }}</td>
            <td>PLN {{ item.invoiceAmountNet }}</td>
          </tr>
          </tbody>
        </template>
      </v-simple-table>
      <v-divider class="my-6"></v-divider>


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
