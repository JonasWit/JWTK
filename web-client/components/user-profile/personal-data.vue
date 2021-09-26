<template>
  <div>
    <v-card flat>
      <v-toolbar color="primary" dense>
        <v-toolbar-title class="white--text">Twoje dane</v-toolbar-title>
        <v-spacer></v-spacer>
        <personal-data-edit-dialog v-on:action-completed="actionDone"/>
        <v-tooltip bottom>
          <template v-slot:activator="{ on, attrs }">
            <v-btn color="success" icon @click="downloadPersonalData" v-bind="attrs" v-on="on">
              <v-icon>mdi-download</v-icon>
            </v-btn>
          </template>
          <span>Pobierz dane</span>
        </v-tooltip>
        <confirm-personal-data-delete v-on:action-completed=""/>
      </v-toolbar>
      <v-card-text>
        <div class="text--primary"><strong>Nazwa Użytkownika:</strong> {{ profile.username }}</div>
        <div class="text--primary">
          <strong>Status:</strong> {{ userRole }}
        </div>
      </v-card-text>
      <v-divider></v-divider>
      <v-card-text>
        <div class="text--primary">
          <strong>Imię:</strong> {{ profile.name }}
        </div>
        <div class="text--primary">
          <strong>Nazwisko:</strong> {{ profile.surname }}
        </div>
        <div class="text--primary">
          <strong>Nazwa firmy:</strong> {{ profile.companyFullName }}
        </div>
        <div class="text--primary">
          <strong>Adres:</strong> {{ profile.address }}
        </div>
        <div class="text--primary">
          <strong>Adres Korespondencyjny:</strong>
          {{ profile.addressCorrespondence }}
        </div>
        <div class="text--primary">
          <strong>NIP:</strong> {{ profile.nip }}
        </div>
        <div class="text--primary">
          <strong>REGON:</strong> {{ profile.regon }}
        </div>
        <div class="text--primary">
          <strong>KRS:</strong> {{ profile.krs }}
        </div>
        <div class="text--primary">
          <strong>Telefon:</strong> {{ profile.phoneNumber }}
        </div>
      </v-card-text>
    </v-card>
  </div>
</template>

<script>
import {mapActions, mapGetters, mapState} from "vuex";
import ConfirmPersonalDataDelete from "@/components/user-profile/confirm-personal-data-delete";
import PersonalDataEditDialog from "@/components/user-profile/personal-data-edit-dialog";

export default {
  name: "personal-data",
  components: {PersonalDataEditDialog, ConfirmPersonalDataDelete},
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
        firma: this.profile.companyFullName,
        imie: this.profile.name,
        nazwisko: this.profile.surname,
        adres: this.profile.address,
        adresKorespondencyjny: this.profile.addressCorrespondence,
        nip: this.profile.nip,
        regon: this.profile.regon,
        krs: this.profile.krs,
      };

      const data = JSON.stringify(payload);
      const blob = new Blob([data], {type: "text/plain"});
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
    async actionDone() {
      await this.initialize()
    }


  },
};
</script>

<style scoped></style>
