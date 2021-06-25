<template>
  <layout>
    <template v-slot:content>
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

    </template>
  </layout>
</template>

<script>
import Layout from "@/components/legal-app/layout";
import {getArchivedCases} from "@/data/endpoints/legal-app/legal-app-case-endpoints";

export default {
  name: "archived-cases",
  components: {Layout},
  data: () => ({
    archivedCasesList: []

  }),
  fetch() {
    return this.getArchivedCasesList()
  },

  methods: {
    async getArchivedCasesList() {
      try {
        let clientId = this.$route.params.client;
        let archivedCases = await this.$axios.$get(getArchivedCases(clientId))
        this.archivedCasesList = archivedCases
        console.warn('archived cases', archivedCases)
      } catch (e) {

      }

    }

  }
}
</script>

<style scoped>

</style>
