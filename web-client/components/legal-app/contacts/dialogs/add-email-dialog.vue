<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn class="mx-3" icon v-on="{ ...tooltip, ...dialog }">
            <v-icon small color="success">mdi-plus</v-icon>
          </v-btn>
        </template>
        <span>Dodaj adres email</span>
      </v-tooltip>
    </template>
    <v-form ref="addNewEmailForm" v-model="validation.valid">
      <v-card>
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Dodaj adres email
          </v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <v-text-field v-model="form.email" :rules="validation.email" label="Dodaj adres email"
                        required></v-text-field>
          <v-text-field v-model="form.comment" :rules="validation.comment" label="Dodaj nazwę" required></v-text-field>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn text color="error" @click="dialog=false">
            Anuluj
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="saveNewEmail()">
            Dodaj
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>
<script>

import {emailRule, notEmptyAndLimitedRule} from "@/data/vuetify-validations";
import {createContactEmail} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "add-email-dialog",
  props: {
    selectedContact: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    dialog: false,
    form: {
      name: "",
      comment: "",
    },
    loading: false,
    validation: {
      valid: false,
      email: emailRule("Proszę podać poprawny format adresu email np. jan-kowalski@gmail.com"),
      comment: notEmptyAndLimitedRule("Pole obowiązkowe. Maksymalan liczba znaków to 50", 1, 50),
    },
  }),

  methods: {
    async saveNewEmail() {
      if (!this.$refs.addNewEmailForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const email = {
        comment: this.form.comment,
        email: this.form.email,
      };
      try {
        let clientId = this.$route.params.client;
        let contactId = this.selectedContact.id;
        await this.$axios.$post(createContactEmail(clientId, contactId), email);
        this.$notifier.showSuccessMessage("Adres email dodany pomyślnie!");
        this.resetForm();
      } catch (error) {
        console.error('creating contact error', error);
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        this.$emit('action-completed');
        this.loading = false;
        this.dialog = false;
      }
    },
    resetForm() {
      this.$refs.addNewEmailForm.reset();
      this.$refs.addNewEmailForm.resetValidation();
    },
  }
};


</script>

<style scoped>

</style>
