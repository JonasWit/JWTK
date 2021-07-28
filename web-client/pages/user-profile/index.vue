<template>
  <v-container class="mt-6" v-if="authenticated">
    <v-toolbar flat color="primary" dark>
      <v-toolbar-title>Profil użytkownika</v-toolbar-title>
    </v-toolbar>
    <v-row>
      <v-col md="4">
        <v-card>
          <main-header class="mt-6"/>
          <v-divider></v-divider>
          <personal-data class="mb-4"/>
        </v-card>
      </v-col>
      <v-col md="8">
        <v-card>
          <if-auth>
            <template v-slot:allowed="{portalAdmin, legalAppAllowed, clientAdmin, legalAppKeyAvailable}">
              <div v-if="legalAppAllowed && legalAppKeyAvailable" class="mt-4">
                <v-card-text>
                  <v-card-title class="d-flex justify-center my-3">
                    Twoje konto
                  </v-card-title>
                  <div class="text--primary py-1">
                    <strong>Ostatnie logowanie:</strong> {{ formatDateToLocaleTimeZone(profile.lastLogin) }}
                  </div>
                  <div class="text--primary py-1">
                    <strong>Ostatnie logowanie:</strong>
                    {{ formatDateForCalendar(profile.legalAppDataAccessKey.expireDate) }}
                  </div>
                </v-card-text>
              </div>
            </template>
          </if-auth>
        </v-card>
        <v-card>
          <if-auth>
            <template v-slot:allowed="{portalAdmin, legalAppAllowed, clientAdmin, legalAppKeyAvailable}">
              <div v-if="legalAppAllowed && (portalAdmin || clientAdmin) && legalAppKeyAvailable" class="mt-4">
                <legalapp-key-remove/>
              </div>
            </template>
          </if-auth>
        </v-card>
        <v-card>
          <if-auth>
            <template v-slot:allowed="{portalAdmin, legalAppAllowed, clientAdmin, legalAppKeyAvailable}">
              <div>
                <account-remove/>
              </div>
            </template>
          </if-auth>
        </v-card>

      </v-col>
    </v-row>
  </v-container>
</template>

<script>
import {mapGetters, mapState} from "vuex";
import MainHeader from "@/components/user-profile/main-header";
import PersonalData from "@/components/user-profile/personal-data";
import AccountRemove from "@/components/user-profile/account-remove";
import IfAuth from "@/components/auth/if-auth";
import LegalappKeyRemove from "@/components/user-profile/legalapp-key-remove";
import {formatDateForInvoice, formatDateToLocaleTimeZone} from "@/data/date-extensions";

export default {
  components: {LegalappKeyRemove, IfAuth, AccountRemove, PersonalData, MainHeader},
  middleware: ["user", "authenticated"],
  data: () => ({}),

  fetch() {
    console.log('profile', this.profile.legalAppDataAccessKeys)
  },
  computed: {
    ...mapState("auth", ["profile"]),
    ...mapGetters('auth', ['authenticated'])
  },
  methods: {
    formatDateToLocaleTimeZone(date) {
      return formatDateToLocaleTimeZone(date)
    },
    formatDateForCalendar(date) {
      return formatDateForInvoice(date)
    }
  }
};
</script>

<style scoped></style>
