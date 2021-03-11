<template>
  <v-dialog persistent v-model="dialog" width="400">
    <template v-slot:activator="{ on, attrs }">
      <v-btn depressed text v-bind="attrs" v-on="on">
        Edytuj
      </v-btn>
    </template>
    <v-card>
      <v-card-title class="justify-center">
        Edycja Danych Personalnych
      </v-card-title>
      <v-form ref="editPersonalDataFrom" v-model="validation.valid">
        <v-text-field class="ma-3" v-model="form.phoneNumber" :rules="validation.normalLength"
                      label="Numer Telefonu"></v-text-field>
        <v-text-field class="ma-3" v-model="form.companyFullName" :rules="validation.normalLength"
                      label="Firma"></v-text-field>
        <v-text-field class="ma-3" v-model="form.name" :rules="validation.normalLength" label="Imię"></v-text-field>
        <v-text-field class="ma-3" v-model="form.surname" :rules="validation.normalLength"
                      label="Nazwisko"></v-text-field>
        <v-text-field class="ma-3" v-model="form.address" :rules="validation.extendedLength"
                      label="Adres"></v-text-field>
        <v-text-field class="ma-3" v-model="form.addressCorrespondence" :rules="validation.extendedLength"
                      label="Adres Korespondencyjny"></v-text-field>
        <v-text-field class="ma-3" v-model="form.nip" :rules="validation.normalLength" label="NIP"></v-text-field>
        <v-text-field class="ma-3" v-model="form.regon" :rules="validation.normalLength" label="REGON"></v-text-field>
        <v-text-field class="ma-3" v-model="form.krs" :rules="validation.normalLength" label="KRS"></v-text-field>
      </v-form>
      <v-divider></v-divider>
      <v-card-actions>
        <v-btn color="warning" text @click="saveData">
          Zapisz
        </v-btn>
        <v-spacer/>
        <v-btn color="success" text @click="closeDialog">
          Anuluj
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>

<script>

import {mapGetters, mapState} from "vuex";

export default {
  name: "personal-data-edit-dialog",
  data: () => ({
    loading: false,
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
    saveData() {
      if (this.loading) return;
      this.loading = true;

      this.form.userId = this.profile.id;
      return this.$axios.$put("/api/users/personal-data/update", this.form)
        .catch((e) => {
        }).finally(() => {
          this.loading = false;
          this.$emit('action-completed');
          this.closeDialog();
        });
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
