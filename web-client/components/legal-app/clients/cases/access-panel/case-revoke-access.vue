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
import {revokeAccess} from "@/data/endpoints/legal-app/legal-app-case-endpoints";
import {mapActions} from "vuex";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "case-revoke-access",
  components: {ProgressBar},
  props: {
    userForAction: {
      required: true
    },
  },
  data: () => ({
    dialog: false,
  }),
  methods: {
    ...mapActions('legal-app-client-store', ['getAllowedUsersForCase']),
    ...mapActions('legal-app-client-store', ['getEligibleUsersForCase']),
    async revokeAccess() {
      this.loader = true
      const payload = {
        userId: this.userForAction.id
      }
      try {
        let caseId = this.$route.params.case
        await this.$axios.$post(revokeAccess(caseId), payload)
        this.$notifier.showSuccessMessage("Dostęp usunięty pomyślnie");
      } catch (error) {
        handleError(error);
      } finally {
        setTimeout(() => {
          let caseId = this.$route.params.case
          this.getAllowedUsersForCase({caseId})
          this.getEligibleUsersForCase({caseId})
          this.loader = false
        }, 1500)
      }
    }
  }

}
</script>

<style scoped>

</style>
