<template>
  <v-card>
    <v-card-title>Invoice content here</v-card-title>
    <v-col>
      <!--      <v-list class="d-flex justify-space-between" v-for="item in selectedRecords" :key="item.id">-->
      <!--        <v-list-item>-->
      <!--          <v-list-item-content>-->
      <!--            <v-list-item-subtitle>Hours: {{ item.hours }}</v-list-item-subtitle>-->
      <!--            <v-list-item-subtitle>Minutes: {{ item.minutes }}</v-list-item-subtitle>-->
      <!--            <v-list-item-subtitle>Rate: {{ item.rate }}</v-list-item-subtitle>-->
      <!--          </v-list-item-content>-->
      <!--        </v-list-item>-->
      <!--      </v-list>-->
    </v-col>


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
export default {
  name: "invoice-template",


}
</script>

<style scoped>

</style>
