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
                        required :rules="validation.fieldLength"></v-text-field>
          <v-text-field v-model="form.name" :rules="validation.fieldLength" label="Dodaj imię"
          ></v-text-field>
          <v-text-field v-model="form.surname" :rules="validation.fieldLength" label="Dodaj nazwisko"
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
          <v-btn color="success" text @click="resetForm()">
            Wyczyść
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>

<script>
import {lengthRule, notEmptyAndLimitedRule} from "@/data/vuetify-validations";
import {updateContact} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

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
      fieldLength: notEmptyAndLimitedRule("Pole nie może być puste. Dozwolona liczba znaków pomiędzy 4, a 50", 4, 50),
      comment: lengthRule("Maksymalna liczba znaków to 200!", 0, 200)
    },

  }),
  fetch() {
    // this.contact = this.selectedContact;
    this.form.title = this.selectedContact.title;
    this.form.name = this.selectedContact.name;
    this.form.surname = this.selectedContact.surname;
    this.form.comment = this.selectedContact.comment;


  },
  methods: {
    async saveContactChange() {
      if (!this.$refs.editContactNameForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const contact = {
        title: this.form.title,
        name: this.form.name,
        surname: this.form.surname,
        comment: this.form.comment,

      }

      try {
        let clientId = this.$route.params.client
        let contactId = this.selectedContact.id
        await this.$axios.$put(updateContact(clientId, contactId), contact)
        this.$notifier.showSuccessMessage("Kontakt zmieniony pomyślnie!");
      } catch (error) {
        console.error('creating contact error', error)
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.$nuxt.refresh()
        this.loading = false;
        this.dialog = false;
      }

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
