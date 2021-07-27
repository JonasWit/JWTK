<template>
  <div class="my-5">

    <v-row>
      <v-col sm="12" xs="12" md="5">
        <v-card-text class="py-0">
          <strong>Nazwa Użytkownika:</strong> {{ profile.username }}
        </v-card-text>
        <v-card-text class="py-0">
          <strong>Status:</strong> {{ userRole }}
        </v-card-text>
        <v-card-text class="py-0">
          <strong>Imię:</strong> {{ profile.name }}
        </v-card-text>
        <v-card-text class="py-0">
          <strong>Nazwisko:</strong> {{ profile.surname }}
        </v-card-text>
        <v-card-text class="py-0">
          <strong>Firma:</strong> {{ profile.companyFullName }}
        </v-card-text>
      </v-col>
      <v-col sm="12" xs="12" md="5">
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
        <v-card-text class="py-0">
          <strong>Telefon:</strong> {{ profile.phoneNumber }}
        </v-card-text>
      </v-col>
      <v-col sm="12" xs="12" md="2">
        <v-card-actions class="pt-0 d-flex justify-space-between">
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
        </v-card-actions>


      </v-col>
    </v-row>


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
