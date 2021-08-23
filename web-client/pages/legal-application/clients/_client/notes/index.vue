<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="mb-4 white--text" color="primary">
        <v-toolbar-title>Moje notatki</v-toolbar-title>
        <v-spacer></v-spacer>
        <add-note/>
      </v-toolbar>
      <v-alert v-if="legalAppTooltips" elevation="5" text type="info">
        Zarządzaj notatkami dla Klienta! Dodawaj notatki ze spotkań, edytuj je lub usuwaj. Nie masz jeszcze żadnej
        notatki. Użyj ikonki "plus", aby dodać pierwszą notkę.
      </v-alert>
      <v-expansion-panels focusable>
        <v-expansion-panel v-for="item in clientNotesList" :key="item[0].created" class="expansion">
          <v-expansion-panel-header class="text-uppercase">
            {{ formatDateToMonth(item[0].created) }}
            <template v-slot:actions>
              <v-icon color="primary">
                $expand
              </v-icon>
            </template>
          </v-expansion-panel-header>
          <v-expansion-panel-content>
            <v-card v-for="object in item" :key="object.id" class="my-4">
              <v-row class="d-flex align-center">
                <v-col cols="4">
                  <v-card-subtitle>
                    <div class="font-weight-bold">Dodano: {{ formatDate(object.created) }}</div>
                    <div> Ostatnia modyfikacja: {{ formatDate(object.updated) }}</div>
                    <div>Użytkownik: {{ object.updatedBy }}</div>
                  </v-card-subtitle>
                </v-col>
                <v-col cols="5">
                  <v-card-text>
                    <div class="font-weight-bold">Tytuł:</div>
                    <div>{{ object.title }}</div>
                  </v-card-text>
                </v-col>
                <v-col cols="3">
                  <notes-details :selected-note="object"/>
                </v-col>
              </v-row>
            </v-card>
          </v-expansion-panel-content>
        </v-expansion-panel>
      </v-expansion-panels>
      <progress-bar v-if="loader"/>
    </template>
  </layout>
</template>

<script>

import Layout from "@/components/legal-app/layout";
import {formatDate, formatDateToMonth} from "@/data/date-extensions";
import NotesDetails from "@/components/legal-app/clients/notes/notes-details";
import AddNote from "@/components/legal-app/clients/notes/add-note";
import {mapActions, mapState} from "vuex";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "index",
  components: {ProgressBar, AddNote, NotesDetails, Layout},
  middleware: ['legal-app-permission', 'user', 'authenticated'],
  data: () => ({
    loader: true
  }),

  async fetch() {
    this.loader = true
    try {
      let clientId = this.$route.params.client
      await this.getClientsNotes(clientId);
    } catch (error) {
      handleError(error);
    } finally {
      this.loader = false
    }
  },
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
    ...mapState('legal-app-client-store', ['clientNotesList'])
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getClientsNotes']),
    formatDateToMonth(date) {
      return formatDateToMonth(date);
    },
    formatDate(date) {
      return formatDate(date);
    },
  }
};
</script>

<style scoped>

</style>
