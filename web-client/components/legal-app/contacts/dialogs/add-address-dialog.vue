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
          <v-text-field v-model="form.postCode" label="Kod pocztowy"
                        required></v-text-field>
          <v-text-field v-model="form.country" label="Państwo"
                        required></v-text-field>
          <small class="grey--text">* Hint text here</small>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn text color="primary" @click="saveNewAddress()">
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
      street: [
        v => !!v || "Nazwa ulicy jest wymagana!",
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
    saveNewAddress() {
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

      return this.$axios.$post(`/api/legal-app-client-contacts/client/${this.$route.params.client}/contact/${this.selectedContact.id}/address`, address)
        .then(() => {
          this.resetForm();
          Object.assign(this.$data, this.$options.data.call(this)); // total data reset (all returning to default data)

          let clientId = this.$route.params.client;
          let contactId = this.selectedContact.id;
          this.getContactDetailsFromFetch({clientId, contactId})

          this.$notifier.showSuccessMessage("Adres dodany pomyślnie!");
        }).catch(() => {
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
        }).finally(() => {
          this.loading = false;
          this.dialog = false;
        })

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
