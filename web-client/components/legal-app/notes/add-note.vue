<template>
  <v-dialog v-model="dialog" max-width="500px">
    <template #activator="{ on: dialog }" v-slot:activator="{ on }">
      <v-tooltip bottom>
        <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
          <v-btn fab class="mx-2" v-on="{ ...tooltip, ...dialog }">
            <v-icon>mdi-plus</v-icon>
          </v-btn>
        </template>
        <span>Dodaj notkę</span>
      </v-tooltip>
    </template>
    <v-form ref="addNoteForm">
      <v-card>
        <v-card-text>
          <v-text-field v-model="form.title" label="Tytuł" required></v-text-field>
          <v-textarea outlined v-model="form.message" label="Treść notatki" :rules="validation.message"></v-textarea>
        </v-card-text>
        <v-divider></v-divider>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn text color="primary" @click="save">
            Zapisz
          </v-btn>
          <v-btn color="success" text @click="dialog = false">
            Anuluj
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-form>
  </v-dialog>
</template>

<script>
import {notEmptyAndLimitedRule} from "@/data/vuetify-validations";
import {createNoteForClient} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "add-note",
  data: () => ({
    dialog: false,
    form: {
      title: "",
      message: "",
    },
    validation: {
      valid: false,
      message: notEmptyAndLimitedRule("Notatka może zawierać maksymalnie 1000 znaków.", 4, 1000),
    },
  }),

  methods: {
    createNoteForClient(clientId) {
      return createNoteForClient(clientId)
    },

    async save() {
      try {
        this.resetForm();
        const note = {
          title: this.form.title,
          message: this.form.message,
        };

        let clientId = this.$route.params.client;

        await this.$axios.$post(createNoteForClient(clientId), note);

        this.$notifier.showSuccessMessage("Notatka dodana pomyślnie!");
      } catch (e) {
        console.error(e);

      } finally {
        this.dialog = false;
      }
    },
    resetForm() {
      this.$refs.addNoteForm.reset();
      this.$refs.addNoteForm.resetValidation();
    },
  }
}
</script>

<style scoped>

</style>
