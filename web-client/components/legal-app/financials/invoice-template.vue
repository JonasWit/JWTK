<template>
  <v-card>
    <v-card-title>Invoice content here</v-card-title>
    <v-card-title>Invoice content here</v-card-title>
    <v-card-title>Invoice content here</v-card-title>
    <v-card-title>Invoice content here</v-card-title>

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
          <!--          <invoice-template :selectedRecords="financialRecords"/>-->
        </section>
      </vue-html2pdf>
    </client-only>
  </v-card>

</template>

<script>
import MyWorkRecordsList from "@/components/legal-app/financials/my-work-records-list";

export default {
  name: "invoice-template",
  components: {MyWorkRecordsList},
}
</script>

<style scoped>

</style>
