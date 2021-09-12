<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="mb-4 white--text" color="primary">
        <add-note/>
        <v-spacer></v-spacer>
        <v-toolbar-title>Moje notatki</v-toolbar-title>

      </v-toolbar>
      <!--      <v-alert v-if="legalAppTooltips" elevation="5" text type="info">-->
      <!--        Zarządzaj notatkami dla Klienta! Dodawaj notatki ze spotkań, edytuj je lub usuwaj. Nie masz jeszcze żadnej-->
      <!--        notatki? Użyj ikonki "plus", aby dodać pierwszą notkę.-->
      <!--      </v-alert>-->
      <v-list>
        <v-list-group :value="true" prepend-icon="mdi-clipboard-text" v-for="item in clientNotesList"
                      :key="item[0].created" no-action>
          <template v-slot:activator>
            <v-list-item-title> {{ formatDateToMonth(item[0].created) }}</v-list-item-title>
          </template>
          <v-list-item v-for="object in item" :key="object.id" link>
            <v-list-item-title>{{ object.title }}</v-list-item-title>
            <v-list-item-subtitle>Dodano: {{ formatDate(object.created) }}</v-list-item-subtitle>
            <v-list-item-subtitle>Ostatnia modyfikacja: {{ formatDate(object.updated) }}</v-list-item-subtitle>
            <v-list-item-action>
              <notes-details :selected-note="object"/>
            </v-list-item-action>
          </v-list-item>
        </v-list-group>
      </v-list>
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
    try {
      let clientId = this.$route.params.client
      await this.getClientsNotes(clientId);
    } catch (error) {
      handleError(error);
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
