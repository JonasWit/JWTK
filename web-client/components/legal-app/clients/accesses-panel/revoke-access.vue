<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn small color="error" v-on="{ ...tooltip, ...dialog }">
            usuń dostęp
          </v-btn>
        </template>
        <span> Usuń dostęp</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń dostęp do Klienta</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz dostęp do Klienta dla wybranego użytkownika. Odzyskanie dostępu
        jest możliwe po ponownyn nadaniu dostępu.
        Zatwierdź operację używjąc guzika 'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="revokeAccess()">
          Potwierdź
        </v-btn>
        <v-spacer/>
        <v-btn color="success" text @click="dialog = false">
          Anuluj
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>

</template>

<script>
import {revokeAccess} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {mapActions} from "vuex";

export default {
  name: "revoke-access",
  props: {
    userForAction: {
      required: true
    },

  },
  data: () => ({
    dialog: false
  }),
  methods: {
    ...mapActions('legal-app-client-store', ['getAllowedUsers']),
    ...mapActions('legal-app-client-store', ['getEligibleUsersList']),
    async revokeAccess() {
      const payload = {
        userId: this.userForAction.id
      }
      try {
        let clientId = this.$route.params.client
        console.warn('user id:', payload)
        await this.$axios.$post(revokeAccess(clientId), payload)
        this.$notifier.showSuccessMessage("Dostęp usunięty pomyślnie");
      } catch (error) {
        console.error(error)
        this.$notifier.showErrorMessage(error);
      } finally {
        let clientId = this.$route.params.client;
        await this.getAllowedUsers({clientId})
        await this.getEligibleUsersList({clientId})
      }


    }
  }

}
</script>

<style scoped>

</style>
