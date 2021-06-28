<template>
  <v-card-text>
    <v-card-title>UŻYTKOWNICY ELIGIBLE DO UZYSKANIA DOSTĘPU</v-card-title>
    <v-card flat v-for="item in eligibleUsers" :key="item.id">
      <v-row class="d-flex justify-space-between align-center">
        <v-card-subtitle>
          Nazwa użytkownika: {{ item.username }}
        </v-card-subtitle>
        <v-card-subtitle>Adres email: {{ item.email }}</v-card-subtitle>
        <v-card-subtitle>Rola: {{ item.role }}</v-card-subtitle>
        <grant-access/>
        <revoke-access/>
      </v-row>
    </v-card>
  </v-card-text>
</template>

<script>
import {getClientAllowedUsers, getClientEligibleUsers} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import GrantAccess from "@/components/legal-app/clients/accesses-panel/grant-access";
import RevokeAccess from "@/components/legal-app/clients/accesses-panel/revoke-access";

export default {
  name: "eligible-users",
  components: {RevokeAccess, GrantAccess},
  props: {
    clientItemForAction: {
      type: Object,
      required: true
    }
  },
  data: () => ({
    eligibleUsers: [],
    allowedUsers: [],

  }),

  async fetch() {
    await this.getEligibleUsers()

  },

  methods: {
    async getEligibleUsers() {
      try {
        let clientId = this.clientItemForAction.id;
        console.warn('clientItemForAction:', this.clientItemForAction)
        this.eligibleUsers = await this.$axios.$get(getClientEligibleUsers(clientId))
        console.warn('eligible users list:', this.eligibleUsers)
      } catch (e) {
        console.warn('error:', e)
      }
    },

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
