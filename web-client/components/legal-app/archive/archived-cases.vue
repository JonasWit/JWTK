<template>
  <v-container>
    <v-toolbar color="primary" class="white--text">
      <v-toolbar-title>Archiwum Spraw</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-icon color="white">mdi-briefcase-account-outline</v-icon>
    </v-toolbar>
    <v-alert v-if="" elevation="5" text type="info" dismissible close-text="Zamknij">
      Aby odzyskać dostęp do zarchiwizowanej Sprawy użyj opcji 'PRZYWRÓĆ'.
    </v-alert>
    <v-card tile v-for="item in archivedCasesList" :key="item.id">
      <v-list>
        <v-list-item class="d-flex justify-space-between">
          <v-list-item-content>
            <v-list-item-title>
              Nazwa: {{ item.name }}
            </v-list-item-title>
            <v-list-item-subtitle>Zarchiwizowano: {{ formatDate(item.created) }}</v-list-item-subtitle>
            <v-list-item-subtitle>Dodane przez: {{ item.createdBy }}</v-list-item-subtitle>
          </v-list-item-content>
          <v-btn class="ma-6" color="error" @click="removeFromArchive(item.id)">Przywróć</v-btn>
        </v-list-item>
      </v-list>
    </v-card>
  </v-container>
</template>
<script>
import Layout from "@/components/legal-app/layout";
import {archiveCase, getArchivedCases} from "@/data/endpoints/legal-app/legal-app-case-endpoints";
import {formatDate} from "@/data/date-extensions";
import {handleError} from "@/data/functions";

export default {
  name: "archived-cases",
  components: {Layout},
  data: () => ({
    archivedCasesList: []

  }),
  async fetch() {
    await this.getArchivedCasesList()
    console.warn('archived cases', this.archivedCasesList)
  },

  methods: {
    async getArchivedCasesList() {
      try {
        let clientId = this.$route.params.client;
        this.archivedCasesList = await this.$axios.$get(getArchivedCases(clientId))
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      }
    },
    formatDate(date) {
      return formatDate(date)
    },
    async removeFromArchive(caseId) {
      try {
        await this.$axios.$put(archiveCase(caseId))
        console.warn(caseId)
        this.$notifier.showSuccessMessage("Sprawa został pomyślnie usunięta z archiwum!");
      } catch (error) {
        handleError(error)
      } finally {
        await this.getArchivedCasesList()
      }
    }

  }
}
</script>

<style scoped>

</style>
