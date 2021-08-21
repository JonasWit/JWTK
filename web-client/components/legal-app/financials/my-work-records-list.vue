<template>
  <div>
    <v-toolbar color="primary" dark v-if="workRecordsList.length > 0">
      <v-toolbar-title>Lista rozliczeń</v-toolbar-title>
    </v-toolbar>
    <v-card v-for="item in workRecordsList" :key="item.id">
      <v-row class="d-flex justify-space-between">
        <v-col>
          <v-list class="d-flex justify-space-between">
            <v-list-item>
              <v-list-item-content>
                <v-list-item-title> Nazwa: {{ item.name }}</v-list-item-title>
                <v-list-item-title> Data zdarzenia: {{ formatDate(item.eventDate) }}</v-list-item-title>
                <v-list-item-subtitle> Created: {{ formatDate(item.created) }}</v-list-item-subtitle>
                <v-list-item-subtitle> Created by: {{ item.createdBy }}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </v-col>
        <v-col>
          <v-list class="d-flex justify-space-between">
            <v-list-item>
              <v-list-item-content>
                <p>Godziny: {{ item.hours }} godz.</p>
                <p>Minuty: {{ item.minutes }} min.</p>
                <p>Stawka godzinowa brutto: {{ item.rate.toLocaleString('pl') }}PLN</p>
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </v-col>
        <v-col>
          <v-list class="d-flex justify-space-between">
            <v-list-item>
              <v-list-item-content class="text--primary">
                <p>Stawka VAT: {{ item.vat }}%</p>
                <p>Kwota VAT: {{ item.invoiceVatAmount.toLocaleString('pl') }}PLN</p>
                <p>Kwota brutto: {{ item.amount.toLocaleString('pl') }}PLN</p>
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </v-col>

        <v-col>
          <v-list class="d-flex justify-md-end justify-sm-space-between">
            <v-list-item>
              <delete-work-record :selected-financial-record="item"/>
              <edit-work-record :selected-financial-record="item"/>
            </v-list-item>
          </v-list>
        </v-col>
      </v-row>
    </v-card>
  </div>
</template>

<script>
import DeleteWorkRecord from "@/components/legal-app/financials/dialogs/delete-work-record";
import EditWorkRecord from "@/components/legal-app/financials/dialogs/edit-work-record";
import {mapGetters, mapMutations} from "vuex";
import {formatDate} from "@/data/date-extensions";
import AddNewWorkRecord from "~/components/legal-app/financials/dialogs/add-new-work-record";

export default {
  name: "my-work-records-list",
  components: {AddNewWorkRecord, EditWorkRecord, DeleteWorkRecord},
  computed: {
    ...mapGetters('legal-app-client-store', ['workRecordsList']),

  },
  methods: {
    ...mapMutations('legal-app-client-store', ['updateFinancialRecordsFromFetch']),
    formatDate(date) {
      return formatDate(date);
    },
  }
}
</script>

<style scoped>

</style>
