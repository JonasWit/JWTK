<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="success">mdi-file-document-edit</v-icon>
          </v-btn>
        </template>
        <span>Edytuj rekord</span>
      </v-tooltip>
    </template>
    <v-form ref="editWorkRecordForm">
      <v-card>
        <v-card-text>
          <v-text-field v-model="workRecord.name" label="Edytuj nazwę"
                        required :rules="validation.name"></v-text-field>
          <v-text-field v-model="workRecord.description" label="Edytuj szczególy"
          ></v-text-field>
          <v-text-field v-model="workRecord.eventDate" label="Edytuj datę zdarzenia"
          ></v-text-field>
          <v-text-field v-model="workRecord.hours" label="Edytuj godziny"
          ></v-text-field>
          <v-text-field v-model="workRecord.minutes" label="Edytuj minuty"
          ></v-text-field>
          <v-text-field v-model="workRecord.rate" label="Edytuj stawkę godzinową"
          ></v-text-field>
          <v-text-field v-model="workRecord.vat" label="Edytuj wartość VAT"
          ></v-text-field>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn text color="primary" @click="saveWorkRecordChange()">
            Zapisz zmianę
          </v-btn>
          <v-btn color="success" text @click="dialog = false">
            Anuluj
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>

<script>

import {nameLengthRule} from "../../../../data/vuetify-validations";

export default {
  name: "edit-work-record",
  props: {
    selectedFinancialRecord: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    workRecord: null,
    dialog: false,
    loading: false,
    form: {
      name: "",
      eventDate: "",
      amount: "",
      hours: "",
      minutes: "",
      vat: "",
      rate: "",
      description: "",
    },
    validation: {
      valid: false,
      name: nameLengthRule(),

    },
  }),

  fetch() {
    console.warn('work record:', this.selectedFinancialRecord)
    this.workRecord = this.selectedFinancialRecord

  },

  methods: {
    async saveWorkRecordChange() {
      if (!this.$refs.editWorkRecordForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const workRecord = {
        name: this.workRecord.name,
        hours: this.workRecord.hours,
        minutes: this.workRecord.minutes,
        vat: this.workRecord.vat,
        rate: this.workRecord.rate,
        amount: this.workRecord.amount,
        eventDate: this.workRecord.eventDate,
        description: this.workRecord.description,
      }
      try {
        console.warn('workRecord for put method:', workRecord)
        return this.$axios.$put(`/api/legal-app-clients-finance/client/${this.$route.params.client}/finance-record/${this.workRecord.id}`, workRecord)
        this.$nuxt.refresh();
        this.$notifier.showSuccessMessage("Kontakt updatowany pomyślnie!");
      } catch (e) {
        this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
      } finally {
        this.dialog = false;

      }


    },
    resetForm() {
      this.$refs.editWorkRecordForm.resetValidation();
    },
  }

}
</script>

<style scoped>

</style>
