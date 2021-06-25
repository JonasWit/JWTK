<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom open-on-hover>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn class="mx-3" icon v-on="{ ...tooltip, ...dialog }">
            <v-icon small color="success">mdi-plus</v-icon>
          </v-btn>
        </template>
        <span>Dodaj numer telefonu</span>
      </v-tooltip>
    </template>
    <v-form ref="addNewPhoneNumberForm" v-model="validation.valid">
      <v-card>
        <v-card-text>
          <v-text-field v-model="form.number" :rules="validation.number" label="Dodaj numer telefonu"
                        required></v-text-field>
          <v-text-field v-model="form.comment" :rules="validation.comment" label="Dodaj szczególy"
                        required></v-text-field>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="saveNewPhoneNumber()">
            Dodaj
          </v-btn>
          <v-btn text color="primary" @click="resetForm()">
            Wyczyść
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>

<script>
import {mapActions} from "vuex";
import {notEmptyAndLimitedRule, phoneNumberRule} from "@/data/vuetify-validations";
import {createContactPhoneNumber} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "add-phone-numbers-dialog",
  props: {
    selectedContact: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    dialog: false,
    loading: false,
    form: {
      number: "",
      comment: "",
    },
    validation: {
      valid: false,
      number: phoneNumberRule(),
      comment: notEmptyAndLimitedRule("Nazwa jest wymagana. Dopuszczalna liczba znaków pomiędzy 4 a 50!", 4, 50),
    },
  }),
  methods: {
    ...mapActions('legal-app-client-store', ['getContactDetailsFromFetch']),
    async saveNewPhoneNumber() {
      if (!this.$refs.addNewPhoneNumberForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const phone = {
        comment: this.form.comment,
        number: this.form.number,
      };
      try {
        let clientId = this.$route.params.client
        let contactId = this.selectedContact.id
        await this.$axios.$post(createContactPhoneNumber(clientId, contactId), phone)
        this.$notifier.showSuccessMessage("Numer dodany pomyślnie!");
        this.resetForm();
      } catch (error) {
        console.error('creating contact error', error)
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        let clientId = this.$route.params.client;
        let contactId = this.selectedContact.id;
        await this.getContactDetailsFromFetch({clientId, contactId})
        this.loading = false;
        this.dialog = false;
      }
    },
    resetForm() {
      this.$refs.addNewPhoneNumberForm.reset();
      this.$refs.addNewPhoneNumberForm.resetValidation();
    },

  }

}
</script>

<style scoped>

</style>
