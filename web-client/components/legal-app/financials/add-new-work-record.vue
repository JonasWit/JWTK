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
    <v-form ref="addNewWorkForm">
      <v-card>
        <v-card-text>
          <v-text-field v-model="form.name" label="Dodaj nazwę"></v-text-field>
          <v-text-field v-model="form.description" label="Dodaj krótki opis"></v-text-field>

          <v-text-field v-model="form.hours" label="Dodaj godziny"
          ></v-text-field>
          <v-text-field v-model="form.minutes" label="Dodaj minuty"></v-text-field>
          <v-text-field v-model="form.rate" label="Dodaj stawkę"></v-text-field>
          <v-dialog
            ref="dialog" v-model="modal" :return-value.sync="form.eventDate" persistent width="290px">
            <template v-slot:activator="{ on, attrs }">
              <v-text-field v-model="form.eventDate" label="Wybierz datę" prepend-icon="mdi-calendar" readonly
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
      eventDate: ""
    },
    modal: false,
    date: new Date().toISOString().substr(0, 10),

  }),
  methods: {
    handleSubmit() {

      const workRecord = {
        name: this.form.name,
        description: this.form.description,
        hours: this.form.hours,
        minutes: this.form.minutes,
        rate: this.form.rate,
        eventDate: this.form.eventDate
      };

      return this.$axios.$post(`/api/legal-app-client-finance/client/${this.$route.params.client}/finance-records`, workRecord)
        .then((workRecord) => {
          this.resetForm();
          Object.assign(this.$data, this.$options.data.call(this)); // total data reset (all returning to default data)
          this.$nuxt.refresh();
          console.warn('Nowy work rekord', workRecord)
          this.$notifier.showSuccessMessage("Czas zarejestrowany pomyślnie!");


        })
        .catch((e) => {
          console.warn('create work record error', e);
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        }).finally(() => {
          this.loading = false;
          this.dialog = false;

        });
    }
  }


}
</script>

<style scoped>

</style>
