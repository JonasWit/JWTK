<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn class="mx-3" fab v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="success">mdi-account-plus</v-icon>
          </v-btn>
        </template>
        <span>Zarejestruj nowy czas</span>
      </v-tooltip>
    </template>
    <v-form ref="createClientWorkForm" v-model="validation.valid">
      <v-card>
        <v-card-text>
          <v-text-field v-model="form.name" :rules="validation.name" label="Dodaj nazwę"></v-text-field>
          <v-text-field v-model="form.layerName" :rules="validation.name" label="Prawnik"></v-text-field>
          <v-text-field v-model="form.description" label="Dodaj krótki opis"></v-text-field>

          <v-text-field v-model="form.hours" required :rules="validation.numberOnly"
                        label="Dodaj liczbę godzin"></v-text-field>
          <v-text-field v-model="form.minutes" required :rules="validation.numberOnly"
                        label="Dodaj liczbę minut"></v-text-field>
          <v-text-field v-model="form.rate" required :rules="validation.numberOnly"
                        label="Dodaj stawkę godzinową"></v-text-field>
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

import {notEmptyAndLimitedRule, numberOnly} from "@/data/vuetify-validations";

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
      layerName: '',
    },
    modal: false,
    date: new Date().toISOString().substr(0, 10),
    validation: {
      valid: false,
      numberOnly: numberOnly(),
      name: notEmptyAndLimitedRule('Nazwa nie może być pusta oraz liczba znaków nie może przekraczać 50 znaków.', 5, 50),
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
      return Math.round((this.hoursSpent + (this.minutesSpent / 60)) * (this.givenRate + (this.givenRate * (this.givenVat / 100)))).toFixed(2);
    }

  },


  methods: {
    handleSubmit() {

      if (!this.$refs.createClientWorkForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const workRecord = {
        name: this.form.name,
        description: this.form.description,
        hours: this.hoursSpent,
        minutes: this.minutesSpent,
        rate: this.form.rate,
        eventDate: this.form.eventDate,
        vat: this.givenVat,
        amount: this.calculatedAmount,
        layerName: this.form.layerName,

      };

      console.warn('Nowy work rekord', workRecord)


      return this.$axios.$post(`/api/legal-app-clients-work/client/${this.$route.params.client}/work-records`, workRecord)
        .then(() => {
          this.resetForm()
          Object.assign(this.$data, this.$options.data.call(this)); // total data reset (all returning to default data)
          this.$nuxt.refresh();
          this.$notifier.showSuccessMessage("Czas zarejestrowany pomyślnie!");
        })
        .catch((e) => {
          console.warn('create work record error', e);
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        }).finally(() => {
          this.loading = false;
          this.dialog = false;

        });
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
