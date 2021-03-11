<template>
  <v-card>
    <v-card-title class="d-flex justify-center"> Dane Personalne </v-card-title>
    <v-card-text class="py-0">
      <strong>Nazwa Użytkownika:</strong> {{ profile.username }}
    </v-card-text>
    <v-card-text class="py-0">
      <strong>Status:</strong> {{ userRole }}
    </v-card-text>
    <v-card-text class="py-0">
      <strong>Telefon:</strong> {{ profile.phoneNumber }}
    </v-card-text>
    <v-card-text class="py-0">
      <strong>Firma:</strong> {{ profile.companyFullName }}
    </v-card-text>
    <v-card-text class="py-0">
      <strong>Imię:</strong> {{ profile.name }}
    </v-card-text>
    <v-card-text class="py-0">
      <strong>Nazwisko:</strong> {{ profile.surname }}
    </v-card-text>
    <v-card-text class="py-0">
      <strong>Adres:</strong> {{ profile.address }}
    </v-card-text>
    <v-card-text class="py-0">
      <strong>Adres Korespondencyjny:</strong>
      {{ profile.addressCorrespondence }}
    </v-card-text>
    <v-card-text class="py-0">
      <strong>NIP:</strong> {{ profile.nip }}
    </v-card-text>
    <v-card-text class="py-0">
      <strong>REGON:</strong> {{ profile.regon }}
    </v-card-text>
    <v-card-text class="py-0">
      <strong>KRS:</strong> {{ profile.krs }}
    </v-card-text>
    <v-divider class="my-2" />
    <v-card-actions class="pt-0">
      <prof-personal-data-edit-dialog v-on:action-completed="initialize" />
      <v-btn text @click="downloadPersonalData">Pobierz</v-btn>
      <v-spacer />
      <prof-confirm-personal-data-delete v-on:action-completed="" />
    </v-card-actions>
  </v-card>
</template>

<script>
import { mapActions, mapGetters, mapState } from "vuex";

export default {
  name: "personal-data",
  data: () => ({
    loading: false,
  }),
  computed: {
    ...mapState("auth", ["profile"]),
    ...mapGetters("auth", ["userRole"]),
  },
  methods: {
    ...mapActions("auth", ["initialize"]),
    downloadPersonalData() {
      const payload = {
        nazwaUzytkownika: this.profile.username,
        numerTelefonu: this.profile.phoneNumber,
        adres: this.profile.address,
      };

      const data = JSON.stringify(payload);
      const blob = new Blob([data], { type: "text/plain" });
      const e = document.createEvent("MouseEvents"),
        a = document.createElement("a");
      a.download = "personal-data.json";
      a.href = window.URL.createObjectURL(blob);
      a.dataset.downloadurl = ["text/json", a.download, a.href].join(":");
      e.initEvent(
        "click",
        true,
        false,
        window,
        0,
        0,
        0,
        0,
        0,
        false,
        false,
        false,
        false,
        0,
        null
      );
      a.dispatchEvent(e);
    },
  },
};
</script>

<style scoped>
</style>
