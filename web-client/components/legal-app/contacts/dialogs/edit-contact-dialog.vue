<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn color="primary" class="mx-2" medium v-on="{ ...tooltip, ...dialog }">
            Edytuj kontakt
          </v-btn>
        </template>
        <span>Edytuj kontakt</span>
      </v-tooltip>
    </template>
    <v-form ref="editContactNameForm">
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

          <v-btn text color="primary" @click="saveContactChange()">
            Zapisz zmianę
          </v-btn>
          <v-btn color="success" text @click="dialog = false">
            Anuluj
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>

<script>
import {lengthRule, notEmptyAndLimitedRule} from "@/data/vuetify-validations";

export default {
  name: "edit-contact-dialog",
  props: {
    selectedContact: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    contact: null,
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
      title: notEmptyAndLimitedRule("Nazwa jest wymagana. Dozwolona liczba znaków pomiędzy 4, a 50", 4, 50),
      name: lengthRule("Dopuszczalna liczba znaków pomiędzy 4 a 50!", 4, 50),
      surname: lengthRule("Dopuszczalna liczba znaków pomiędzy 4 a 50!", 4, 50),
      comment: lengthRule("Dopuszczalna liczba znaków pomiędzy 4 a 200!", 4, 200)
    },

  }),
  fetch() {
    this.contact = this.selectedContact;
    this.form.title = this.selectedContact.title;
    this.form.name = this.selectedContact.name;
    this.form.surname = this.selectedContact.surname;
    this.form.comment = this.selectedContact.comment;


  },
  methods: {

    saveContactChange() {
      if (!this.$refs.editContactNameForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const contact = {
        title: this.contact.title,
        name: this.contact.name,
        surname: this.contact.surname,
        comment: this.contact.comment,

      }

      this.$axios.$put(`/api/legal-app-client-contacts/client/${this.$route.params.client}/contact/${this.selectedContact.id}`, contact)
        .then(() => {
          this.$notifier.showSuccessMessage("Kontakt updatowany pomyślnie!");
        })
        .catch(() => {
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        }).finally(() => {
        this.setContactForAction(this.selectedContact)
        this.dialog = false;
      });

    },
    resetForm() {
      this.$refs.editContactNameForm.reset();
      this.$refs.editContactNameForm.resetValidation();
    },
  }
}
</script>

<style scoped>

</style>
