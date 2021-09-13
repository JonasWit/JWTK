<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="white--text" color="primary">
        <v-toolbar-title>Terminy procesowe</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-toolbar-title>Tytuł sprawy: {{ clientCaseDetails.name }}</v-toolbar-title>
        <v-spacer></v-spacer>
        <add-deadline/>
      </v-toolbar>
      <v-alert v-if="legalAppTooltips" elevation="5" text type="info" dismissible close-text="Zamknij">
        Zarządzaj terminami dla Sprawy! Dodawaj terminy procesowe i inne. Skorzystaj z widoku listy lub kalendarza, aby
        sprawdzić nadchodzące terminy. Nie masz jeszcze żadnego terminu? Użyj ikonki "plus", aby dodać pierwszy termin.
      </v-alert>
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
                  <delete-deadline :deadline-for-action="item"/>
                </v-col>
              </v-row>
            </v-card>
          </v-tab-item>
          <v-tab-item :value="'tab-2'">
            <deadline-planner-view v-on:deadlines-needed="actionDone"/>
          </v-tab-item>
        </v-tabs-items>
      </v-tabs>
    </template>
  </layout>
</template>
<script>
import {formatDate, queryDateExtended, queryDateForFloatingBell, todayDate} from "@/data/date-extensions";
import Layout from "@/components/legal-app/layout";
import AddDeadline from "@/components/legal-app/clients/cases/deadlines/add-deadline";
import {mapActions, mapState} from "vuex";
import DeleteDeadline from "@/components/legal-app/clients/cases/deadlines/delete-deadline";
import DeadlinePlannerView from "@/components/legal-app/clients/cases/deadlines/deadline-planner-view";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "index",
  components: {ProgressBar, DeadlinePlannerView, DeleteDeadline, AddDeadline, Layout},
  middleware: ['legal-app-permission', 'user', 'authenticated'],
  data: () => ({
    tab: null,
  }),
  async fetch() {
    try {
      let caseId = this.$route.params.case;
      let query = this.query;
      await this.getCaseDeadlines({caseId, query});
      await this.getCaseDetails({caseId});
    } catch (error) {
      handleError(error);
    }
  },
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
    ...mapState('legal-app-client-store', ['clientCaseDetails', 'deadlines']),
    ...mapState('legal-app-client-store', ['notesListForCases', 'clientCaseDetails']),
    todayDate() {
      return todayDate();
    },
    query() {
      return queryDateExtended(this.todayDate, 3600);
    }
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getCaseDetails', 'getCaseDeadlines', 'getEventsForNotifications']),

    formatDate(date) {
      return formatDate(date);
    },
    async actionDone() {
      try {
        let caseId = this.$route.params.case;
        let query = this.query;
        let dates = queryDateForFloatingBell(todayDate())
        await this.getCaseDeadlines({caseId, query});
        await this.getEventsForNotifications({dates});
      } catch (error) {
        handleError(error);
      }
    }
  }
};
</script>

<style scoped>

</style>
