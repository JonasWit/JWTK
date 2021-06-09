<template>
  <div>
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
                <v-list-item-subtitle>Hours: {{ item.hours }}</v-list-item-subtitle>
                <v-list-item-subtitle>Minutes: {{ item.minutes }}</v-list-item-subtitle>
                <v-list-item-subtitle>Rate: {{ item.rate }}</v-list-item-subtitle>
              </v-list-item-content>
            </v-list-item>
          </v-list>
        </v-col>
        <v-col>
          <v-list class="d-flex justify-space-between">
            <v-list-item>
              <v-list-item-content>
                <v-list-item-subtitle>Rate: {{ item.rate }}</v-list-item-subtitle>
                <v-list-item-subtitle>VAT: {{ item.vat }}</v-list-item-subtitle>
                <v-list-item-subtitle>Amount: {{ item.amount }}</v-list-item-subtitle>
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

export default {
  name: "my-work-records-list",
  components: {EditWorkRecord, DeleteWorkRecord},


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
