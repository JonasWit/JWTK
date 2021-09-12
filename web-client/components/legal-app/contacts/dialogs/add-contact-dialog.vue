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
                        required :rules="[validation.mandatory, validation.counter]"
          ></v-text-field>
          <v-text-field v-model="form.name" :rules="[validation.counter]"
                        label="Dodaj imię*"
          ></v-text-field>
          <v-text-field v-model="form.surname" :rules="[validation.counter]"
                        label="Dodaj nazwisko*"
          ></v-text-field>
          <v-text-field v-model="form.comment" :rules="[validation.maximum]"
                        label="Dodaj komentarz*"
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
      mandatory: v => !!v || 'Pole obowiązkowe',
      counter: v => {
        if (v) return v.length <= 50 || 'Dozwolona liczba znaków 50';
        else return true;
      },
      maximum: v => {
        if (v) return v.length <= 200 || 'Dozwolona liczba znaków 200';
        else return true;
      },
    },

  }),
  watch: {
    dialog(visible) {
      this.$nextTick(() => {
        if (visible) {
          // Clear the form and reset the validation when the dialog is opening
          this.$refs.addNewContactForm.reset();
          this.$refs.addNewContactForm.resetValidation();
        } else {
          // Do stuff when the dialog is closing
        }
      })
    },
  },

  computed: {
    ...mapState('cookies-store', ['legalAppTooltips'])
  },

  methods: {

    handleSubmit: async function () {
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
        this.$nuxt.refresh()
        this.dialog = false;
        this.$refs.addNewContactForm.reset();
        this.$refs.addNewContactForm.resetValidation();

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
