<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn fab class="mx-2" v-on="{ ...tooltip, ...dialog }">
            <v-icon color="success" x-large>mdi-plus</v-icon>
          </v-btn>
        </template>
        <span>Dodaj</span>
      </v-tooltip>
    </template>
    <v-form ref="addNewReminderForm" v-model="validation.valid">
      <v-card>
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Dodaj
          </v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <v-switch v-model="form.switcher" color="primary" label="Cały dzień"></v-switch>
          <v-row class="d-flex justify=space-between">
            <v-dialog ref="dialogFrom" v-model="modalFrom" :return-value.sync="form.dateFrom" persistent width="290px">
              <template v-slot:activator="{ on, attrs }">
                <v-text-field v-model="form.dateFrom" label="Wybierz datę początkową" prepend-icon="mdi-calendar"
                              readonly v-bind="attrs" v-on="on" :rules="validation.date"></v-text-field>
              </template>
              <v-date-picker v-model="form.dateFrom" scrollable locale="pl"
                             @change="$refs.dialogFrom.save(form.dateFrom)">

                <v-btn text color="error" @click="modalFrom = false">
                  Anuluj
                </v-btn>
                <v-spacer></v-spacer>
              </v-date-picker>
            </v-dialog>
            <v-dialog v-if="!form.switcher" ref="dialogTimeFrom" v-model="modalTimeFrom"
                      :return-value.sync="form.timeFrom" width="290px">
              <template v-slot:activator="{ on, attrs }">
                <v-text-field v-model="form.timeFrom" label="Wybierz godzinę" prepend-icon="mdi-clock-time-four-outline"
                              readonly v-bind="attrs" v-on="on" :rules="validation.time"></v-text-field>
              </template>
              <v-time-picker v-if="modalTimeFrom" v-model="form.timeFrom" full-width format="24hr">
                <v-btn text color="error" @click="modalTimeFrom = false">
                  Anuluj
                </v-btn>
                <v-spacer></v-spacer>
                <v-btn text color="primary" @click="$refs.dialogTimeFrom.save(form.timeFrom)">
                  Wybierz
                </v-btn>
              </v-time-picker>
            </v-dialog>
          </v-row>
          <v-row class="d-flex justify=space-between">
            <v-dialog ref="dialogTo" v-model="modalTo" :return-value.sync="form.dateTo" persistent width="290px">
              <template v-slot:activator="{ on, attrs }">
                <v-text-field v-model="form.dateTo" label="Wybierz datę końcową" prepend-icon="mdi-calendar" readonly
                              v-bind="attrs" v-on="on" :rules="validation.date"></v-text-field>
              </template>
              <v-date-picker v-model="form.dateTo" scrollable locale="pl" @change="$refs.dialogTo.save(form.dateTo)">
                <v-btn text color="error" @click="modalTo = false">
                  Anuluj
                </v-btn>
                <v-spacer></v-spacer>
              </v-date-picker>
            </v-dialog>
            <v-dialog v-if="!form.switcher" ref="dialogTimeTo" v-model="modalTimeTo" :return-value.sync="form.timeTo"
                      width="290px">
              <template v-slot:activator="{ on, attrs }">
                <v-text-field v-model="form.timeTo" label="Wybierz godzinę" prepend-icon="mdi-clock-time-four-outline"
                              readonly v-bind="attrs" v-on="on" :rules="validation.time"></v-text-field>
              </template>
              <v-time-picker v-if="modalTimeTo" v-model="form.timeTo" full-width format="24hr">
                <v-spacer></v-spacer>
                <v-btn text color="error" @click="modalTimeTo = false">
                  Anuluj
                </v-btn>
                <v-btn text color="primary" @click="$refs.dialogTimeTo.save(form.timeTo)">
                  Wybierz
                </v-btn>
              </v-time-picker>
            </v-dialog>
          </v-row>
          <v-alert v-if="legalAppTooltips" border="left" type="error" outlined>
            Proszę wybrać poprawny zakres dat. Data początkowa nie może być większa od daty końcowej."
          </v-alert>
          <v-text-field v-model="form.name" label="Nazwa" required :rules="validation.name"></v-text-field>
          <v-text-field v-model="form.message" label="Opis" required :rules="validation.message"></v-text-field>
          <v-select v-model="form.selectedCategory" :items="items" item-text="text" :item-value="value" return-object
                    label="Kategoria" :rules="validation.category" required></v-select>
          <v-alert class="mt-3" v-if="form.public" v-model="alert2" elevation="5" text type="info" dismissible
                   close-text="Zamknij">
            Status publiczny oznacza, że przypomnienia, zadania lub zaplanowane spotkania będą widoczne dla wszystkich
            użytkowników. Jeśli chcesz, zmienić status na prywatny odznacz flagę.
          </v-alert>
          <v-checkbox v-model="form.public" label="Status publiczny" color="red darken-3"></v-checkbox>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn color="error" text @click="dialog = false">
            Anuluj
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="addNewReminder">
            Zapisz
          </v-btn>

        </v-card-actions>
      </v-card>
    </v-form>
    <progress-bar v-if="loader"/>
  </v-dialog>
</template>

<script>

import {lengthRule, notEmptyAndLimitedRule, notEmptyRule} from "@/data/vuetify-validations";
import {mapState} from "vuex";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "add-reminder",
  components: {ProgressBar},
  data: () => ({
    loader: false,
    dialog: false,
    menu2: false,
    form: {
      timeFrom: null,
      timeTo: null,
      name: "",
      message: "",
      public: true,
      selectedCategory: null,
      dateFrom: null,
      dateTo: null,
      switcher: false
    },
    validation: {
      valid: false,
      name: notEmptyAndLimitedRule('Nazwa nie może być pusta i nie może zawierać więcej niż 100 znaków', 1, 100),
      message: lengthRule("Opis nie może zawierać więcej niż 200 znaków!", 0, 200),
      date: notEmptyRule('Proszę wybrać datę'),
      time: notEmptyRule('Proszę wybrać godzinę'),
      category: notEmptyRule('Proszę wybrać kategorię')
    },
    modalFrom: false,
    modalTo: false,
    alert2: true,
    checkbox: true,
    items: [{text: 'Spotkanie', value: 0}, {text: 'Przypomnienie', value: 1}, {text: 'Zadanie', value: 2},],
    catNumber: {},
    value: null,
    modalTimeFrom: false,
    modalTimeTo: false,
  }),
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
    submittableDateStart() {
      if (this.form.switcher) {
        return new Date(this.form.dateFrom);
      } else {
        const date = new Date(this.form.dateFrom);
        if (typeof this.form.timeFrom === 'string') {
          const hours = this.form.timeFrom.match(/^(\d+)/)[1];
          const minutes = this.form.timeFrom.match(/:(\d+)/)[1];
          date.setHours(hours);
          date.setMinutes(minutes);
        } else {
          date.setHours(this.form.timeFrom.getHours());
          date.setMinutes(this.form.timeFrom.getMinutes());
        }
        console.log('UTC data start', date);
        return date;
      }
    },
    submittableDateEnd() {
      if (this.form.switcher) {
        return new Date(this.form.dateTo);
      } else {
        const date = new Date(this.form.dateTo);
        if (typeof this.form.timeTo === 'string') {
          const hours = this.form.timeTo.match(/^(\d+)/)[1];
          const minutes = this.form.timeTo.match(/:(\d+)/)[1];
          date.setHours(hours);
          date.setMinutes(minutes);
        } else {
          date.setHours(this.form.timeTo.getHours());
          date.setMinutes(this.form.timeTo.getMinutes());
        }
        console.log('UTC data end', date);
        return date;
      }
    }
  },

  methods: {
    addNewReminder: async function () {
      if (!this.$refs.addNewReminderForm.validate()) return;
      if (this.submittableDateStart > this.submittableDateEnd) {
        return this.alert = true;
      } else {
        this.loader = true;
        try {
          const newReminder = {
            active: true,
            name: this.form.name,
            message: this.form.message,
            start: this.submittableDateStart.toISOString(),
            end: this.submittableDateEnd.toISOString(),
            public: this.form.public,
            reminderCategory: this.form.selectedCategory.value,
            allDayEvent: this.form.switcher
          };
          console.warn('nowy reminder', newReminder);
          await this.$axios.$post(`/api/legal-app-reminders/reminder`, newReminder);
          this.$notifier.showSuccessMessage("Kalendarz zaktualizowany pomyślnie!");
          Object.assign(this.$data, this.$options.data());
          this.resetForm();
        } catch (error) {
          handleError(error);
        } finally {
          this.$emit('action-completed');
          this.dialog = false;
          this.loader = false;
        }
      }
    },
    resetForm() {
      this.$refs.addNewReminderForm.resetValidation();
    }
  }
};
</script>

<style scoped>

</style>
