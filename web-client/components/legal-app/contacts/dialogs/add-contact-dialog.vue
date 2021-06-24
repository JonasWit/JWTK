<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn color="white" class="mx-3" fab v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="success">mdi-account-plus</v-icon>
          </v-btn>
        </template>
        <span>Dodaj kontakt</span>
      </v-tooltip>
    </template>
    <v-form ref="addNewContactForm" v-model="validation.valid">
      <v-card>
        <v-card-text>
          <v-text-field v-model="form.title" label="Dodaj nazwę"
                        required :rules="validation.title"></v-text-field>
          <v-text-field v-model="form.name" :rules="validation.name" label="Dodaj imię*"
          ></v-text-field>
          <v-text-field v-model="form.surname" :rules="validation.surname" label="Dodaj nazwisko*"
          ></v-text-field>
          <v-text-field v-model="form.comment" :rules="validation.comment" label="Dodaj szczególy*"
          ></v-text-field>
          <small class="grey--text">* Dane opcjonalne</small>
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
import {mapActions} from "vuex";
import {lengthRule, notEmptyAndLimitedRule} from "@/data/vuetify-validations";

export default {
  name: "add-contact-dialog",

  data: () => ({
    dialog: false,
    form: {
      title: "",
      name: "",
      surname: "",
      comment: "",

    },
    loading: false,
    validation: {
      valid: false,
      title: notEmptyAndLimitedRule("Nazwa nie może być pusta. Dozwolona liczba znaków pomiędzy 4, a 50", 4, 50),
      name: lengthRule("Dopuszczalna liczba znaków pomiędzy 4 a 50!", 0, 50),
      surname: lengthRule("Dopuszczalna liczba znaków pomiędzy 4 a 50!", 0, 50),
      comment: lengthRule("Dopuszczalna liczba znaków pomiędzy 4 a 200!", 0, 200)
    },

  }),
  methods: {
    ...mapActions('legal-app-client-store', ['getContactDetailsFromFetch']),
    handleSubmit() {
      if (!this.$refs.addNewContactForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const contact = {
        title: this.form.title,
        name: this.form.name,
        surname: this.form.surname,
        comment: this.form.comment,
      };
      return this.$axios.$post(`/api/legal-app-client-contacts/client/${this.$route.params.client}/contact/create`, contact)
        .then(() => {
          this.resetForm();
          Object.assign(this.$data, this.$options.data.call(this)); // total data reset (all returning to default data)
          let clientId = this.$route.params.client;
          let contactId = this.selectedContact.id;
          this.getContactDetailsFromFetch({clientId, contactId})
          this.$notifier.showSuccessMessage("Kontakt dodany pomyślnie!");
        })
        .catch((e) => {
          console.warn(e, 'błąd dodania kontaktu')
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
