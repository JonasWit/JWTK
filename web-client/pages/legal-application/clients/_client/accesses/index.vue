<template>

  <layout>
    <template v-slot:content>
      <v-container>
        <v-card>
          <v-toolbar class="mb-2 white--text" color="primary">
            <v-toolbar-title>Panel dostępów do sprawy</v-toolbar-title>
            <v-spacer></v-spacer>
            <v-toolbar-title>Nazwa klienta: {{ clientDataFromFetch.name }}</v-toolbar-title>
          </v-toolbar>
          <v-alert elevation="5" text type="info">
            W panelu dostępów możesz nadać lub usunąć dostęp do Klienta. Użyj opcji "Nadaj dostęp",
            aby
            zobaczyć listę użytkowników, którzy mogą uzyskać dostęp do Klienta.
          </v-alert>
          <v-card-text class="d-flex justify-space-between mx-3 my-5">
            <grant-access v-if="clientAdmin"/>
          </v-card-text>
          <v-row class="mb-3">
            <allowed-users/>
          </v-row>
        </v-card>
      </v-container>
    </template>
  </layout>

</template>

<script>
import Layout from "@/components/legal-app/layout";
import AllowedUsers from "@/components/legal-app/clients/accesses-panel/allowed-users";
import GrantAccess from "@/components/legal-app/clients/accesses-panel/grant-access";
import {mapActions, mapGetters, mapState} from "vuex";

export default {
  name: "index",
  components: {GrantAccess, AllowedUsers, Layout},
  middleware: ['legal-app-permission', 'user', 'authenticated'],

  async fetch() {
    let clientId = this.$route.params.client
    await this.getClientData({clientId})
  },

  computed: {
    ...mapState('legal-app-client-store', ['clientDataFromFetch']),
    ...mapGetters('auth', ['clientAdmin']),

  },
  methods: {
    ...mapActions('legal-app-client-store', ['getClientData'])
  }
}
</script>

<style scoped>

</style>
