<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn class="mx-3" fab v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="success">mdi-account-plus</v-icon>
          </v-btn>
        </template>
        <span>Dodaj kontakt</span>
      </v-tooltip>
    </template>
    <v-form ref="addNewContactForm" v-model="validation.valid">
      <v-card>
        <v-card-text>
          <v-text-field v-model="form.name" :rules="validation.name" label="Dodaj nazwę"
                        required></v-text-field>
          <v-text-field v-model="form.comment" :rules="validation.comment" label="Dodaj szczególy"
                        required></v-text-field>
          <small class="grey--text">* Hint text here</small>
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
  name: "add-contact-dialog",
  data: () => ({
    dialog: false,
    form: {
      name: "",
      comment: "",

    },
    validation: {
      valid: false,
      name: [
        v => !!v || "Nazwa jest wymagana!",
        v => (v?.length >= 10 && v?.length <= 50) || "Between 10 and 50 characters!",
      ],
      comment: [
        v => !!v || "Nazwa jest wymagana!",
        v => (v?.length >= 10 && v?.length <= 50) || "Between 10 and 50 characters!",
      ],
    },

  }),
  methods: {
    handleSubmit() {
      if (!this.$refs.addNewContactForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const contact = {
        comment: this.form.comment,
        name: this.form.name,

      };
      return this.$axios.$post(`/api/legal-app-client-contacts/client/${this.$route.params.client}/contact/create`, contact)
        .then(() => {
          this.resetForm();
          Object.assign(this.$data, this.$options.data.call(this)); // total data reset (all returning to default data)
          this.$nuxt.refresh()
          console.warn('Contact list refreshed after client creation', contact)
          this.$notifier.showSuccessMessage("Kontakt dodany pomyślnie!");

        })
        .catch((e) => {
          console.warn('create contact error', e);
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        }).finally(() => {
          this.loading = false;
          this.dialog = false;
        });


    },
    resetForm() {
      this.$refs.addNewContactForm.reset();
      this.$refs.addNewContactForm.resetValidation();
    },


  }
}
</script>

<style scoped>

</style>
