<template>
  <layout>
    <template v-slot:content>
      <v-container>
        <v-card>
          <v-toolbar class="mb-2 white--text" color="primary">
            <v-toolbar-title>Panel dostępów do sprawy</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-title>Tytuł sprawy: {{ clientCaseDetails.name }}</v-toolbar-title>
          </v-toolbar>
          <v-alert elevation="5" text type="info">
            W panelu dostępów możesz nadać lub usunąć dostęp do wybranej sprawy. Użyj opcji "Nadaj dostęp",
            aby zobaczyć listę użytkowników, którzy mogą uzyskać dostęp do Sprawy.
          </v-alert>
          <v-card-text class="d-flex justify-space-between mx-3">
            <case-grant-access v-if="clientAdmin"/>
          </v-card-text>
          <v-card-text v-if="allowedUsersForCase.length > 0">
            <v-card-title>Lista użytkowników z dostępem do sprawy</v-card-title>
            <v-card flat v-for="item in allowedUsersForCase" :key="item.id">
              <v-row class="d-flex justify-space-between align-center mx-3">
                <v-card-subtitle>
                  Nazwa użytkownika: {{ item.username }}
                </v-card-subtitle>
                <v-card-subtitle>Adres email: {{ item.email }}</v-card-subtitle>
                <case-revoke-access :user-for-action="item"/>
              </v-row>
            </v-card>
          </v-card-text>
        </v-card>
      </v-container>
    </template>
  </layout>
</template>
<script>
import {mapActions, mapGetters, mapState} from "vuex";
import CaseGrantAccess from "@/components/legal-app/clients/cases/access-panel/case-grant-access";
import CaseRevokeAccess from "@/components/legal-app/clients/cases/access-panel/case-revoke-access";
import Layout from "@/components/legal-app/layout";

export default {
  name: "index",
  components: {Layout, CaseRevokeAccess, CaseGrantAccess},
  middleware: ['legal-app-permission', 'user-admin', 'authenticated'],

  async fetch() {
    let caseId = this.$route.params.case
    await this.getAllowedUsersForCase({caseId})
    await this.getCaseDetails({caseId})
    console.warn('case details', this.clientCaseDetails)
  },
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
    ...mapState('legal-app-client-store', ['allowedUsersForCase', 'clientCaseDetails']),
    ...mapGetters('auth', ['clientAdmin']),
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getAllowedUsersForCase', 'getCaseDetails']),

  }

}
</script>

<style scoped>

</style>
