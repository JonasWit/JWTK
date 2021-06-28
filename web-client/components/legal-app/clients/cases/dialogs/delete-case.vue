<template>
  <v-dialog v-model="dialog" :value="caseForAction" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn elevation="2" small class="mx-2" color="error" v-on="{ ...tooltip, ...dialog }">
            Usuń
          </v-btn>
        </template>
        <span>Usuń klienta</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń Notatkę</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz notatkę. Odzyskanie dostępu będzie niemożliwe.
        Zatwierdź operację używjąc guzika 'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deleteClientCase">
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
import {deleteCase} from "@/data/endpoints/legal-app/legal-app-case-endpoints";

export default {
  name: "delete-case",
  props: {
    caseForAction: {
      required: true,
      default: null
    }
  },

  data: () => ({
    dialog: false,
  }),

  methods: {

    async deleteClientCase() {
      try {
        let caseId = this.caseForAction.id;
        await this.$axios.$delete(deleteCase(caseId));

      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.dialog = false;
        this.$emit('delete-completed');
      }
    }
  }
}
</script>

<style scoped>

</style>
