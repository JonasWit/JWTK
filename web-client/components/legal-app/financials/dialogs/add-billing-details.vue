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
                  <v-text-field v-model="form.name" label="Nazwa"
                                required :rules="validation.notEmpty"></v-text-field>
                  <v-text-field v-model="form.street" label="Ulica"
                                required></v-text-field>
                  <v-text-field v-model="form.address" label="Nr budynku i lokalu" required
                  ></v-text-field>
                  <text-field-wrapper v-model="form.postalCode" filed-label="Kod pocztowy"
                                      :map-operation="postalCodeMapping" :validation="validation.notEmpty"/>
                </v-card-text>
                <!--                <text-field-wrapper v-model="form.name" filed-label="Nazwa" :rules="validation.name"/>-->


                <!--                <v-card-text>-->
                <!--                  <v-text-field v-model="form.postalCode" required-->
                <!--                                label="Kod pocztowy"></v-text-field>-->
                <!--                  <v-text-field v-model="form.city" required-->
                <!--                                label="Miasto"></v-text-field>-->
                <!--                  <v-text-field v-model="form.phoneNumber" required-->
                <!--                                label="Telefon"></v-text-field>-->
                <!--                  <v-text-field v-model="form.faxNumber" required-->
                <!--                                label="Fax"></v-text-field>-->
                <!--                  <v-text-field v-model="form.nip" required-->
                <!--                                label="NIP"></v-text-field>-->
                <!--                  <v-text-field v-model="form.regon" required-->
                <!--                                label="Regon"></v-text-field>-->
                <!--                </v-card-text>-->
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
        <billing-details/>
      </v-card>
    </v-col>
  </v-row>


</template>

<script>

import BillingDetails from "@/components/legal-app/financials/billing-details";
import AddBillingDataInputFields from "@/components/legal-app/financials/dialogs/add-billing-data-input-fields";
import TextFieldWrapper from "@/components/legal-app/financials/dialogs/text-field-wrapper";
import {notEmptyAndLimitedRule,} from "@/data/vuetify-validations";

export default {
  name: "add-billing-details",
  components: {TextFieldWrapper, AddBillingDataInputFields, BillingDetails},
  data: () => ({
    dialog: false,
    loading: false,
    form: {
      name: "",
      street: "",
      address: "",
      // phoneNumber: "",
      // faxNumber: "",
      postalCode: "",
      // city: "",
      // nip: "",
      // regon: "",
    },
    billingData: [],
    modal: false,
    validation: {
      valid: false,
      notEmptyAndLenght: notEmptyAndLimitedRule()
    },

  }),

  methods: {
    async handleSubmit() {
      // if (!this.$refs.addBillingDataForm.validate()) return;
      // if (this.loading) return;
      // this.loading = true;

      const data = {
        name: this.form.name,
        street: this.form.street,
        address: this.form.address,
        // phoneNumber: this.phoneNumber,
        // faxNumber: this.form.faxNumber,
        // nip: this.form.nip,
        // regon: this.form.regon,
        postalCode: this.form.postalCode,
      }

      console.warn('billingData', data)
      try {
        await this.$axios.$post('/api/legal-app-billing/create', data);
        this.$nuxt.refresh();
        this.$notifier.showSuccessMessage("Dane zostały uzupełnione pomyślnie");

      } catch (e) {
        this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj jeszcze raz!");
      } finally {
        this.dialog = false;

      }


    },

    postalCodeMapping(text) {
      return text = text.replace(/-/g, "").replace(/^(?=[0-9]{5})([0-9]{2})([0-9]{3})$/, "$1-$2");
    },

    // testmapping(text) {
    //   console.warn("mapping function", text)
    //   let realNumber = text.replace(/-/gi, '');
    //   let dashedNumber = realNumber.match(/.{1,3}/g);
    //   return dashedNumber.join('-');
    // }
  }

}
</script>

<style scoped>

</style>
