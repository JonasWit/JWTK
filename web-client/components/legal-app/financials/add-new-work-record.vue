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
    <v-form ref="createClientWorkForm">
      <v-card>
        <v-card-text>
          <v-text-field v-model="form.name" label="Dodaj nazwę"></v-text-field>
          <v-text-field v-model="form.description" label="Dodaj krótki opis"></v-text-field>

          <v-text-field v-model="form.hours" label="Dodaj godziny"></v-text-field>
          <v-text-field v-model="form.minutes" label="Dodaj minuty"></v-text-field>
          <v-text-field v-model.number="form.rate" label="Dodaj stawkę" type="number"></v-text-field>
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

  // computed: {
  //
  //   hoursSpent() {
  //     if (!this.form.hours) {
  //       return parseInt(this.form.hours)
  //     } else {
  //       return this.form.hours
  //     }
  //   },
  //
  //   minutesSpent() {
  //     return parseInt(this.form.minutes)
  //   }
  // },

  methods: {
    handleSubmit() {

      let hoursSpent = parseInt(this.form.hours)
      let minutesSpent = parseInt(this.form.minutes)

      console.warn('parseInt godziny', hoursSpent)
      console.warn('parseInt minuty', minutesSpent)


      const workRecord = {
        name: this.form.name,
        description: this.form.description,
        hours: hoursSpent,
        minutes: minutesSpent,
        rate: this.form.rate,
        eventDate: this.form.eventDate
      };

      console.warn('Nowy work rekord: godziny', this.hours)
      console.warn('Nowy work rekord: minuty', this.hours)

      return this.$axios.$post(`/api/legal-app-clients-finance/client/${this.$route.params.client}/finance-records`, workRecord)
        .then((workRecord) => {


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
    }
  }


}
</script>

<style scoped>

</style>
