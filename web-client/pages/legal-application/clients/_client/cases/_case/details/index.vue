<template>
  <layout>
    <template v-slot:content>
      <v-row justify="center">
        <v-card v-if="clientCaseDetails">
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
            <v-alert elevation="5" text type="info" dismissible close-text="Zamknij">
              Jeśli chcesz zmodyfikować opis sprawy, użyj guzika "EDYTUJ". W nowym okienku będziesz mógł dokonać
              zmian.
              Jeśli już nie potrzebujesz tej sprawy, użyj guzika "USUŃ" lub "ARCHIWIZUJ" i potwierdź operację.
            </v-alert>
            <v-subheader class="mx-2"><span
              class="font-weight-bold">Utworzono: </span> {{ formatDateWithHours(clientCaseDetails.created) }}
            </v-subheader>
            <v-subheader class="mx-2"><span class="font-weight-bold">Utworzone przez: </span>
              {{ clientCaseDetails.createdBy }}
            </v-subheader>
            <v-row class="mx-2 my-2 d-flex align-center">
              <v-card-title>Opis sprawy</v-card-title>
              <v-card-title> Sygnatura: {{ clientCaseDetails.signature }}</v-card-title>
              <edit-case-dialog :case-for-action="clientCaseDetails" v-on:action-completed="editDone"/>
              <delete-case :case-for-action="clientCaseDetails" v-on:delete-completed="deleteDone"/>
              <archive-case :case-for-action="clientCaseDetails" v-on:delete-completed="deleteDone"/>
            </v-row>
            <v-card-text>
              {{ clientCaseDetails.description }}
            </v-card-text>
            <v-row class="mx-2 mb-4 d-flex align-center">
              <v-card-subtitle><span class="font-weight-bold">Edytowano:</span>
                {{ formatDateWithHours(clientCaseDetails.updated) }}
              </v-card-subtitle>
              <v-card-subtitle><span class="font-weight-bold">Edytowane przez:</span> {{
                  clientCaseDetails.updatedBy
                }}
              </v-card-subtitle>
            </v-row>
          </v-card>
          <v-divider></v-divider>
        </v-card>

      </v-row>
    </template>
  </layout>
</template>

<script>
import Layout from "@/components/legal-app/layout";
import CaseDetails from "@/components/legal-app/clients/cases/case-details";
import ArchiveCase from "@/components/legal-app/clients/cases/dialogs/archive-case";
import DeleteCase from "@/components/legal-app/clients/cases/dialogs/delete-case";
import EditCaseDialog from "@/components/legal-app/clients/cases/dialogs/edit-case-dialog";
import {mapActions, mapGetters, mapState} from "vuex";
import {formatDateWithHours} from "@/data/date-extensions";

export default {
  name: "index",
  components: {ArchiveCase, DeleteCase, EditCaseDialog, Layout, CaseDetails},
  data: () => ({
    // dialog: false,
    caseForAction: null,
    tab: null,
  }),

  async fetch() {
    let caseId = this.$route.params.case
    await this.getCaseDetails({caseId})
  },

  computed: {
    ...mapState('legal-app-client-store', ['clientCaseDetails']),
    ...mapGetters('auth', ['clientAdmin']),
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getCaseDetails']),
    ...mapActions('legal-app-client-store', ['getListOfGroupedCases']),

    formatDateWithHours(date) {
      return formatDateWithHours(date)
    },
    async editDone() {
      let clientId = this.$route.params.client;
      await this.getListOfGroupedCases({clientId});
    },
    async deleteDone() {
      try {
        let clientId = this.$route.params.client;
        await this.getListOfGroupedCases({clientId});
      } catch (error) {
      } finally {
        await this.$router.push(`/legal-application/clients/${this.$route.params.client}/cases/`)
      }
    },
  }
}
</script>

<style scoped>

</style>
