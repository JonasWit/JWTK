<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-btn color="error" v-on="{ ...dialog }">
        Dodaj rozliczenie
      </v-btn>
    </template>
    <v-form ref="createClientWorkForm" v-model="validation.valid">
      <v-card>
        <v-card-text>
          <v-text-field v-model="form.name" :rules="validation.name" label="Dodaj nazwę"></v-text-field>
          <v-text-field v-model="form.lawyerName" :rules="validation.name" label="Prawnik"></v-text-field>
          <v-text-field v-model="form.description" :rules="validation.length" label="Dodaj krótki opis"></v-text-field>

          <v-text-field v-model="form.hours" required :rules="validation.hours"
                        label="Dodaj liczbę godzin"></v-text-field>
          <v-text-field v-model="form.minutes" required :rules="validation.minutes"
                        label="Dodaj liczbę minut"></v-text-field>
          <v-text-field v-model="form.rate" required :rules="validation.numberOnly"
                        label="Dodaj stawkę godzinową brutto"></v-text-field>
          <v-select :items="items" v-model="form.vat" label="Dodaj stawkę VAT" item-text="text" :item-value="value"
                    return-object></v-select>
          <v-dialog
            ref="dialog" v-model="modal" :return-value.sync="form.eventDate" persistent width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field v-model="form.eventDate" required label="Wybierz datę zdarzenia" prepend-icon="mdi-calendar"
                            readonly
                            v-bind="attrs" v-on="on"></v-text-field>
            </template>
            <v-date-picker v-model="date" scrollable @change="$refs.dialog.save(date)" locale="pl">
              <v-btn text color="error" @click="modal = false">
                Anuluj
              </v-btn>
              <v-spacer></v-spacer>
            </v-date-picker>
          </v-dialog>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn text color="error" @click="resetForm()">
            Wyczyść

          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="handleSubmit()">
            Dodaj
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
    <progress-bar v-if="loader"/>
  </v-dialog>
</template>

<script>

import {
  hoursValidation,
  lengthRule,
  minutesValidation,
  notEmptyAndLimitedRule,
  numberOnly
} from "@/data/vuetify-validations";
import {createWorkRecord} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {mapActions} from "vuex";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "add-new-work-record",
  components: {ProgressBar},
  data: () => ({
      dialog: false,
      loader: false,
      items: [{text: '0%', value: 0}, {text: '5%', value: 5}, {text: '8%', value: 8}, {text: '23%', value: 23}],
      value: null,
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
        name: notEmptyAndLimitedRule('Nazwa nie może być pusta oraz liczba znaków nie może przekraczać 50 znaków.', 1, 50),
        length: lengthRule('Maksymalna liczba znaków to 300 znaków', 1, 300),
        hours: hoursValidation(),
        minutes: minutesValidation()
      },

    }
  ),

  computed: {
    hoursSpent() {
      return parseInt(this.form.hours);
    }
    ,
    minutesSpent() {
      return parseInt(this.form.minutes);
    }
    ,
    givenRate() {
      return (parseFloat(this.form.rate));
    }
    ,
    calculatedAmount() {
      return Math.round((this.hoursSpent + (this.minutesSpent / 60)) * this.givenRate)
    },
  },


  methods: {
    ...mapActions('legal-app-client-store', ['getFinancialRecordsFromFetch']),
    async handleSubmit() {
      if (!this.$refs.createClientWorkForm.validate()) return;
      this.loader = true
      const workRecord = {
        name: this.form.name,
        description: this.form.description,
        hours: this.hoursSpent,
        minutes: this.minutesSpent,
        rate: this.givenRate,
        eventDate: this.form.eventDate,
        vat: this.form.vat.value,
        amount: this.calculatedAmount,
        lawyerName: this.form.lawyerName,

      };
      try {
        let clientId = this.$route.params.client
        await this.$axios.$post(createWorkRecord(clientId), workRecord)
        this.$notifier.showSuccessMessage("Czas zarejestrowany pomyślnie!");
        this.resetForm();
      } catch (error) {
        handleError(error);
      } finally {
        this.$emit('action-completed');
        this.loader = false;
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
