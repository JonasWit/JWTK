<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn elevation="2" small class="mx-2" color="secondary" v-on="{ ...tooltip, ...dialog }">
            Edytuj
          </v-btn>
        </template>
        <span>Edytuj rekord</span>
      </v-tooltip>
    </template>
    <v-form ref="editBillingDataForm" v-model="validation.valid">
      <v-card>
        <v-card-text>
          <v-text-field v-model="form.name" label="Nazwa" required
                        :rules="validation.generalRule"></v-text-field>
          <v-text-field v-model="form.street" label="Ulica" required
                        :rules="validation.generalRule"></v-text-field>
          <v-text-field v-model="form.address" label="Nr budynku i lokalu" required
                        :rules="validation.generalRule"></v-text-field>
          <v-text-field v-model="form.postalCode" label="Kod pocztowy"
                        :rules="validation.postal"></v-text-field>
          <v-text-field v-model="form.city" label="Miasto"
                        :rules="validation.city"></v-text-field>
          <v-text-field v-model="form.phoneNumber"
                        label="Telefon"></v-text-field>
          <v-text-field v-model="form.faxNumber" label="Fax"></v-text-field>
          <v-text-field v-model="form.nip" label="NIP"></v-text-field>
          <v-text-field v-model="form.regon" label="REGON" :rules="validation.regon"></v-text-field>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn color="error" text @click="dialog = false">
            Anuluj
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="saveBillingDataChange()">
            Zapisz zmianę
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
    <progress-bar v-if="loader"/>
  </v-dialog>

</template>

<script>
import {lengthRule, notEmptyAndLimitedRule, postalCode} from "@/data/vuetify-validations";
import {mapActions} from "vuex";
import {updateBillingData} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "edit-billing-data",
  components: {ProgressBar},
  props: {
    selectedBillingRecord: {
      required: true,
      type: Object,
      default: null
    }

  },
  data: () => ({
    dialog: false,
    loader: false,
    billingRecord: null,
    modal: false,
    form: {
      name: '',
      street: '',
      address: '',
      phoneNumber: '',
      faxNumber: '',
      nip: '',
      regon: '',
      postalCode: '',
      city: '',
    },

    validation: {
      valid: false,
      generalRule: notEmptyAndLimitedRule('Pole nie może być puste oraz liczba znaków nie może przekraczać 50', 3, 50),
      postal: postalCode(),
      nip: lengthRule('Poprawna liczba znaków dla numeru NIP to 10. Sprawdź poprawność danych.', 10, 10),
      regon: lengthRule('Liczba znaków nie może przekraczać 20', 10, 20),
      city: lengthRule('Liczba znaków nie może przekraczać 50', 0, 50)

    }
  }),
  fetch() {
    this.billingRecord = this.selectedBillingRecord
    this.form.name = this.billingRecord.name
    this.form.street = this.billingRecord.street
    this.form.address = this.billingRecord.address
    this.form.phoneNumber = this.billingRecord.phoneNumber
    this.form.faxNumber = this.billingRecord.faxNumber
    this.form.nip = this.billingRecord.nip
    this.form.regon = this.billingRecord.regon
    this.form.postalCode = this.billingRecord.postalCode
    this.form.city = this.billingRecord.city

  },
  methods: {
    ...mapActions('legal-app-client-store', ['getBillingDataFromFetch']),
    async saveBillingDataChange() {
      if (!this.$refs.editBillingDataForm.validate()) return;
      this.loader = true;
      const data = {
        name: this.form.name,
        street: this.form.street,
        address: this.form.address,
        phoneNumber: this.form.phoneNumber,
        faxNumber: this.form.faxNumber,
        nip: this.form.nip,
        regon: this.form.regon,
        postalCode: this.form.postalCode,
        city: this.form.city
      }
      try {
        let billingRecordId = this.billingRecord.id
        await this.$axios.$put(updateBillingData(billingRecordId), data);
        this.$notifier.showSuccessMessage("Dane zostały uzupełnione pomyślnie");
      } catch (error) {
        handleError(error);
      } finally {
        setTimeout(() => {
          this.getBillingDataFromFetch()
          this.dialog = false;
          this.loader = false;
        }, 1500)
      }
    }
  },
}
</script>

<style scoped>

</style>
