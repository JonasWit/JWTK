<template>
  <v-dialog v-model="dialog" :value="caseForAction" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="error">mdi-delete</v-icon>
          </v-btn>
        </template>
        <span>Usuń sprawę</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń Sprawę</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz sprawę. Odzyskanie dostępu będzie niemożliwe.
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
import {handleError} from "@/data/functions";

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
        this.$notifier.showSuccessMessage("Sprawa została usunięta!");
      } catch (error) {
        handleError(error);
      } finally {
        this.$nuxt.refresh()
        this.dialog = false;
      }
    }
  }
}
</script>

<style scoped>

</style>
