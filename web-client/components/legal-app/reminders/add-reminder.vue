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
          <v-dialog ref="dialogFrom" v-model="modalFrom" :return-value.sync="dateFrom" persistent width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field v-model="dateFrom" label="Wybierz datę początkową" prepend-icon="mdi-calendar" readonly
                            v-bind="attrs"
                            v-on="on"></v-text-field>
            </template>
            <v-date-picker v-model="dateFrom" scrollable>
              <v-spacer></v-spacer>
              <v-btn text color="primary" @click="modalFrom = false">
                Anuluj
              </v-btn>
              <v-btn text color="primary" @click="$refs.dialogFrom.save(dateFrom)">
                OK
              </v-btn>
            </v-date-picker>
          </v-dialog>
          <v-dialog ref="dialogTo" v-model="modalTo" :return-value.sync="dateTo" persistent
                    width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field v-model="dateTo" label="Wybierz datę końcową" prepend-icon="mdi-calendar" readonly
                            v-bind="attrs"
                            v-on="on"></v-text-field>
            </template>
            <v-date-picker v-model="dateTo" scrollable>
              <v-spacer></v-spacer>
              <v-btn text color="primary" @click="modalTo = false">
                Anuluj
              </v-btn>
              <v-btn text color="primary" @click="$refs.dialogTo.save(dateTo)">
                OK
              </v-btn>
            </v-date-picker>
          </v-dialog>
          <v-alert v-model="alert" border="left" close-text="Zamknij" type="error" outlined dismissible>
            Proszę wybrać poprawny zakres dat. Data początkowa nie może być większa od daty końcowej."
          </v-alert>
          <v-text-field v-model="form.message" label="Opis" required :rules="validation.message"></v-text-field>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn color="error" text @click="dialog = false">
            Anuluj
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="addNewDeadline">
            Zapisz
          </v-btn>

        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>

<script>
import {notEmptyAndLimitedRule, notEmptyRule} from "@/data/vuetify-validations";
import {mapActions} from "vuex";
import {createDeadline} from "@/data/endpoints/legal-app/legal-app-case-endpoints";

export default {
  name: "add-reminder",
  data: () => ({
    loading: false,
    dialog: false,
    deadline: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
    menu2: false,
    form: {
      message: "",


    },
    validation: {
      valid: false,
      message: notEmptyAndLimitedRule("Opis nie może byc pusty i może zawierać maksymalnie 200 znaków.", 1, 200),
      deadline: notEmptyRule("Data nie może być pusta!")

    },

    dateFrom: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
    dateTo: (new Date(Date.now() - (new Date()).getTimezoneOffset() * 60000)).toISOString().substr(0, 10),
    modalFrom: false,
    modalTo: false,
    alert: false,
  }),

  methods: {
    ...mapActions('legal-app-client-store', ['getClientsNotes']),

    async addNewDeadline() {
      if (!this.$refs.addNewDeadlineForm.validate()) return;
      if (this.loading) return;
      this.loading = true;
      try {
        const newDeadline = {
          deadline: new Date(`${this.deadline}T23:59:59`),
          message: this.form.message,
        };
        let caseId = this.$route.params.case
        await this.$axios.$post(createDeadline(caseId), newDeadline);
        console.warn('nowy termin', newDeadline)
        this.$nuxt.refresh()
        this.$notifier.showSuccessMessage("Termin dodany pomyślnie!");
        this.resetForm()
      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.dialog = false;
        this.loading = false;
      }
    },
    resetForm() {
      this.$refs.addNewDeadlineForm.reset();
      this.$refs.addNewDeadlineForm.resetValidation();
    },
  }
}
</script>

<style scoped>

</style>
