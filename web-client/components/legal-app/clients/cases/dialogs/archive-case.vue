<template>
  <v-dialog v-model="dialog" :value="caseForAction" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="warning">mdi-archive-arrow-down</v-icon>
          </v-btn>
        </template>
        <span>Archiwizuj sprawę</span>
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
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "archive-case",
  components: {ProgressBar},
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
