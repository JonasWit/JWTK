<template>
  <v-app :dark="isDarkTheme">
    <client-only>
      <cookie-consent/>
    </client-only>
    <snackbar/>
    <v-app-bar app>

      <nuxt-link class="text-h5 text--primary mr-2" style="text-decoration: none;" to="/">
        <span class="d-none d-md-flex">SystemyWP</span>
        <span class="d-flex d-md-none">SWP</span>
      </nuxt-link>
      <home-navbar/>
      <v-spacer></v-spacer>
      <div class="mr-5">
        <v-switch prepend-icon="mdi-theme-light-dark" hide-details="hide-details" v-model="themeSwitch"/>
      </div>
      <if-auth>
        <template v-slot:allowed="{portalAdmin, legalAppAllowed, client}">
          <div class="d-flex align-center">
            <v-menu offset-y>
              <template v-slot:activator="{ on, attrs }">
                <v-btn icon v-bind="attrs" v-on="on">
                  <user-header :image-url="profile.image" :link="false" size="36"/>
                </v-btn>
              </template>
              <v-list>
                <v-list-item to="/user-profile">
                  <v-list-item-title>
                    <v-icon left>mdi-clipboard-edit-outline</v-icon>
                    Mój Profil
                  </v-list-item-title>
                </v-list-item>
                <v-list-item v-if="portalAdmin" to="/admin-panel">
                  <v-list-item-title>
                    <v-icon left>mdi-cogs</v-icon>
                    Admin Panel
                  </v-list-item-title>
                </v-list-item>
                <v-list-item v-if="legalAppAllowed" to="/legal-application">
                  <v-list-item-title>
                    <v-icon left>mdi-scale-balance</v-icon>
                    Moja Kancelaria
                  </v-list-item-title>
                </v-list-item>
                <v-list-item @click="changePassword">
                  <v-list-item-title>
                    <v-icon left>mdi-lock-reset</v-icon>
                    Zmiana Hasła
                  </v-list-item-title>
                </v-list-item>
                <v-list-item @click="logout">
                  <v-list-item-title>
                    <v-icon left>mdi-logout</v-icon>
                    Wyloguj
                  </v-list-item-title>
                </v-list-item>
              </v-list>
            </v-menu>
          </div>
        </template>
        <template v-slot:forbidden="{login}">
          <v-btn class="d-none d-md-flex" outlined @click="login">
            <v-icon left>mdi-login</v-icon>
            Zaloguj
          </v-btn>
          <v-btn class="d-flex d-md-none" icon @click="login">
            <v-icon>mdi-login</v-icon>
          </v-btn>
        </template>
      </if-auth>

    </v-app-bar>
    <v-main>
      <v-container fluid>
        <nuxt/>
      </v-container>

    </v-main>

    <portal-footer/>

  </v-app>
</template>

<script>
import {mapActions, mapGetters, mapMutations, mapState,} from "vuex";
import IfAuth from "@/components/auth/if-auth";
import SnackbarNotifier from "@/components/snackbar";
import {checkCookie, getCookieFromRequest, getGDPRConsent, setCookie} from "@/data/cookie-handlers";

export default {
  name: "default",
  components: {SnackbarNotifier, IfAuth},
  data: () => ({
    goDark: false,
    themeSwitch: false
  }),
  created() {
    let themeEnabled;
    if (process.server) {
      themeEnabled = getCookieFromRequest("custom-color-theme", this.$nuxt.context.req.headers.cookie) === 'dark';
    } else {
      themeEnabled = this.darkThemeStored === true;
    }
    this.goDark = themeEnabled;

    if (this.goDark) {
      this.setDarkTheme();
    } else {
      this.setLightTheme();
      this.themeSwitch = true;
    }
  },
  watch: {
    themeSwitch: function (val) {
      if (process.client && this.darkThemeStored !== !val) {
        if (val) {
          this.$vuetify.theme.light = true;
          this.$vuetify.theme.dark = false;
          this.setLightTheme();

          if (getGDPRConsent()) {
            if (checkCookie("custom-color-theme")) {
              setCookie("custom-color-theme", "", 0);
              setCookie("custom-color-theme", "light", 365);
            } else {
              setCookie("custom-color-theme", "light", 365);
            }
          }
        } else {
          this.$vuetify.theme.light = false;
          this.$vuetify.theme.dark = true;
          this.setDarkTheme();

          if (getGDPRConsent()) {
            if (checkCookie("custom-color-theme")) {
              setCookie("custom-color-theme", "", 0);
              setCookie("custom-color-theme", "dark", 365);
            } else {
              setCookie("custom-color-theme", "dark", 365);
            }
          }
        }

        this.goDark = this.darkThemeStored;
      }
    }
  },
  computed: {
    ...mapState('auth', ['profile', 'darkThemeStored']),
    ...mapGetters('auth', ['client', 'clientAdmin', 'portalAdmin', 'authenticated']),
    isDarkTheme() {
      return this.$vuetify.theme.dark = this.goDark;
    }
  },
  methods: {
    ...mapMutations('auth', ['setLightTheme', 'setDarkTheme']),
    ...mapActions('auth', ['logout', 'initialize', 'changePassword']),
  }
};
</script>
<style>

.container {
  padding: 0px;
}

.theme--dark.v-application {
  background-color: #000000 !important;
}

.theme--light.v-application {
  background-color: #FCFCFC !important;
}

.page-enter-active,
.page-leave-active {
  transition: opacity 0.5s;
}

.page-enter,
.page-leave-to {
  opacity: 0;
}

.layout-enter-active,
.layout-leave-active {
  transition: opacity 0.5s;
}

.layout-enter,
.layout-leave-to {
  opacity: 0;
}

.slide-bottom-enter-active,
.slide-bottom-leave-active {
  transition: opacity 0.25s ease-in-out, transform 0.25s ease-in-out;
}

.slide-bottom-enter,
.slide-bottom-leave-to {
  opacity: 0;
  transform: translate3d(0, 15px, 0);
}


</style>
