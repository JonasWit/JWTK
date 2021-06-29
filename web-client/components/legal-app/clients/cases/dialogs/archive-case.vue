<template>
  <v-dialog v-model="dialog" :value="caseForAction" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn elevation="2" small class="mx-2" color="warning" v-on="{ ...tooltip, ...dialog }">
            Archiwizuj
          </v-btn>
        </template>
        <span>Dodaj do archiwum</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Archiwizuj Sprawę</v-card-title>
      <v-card-subtitle>Potwierdzając operację dodasz sprawę do archiwum spraw.Zatwierdź operację używjąc guzika
        'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="addToCaseArchive">
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
import {archiveCase} from "@/data/endpoints/legal-app/legal-app-case-endpoints";

export default {
  name: "archive-case",
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
    async addToCaseArchive() {
      try {
        let caseId = this.caseForAction.id;
        await this.$axios.put(archiveCase(caseId));
        this.$notifier.showSuccessMessage("Sprawa została dodana do archiwum!");
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
