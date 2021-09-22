<template>
  <layout>
    <template v-slot:content>
      <v-row justify="center">
        <v-card v-if="clientCaseDetails" width="100vw" class="mt-6">
          <v-toolbar dark color="primary light">
            <v-toolbar-title>
              Tytuł sprawy: {{ clientCaseDetails.name }}
            </v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-title>
              Kategoria: {{ clientCaseDetails.group }}
            </v-toolbar-title>
          </v-toolbar>
          <v-card flat>
            <v-alert elevation="5" text type="info" v-if="legalAppTooltips">
              Jeśli chcesz zmodyfikować opis sprawy, użyj guzika "EDYTUJ". W nowym okienku będziesz mógł dokonać
              zmian.
              Jeśli już nie potrzebujesz tej sprawy, użyj guzika "USUŃ" lub "ARCHIWIZUJ" i potwierdź operację.
            </v-alert>
            <v-row class="mx-2 my-2 d-flex align-center">
              <v-card-title> Sygnatura: {{ clientCaseDetails.signature }}</v-card-title>
              <v-subheader class="mx-2"><span
                class="font-weight-bold">Utworzono: </span> {{ formatDateWithHours(clientCaseDetails.created) }}
              </v-subheader>
              <v-subheader class="mx-2"><span class="font-weight-bold">Utworzone przez: </span>
                {{ clientCaseDetails.createdBy }}
              </v-subheader>
            </v-row>
            <v-divider></v-divider>
            <v-row class="mx-2 my-2 d-flex align-center">
              <v-card-title>Opis sprawy</v-card-title>
              <edit-case-dialog :case-for-action="clientCaseDetails" v-on:action-completed="editDone"/>
            </v-row>
            <v-card-text>
              <p class=" mx-2 text--primary"> {{ clientCaseDetails.description }}</p>
            </v-card-text>
            <v-divider></v-divider>
            <v-row class="mx-2 mb-4 my-4 d-flex align-center">
              <v-card-subtitle><span class="font-weight-bold">Edytowano:</span>
                {{ formatDateWithHours(clientCaseDetails.updated) }}
              </v-card-subtitle>
              <v-card-subtitle><span class="font-weight-bold">Edytowane przez:</span> {{
                  clientCaseDetails.updatedBy
                }}
              </v-card-subtitle>
            </v-row>
          </v-card>
        </v-card>
      </v-row>
    </template>
  </layout>
</template>

<script>
import Layout from "@/components/legal-app/layout";
import ArchiveCase from "@/components/legal-app/clients/cases/dialogs/archive-case";
import DeleteCase from "@/components/legal-app/clients/cases/dialogs/delete-case";
import EditCaseDialog from "@/components/legal-app/clients/cases/dialogs/edit-case-dialog";
import {mapActions, mapState} from "vuex";
import {formatDateWithHours} from "@/data/date-extensions";
import {handleError} from "@/data/functions";

export default {
  name: "index",
  components: {ArchiveCase, DeleteCase, EditCaseDialog, Layout,},
  middleware: ['legal-app-permission', 'user', 'authenticated'],
  data: () => ({
    caseForAction: null,
    tab: null,
  }),

  async fetch() {
    try {
      let caseId = this.$route.params.case
      await this.getCaseDetails({caseId})
    } catch (error) {
      handleError(error);
    }

  },

  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
    ...mapState('legal-app-client-store', ['clientCaseDetails']),

  },
  methods: {
    ...mapActions('legal-app-client-store', ['getCaseDetails']),
    ...mapActions('legal-app-client-store', ['getListOfGroupedCases']),

    formatDateWithHours(date) {
      return formatDateWithHours(date)
    },
    async editDone() {
      try {
        let clientId = this.$route.params.client;
        await this.getListOfGroupedCases({clientId});
      } catch (error) {
        handleError(error);
      }

    },
  }
}
</script>

<style scoped>

</style>
