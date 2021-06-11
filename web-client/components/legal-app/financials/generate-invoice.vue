<template>
  <div>
    <v-container fluid>
      <v-row align="center">
        <v-col
          class="d-flex"
          cols="12"
          sm="6"
        >
          <v-select
            :items="items"
            label="Standard"
          ></v-select>
        </v-col>

        <v-col
          class="d-flex"
          cols="12"
          sm="6"
        >
          <v-select
            :items="items"
            filled
            label="Filled style"
          ></v-select>
        </v-col>

        <v-col
          class="d-flex"
          cols="12"
          sm="6"
        >
          <v-select
            :items="items"
            label="Outlined style"
            outlined
          ></v-select>
        </v-col>

        <v-col
          class="d-flex"
          cols="12"
          sm="6"
        >
          <v-select
            :items="items"
            label="Solo field"
            solo
          ></v-select>
        </v-col>
      </v-row>
    </v-container>

    <my-work-date-picker/>
    <my-work-records-list/>
    <v-divider></v-divider>

    <v-row class="d-flex justify-space-around my-7 ">

      <v-btn depressed color="primary" @click="generateReportView">
        Generuj podgląd raportu
      </v-btn>

      <v-btn depressed color="primary" @click="generateReport">
        Generuj raport
      </v-btn>

    </v-row>
    <v-divider></v-divider>

    <invoice-template/>

  </div>
</template>

<script>
import MyWorkDatePicker from "@/components/legal-app/financials/my-work-date-picker";
import AddBillingDetails from "@/components/legal-app/financials/dialogs/add-billing-details";
import InvoiceTemplate from "@/components/legal-app/financials/invoice-template";
import MyWorkRecordsList from "@/components/legal-app/financials/my-work-records-list";

export default {
  name: "generate-invoice",
  components: {MyWorkRecordsList, InvoiceTemplate, AddBillingDetails, MyWorkDatePicker},
  methods: {

    generateReport() {
      this.$refs.html2Pdf.generatePdf()
    }
  }
}
</script>

<style scoped>

</style>
