<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn class="mx-3 mb-12" color="primary" v-on="{ ...tooltip, ...dialog }">
            Zarejestruj nowe rozliczenie
          </v-btn>
        </template>
        <span>Zarejestruj nowe rozliczenie</span>
      </v-tooltip>
    </template>
    <v-form ref="createClientWorkForm" v-model="validation.valid">
      <v-card>
        <v-card-text>
          <v-text-field v-model="form.name" :rules="validation.name" label="Dodaj nazwę"></v-text-field>
          <v-text-field v-model="form.lawyerName" :rules="validation.name" label="Prawnik"></v-text-field>
          <v-text-field v-model="form.description" :rules="validation.length" label="Dodaj krótki opis"></v-text-field>

          <v-text-field v-model="form.hours" required :rules="validation.numberOnly"
                        label="Dodaj liczbę godzin"></v-text-field>
          <v-text-field v-model="form.minutes" required :rules="validation.numberOnly"
                        label="Dodaj liczbę minut"></v-text-field>
          <v-text-field v-model="form.rate" required :rules="validation.numberOnly"
                        label="Dodaj stawkę godzinową brutto"></v-text-field>
          <v-text-field v-model="form.vat" required :rules="validation.numberOnly"
                        label="Dodaj stawkę VAT"></v-text-field>
          <v-dialog
            ref="dialog" v-model="modal" :return-value.sync="form.eventDate" persistent width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field v-model="form.eventDate" required label="Wybierz datę zdarzenia" prepend-icon="mdi-calendar"
                            readonly
                            v-bind="attrs" v-on="on"></v-text-field>
            </template>
            <v-date-picker v-model="date" scrollable>
              <v-spacer></v-spacer>
              <v-btn text color="primary" @click="modal = false">
                Anuluj
              </v-btn>
              <v-btn text color="primary" @click="$refs.dialog.save(date)">
                Zapisz
              </v-btn>
            </v-date-picker>
          </v-dialog>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn text color="primary" @click="resetForm()">
            Wyczyść

          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="handleSubmit()">
            Dodaj
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>

<script>

import {lengthRule, notEmptyAndLimitedRule, numberOnly} from "@/data/vuetify-validations";
import {mapActions} from "vuex";
import {createWorkRecord} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "add-new-work-record",
  data: () => ({
    dialog: false,
    loading: false,
    form: {
      name: "",
      description: "",
      hours: "",
      minutes: "",
      rate: "",
      eventDate: "",
      vat: "",
      lawyerName: '',
    },
    modal: false,
    date: new Date().toISOString().substr(0, 10),
    validation: {
      valid: false,
      numberOnly: numberOnly(),
      name: notEmptyAndLimitedRule('Nazwa nie może być pusta oraz liczba znaków nie może przekraczać 50 znaków.', 5, 50),
      length: lengthRule('Maksymalna liczba znaków to 300 znaków', 5, 300)
    },

  }),

  computed: {
    hoursSpent() {
      return parseInt(this.form.hours);
    },
    minutesSpent() {
      return parseInt(this.form.minutes);
    },
    givenRate() {
      return (parseFloat(this.form.rate));
    },
    givenVat() {
      return (parseInt(this.form.vat));
    },
    calculatedAmount() {
      return Math.round((this.hoursSpent + (this.minutesSpent / 60)) * this.givenRate)
    }
  },


  methods: {
    ...mapActions('legal-app-client-store', ['getAllWorkRecordsOnFetch']),

    async handleSubmit() {
      if (!this.$refs.createClientWorkForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const workRecord = {
        name: this.form.name,
        description: this.form.description,
        hours: this.hoursSpent,
        minutes: this.minutesSpent,
        rate: this.givenRate,
        eventDate: this.form.eventDate,
        vat: this.givenVat,
        amount: this.calculatedAmount,
        lawyerName: this.form.lawyerName,

      };
      console.warn('work record:', workRecord)
      try {
        let clientId = this.$route.params.client
        await this.$axios.$post(createWorkRecord(clientId), workRecord)
        this.$notifier.showSuccessMessage("Czas zarejestrowany pomyślnie!");
        this.resetForm();
      } catch (error) {
        console.error(error)
        this.$notifier.showErrorMessage(error);
      } finally {
        let clientId = this.$route.params.client
        await this.getAllWorkRecordsOnFetch({clientId});
        this.loading = false;
        this.dialog = false;

      }
    },
    resetForm() {
      this.$refs.createClientWorkForm.reset();
      this.$refs.createClientWorkForm.resetValidation();
    },
  }


}
</script>

<style scoped>

</style>
