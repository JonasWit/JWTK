<template>
  <v-dialog v-model="dialog" :value="caseForAction" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn elevation="2" small class="mx-2" color="error" v-on="{ ...tooltip, ...dialog }">
            Usuń
          </v-btn>
        </template>
        <span>Usuń notatkę</span>
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
    <progress-bar v-if="loader"/>
  </v-dialog>
</template>

<script>
import {deleteCase} from "@/data/endpoints/legal-app/legal-app-case-endpoints";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "delete-case",
  components: {ProgressBar},
  props: {
    caseForAction: {
      required: true,
      default: null
    }
  },
  data: () => ({
    dialog: false,
    loader: false
  }),
  methods: {
    async deleteClientCase() {
      this.loader = true
      try {
        let caseId = this.caseForAction.id;
        await this.$axios.$delete(deleteCase(caseId));
        this.$notifier.showSuccessMessage("Sprawa została usunięta!");
      } catch (error) {
        handleError(error);
      } finally {
        this.dialog = false;
        this.loader = false
        this.$emit('delete-completed');
      }
    }
  }
}
</script>

<style scoped>

</style>
