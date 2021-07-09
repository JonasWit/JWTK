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
        <v-alert elevation="5" text type="info" dismissible close-text="Zamknij">
          Dodaj nowe przypomnienie, zadanie lub zaplanuj spotkanie.
        </v-alert>
        <v-card-text>
          <v-dialog ref="dialogFrom" v-model="modalFrom" :return-value.sync="form.dateFrom" persistent width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field v-model="form.dateFrom" label="Wybierz datę początkową" prepend-icon="mdi-calendar" readonly
                            v-bind="attrs"
                            v-on="on"></v-text-field>
            </template>
            <v-date-picker v-model="form.dateFrom" scrollable>
              <v-spacer></v-spacer>
              <v-btn text color="primary" @click="modalFrom = false">
                Anuluj
              </v-btn>
              <v-btn text color="primary" @click="$refs.dialogFrom.save(form.dateFrom)">
                OK
              </v-btn>
            </v-date-picker>
          </v-dialog>
          <v-dialog ref="dialogTo" v-model="modalTo" :return-value.sync="form.dateTo" persistent
                    width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field v-model="form.dateTo" label="Wybierz datę końcową" prepend-icon="mdi-calendar" readonly
                            v-bind="attrs"
                            v-on="on"></v-text-field>
            </template>
            <v-date-picker v-model="form.dateTo" scrollable>
              <v-spacer></v-spacer>
              <v-btn text color="primary" @click="modalTo = false">
                Anuluj
              </v-btn>
              <v-btn text color="primary" @click="$refs.dialogTo.save(form.dateTo)">
                OK
              </v-btn>
            </v-date-picker>
          </v-dialog>
          <v-alert v-model="alert" border="left" close-text="Zamknij" type="error" outlined dismissible>
            Proszę wybrać poprawny zakres dat. Data początkowa nie może być większa od daty końcowej."
          </v-alert>
          <v-text-field v-model="form.name" label="Nazwa" required :rules="validation.name"></v-text-field>
          <v-text-field v-model="form.message" label="Opis" required :rules="validation.message"></v-text-field>
          <v-select v-model="form.selectedCategory" :items="items" item-text="text" :item-value="value" return-object
                    label="Kategoria"></v-select>
          <v-alert v-if="form.public" v-model="alert2" elevation="5" text type="info" dismissible
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
  </v-dialog>
</template>

<script>
import {notEmptyAndLimitedRule, notEmptyRule} from "@/data/vuetify-validations";
import {createReminder} from "@/data/endpoints/legal-app/legal-app-reminders-endpoints";

export default {
  name: "add-reminder",
  data: () => ({
    loading: false,
    dialog: false,
    deadline: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
    menu2: false,
    form: {
      name: "",
      message: "",
      public: true,
      selectedCategory: {},
      dateFrom: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
      dateTo: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
    },
    validation: {
      valid: false,
      message: notEmptyAndLimitedRule("Opis nie może byc pusty i może zawierać maksymalnie 200 znaków.", 1, 200),
      deadline: notEmptyRule("Data nie może być pusta!")
    },
    modalFrom: false,
    modalTo: false,
    alert: false,
    alert2: true,
    checkbox: true,
    items: [{text: 'Spotkanie', value: 0}, {text: 'Zadanie', value: 2}, {text: 'Przypomnienie', value: 1}],
    catNumber: {},
    value: null
  }),

  methods: {
    async addNewReminder() {
      if (!this.$refs.addNewReminderForm.validate()) return;
      if (this.loading) return;
      this.loading = true;
      try {
        const newReminder = {
          active: true,
          name: this.form.name,
          message: this.form.message,
          start: this.form.dateFrom,
          end: this.form.dateTo,
          public: this.form.public,
          reminderCategory: this.form.selectedCategory.value
        };
        console.warn('nowy reminder', newReminder)
        await this.$axios.$post(createReminder(), newReminder);

        this.$nuxt.refresh()
        this.$notifier.showSuccessMessage("Kalendarz zaktualizowany pomyślnie!");
        this.resetForm()
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.dialog = false;
        this.loading = false;
      }
    }
    ,
    resetForm() {
      this.$refs.addNewReminderForm.reset();
      this.$refs.addNewReminderForm.resetValidation();
    }
    ,
    // labelCondition(val) {
    //   if (val) {
    //     return "Status publiczny"
    //   }
    //   this.alert2 = true
    //   return "Status prywatny"
    // }
  }
}
</script>

<style scoped>

</style>
