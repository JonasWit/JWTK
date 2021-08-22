<template>
  <div>
    <v-toolbar color="primary" class="white--text">
      <v-toolbar-title>Archiwum Klientów</v-toolbar-title>
      <v-spacer></v-spacer>
      <v-icon color="white">mdi-account-tie</v-icon>
      <v-icon color="white">mdi-archive</v-icon>
    </v-toolbar>
    <v-alert v-if="legalAppTooltips" elevation="5" text type="info" dismissible close-text="Zamknij">
      Aby odzyskać dostęp do zarchiwizowanego Klienta i jego danych oraz spraw użyj opcji 'PRZYWRÓĆ'.
    </v-alert>
    <v-card tile v-for="item in archivedClientsList" :key="item.id">
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
    <progress-bar v-if="loader"/>
  </div>
</template>

<script>
import Layout from "@/components/legal-app/layout";
import {archiveClient, getArchivedClient} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {formatDate} from "@/data/date-extensions";
import {handleError} from "@/data/functions";
import {mapState} from "vuex";
import ProgressBar from "@/components/legal-app/progress-bar";

export default {
  name: "archived-clients",
  components: {ProgressBar, Layout},
  data: () => ({
    archivedClientsList: [],
    loader: false

  }),

  async fetch() {
    this.loader = true
    try {
      await this.getArchivedClientsFromFetch()
    } catch (error) {
      handleError(error);
    } finally {
      this.loader = false
    }
  },
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips'])
  },
  methods: {
    formatDate(date) {
      return formatDate(date)
    },
    async getArchivedClientsFromFetch() {
      this.loader = true
      try {
        this.archivedClientsList = await this.$axios.$get(getArchivedClient())
      } catch (error) {
        handleError(error);
      } finally {
        this.loader = false
      }
    },
    async removeFromArchive(clientId) {
      this.loader = true
      try {
        await this.$axios.$put(archiveClient(clientId))
        console.warn(clientId)
        this.$notifier.showSuccessMessage("Klient został pomyślnie usuniety z archiwum!");
      } catch (error) {
        handleError(error)
      } finally {
        await this.getArchivedClientsFromFetch()
        this.loader = false
      }
    }
  },
}
</script>

<style scoped>

</style>
