<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn elevation="2" small class="mx-2" color="secondary" v-on="{ ...tooltip, ...dialog }">
            Edytuj
          </v-btn>
        </template>
        <span>Edytuj treść</span>
      </v-tooltip>
    </template>
    <v-form ref="editNoteForm">
      <v-card>
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Edytuj notatkę
          </v-toolbar-title>
        </v-toolbar>
        <v-alert v-if="legalAppTooltips" elevation="5" text type="info">
          Każda nowa sprawa jest prywatna, co oznacza, że będzie widoczna tylko dla użytkownika, który ją stworzył.
          Jeśli chcesz, aby sprawa była widoczna dla innych, oznacz ją jako publiczną.
        </v-alert>
        <v-card-text>
          <v-text-field v-model="form.name" label="Tytuł sprawy" required :rules="validation.name"></v-text-field>
          <v-text-field v-model="form.group" label="Kategoria sprawy" required :rules="validation.group"></v-text-field>
          <v-text-field v-model="form.signature" label="Sygnatura sprawy" required
                        :rules="validation.signature"></v-text-field>
          <v-textarea outlined v-model="form.description" label="Opis sprawy" required
                      :rules="validation.description"></v-textarea>
          <v-checkbox v-model="form.public" label="Sprawa publiczna" color="red darken-3"></v-checkbox>
          <small class="grey--text">* Dane opcjonalne</small>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-btn color="error" text @click="dialog = false">
            Anuluj
          </v-btn>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="saveChanges()">
            Zapisz zmianę
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
    <progress-bar v-if="loader"/>
  </v-dialog>

</template>

<script>

import {mapActions, mapState} from "vuex";
import {lengthRule, notEmptyAndLimitedRule} from "@/data/vuetify-validations";
import {updateCase} from "@/data/endpoints/legal-app/legal-app-case-endpoints";
import ProgressBar from "@/components/legal-app/progress-bar";
import {handleError} from "@/data/functions";

export default {
  name: "edit-case-dialog",
  components: {ProgressBar},
  props: {
    caseForAction: {
      required: true,
      default: null
    }
  },
  data: () => ({
    dialog: false,
    loader: false,
    form: {
      name: "",
      signature: "",
      description: "",
      group: ""
    },
    validation: {
      valid: false,
      name: notEmptyAndLimitedRule("Tytuł sprawy nie może być pusty i liczba znaków nie może przekraczać 50", 1, 50),
      group: notEmptyAndLimitedRule("Nazwa kategorii nie może byc pusta i może zawierać maksymalnie 200 znaków.", 1, 200),
      signature: lengthRule("Liczba znaków nie może przekraczać 200", 0, 200),
      description: lengthRule("Liczba znaków nie może przekraczać 1000", 0, 1000)
    },
  }),
  async beforeMount() {
    this.loader = true
    try {
      this.form.name = this.caseForAction.name
      this.form.signature = this.caseForAction.signature
      this.form.description = this.caseForAction.description
      this.form.group = this.caseForAction.group
      this.form.public = this.caseForAction.public
    } catch (error) {
      handleError(error);
    } finally {
      this.loader = false
    }

  },
  computed: {
    ...mapState('cookies-store', ['legalAppTooltips']),
  },
  methods: {
    ...mapActions('legal-app-client-store', ['getCaseDetails']),
    async saveChanges() {
      this.loader = true
      try {
        const newCase = {
          name: this.form.name,
          signature: this.form.signature,
          description: this.form.description,
          group: this.form.group,
          public: this.form.public
        };
        let caseId = this.caseForAction.id;
        await this.$axios.$put(updateCase(caseId), newCase);
        this.$notifier.showSuccessMessage("Zmiany zostały zapisane!");
      } catch (error) {
        handleError(error);
      } finally {
        let caseId = this.caseForAction.id;
        await this.getCaseDetails({caseId})
        this.dialog = false;
        this.loader = false
        this.$emit('action-completed');
      }
    }
  }
};
</script>

<style scoped>

</style>
