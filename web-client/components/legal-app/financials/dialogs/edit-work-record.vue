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
          <v-text-field v-model="form.name" label="Edytuj nazwę"
                        required :rules="validation.name"></v-text-field>
          <v-text-field v-model="form.description" label="Edytuj opis"
          ></v-text-field>
          <v-text-field v-model="form.lawyerName" :rules="validation.name" label="Prawnik"></v-text-field>
          <v-dialog ref="dialogTo" v-model="modal" :return-value.sync="form.eventDate" persistent width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field v-model="form.eventDate" label="Wybierz datę zdarzenia" prepend-icon="mdi-calendar" readonly
                            v-bind="attrs" v-on="on" :rules="validation.date"></v-text-field>
            </template>
            <v-date-picker v-model="form.eventDate" scrollable @change="$refs.dialogTo.save(form.eventDate)">
              <v-btn text color="error" @click="modal = false">
                Anuluj
              </v-btn>
              <v-spacer></v-spacer>
            </v-date-picker>
          </v-dialog>
          <v-text-field v-model="form.hours" :rules=validation.numberOnly label="Edytuj godziny"
          ></v-text-field>
          <v-text-field v-model="form.minutes" :rules=validation.numberOnly label="Edytuj minuty"
          ></v-text-field>
          <v-text-field v-model="form.rate" :rules=validation.numberOnly label="Edytuj stawkę godzinową"
          ></v-text-field>

          <v-select :items="items" v-model="form.vat" label="Edytuj wartość VAT" item-text="text" :item-value="value"
                    return-object></v-select>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>

          <v-btn color="error" text @click="dialog = false">
            Anuluj
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="saveWorkRecordChange()">
            Zapisz zmianę
          </v-btn>


        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>

<script>

import {notEmptyAndLimitedRule, numberOnly} from "@/data/vuetify-validations";
import {updateWorkRecord} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

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
    items: [{text: '0%', value: 0}, {text: '5%', value: 5}, {text: '8%', value: 8}, {text: '23%', value: 23}],
    value: null,
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
    modal: false,
    validation: {
      valid: false,
      name: notEmptyAndLimitedRule('Nazwa nie może być pusta oraz liczba znaków nie może przekraczać 50 znaków.', 1, 10),
      numberOnly: numberOnly(),

    },
  }),

  fetch() {
    this.workRecord = this.selectedFinancialRecord
    console.log(this.selectedFinancialRecord)
    this.form.name = this.workRecord.name
    this.form.hours = this.workRecord.hours
    this.form.minutes = this.workRecord.minutes
    this.form.vat = this.selectedDefault
    this.form.rate = this.workRecord.rate
    this.form.amount = this.workRecord.amount
    this.form.eventDate = (this.workRecord.eventDate).substr(0, 10)
    this.form.description = this.workRecord.description
    this.form.lawyerName = this.workRecord.lawyerName

  },
  computed: {
    selectedDefault() {
      if (this.selectedFinancialRecord.vat === 0) {
        return {text: '0%', value: 0};
      }
      if (this.selectedFinancialRecord.vat === 5) {
        return {text: '5%', value: 5};
      }
      if (this.selectedFinancialRecord.vat === 8) {
        return {text: '8%', value: 8};
      }
      if (this.selectedFinancialRecord.vat === 23) {
        return {text: '23%', value: 23};
      }

    },
  },

  methods: {

    async saveWorkRecordChange() {
      if (!this.$refs.editWorkRecordForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const workRecord = {
        name: this.form.name,
        hours: this.form.hours,
        minutes: this.form.minutes,
        vat: this.form.vat.value,
        rate: this.form.rate,
        amount: this.form.amount,
        eventDate: this.form.eventDate,
        description: this.form.description,
        lawyerName: this.form.lawyerName,
      }
      console.warn('workRecord', workRecord)
      console.warn('workRecord', this.selectedFinancialRecord.id)
      try {
        let clientId = this.$route.params.client
        let workRecordId = this.selectedFinancialRecord.id
        await this.$axios.$put(updateWorkRecord(clientId, workRecordId), workRecord)
        this.$notifier.showSuccessMessage("Rekord zmieniony pomyślnie!");
      } catch (error) {
        console.error(error)
        this.$notifier.showErrorMessage(error);
      } finally {
        this.$emit('action-completed');
        this.dialog = false;
        this.loading = false;
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
