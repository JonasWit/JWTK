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
            <th class="text-left">
              <span class="font-weight-bold">Data</span>
              <span class="font-italic">(Date)</span>
            </th>
            <th class="text-left">
              <span class="font-weight-bold">Prawnik</span>
              <span class="font-italic">(Layer name)</span>
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
              <span class="font-weight-bold">Stawka</span>
              <span class="font-italic">(Rate)</span>
            </th>
            <th class="text-left">
              <span class="font-weight-bold">Suma</span>
              <span class="font-italic">(Total Amount)</span>
            </th>
          </tr>
          </thead>
          <tbody>
          <tr v-for="item in selectedWorkRecords" :key="item.id">
            <td>{{ formatDateForInvoice(item.eventDate) }}</td>
            <td>{{ item.lawyerName }}</td>
            <td>{{ item.description }}</td>
            <td>{{ item.hours }}</td>
            <td>PLN {{ item.rate }}</td>
            <td>PLN {{ item.amount }}</td>

            <td></td>
          </tr>
          </tbody>
        </template>
      </v-simple-table>

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
    hoursSpent() {
      return Number(this.selectedWorkRecords.hours);
    },

    minutesSpent() {
      return Number(this.selectedWorkRecords.minutes);
    },

    givenVat() {
      return (parseInt(this.selectedWorkRecords.vat));
    },

    timeSpent() {
      return Math.round(this.hoursSpent + (this.minutesSpent / 60));
    }


  },

  methods: {

    formatDateForInvoice(date) {
      return formatDateForInvoice(date);
    },

    getTimeStamp() {
      return timeStamp();
    }
  }

}
</script>

<style scoped>

</style>
