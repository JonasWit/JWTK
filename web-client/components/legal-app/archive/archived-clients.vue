<template>
  <div>
    <v-toolbar color="primary" class="white--text">
      <v-toolbar-title>Archiwum Klientów</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-icon color="white">mdi-account-tie</v-icon>
      <v-icon color="white">mdi-archive</v-icon>
    </v-toolbar>
    <v-card>
      <v-list>
        <v-list-item v-for="item in archivedClientsList" :key="item.id">
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
  </div>
</template>

<script>
import Layout from "@/components/legal-app/layout";
import {getArchivedClient} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {formatDate} from "@/data/date-extensions";

export default {
  name: "archived-clients",
  components: {Layout},
  data: () => ({
    archivedClientsList: [],
  }),

  fetch() {
    return this.getArchivedClientsFromFetch()
  },
  methods: {
    formatDate(date) {
      return formatDate(date)
    },
    async getArchivedClientsFromFetch() {
      try {
        let archivedClients = await this.$axios.$get(getArchivedClient())
        this.archivedClientsList = archivedClients
        console.warn('archived clients', archivedClients)

      } catch (error) {
        console.error('creating contact error', error)
        this.$notifier.showErrorMessage(error.response.data);
      }
    }
  }
}
</script>

<style scoped>

</style>
