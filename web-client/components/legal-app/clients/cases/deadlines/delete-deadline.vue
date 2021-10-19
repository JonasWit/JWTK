<template>
  <v-dialog v-model="dialog" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="error">mdi-delete</v-icon>
          </v-btn>
        </template>
        <span>Usuń termin</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń Termin</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz termin. Zatwierdź operację używjąc guzika 'POTWIERDŹ'
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
  </v-dialog>
</template>

<script>
import {deleteDeadline} from "@/data/endpoints/legal-app/legal-app-case-endpoints";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";
import {mapActions} from "vuex";
import {queryDateForFloatingBell, todayDate} from "@/data/date-extensions";

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
  }),
  computed: {
    todayDate() {
      return todayDate()
    },
    query() {
      return queryDateForFloatingBell(this.todayDate)
    },
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getEventsForNotifications']),
    async deleteDeadline() {
      try {
        let caseId = this.$route.params.case;
        let deadlineId = this.deadlineForAction.id;
        let dates = this.query;
        await this.$axios.$delete(deleteDeadline(caseId, deadlineId));
        await this.getEventsForNotifications({dates})
        this.$notifier.showSuccessMessage("Termin został usunięty!");
      } catch (error) {
        handleError(error);
      } finally {
        this.$nuxt.refresh()
        this.dialog = false;
      }
    }
  }
};
</script>

<style scoped>

</style>
