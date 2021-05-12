<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template v-slot:activator="{ on, attrs }">
      <v-btn class="mx-3" icon v-bind="attrs" v-on="on">
        <v-icon medium color="success">mdi-plus</v-icon>
      </v-btn>
    </template>
    <v-form ref="addNewClientForm" v-model="validation.valid">
      <v-card-text>
        <v-text-field v-model="form.name" :rules="validation.name" label="Dodaj nowego Klienta"
                      required></v-text-field>
        <small class="grey--text">* Hint text here</small>
      </v-card-text>
      <v-card-actions>
        <v-spacer></v-spacer>
        <v-btn text color="primary" @click="handleSubmit()">
          Dodaj
        </v-btn>
      </v-card-actions>
    </v-form>
  </v-dialog>
</template>

<script>
export default {
  name: "client-create-dialog",
  data: () => ({
    dialog: false,
    form: {
      name: "",

    },
    validation: {
      valid: false,
      name: [
        v => !!v || "Nazwa jest wymagana!",
        v => (v?.length >= 10 && v?.length <= 50) || "Between 10 and 50 characters!",
      ],
    },

  }),
  methods: {
    handleSubmit() {
      if (!this.$refs.addNewClientForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const client = {
        name: this.form.name,
      };
      return this.$axios.$post("/api/legal-app-clients/create", client)
        .then((resp) => {
          console.warn('create client response', resp);
          this.resetForm();
          this.$notifier.showSuccessMessage("Klient dodany pomyślnie!");
        })
        .catch((e) => {
          console.warn('create client error', e);
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        }).finally(() => {
          this.$nuxt.refresh();
          this.loading = false;
          this.dialog = false;

        });


    },
    resetForm() {
      this.$refs.addNewClientForm.reset();
      this.$refs.addNewClientForm.resetValidation();
    },
  }


}
</script>

<style scoped>

</style>
