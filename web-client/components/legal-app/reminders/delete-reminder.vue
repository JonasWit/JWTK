<template>
  <v-dialog v-model="dialog" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn text small class="mx-2" color="error" v-on="{ ...tooltip, ...dialog }">
            Usuń
          </v-btn>
        </template>
        <span>Usuń Przypomnienie</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń Termin</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz przypomnienie. Odzyskanie dostępu będzie niemożliwe.
        Zatwierdź operację używjąc guzika 'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deleteEvent">
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
import {deleteReminder} from "@/data/endpoints/legal-app/legal-app-reminders-endpoints";

export default {
  name: "delete-reminder",
  props: {
    eventForAction: {
      required: true,
      default: null
    }
  },
  data: () => ({
    dialog: false,
  }),
  methods: {
    async deleteEvent() {
      try {
        let reminderId = this.eventForAction.id
        console.warn('reminder id', reminderId)
        await this.$axios.$delete(deleteReminder(reminderId));
        this.$notifier.showSuccessMessage("Przypomnienie zostało usunięte!");
      } catch (e) {
        console.error('error', e)
      } finally {
        this.$emit('delete-completed');
        this.dialog = false;

      }
    }
  }
}
</script>

<style scoped>

</style>
