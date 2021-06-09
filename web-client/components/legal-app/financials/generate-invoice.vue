<template>
  <div>

    <add-billing-details/>
    <my-work-date-picker/>


    <v-row class="d-flex align-center my-3 ">
      <v-col cols="12">
        <v-btn depressed color="primary" @click="generateReport">
          Generuj raport
        </v-btn>
      </v-col>
    </v-row>

  </div>
</template>

<script>
import MyWorkDatePicker from "@/components/legal-app/financials/my-work-date-picker";
import AddBillingDetails from "@/components/legal-app/financials/dialogs/add-billing-details";

export default {
  name: "generate-invoice",
  components: {AddBillingDetails, MyWorkDatePicker},
  methods: {

    generateReport() {
      this.$refs.html2Pdf.generatePdf()
    }
  }
}
</script>

<style scoped>

</style>
