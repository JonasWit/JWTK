<template>
  <v-card-text v-if="allowedUsers.length > 0">
    <v-card-title>Lista użytkowników z dostępem do klienta</v-card-title>
    <v-card flat v-for="item in allowedUsers" :key="item.id">
      <v-row class="d-flex justify-space-between align-center mx-3">
        <v-card-subtitle>
          Nazwa użytkownika: {{ item.username }}
        </v-card-subtitle>
        <v-card-subtitle>Adres email: {{ item.email }}</v-card-subtitle>
        <revoke-access :user-for-action="item"/>
      </v-row>
    </v-card>
  </v-card-text>
</template>

<script>
import GrantAccess from "@/components/legal-app/clients/accesses-panel/grant-access";
import RevokeAccess from "@/components/legal-app/clients/accesses-panel/revoke-access";
import {mapActions, mapGetters} from "vuex";

export default {
  name: "allowed-users",
  components: {RevokeAccess, GrantAccess},


  async fetch() {
    let clientId = this.$route.params.client;
    await this.getAllowedUsers({clientId})
  },
  computed: {
    ...mapGetters('legal-app-client-store', ['allowedUsers'])
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getAllowedUsers']),

  }
}
</script>

<style scoped>

</style>
