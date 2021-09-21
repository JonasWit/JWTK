<template>

  <v-card>
    <v-toolbar>
      <v-toolbar-title>Twoje dane do rozliczenia</v-toolbar-title>
      <v-dialog v-model="dialog" max-width="500px">
        <template #activator="{ on: dialog }" v-slot:activator="{ on }">
          <v-tooltip bottom>
            <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
              <v-btn color="error" class="mx-3" fab v-on="{ ...tooltip, ...dialog }">
                <v-icon>mdi-plus</v-icon>
              </v-btn>
            </template>
            <span>Dodaj dane do rozliczenia</span>
          </v-tooltip>
        </template>
        <v-form ref="addBillingDataForm" v-model="validation.valid">
          <v-card>
            <v-card-text>
              <v-text-field v-model="name" label="Nazwa" required :rules="validation.generalRule"></v-text-field>
              <v-text-field v-model="street" label="Ulica" required :rules="validation.generalRule"></v-text-field>
              <v-text-field v-model="address" label="Nr budynku i lokalu" required
                            :rules="validation.generalRule"></v-text-field>
              <v-text-field v-model="postalCode" label="Kod pocztowy" :rules="validation.postal"></v-text-field>
              <v-text-field v-model="city" label="Miasto" :rules="validation.lengthRule"></v-text-field>
              <v-text-field v-model="phoneNumber" label="Telefon"></v-text-field>
              <v-text-field v-model="faxNumber" label="Fax"></v-text-field>
              <v-text-field v-model="nipNumber" label="NIP"></v-text-field>
              <v-text-field v-model="regonNumber" label="REGON" :rules="validation.regon"></v-text-field>
            </v-card-text>
            <v-divider></v-divider>
            <v-card-actions>
              <v-btn text color="error" @click="resetForm()">
                Wyczyść
              </v-btn>
              <v-spacer></v-spacer>
              <v-btn text color="primary" @click="handleSubmit()">
                Dodaj
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-form>
      </v-dialog>
    </v-toolbar>
  </v-card>
</template>
<script>


import {lengthRule, notEmptyAndLimitedRule, postalCode} from "@/data/vuetify-validations";
import BillingDetailsList from "@/components/legal-app/financials/billing-details-list";
import {mapActions} from "vuex";
import {addBillingData} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {handleError} from "@/data/functions";

export default {
  name: "add-billing-details",
  components: {BillingDetailsList},
  data: () => ({
    dialog: false,
    billingData: [],
    modal: false,
    name: '',
    street: '',
    address: '',
    phoneNumber: '',
    faxNumber: '',
    nipNumber: '',
    regonNumber: '',
    postalCode: '',
    city: '',

    validation: {
      valid: false,
      generalRule: notEmptyAndLimitedRule('Pole nie może być puste oraz liczba znaków nie może przekraczać 50', 1, 50),
      postal: postalCode(),
      nip: lengthRule('Poprawna liczba znaków dla numeru NIP to 10. Sprawdź poprawność danych.', 10, 10),
      regon: lengthRule('Liczba znaków nie może przekraczać 20', 10, 20),

    }
  }),

  watch: {
    phoneNumber() {
      if (this.phoneNumber) {
        let realNumber = this.phoneNumber.replace(/-/gi, '');
        let dashedNumber = realNumber.match(/.{1,3}/g);
        this.phoneNumber = dashedNumber.join('-');
      }
    },
    postalCode() {
      if (this.postalCode) {
        let realNumber = this.postalCode.replace(/-/gi, '');
        let dashedNumber = realNumber.replace(/^(?=[0-9]{5})([0-9]{2})([0-9]{3})$/, "$1-$2");
        this.postalCode = dashedNumber;
      }
    },

    faxNumber() {
      if (this.faxNumber) {
        let realNumber = this.faxNumber.replace(/-/gi, '');
        let dashedNumber = realNumber.replace(/^(?=[0-9]{9})([0-9]{2})([0-9]{3})([0-9]{2})([0-9]{2})$/, "$1-$2-$3-$4");
        this.faxNumber = dashedNumber;
      }
    },

    nipNumber() {
      if (this.nipNumber) {
        let realNumber = this.nipNumber.replace(/-/gi, '');
        let dashedNumber = realNumber.replace(/^(?=[0-9]{10})([0-9]{3})([0-9]{3})([0-9]{2})([0-9]{2})$/, "$1-$2-$3-$4");
        this.nipNumber = dashedNumber;
      }
    }
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getBillingDataFromFetch']),
    async handleSubmit() {
      if (!this.$refs.addBillingDataForm.validate()) return;
      const data = {
        name: this.name,
        street: this.street,
        address: this.address,
        phoneNumber: this.phoneNumber,
        faxNumber: this.faxNumber,
        nip: this.nipNumber,
        regon: this.regonNumber,
        postalCode: this.postalCode,
      };
      try {
        await this.$axios.$post(addBillingData(), data);
        this.$notifier.showSuccessMessage("Dane zostały uzupełnione pomyślnie");
        this.resetForm();
      } catch (error) {
        handleError(error);
      } finally {
        this.getBillingDataFromFetch();
        this.dialog = false;
        this.$refs.addBillingDataForm.reset();
      }
    },
    resetForm() {
      this.$refs.addBillingDataForm.reset();
      this.$refs.addBillingDataForm.resetValidation();
    },
  }
};
</script>

<style scoped>

</style>
