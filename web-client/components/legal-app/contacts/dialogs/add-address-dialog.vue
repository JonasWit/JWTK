<template>
  <v-dialog v-model="dialog1" max-width="500px">
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
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Dodaj adres
          </v-toolbar-title>
        </v-toolbar>
        <v-card-text>
          <v-text-field v-model="form.comment" :rules="validation.comment" label="Nazwa"
                        required></v-text-field>
          <v-text-field v-model="form.street" :rules="validation.street" label="Ulica"
                        required></v-text-field>
          <v-text-field v-model="form.building" label="Numer budynki i lokalu*"
                        required></v-text-field>
          <v-text-field v-model="form.city" label="Miasto*"
                        required></v-text-field>
          <v-text-field v-model="form.postCode" label="Kod pocztowy*"
                        :rules="validation.postal"></v-text-field>
          <v-text-field v-model="form.country" label="Państwo*"
                        required></v-text-field>
          <small class="grey--text">* Dane opcjonalne</small>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn text color="error" @click="dialog1=false">
            Anuluj
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="saveNewAddress()">
            Dodaj
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
    <progress-bar v-if="loader"/>
  </v-dialog>
</template>

<script>
import {notEmptyAndLimitedRule, postalCode} from "@/data/vuetify-validations";
import {createContactPhysicalAddress} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {handleError} from "~/data/functions";
import ProgressBar from "@/components/legal-app/progress-bar";

export default {
  name: "add-address-dialog",
  components: {ProgressBar},
  props: {
    selectedContact: {
      required: true,
      type: Object,
      default: null
    }
  },
  data: () => ({
    dialog1: false,
    form: {
      comment: "",
      street: "",
      building: "",
      city: "",
      postCode: "",
      country: ""

    },
    loader: false,
    validation: {
      valid: false,
      street: notEmptyAndLimitedRule("Pole obowiązkowe. Maksymalna liczba znaków to 50", 1, 50),
      comment: notEmptyAndLimitedRule("Pole obowiązkowe. Maksymalna liczba znaków to 50", 1, 50),
      postal: postalCode(),
      city: notEmptyAndLimitedRule("Pole obowiązkowe. Maksymalna liczba znaków to 50", 1, 50)
    },
  }),

  methods: {
    async saveNewAddress() {
      if (!this.$refs.addNewAddressForm.validate()) return;
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
        handleError(error)
      } finally {
        this.$emit('action-completed');
        this.dialog1 = false;
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
