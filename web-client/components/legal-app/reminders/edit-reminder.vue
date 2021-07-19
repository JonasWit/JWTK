<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon small class="mx-2" v-on="{ ...tooltip, ...dialog }">
            <v-icon>mdi-pencil</v-icon>
          </v-btn>
        </template>
        <span>Edytuj treść</span>
      </v-tooltip>
    </template>
    <v-form ref="editCalendarEventForm">
      <v-card>
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Edytuj
          </v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <v-switch v-model="form.switcher" color="primary" label="Cały dzień"></v-switch>
          <v-row class="d-flex justify=space-between">
            <v-dialog ref="dialogFrom" v-model="modalFrom" :return-value.sync="form.dateFrom" persistent width="290px">
              <template v-slot:activator="{ on, attrs }">
                <v-text-field v-model="form.dateFrom" label="Wybierz datę początkową" prepend-icon="mdi-calendar"
                              readonly
                              v-bind="attrs" v-on="on"></v-text-field>
              </template>
              <v-date-picker v-model="form.dateFrom" scrollable>
                <v-btn text color="error" @click="modalFrom = false">
                  Anuluj
                </v-btn>
                <v-spacer></v-spacer>
                <v-btn text color="primary" @click="$refs.dialogFrom.save(form.dateFrom)">
                  Wybierz
                </v-btn>
              </v-date-picker>
            </v-dialog>
            <v-dialog v-if="!form.switcher" ref="dialogTimeFrom" v-model="modalTimeFrom"
                      :return-value.sync="form.timeFrom"
                      width="290px">
              <template v-slot:activator="{ on, attrs }">
                <v-text-field v-model="form.timeFrom" label="Picker in dialog"
                              prepend-icon="mdi-clock-time-four-outline"
                              readonly v-bind="attrs" v-on="on"></v-text-field>
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
            <v-dialog v-if="!form.switcher" ref="dialogTo" v-model="modalTo" :return-value.sync="form.dateTo" persistent
                      width="290px">
              <template v-slot:activator="{ on, attrs }">
                <v-text-field v-model="form.dateTo" label="Wybierz datę końcową" prepend-icon="mdi-calendar" readonly
                              v-bind="attrs" v-on="on"></v-text-field>
              </template>
              <v-date-picker v-model="form.dateTo" scrollable>
                <v-btn text color="error" @click="modalTo = false">
                  Anuluj
                </v-btn>
                <v-spacer></v-spacer>
                <v-btn text color="primary" @click="$refs.dialogTo.save(form.dateTo)">
                  Wybierz
                </v-btn>
              </v-date-picker>
            </v-dialog>
            <v-dialog v-if="!form.switcher" ref="dialogTimeTo" v-model="modalTimeTo" :return-value.sync="form.timeTo"
                      width="290px">
              <template v-slot:activator="{ on, attrs }">
                <v-text-field v-model="form.timeTo" label="Picker in dialog" prepend-icon="mdi-clock-time-four-outline"
                              readonly v-bind="attrs" v-on="on"></v-text-field>
              </template>
              <v-time-picker v-if="modalTimeTo" v-model="form.timeTo" full-width format="24hr">
                <v-btn text color="error" @click="modalTimeTo = false">
                  Anuluj
                </v-btn>
                <v-spacer></v-spacer>
                <v-btn text color="primary" @click="$refs.dialogTimeTo.save(form.timeTo)">
                  Wybierz
                </v-btn>
              </v-time-picker>
            </v-dialog>
          </v-row>
          <v-alert v-model="alert" border="left" close-text="Zamknij" type="error" outlined dismissible>
            Proszę wybrać poprawny zakres dat. Data początkowa nie może być większa od daty końcowej."
          </v-alert>
          <v-text-field v-model="form.name" label="Nazwa" required></v-text-field>
          <v-text-field v-model="form.message" label="Opis" required></v-text-field>
          <v-select v-model="form.selectedCategory" :items="items" item-text="text" :item-value="value" return-object
                    label="Kategoria"></v-select>
          <v-alert v-if="form.public" v-model="alert2" elevation="5" text type="info" dismissible close-text="Zamknij">
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
          <v-btn text color="primary" @click="saveChanges()">
            Zapisz zmianę
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>


</template>

<script>
import {updateReminder} from "@/data/endpoints/legal-app/legal-app-reminders-endpoints";

export default {
  name: "edit-reminder",
  props: {
    eventForAction: {
      required: true,
      default: null
    }
  },
  data: () => ({
    loading: false,
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
    modalFrom: false,
    modalTo: false,
    alert: false,
    alert2: true,
    checkbox: true,
    items: [{text: 'Spotkanie', value: 0}, {text: 'Przypomnienie', value: 1}, {text: 'Zadanie', value: 2},],
    catNumber: {},
    value: null,
    modalTimeFrom: false,
    modalTimeTo: false,
  }),
  fetch() {
    this.form.name = this.eventForAction.name;
    this.form.message = this.eventForAction.details;
    this.form.public = this.eventForAction.public;
    this.form.switcher = this.eventForAction.timed;
    this.form.dateFrom = (this.eventForAction.start).substr(0, 10);
    this.form.timeFrom = (this.eventForAction.start).substr(10, 6);
    this.form.dateTo = (this.eventForAction.end).substr(0, 10);
    this.form.timeTo = (this.eventForAction.end).substr(10, 6);
    this.form.selectedCategory = this.selectedDefault;
  },

  computed: {
    selectedDefault() {
      if (this.eventForAction.category === 0) {
        return {text: 'Spotkanie', value: 0}
      }
      if (this.eventForAction.category === 1) {
        return {text: 'Przypomnienie', value: 1}
      }
      if (this.eventForAction.category === 1) {
        return {text: 'Zadanie', value: 2}
      }
    },

    submittableDateStart() {
      if (this.form.switcher) {
        return new Date(this.form.dateFrom)
      } else {
        const date = new Date(this.form.dateFrom)
        if (typeof this.form.timeFrom === 'string') {
          const hours = this.form.timeFrom.match(/^(\d+)/)[1]
          const minutes = this.form.timeFrom.match(/:(\d+)/)[1]
          date.setHours(hours)
          date.setMinutes(minutes)
        } else {
          date.setHours(this.form.timeFrom.getHours())
          date.setMinutes(this.form.timeFrom.getMinutes())
        }
        console.log('UTC data start', date)
        return date
      }
    },
    submittableDateEnd() {
      if (this.form.switcher) {
        return new Date(this.form.dateTo)
      } else {
        const date = new Date(this.form.dateTo)
        if (typeof this.form.timeTo === 'string') {
          const hours = this.form.timeTo.match(/^(\d+)/)[1]
          const minutes = this.form.timeTo.match(/:(\d+)/)[1]
          date.setHours(hours)
          date.setMinutes(minutes)
        } else {
          date.setHours(this.form.timeTo.getHours())
          date.setMinutes(this.form.timeTo.getMinutes())
        }
        console.log('UTC data end', date)
        return date
      }
    }
  },

  methods: {
    async saveChanges() {
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
        let reminderId = this.eventForAction.id;
        console.warn('edited reminder', newReminder)
        await this.$axios.$put(updateReminder(reminderId), newReminder);
        this.$notifier.showSuccessMessage("Zmiany zostały zapisane!");
      } catch (e) {
        console.error('error in edit mode', e);
      } finally {
        this.dialog = false;
        this.$emit('action-completed');
      }

    }
  }
}
</script>

<style scoped>

</style>
