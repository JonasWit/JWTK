<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn color="white" fab v-on="{ ...tooltip, ...dialog }">
            <v-icon x-large color="success">mdi-plus</v-icon>
          </v-btn>
        </template>
        <span>Dodaj klienta</span>
      </v-tooltip>
    </template>
    <v-form ref="addNewClientForm" v-model="validation.valid">
      <v-card>
        <v-card-text>
          <v-text-field v-model="form.name" :rules="validation.name" label="Dodaj nazwę nowego Klienta"
                        required></v-text-field>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>

          <v-btn text color="primary" @click="handleSubmit">
            Dodaj
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>

</template>

<script>

import {notEmptyAndLimitedRule} from "@/data/vuetify-validations";
import {createClient} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "add-client-dialog",
  data: () => ({
    dialog: false,
    form: {
      name: "",
    },
    validation: {
      valid: false,
      name: notEmptyAndLimitedRule("Nazwa jest wymagana. Dozwolona liczba znaków pomiędzy 4, a 50", 4, 50),
    },
  }),
  methods: {
    async handleSubmit() {
      if (!this.$refs.addNewClientForm.validate()) return;

      const client = {
        name: this.form.name,
      };

      try {
        await this.$axios.$post(createClient(), client);
        this.$notifier.showSuccessMessage("Klient dodany");
        this.resetForm();

      } catch (error) {
        this.$notifier.showErrorMessage(error.response.data);
      } finally {
        Object.assign(this.$data, this.$options.data.call(this));
        this.$nuxt.refresh();
      }
    },
    resetForm() {
      this.$refs.addNewClientForm.reset();
      this.$refs.addNewClientForm.resetValidation();
    },
  }
};
</script>

<style scoped>

</style>
