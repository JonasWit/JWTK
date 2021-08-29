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
      <v-card-title class="justify-center my-1">Usuń dostęp do aplikacji</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz dostęp do aplikacji dla wybranego powiązanego użytkownika.
        Odzyskanie dostępu
        jest możliwe po ponownyn nadaniu dostępu. Zatwierdź operację używjąc guzika 'POTWIERDŹ'
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
    <progress-bar v-if="loader"/>
  </v-dialog>

</template>

<script>
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "revoke-access-related-users",
  components: {ProgressBar},
  props: {
    userForAction: {
      required: true
    },

  },
  data: () => ({
    dialog: false,
    loader: false
  }),
  methods: {
    async revokeAccess() {
      this.loader = true;
      const payload = {
        userId: this.userForAction.id
      };
      console.log('user for action', payload);
      try {
        await this.$axios.$post('/api/portal-admin/key-admin/legal-app/access-key/revoke', payload);
        this.$notifier.showSuccessMessage("Dostęp usunięty pomyślnie");
      } catch (error) {
        handleError(error);
      } finally {
        this.loader = false;
      }
    }
  }

};
</script>

<style scoped>

</style>
