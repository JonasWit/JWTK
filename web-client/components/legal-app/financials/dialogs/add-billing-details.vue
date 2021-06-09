<template>
  <v-row class="my-3 mx-3">
    <v-col>
      <v-card>
        <v-toolbar>
          <v-toolbar-title>Twoje dane do faktury</v-toolbar-title>
          <v-dialog v-model="dialog" max-width="500px">
            <template #activator="{ on: dialog }" v-slot:activator="{ on }">
              <v-tooltip bottom>
                <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
                  <v-btn class="mx-3" color="pink" fab dark small
                         v-on="{ ...tooltip, ...dialog }">
                    <v-icon>mdi-plus</v-icon>
                  </v-btn>
                </template>
                <span>Dodaj dane do faktury</span>
              </v-tooltip>
            </template>
            <v-form ref="addBillingDataForm" v-model="validation.valid">
              <v-card>
                <v-card-text>
                  <v-text-field v-model="form.name" :rules="validation.name" label="Nazwa"></v-text-field>
                  <v-text-field v-model="form.street" label="Ulica"></v-text-field>

                  <v-text-field v-model="form.address" required
                                label="Nr budynku i lokalu"></v-text-field>
                  <v-text-field v-model="form.postalCode" required
                                label="Kod pocztowy"></v-text-field>
                  <v-text-field v-model="form.city" required
                                label="Miasto"></v-text-field>
                  <v-text-field v-model="form.phoneNumber" required :rules="validation.phoneNumber"
                                label="Telefon"></v-text-field>
                  <v-text-field v-model="form.faxNumber" required :rules="validation.phoneNumber"
                                label="Fax"></v-text-field>
                  <v-text-field v-model="form.nip" required :rules="validation.numberOnly"
                                label="NIP"></v-text-field>
                  <v-text-field v-model="form.regon" required :rules="validation.numberOnly"
                                label="Regon"></v-text-field>
                </v-card-text>
                <v-divider></v-divider>
                <v-card-actions>
                  <v-btn text color="primary" @click="resetForm()">
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

        <v-card-text v-if="!billingData.length">W tym miejscu możesz dodać dane, które znajdą się na raporcie
          rozliczeniowym:
        </v-card-text>
        <v-row>
          <v-col>
            <v-list>
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-subtitle>Nazwa firmy: {{ billingData.name }}</v-list-item-subtitle>
                  <v-list-item-subtitle>Ulica: {{ billingData.street }}</v-list-item-subtitle>
                  <v-list-item-subtitle>Nr budynku i lokalu: {{ billingData.address }}</v-list-item-subtitle>
                  <v-list-item-subtitle>Kod pocztowy: {{ billingData.postalCode }}</v-list-item-subtitle>
                  <v-list-item-subtitle>Miasto: {{ billingData.city }}</v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-list>
          </v-col>
          <v-col>
            <v-list>
              <v-list-item>
                <v-list-item-content>
                  <v-list-item-subtitle>Nr telefonu: {{ billingData.phoneNumber }}</v-list-item-subtitle>
                  <v-list-item-subtitle>Fax: {{ billingData.faxNumber }}</v-list-item-subtitle>
                  <v-list-item-subtitle>NIP: {{ billingData.nip }}</v-list-item-subtitle>
                  <v-list-item-subtitle>REGON: {{ billingData.regon }}</v-list-item-subtitle>
                </v-list-item-content>
              </v-list-item>
            </v-list>
          </v-col>
        </v-row>


      </v-card>
    </v-col>
  </v-row>


</template>

<script>
import {notEmptyAndLimitedRule, numberOnly, phoneNumberRule} from "@/data/vuetify-validations";

export default {
  name: "add-billing-details",
  data: () => ({
    dialog: false,
    loading: false,
    form: {
      name: "",
      street: "",
      address: "",
      phoneNumber: "",
      faxNumber: "",
      postalCode: "",
      city: "",
      nip: "",
      regon: "",
    },
    billingData: [],
    modal: false,
    validation: {
      valid: false,
      numberOnly: numberOnly(),
      name: notEmptyAndLimitedRule('Nazwa nie może być pusta oraz liczba znaków nie może przekraczać 50 znaków.', 5, 50),
      phoneNumber: phoneNumberRule(),

    },

  }),
  methods: {
    handleSubmit() {
      if (!this.$refs.addBillingDataForm.validate()) return;
      if (this.loading) return;
      this.loading = true;

      const data = {
        name: this.form.name,
        street: this.form.street,
        address: this.form.address,
        phoneNumber: this.form.phoneNumber,
        faxNumber: this.form.faxNumber,
        nip: this.form.nip,
        regon: this.form.regon,
      }

      this.billingData = data
      this.dialog = false
    }
  }

}
</script>

<style scoped>

</style>
