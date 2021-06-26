<template>
  <v-card-text>

  </v-card-text>
</template>

<script>

import {getClientAllowedUsers} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "allowed-users",
  props: {
    clientItemForAction: {
      type: Object,
      required: true
    }
  },

  // async fetch() {
  //   await this.getAllowedUsers()
  //
  // },

  methods: {
    async getAllowedUsers() {
      try {
        let clientId = this.clientItemForAction.id;
        console.warn('clientItemForAction:', this.clientItemForAction)
        let allowedUsers = await this.$axios.$get(getClientAllowedUsers(clientId))
        console.warn('allowed users list:', allowedUsers)
      } catch (e) {
        console.warn('error:', e)
      }
    }
  }
}
</script>

<style scoped>

</style>
