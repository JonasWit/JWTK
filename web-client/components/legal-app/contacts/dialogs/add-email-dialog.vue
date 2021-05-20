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
        <v-card-text>
          <v-text-field v-model="form.email" :rules="validation.email" label="Dodaj adres email"
                        required></v-text-field>
          <v-text-field v-model="form.comment" :rules="validation.comment" label="Dodaj nazwę"
                        required></v-text-field>
          <small class="grey--text">* Hint text here</small>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
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

import {mapActions} from "vuex";

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
      email: [
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
    ...mapActions('legal-app-client-store', ['getContactDetailsFromFetch']),
    saveNewEmail() {
      if (!this.$refs.addNewEmailForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const email = {
        comment: this.form.comment,
        email: this.form.email,
      };

      return this.$axios.$post(`/api/legal-app-client-contacts/client/${this.$route.params.client}/contact/${this.selectedContact.id}/emails`, email)
        .then(() => {
          this.resetForm();
          Object.assign(this.$data, this.$options.data.call(this)); // total data reset (all returning to default data)

          let clientId = this.$route.params.client;
          let contactId = this.selectedContact.id;
          this.getContactDetailsFromFetch({clientId, contactId})

          this.$notifier.showSuccessMessage("Adres email dodany pomyślnie!");
        }).catch(() => {
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        }).finally(() => {
          this.loading = false;
          this.dialog = false;
        })

    },
    resetForm() {
      this.$refs.addNewEmailForm.reset();
      this.$refs.addNewEmailForm.resetValidation();
    },
  }
}


</script>

<style scoped>

</style>
