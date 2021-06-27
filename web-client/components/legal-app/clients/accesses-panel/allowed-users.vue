<template>
  <v-card-text>
    <v-card-title>UŻYTKOWNICY ALLOWED DOSTĘPU</v-card-title>
    <v-card flat v-for="item in allowedUsers" :key="item.id">
      <v-row class="d-flex justify-space-between align-center">
        <v-card-subtitle>
          Nazwa użytkownika: {{ item.username }}
        </v-card-subtitle>
        <v-card-subtitle>Adres email: {{ item.email }}</v-card-subtitle>
        <v-card-subtitle>Rola: {{ item.role }}</v-card-subtitle>
        <revoke-access :user-for-action="item" :client-item-for-action="clientItemForAction"/>
      </v-row>
    </v-card>
  </v-card-text>
</template>

<script>
import {getClientAllowedUsers} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import GrantAccess from "@/components/legal-app/clients/accesses-panel/grant-access";
import RevokeAccess from "@/components/legal-app/clients/accesses-panel/revoke-access";

export default {
  name: "allowed-users",
  components: {RevokeAccess, GrantAccess},
  props: {
    clientItemForAction: {
      type: Object,
      required: true
    }
  },
  data: () => ({
    allowedUsers: [],

  }),

  async fetch() {
    await this.getAllowedUsers()

  },

  methods: {
    async getAllowedUsers() {
      try {
        let clientId = this.clientItemForAction.id;
        console.warn('clientItemForAction:', this.clientItemForAction)
        this.allowedUsers = await this.$axios.$get(getClientAllowedUsers(clientId))
        console.warn('allowed users list:', this.allowedUsers)
      } catch (e) {
        console.warn('error:', e)
      }
    },


  }
}
</script>

<style scoped>

</style>
