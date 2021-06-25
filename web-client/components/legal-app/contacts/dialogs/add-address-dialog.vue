<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn class="mx-3" icon v-on="{ ...tooltip, ...dialog }">
            <v-icon small color="success">mdi-plus</v-icon>
          </v-btn>
        </template>
        <span>Dodaj nowy adres</span>
      </v-tooltip>
    </template>
    <v-form ref="addNewAddressForm" v-model="validation.valid">
      <v-card>
        <v-card-text>
          <v-text-field v-model="form.comment" :rules="validation.comment" label="Nazwa"
                        required></v-text-field>
          <v-text-field v-model="form.street" :rules="validation.street" label="Ulica"
                        required></v-text-field>
          <v-text-field v-model="form.building" label="Numer budynki i lokalu"
                        required></v-text-field>
          <v-text-field v-model="form.city" label="Miasto"
                        required></v-text-field>
          <v-text-field v-model="form.postCode" label="Kod pocztowy*"
                        :rules="validation.postal"></v-text-field>
          <v-text-field v-model="form.country" label="Państwo"
                        required></v-text-field>
          <small class="grey--text">* Dane opcjonalne</small>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn text color="primary" @click="saveNewAddress()">
            Dodaj
          </v-btn>
          <v-btn text color="error" @click="resetForm()">
            Wyczyść
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>

<script>
import {mapActions} from "vuex";
import {notEmptyAndLimitedRule, postalCode} from "@/data/vuetify-validations";
import {createContactPhysicalAddress} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "add-address-dialog",
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
      comment: "",
      street: "",
      building: "",
      city: "",
      postCode: "",
      country: ""

    },
    loading: false,
    validation: {
      valid: false,
      street: notEmptyAndLimitedRule("Nazwa ulicy jest wymagana! Maksymalna liczba znaków to 50", 2, 50),
      comment: notEmptyAndLimitedRule("Pole jest wymagane. Maksymalna liczba znaków to 50", 0, 50),
      postal: postalCode(),
    },
  }),

  methods: {
    ...mapActions('legal-app-client-store', ['getContactDetailsFromFetch']),
    async saveNewAddress() {
      if (!this.$refs.addNewAddressForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const address = {
        comment: this.form.comment,
        street: this.form.street,
        building: this.form.building,
        city: this.form.city,
        postCode: this.form.postCode,
        country: this.form.country,

      };
      try {
        let clientId = this.$route.params.client
        let contactId = this.selectedContact.id
        await this.$axios.$post(createContactPhysicalAddress(clientId, contactId), address)
        this.$notifier.showSuccessMessage("Kontakt dodany pomyślnie!");
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
      this.$refs.addNewAddressForm.reset();
      this.$refs.addNewAddressForm.resetValidation();
    },
  }
}
</script>

<style scoped>

</style>
