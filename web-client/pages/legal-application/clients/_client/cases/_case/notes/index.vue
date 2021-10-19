<template>
  <layout>
    <template v-slot:content>
      <v-toolbar class="mb-4 white--text" color="primary">
        <v-toolbar-title>Notes sprawy</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-toolbar-title>Tytuł sprawy: {{ clientCaseDetails.name }}</v-toolbar-title>
        <v-spacer></v-spacer>
        <case-add-note/>
      </v-toolbar>
      <v-alert v-if="legalAppTooltips" elevation="5" text type="info">
        Zarządzaj notatkami dla Sprawy! Dodawaj notatki ze spotkań, edytuj je lub usuwaj. Nie masz jeszcze żadnej
        notatki. Użyj ikonki "plus", aby dodać pierwszą notkę.
      </v-alert>
      <v-list>
        <v-list-group :value="false" prepend-icon="mdi-clipboard-text" v-for="item in notesListForCases"
                      :key="item[0].created" no-action class="expansion">
          <template v-slot:activator>
            <v-list-item-title> {{ formatDateToMonth(item[0].created) }}</v-list-item-title>
          </template>
          <v-list-item v-for="object in item" :key="object.id" link>
            <v-list-item-title>{{ object.title }}</v-list-item-title>
            <v-list-item-subtitle>Dodano: {{ formatDate(object.created) }}</v-list-item-subtitle>
            <v-list-item-subtitle>Ostatnia modyfikacja: {{ formatDate(object.updated) }}</v-list-item-subtitle>
            <v-list-item-action>
              <case-notes-details :selected-note="object"/>
            </v-list-item-action>
            <v-list-item-action>
              <case-delete-note-dialog :note-for-action="object"/>
            </v-list-item-action>
          </v-list-item>
        </v-list-group>
      </v-list>
    </template>
  </layout>
</template>
<script>
import Layout from "@/components/legal-app/layout";
import {mapActions, mapState} from "vuex";
import {formatDate, formatDateToMonth} from "@/data/date-extensions";
import CaseAddNote from "@/components/legal-app/clients/cases/notes/case-add-note";
import CaseNotesDetails from "@/components/legal-app/clients/cases/notes/case-notes-details";
import {handleError} from "@/data/functions";
import CaseDeleteNoteDialog from "@/components/legal-app/clients/cases/notes/case-delete-note-dialog";

export default {
  name: "index",
  components: {CaseDeleteNoteDialog, CaseNotesDetails, CaseAddNote, Layout},
  middleware: ['legal-app-permission', 'user', 'authenticated'],
  data: () => ({}),

  async fetch() {
    try {
      let caseId = this.$route.params.case
      await this.getNotesListForCases({caseId});
      await this.getCaseDetails({caseId})
    } catch (error) {
      handleError(error);
    }
  },
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
    ...mapState('legal-app-client-store', ['notesListForCases', 'clientCaseDetails'])
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getNotesListForCases', 'getCaseDetails']),
    formatDateToMonth(date) {
      return formatDateToMonth(date);
    },
    formatDate(date) {
      return formatDate(date);
    },
  }
}
</script>

<style scoped>

</style>
