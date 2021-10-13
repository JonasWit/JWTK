<template>
  <div>
    <v-dialog persistent v-model="dialog" width="400">
      <template #activator="{ on: dialog }" v-slot:activator="{ on }">
        <v-tooltip bottom>
          <template #activator="{ on: tooltip }" v-slot:activator="{ on }">
            <v-btn color="warning" icon v-on="{ ...tooltip, ...dialog }">
              <v-icon>
                mdi-account-edit
              </v-icon>
            </v-btn>
          </template>
          <span>Edytuj dane</span>
        </v-tooltip>
      </template>
      <v-card>
        <v-toolbar color="primary" dark>
          <v-toolbar-title>
            Edycja Danych Personalnych
          </v-toolbar-title>
        </v-toolbar>
        <v-form ref="editPersonalDataFrom" v-model="validation.valid">
          <v-text-field class="ma-3" v-model="form.name" :rules="validation.smallLength" label="Imię"></v-text-field>
          <v-text-field class="ma-3" v-model="form.surname" :rules="validation.smallLength"
                        label="Nazwisko"></v-text-field>
          <v-text-field class="ma-3" v-model="form.companyFullName" :rules="validation.normalLength"
                        label="Firma"></v-text-field>
          <v-text-field class="ma-3" v-model="form.address" :rules="validation.extendedLength"
                        label="Adres"></v-text-field>
          <v-text-field class="ma-3" v-model="form.addressCorrespondence" :rules="validation.extendedLength"
                        label="Adres Korespondencyjny"></v-text-field>
          <v-text-field class="ma-3" v-model="form.nip" :rules="validation.smallLength" label="NIP"></v-text-field>
          <v-text-field class="ma-3" v-model="form.regon" :rules="validation.smallLength" label="REGON"></v-text-field>
          <v-text-field class="ma-3" v-model="form.krs" :rules="validation.smallLength" label="KRS"></v-text-field>
          <v-text-field class="ma-3" v-model="form.phoneNumber" :rules="validation.smallLength"
                        label="Numer Telefonu"></v-text-field>
        </v-form>
        <v-card-actions>
          <v-btn color="error" text @click="closeDialog">
            Anuluj
          </v-btn>
          <v-spacer/>
          <v-btn color="primary" text @click="saveData">
            Zapisz
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </div>
</template>

<script>

import {mapGetters, mapState} from "vuex";
import {handleError} from "@/data/functions";
import ProgressBar from "@/components/legal-app/progress-bar";
import {editPersonalData} from "@/data/endpoints/legal-app/user-profile-endpoints";

export default {
  name: "personal-data-edit-dialog",
  components: {ProgressBar},
  data: () => ({
    dialog: false,
    form: {
      userId: "",
      phoneNumber: "",
      companyFullName: "",
      name: "",
      surname: "",
      address: "",
      addressCorrespondence: "",
      city: "",
      vivodership: "",
      country: "",
      postCode: "",
      nip: "",
      krs: "",
      regon: "",
    },
    validation: {
      valid: false,
      smallLength: [
        v => v?.length <= 20 || "Nie więcej niż 20 znaków."
      ],
      normalLength: [
        v => v?.length <= 50 || "Nie więcej niż 50 znaków."
      ],
      extendedLength: [
        v => v?.length <= 200 || "Nie więcej niż 200 znaków."
      ],
    },
  }),
  watch: {
    dialog(visible) {
      if (visible) {
        this.form.phoneNumber = this.profile.phoneNumber;
        this.form.companyFullName = this.profile.companyFullName;
        this.form.name = this.profile.name;
        this.form.surname = this.profile.surname;
        this.form.address = this.profile.address;
        this.form.addressCorrespondence = this.profile.addressCorrespondence;
        this.form.nip = this.profile.nip;
        this.form.krs = this.profile.krs;
        this.form.regon = this.profile.regon;
      }
    }
  },
  computed: {
    ...mapState('auth', ['profile']),
    ...mapGetters('auth', ['userRole'])
  },
  methods: {
    async saveData() {
      try {
        this.form.userId = this.profile.id;
        await this.$axios.$put(editPersonalData(), this.form)
        this.$notifier.showSuccessMessage("Dane zmienione pomyślnie!");
      } catch (error) {
        handleError(error)
      } finally {
        this.$emit('action-completed');
        this.closeDialog();
      }

    },
    closeDialog() {
      this.$refs.editPersonalDataFrom.reset();
      this.$refs.editPersonalDataFrom.resetValidation();
      this.dialog = false;
    },
  }
};
</script>

<style scoped>

</style>
