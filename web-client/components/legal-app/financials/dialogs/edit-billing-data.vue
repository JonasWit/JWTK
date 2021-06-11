<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn icon v-on="{ ...tooltip, ...dialog }">
            <v-icon medium color="success">mdi-file-document-edit</v-icon>
          </v-btn>
        </template>
        <span>Edytuj rekord</span>
      </v-tooltip>
    </template>
    <v-form ref="editBillingDataForm" v-model="validation.valid">
      <v-card>
        <v-card-text>
          <v-text-field v-model="billingRecord.name" label="Nazwa" required
                        :rules="validation.generalRule"></v-text-field>
          <v-text-field v-model="billingRecord.street" label="Ulica" required
                        :rules="validation.generalRule"></v-text-field>
          <v-text-field v-model="billingRecord.address" label="Nr budynku i lokalu" required
                        :rules="validation.generalRule"></v-text-field>
          <v-text-field v-model="billingRecord.postalCode" label="Kod pocztowy"
                        :rules="validation.postal"></v-text-field>
          <v-text-field v-model="billingRecord.city" label="Miasto"
                        :rules="validation.city"></v-text-field>
          <v-text-field v-model="billingRecord.phoneNumber"
                        label="Telefon"></v-text-field>
          <v-text-field v-model="billingRecord.faxNumber" label="Fax"></v-text-field>
          <v-text-field v-model="billingRecord.nip" label="NIP"></v-text-field>
          <v-text-field v-model="billingRecord.regon" label="REGON" :rules="validation.regon"></v-text-field>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn text color="primary" @click="saveBillingDataChange()">
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
import {lengthRule, notEmptyAndLimitedRule, postalCode} from "@/data/vuetify-validations";

export default {
  name: "edit-billing-data",
  props: {
    selectedBillingRecord: {
      required: true,
      type: Object,
      default: null
    }

  },
  data: () => ({
    dialog: false,
    loading: false,
    billingRecord: null,
    modal: false,
    name: '',
    street: '',
    address: '',
    phoneNumber: '',
    faxNumber: '',
    nip: '',
    regon: '',
    postalCode: '',
    city: '',

    validation: {
      valid: false,
      generalRule: notEmptyAndLimitedRule('Pole nie może być puste oraz liczba znaków nie może przekraczać 50', 3, 50),
      postal: postalCode(),
      nip: lengthRule('Poprawna liczba znaków dla numeru NIP to 10. Sprawdź poprawność danych.', 10, 10),
      regon: lengthRule('Liczba znaków nie może przekraczać 20', 10, 20),
      city: lengthRule('Liczba znaków nie może przekraczać 50', 3, 50)

    }
  }),
  async fetch() {
    this.billingRecord = this.selectedBillingRecord

  },

  methods: {
    async saveBillingDataChange() {
      if (!this.$refs.editBillingDataForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const data = {
        name: this.billingRecord.name,
        street: this.billingRecord.street,
        address: this.billingRecord.address,
        phoneNumber: this.billingRecord.phoneNumber,
        faxNumber: this.billingRecord.faxNumber,
        nip: this.billingRecord.nip,
        regon: this.billingRecord.regon,
        postalCode: this.billingRecord.postalCode,
      }

      console.warn('edited billingData', data)
      try {
        await this.$axios.$put(`/api/legal-app-billing/update/${this.billingRecord.id}`, data);
        this.$nuxt.refresh();
        this.$notifier.showSuccessMessage("Dane zostały uzupełnione pomyślnie");

      } catch (e) {
        this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
      } finally {
        this.dialog = false;

      }


    }


  },

}
</script>

<style scoped>

</style>
