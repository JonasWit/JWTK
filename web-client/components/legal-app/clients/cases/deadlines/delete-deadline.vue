<template>
  <v-dialog v-model="dialog" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn elevation="2" small class="mx-2" color="error" v-on="{ ...tooltip, ...dialog }">
            Usuń
          </v-btn>
        </template>
        <span>Usuń Termin</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń Termin</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz sprawę. Odzyskanie dostępu będzie niemożliwe.
        Zatwierdź operację używjąc guzika 'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deleteDeadline">
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
import {deleteDeadline} from "@/data/endpoints/legal-app/legal-app-case-endpoints";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "delete-deadline",
  components: {ProgressBar},
  props: {
    deadlineForAction: {
      required: true,
      default: null
    }
  },
  data: () => ({
    dialog: false,
    loader: false
  }),
  methods: {
    async deleteDeadline() {
      this.loader = true
      try {
        let caseId = this.$route.params.case;
        let deadlineId = this.deadlineForAction.id;
        await this.$axios.$delete(deleteDeadline(caseId, deadlineId));
        this.$notifier.showSuccessMessage("Termin został usunięty!");
      } catch (error) {
        handleError(error);
      } finally {
        this.dialog = false;
        this.$emit('delete-completed');
        this.loader = false
      }
    }
  }
};
</script>

<style scoped>

</style>
