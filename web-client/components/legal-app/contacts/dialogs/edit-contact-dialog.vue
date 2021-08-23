<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-btn color="primary" class="mx-2" medium v-on="{ ...dialog }">
        Edytuj kontakt
      </v-btn>
    </template>
    <v-form ref="editContactNameForm">
      <v-card>
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Edytuj kontakt
          </v-toolbar-title>
        </v-toolbar>
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
          <v-btn color="error" text @click="dialog=false">
            Anuluj
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="saveContactChange()">
            Zapisz zmianę
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
    <progress-bar v-if="loader"/>
  </v-dialog>
</template>

<script>
import {lengthRule} from "@/data/vuetify-validations";
import {updateContact} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "edit-contact-dialog",
  components: {ProgressBar},
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
    loader: false,
    validation: {
      valid: false,
      fieldLength: lengthRule("Dozwolona liczba znaków to 50", 0, 50),
      comment: lengthRule("Maksymalna liczba znaków to 200", 0, 200)
    },

  }),
  fetch() {
    this.form.title = this.selectedContact.title;
    this.form.name = this.selectedContact.name;
    this.form.surname = this.selectedContact.surname;
    this.form.comment = this.selectedContact.comment;
  },
  methods: {
    async saveContactChange() {
      if (!this.$refs.editContactNameForm.validate()) return;
      this.loader = true
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
        handleError(error);
      } finally {
        this.$nuxt.refresh()
        this.loader = false;
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
