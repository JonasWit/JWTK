<template>
  <v-dialog v-model="dialog" :value="selectedBillingRecord" persistent width="500">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn elevation="2" small class="mx-2" color="error" v-on="{ ...tooltip, ...dialog }">
            Usuń
          </v-btn>
        </template>
        <span>Usuń dane</span>
      </v-tooltip>
    </template>
    <v-card>
      <v-card-title class="justify-center">Usuń dane do rozliczenia</v-card-title>
      <v-card-subtitle>Potwierdzając operację usuniesz wybrany rekord. Odzyskanie dostępu będzie niemożliwe.
        Zatwierdź operację używjąc guzika 'POTWIERDŹ'
      </v-card-subtitle>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="error" text @click="deleteBillingRecord">
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
import {mapActions} from "vuex";
import {deleteBillingData} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "delete-billing-data",
  props: {
    selectedBillingRecord: {
      required: true,
      type: Object,
      default: null
    },

  },
  data: () => ({
    dialog: false,
    loading: false,
    billingRecord: null,
  }),
  async fetch() {
    this.billingRecord = this.selectedBillingRecord
  },

  methods: {
    ...mapActions('legal-app-client-store', ['getBillingDataFromFetch']),
    async deleteBillingRecord() {
      try {
        let billingRecordId = this.billingRecord.id
        await this.$axios.$delete(deleteBillingData(billingRecordId))
        this.$notifier.showSuccessMessage("Rekord usunięty pomyślnie!");
      } catch (error) {
        console.error(error)
        this.$notifier.showErrorMessage(error);
      } finally {
        await this.getBillingDataFromFetch()
        this.dialog = false;
        this.loading = false;
      }
    }
  }
}
</script>

<style scoped>

</style>
