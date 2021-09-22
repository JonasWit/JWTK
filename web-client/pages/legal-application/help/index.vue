<template>
  <layout>
    <template v-slot:content>
      <v-toolbar color="primary" dark>
        <v-toolbar-title>
          Pomoc
        </v-toolbar-title>
      </v-toolbar>

      <v-form ref="contactForm" v-model="validation.valid">
        <v-card elevation="2" class="py-7 px-7" outlined>
          <v-select v-model="form.selectedCategory" :items="items" item-text="text" :item-value="value" return-object
                    label="Wybierz temat" required></v-select>
          <v-textarea v-model="form.message" label="Wiadomość" required :rules="validation.message" name="input-7-1"
                      filled auto-grow></v-textarea>
          <v-card-actions>
            <v-btn class="mt-3" @click="reset" color="warning">
              Wyczyść
            </v-btn>
            <v-spacer></v-spacer>
            <v-btn :disabled="!valid" @click.prevent="submit" class="mr-4 mt-3" color="error">
              Wyślij
            </v-btn>
          </v-card-actions>
        </v-card>
      </v-form>

    </template>
  </layout>

</template>

<script>
import Layout from "@/components/legal-app/layout";
import {notEmptyAndLimitedRule} from "@/data/vuetify-validations";

export default {
  name: "index",
  components: {Layout},
  middleware: ['legal-app-permission', 'user', 'authenticated'],
  data: () => ({
    items: [
      {text: 'Zgłaszam błąd'},
      {text: 'Potrzebuję pomocy'},
      {text: 'Proszę o dodanie użytkownika'},
      {text: 'Proszę o zmianę funkcjonalności'},
      {text: 'Proszę o nową funkcjonalność'}
    ],
    valid: true,
    value: null,
    validation: {
      message: notEmptyAndLimitedRule("Nazwa jest wymagana. Dozwolona liczba znaków pomiędzy 4, a 5000", 4, 5000)
    },
    form: {
      selectedCategory: null,
      message: null,
    },
  }),
  methods: {
    async submit() {
      if (!this.$refs.contactForm.validate()) return;
      try {
        const payload = {
          subject: this.form.selectedCategory.text,
          body: this.form.message,
        };
        console.log('payload', payload);
        await this.$axios.$post('/api/portal/contact/lapp/support-request', payload);
        this.$notifier.showSuccessMessage("Wiadomość wysłana pomyślnie!");
      } catch (e) {
        if (e?.response?.status === 429) {
          this.$notifier.showWarningMessage("Spróbuj ponownie za chwilę");
        } else {
          console.warn('error', e);
          this.$notifier.showErrorMessage("Wystąpił błąd, spróbuj ponownie");
        }
      } finally {
        this.$refs.contactForm.reset();
      }
    },
    reset() {
      this.$refs.contactForm.reset();
    }
  }
};
</script>

<style scoped>

</style>
