<template>
  <v-container class="mt-6">
    <v-card v-if="authenticated">
      <v-toolbar flat color="primary" dark extended>
        <main-header class="mt-6"/>
        <v-spacer></v-spacer>
        <v-toolbar-title>Profil użytkownika</v-toolbar-title>
      </v-toolbar>
      <v-tabs vertical>
        <v-tab class="d-flex justify-start">
          <v-icon>
            mdi-account
          </v-icon>
          Dane podstawowe
        </v-tab>
        <v-tab class="d-flex justify-start">
          <v-icon>
            mdi-delete-alert
          </v-icon>
          Dane Kancelarii
        </v-tab>
        <v-tab class="d-flex justify-start">
          <v-icon>
            mdi-account-cancel
          </v-icon>
          Twoje konto
        </v-tab>
        <v-tab-item>
          <v-card flat>
            <personal-data/>
          </v-card>
        </v-tab-item>
        <v-tab-item>
          <v-card flat>
            <if-auth>
              <template v-slot:allowed="{portalAdmin, legalAppAllowed, clientAdmin, legalAppKeyAvailable}">
                <div v-if="legalAppAllowed && (portalAdmin || clientAdmin) && legalAppKeyAvailable" class="mt-4">
                  <legalapp-key-remove/>
                </div>
              </template>
            </if-auth>
          </v-card>
        </v-tab-item>
        <v-tab-item>
          <v-card flat>
            <if-auth>
              <template v-slot:allowed="{portalAdmin, legalAppAllowed, clientAdmin, legalAppKeyAvailable}">
                <div>
                  <account-remove/>
                </div>
              </template>
            </if-auth>
          </v-card>
        </v-tab-item>
      </v-tabs>
    </v-card>
  </v-container>
</template>

<script>
import {mapGetters, mapState} from "vuex";
import MainHeader from "@/components/user-profile/main-header";
import PersonalData from "@/components/user-profile/personal-data";
import AccountRemove from "@/components/user-profile/account-remove";
import IfAuth from "@/components/auth/if-auth";
import LegalappKeyRemove from "@/components/user-profile/legalapp-key-remove";

export default {
  components: {LegalappKeyRemove, IfAuth, AccountRemove, PersonalData, MainHeader},
  middleware: ["user", "authenticated"],
  data: () => ({}),
  computed: {
    ...mapState("auth", ["profile"]),
    ...mapGetters('auth', ['authenticated'])
  },
};
</script>

<style scoped></style>
