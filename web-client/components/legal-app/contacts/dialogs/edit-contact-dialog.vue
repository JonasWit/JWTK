<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="success">mdi-file-document-edit</v-icon>
          </v-btn>
        </template>
        <span>Edytuj Kontakt</span>
      </v-tooltip>
    </template>
    <v-form ref="editContactNameForm" v-model="validation.valid">
      <v-card>
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Edytuj kontakt
          </v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <v-text-field v-model="form.title" label="Dodaj nazwę" required
                        :rules="[v => !!v, v => (v && v.length <= 50) || 'Pole obowiązkowe. Dozwolona liczba znaków to 50']"></v-text-field>
          <v-text-field v-model="form.name" :rules="[v => (v.length <= 50) || 'Dozwolona liczba znaków to 50']"
                        label="Dodaj imię"></v-text-field>
          <v-text-field v-model="form.surname" :rules="[v => (v.length <= 50) || 'Dozwolona liczba znaków to 50']"
                        label="Dodaj nazwisko"></v-text-field>
          <v-text-field v-model="form.comment" :rules="[v => (v.length <= 200) || 'Dozwolona liczba znaków to 200']"
                        label="Dodaj szczególy*"></v-text-field>
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
  </v-dialog>
</template>

<script>
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
    validation: {
      valid: false,
    },

  }),
  fetch() {
    this.form.title = this.selectedContact.title ? this.selectedContact.title : "";
    this.form.name = this.selectedContact.name ? this.selectedContact.name : "";
    this.form.surname = this.selectedContact.surname ? this.selectedContact.surname : "";
    this.form.comment = this.selectedContact.comment ? this.selectedContact.comment : "";
    console.warn("FORM", this.form);
  },
  methods: {
    async saveContactChange() {
      if (!this.$refs.editContactNameForm.validate()) return;
      const contact = {
        title: this.form.title,
        name: this.form.name,
        surname: this.form.surname,
        comment: this.form.comment,
      };
      try {
        let clientId = this.$route.params.client;
        let contactId = this.selectedContact.id;
        await this.$axios.$put(updateContact(clientId, contactId), contact);
        this.$notifier.showSuccessMessage("Kontakt zmieniony pomyślnie!");
      } catch (error) {
        handleError(error);
      } finally {
        this.$nuxt.refresh();
        this.resetForm();


      }
    },
    resetForm() {
      this.$refs.editContactNameForm.reset();
      this.$refs.editContactNameForm.resetValidation();
      this.dialog = false;
    },
  }
};
</script>

<style scoped>

</style>
