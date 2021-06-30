<template>
  <v-container>
    <v-toolbar color="primary" class="white--text">
      <v-toolbar-title>Archiwum Spraw</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-icon color="white">mdi-briefcase-account-outline</v-icon>
    </v-toolbar>
    <v-card>
      <v-list>
        <v-list-item v-for="item in archivedCasesList" :key="item.id">
          <v-list-item-content>
            <v-list-item-title>
              Nazwa: {{ item.name }}
            </v-list-item-title>
            <v-list-item-subtitle>Zarchiwizowano: {{ formatDate(item.created) }}</v-list-item-subtitle>
            <v-list-item-subtitle>Dodane przez: {{ item.createdBy }}</v-list-item-subtitle>
          </v-list-item-content>
        </v-list-item>
      </v-list>
    </v-card>
  </v-container>
</template>
<script>
import Layout from "@/components/legal-app/layout";
import {getArchivedCases} from "@/data/endpoints/legal-app/legal-app-case-endpoints";
import {formatDate} from "@/data/date-extensions";

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
    }

  }
}
</script>

<style scoped>

</style>
