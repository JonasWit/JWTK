<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="white--text" color="primary">
        <v-toolbar-title>Terminy procesowe</v-toolbar-title>
        <v-spacer></v-spacer>

        <add-deadline/>
      </v-toolbar>
      <v-alert v-if="deadlines.length === 0" elevation="5" text type="info" dismissible close-text="Zamknij">
        Zarządzaj notatkami dla Sprawy! Dodawaj notatki ze spotkań, edytuj je lub usuwaj. Nie masz jeszcze żadnej
        notatki. Użyj ikonki "plus", aby dodać pierwszą notkę.
      </v-alert>
      <v-card>
        <v-tabs v-model="tab" centered dark icons-and-text background-color="primary">
          <v-tabs-slider></v-tabs-slider>
          <v-tab href="#tab-1">
            Lista terminów
            <v-icon>mdi-format-list-checkbox</v-icon>
          </v-tab>
          <v-tab href="#tab-2">
            Kalendarz terminów
            <v-icon>mdi-calendar-month</v-icon>
          </v-tab>
          <v-tabs-items v-model="tab">
            <v-tab-item :value="'tab-1'">
              <v-card v-for="item in deadlines" :key="item.id">
                <v-row class="d-flex justify-space-between align-center">
                  <v-col>
                    <v-card-title>
                      TERMIN: {{ formatDate(item.deadline) }}
                    </v-card-title>
                  </v-col>
                  <v-col>
                    <v-card-text>Opis: {{ item.message }}</v-card-text>
                  </v-col>
                  <v-col>
                    <v-card-text class="my-4">
                      <div class="font-weight-bold">Dodano: {{ formatDate(item.created) }}</div>
                      <div> Użytkownik: {{ item.createdBy }}</div>
                    </v-card-text>
                  </v-col>
                  <v-col>
                    <delete-deadline :deadline-for-action="item" v-on:delete-completed="deleteDone"/>
                  </v-col>
                </v-row>
              </v-card>
            </v-tab-item>
            <v-tab-item :value="'tab-2'">
              calendar
            </v-tab-item>
          </v-tabs-items>

        </v-tabs>
      </v-card>


    </template>
  </layout>
</template>
<script>
import {addDays, formatDate} from "@/data/date-extensions";
import Layout from "@/components/legal-app/layout";
import AddDeadline from "@/components/legal-app/clients/cases/deadlines/add-deadline";
import {mapActions, mapState} from "vuex";
import DeleteDeadline from "@/components/legal-app/clients/cases/deadlines/delete-deadline";

export default {
  name: "index",
  components: {DeleteDeadline, AddDeadline, Layout},
  data: () => ({
    tab: null,
  }),
  async fetch() {
    let caseId = this.$route.params.case
    let query = this.query
    console.warn('dates', this.query)
    await this.getCaseDeadlines({caseId, query})
    await this.getCaseDetails({caseId})
  },
  computed: {
    ...mapState('legal-app-client-store', ['clientCaseDetails', 'deadlines']),

    todayDate() {
      return (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)

    },
    query() {
      let fromDate = (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10)
      let toDate = (addDays(this.todayDate, 30)).toISOString().substr(0, 10)
      return `?from=${fromDate}&to=${toDate}`;
    },
  },

  methods: {
    ...mapActions('legal-app-client-store', ['getCaseDetails', 'getCaseDeadlines']),
    formatDate(date) {
      return formatDate(date)
    },
    async deleteDone() {
      let caseId = this.$route.params.case
      let query = this.query
      await this.getCaseDeadlines({caseId, query})

    }
  }
}
</script>

<style scoped>

</style>
