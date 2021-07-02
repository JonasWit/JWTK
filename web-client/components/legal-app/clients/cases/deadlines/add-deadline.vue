<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn fab class="mx-2" v-on="{ ...tooltip, ...dialog }">
            <v-icon color="success" x-large>mdi-plus</v-icon>
          </v-btn>
        </template>
        <span>Dodaj Sprawę</span>
      </v-tooltip>
    </template>
    <v-form ref="addNewDeadlineForm" v-model="validation.valid">
      <v-card>
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Dodaj Termin
          </v-toolbar-title>
        </v-toolbar>
        <v-alert elevation="5" text type="info" dismissible close-text="Zamknij">
          Każda nowa sprawa jest prywatna, co oznacza, że będzie widoczna tylko dla użytkownika, który ją stworzył.
          Jeśli chcesz, aby sprawa była widoczna dla innych, oznacz ją jako publiczną.
        </v-alert>
        <v-card-text>
          <v-menu v-model="menu2" :close-on-content-click="false" :nudge-right="40" transition="scale-transition"
                  offset-y min-width="auto">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field v-model="deadline" label="Wybierz termin" prepend-icon="mdi-calendar" readonly
                            v-bind="attrs" v-on="on" :rules="validation.deadline"></v-text-field>
            </template>
            <v-date-picker v-model="deadline" @input="menu2 = false"></v-date-picker>
          </v-menu>
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
  name: "add-deadline",
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
  }),

  methods: {
    ...mapActions('legal-app-client-store', ['getClientsNotes']),

    async addNewDeadline() {
      if (!this.$refs.addNewDeadlineForm.validate()) return;
      if (this.loading) return;
      this.loading = true;
      try {
        const newDeadline = {
          deadline: this.deadline,
          message: this.form.message,
        };
        let caseId = this.$route.params.case
        await this.$axios.$post(createDeadline(caseId), newDeadline);
        console.warn('nowy termin', newDeadline)
        this.$nuxt.refresh()
        this.$notifier.showSuccessMessage("Termin dodany pomyślnie!");
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
