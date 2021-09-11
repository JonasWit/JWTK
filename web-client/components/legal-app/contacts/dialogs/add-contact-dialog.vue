<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn color="white" class="mx-3" fab v-on="{ ...tooltip, ...dialog }">
            <v-icon x-large color="success">mdi-plus</v-icon>
          </v-btn>
        </template>
        <span>Dodaj kontakt</span>
      </v-tooltip>
    </template>
    <v-form ref="addNewContactForm" v-model="validation.valid">
      <v-card>
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Dodaj kontakt
          </v-toolbar-title>
        </v-toolbar>
        <v-alert v-if="legalAppTooltips" elevation="5" text type="info">
          Dodaj nowy punkt kontaktu dla Klienta, a następie utwórz swoją bazę adresową. Po dodaniu nazwy głównej
          uzyskasz dostęp do szczegółowych sekcji adresowych.
        </v-alert>
        <v-card-text>
          <v-text-field v-model="form.title" label="Dodaj nazwę"
                        required :rules="validation.title"></v-text-field>
          <v-text-field v-model="form.name" :rules="validation.name" label="Dodaj imię*"
          ></v-text-field>
          <v-text-field v-model="form.surname" :rules="validation.surname" label="Dodaj nazwisko*"
          ></v-text-field>
          <v-text-field v-model="form.comment" :rules="validation.comment" label="Dodaj komentarz*"
          ></v-text-field>
          <small class="grey--text">* Dane opcjonalne</small>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn text color="error" @click="resetForm()">
            Anuluj
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="handleSubmit()">
            Dodaj
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
    <progress-bar v-if="loader"/>
  </v-dialog>
</template>

<script>
import {lengthRule, notEmptyAndLimitedRule} from "@/data/vuetify-validations";
import {createContact} from "@/data/endpoints/legal-app/legal-app-client-endpoints";
import {mapState} from "vuex";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "add-contact-dialog",
  components: {ProgressBar},
  data: () => ({
    dialog: false,
    form: {
      title: "",
      name: "",
      surname: "",
      comment: "",

    },
    loader: false,
    validation: {
      valid: false,
      title: notEmptyAndLimitedRule("Pole obowiązkowe. Maksymalan liczba znaków to 50", 1, 50),
      name: lengthRule("Maksymalan liczba znaków to 50", 0, 50),
      surname: lengthRule("Maksymalan liczba znaków to 50", 0, 50),
      comment: lengthRule("Maksymalan liczba znaków to 200", 0, 200)
    },

  }),
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips'])
  },

  methods: {

    async handleSubmit() {
      if (!this.$refs.addNewContactForm.validate()) return;
      const contact = {
        title: this.form.title,
        name: this.form.name,
        surname: this.form.surname,
        comment: this.form.comment,
      };
      try {
        let clientId = this.$route.params.client
        await this.$axios.$post(createContact(clientId), contact)
        this.$notifier.showSuccessMessage("Kontakt dodany pomyślnie!");
      } catch (error) {
        handleError(error);
      } finally {
        this.$refs.addNewContactForm.reset();
        this.$refs.addNewContactForm.resetValidation();
        this.$nuxt.refresh()
        this.dialog = false;
      }
    },
    resetForm() {
      this.$refs.addNewContactForm.reset();
      this.$refs.addNewContactForm.resetValidation();
      this.dialog = false;
    },
  }
}
</script>

<style scoped>

</style>
