<template>
  <v-btn x-small color="error" @click="revokeAccess()">
    revoke access
  </v-btn>
</template>

<script>
import {revokeAccess} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "revoke-access",
  props: {
    userForAction: {
      required: true
    },
    clientItemForAction: {
      type: Object,
      required: true
    }
  },
  methods: {
    async revokeAccess() {
      try {
        let clientId = this.clientItemForAction.id
        let userId = this.userForAction.id
        console.warn('user id:', userId)
        await this.$axios.$post(revokeAccess(clientId, userId))
        this.$nuxt.refresh()
        this.$notifier.showSuccessMessage("Dostęp usunięty pomyślnie");
      } catch (error) {
        console.error(error)
        this.$notifier.showErrorMessage(error);
      }


    }
  }

}
</script>

<style scoped>

</style>
