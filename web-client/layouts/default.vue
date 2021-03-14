<template>
  <v-app dark>
    <v-app-bar app>

      <nuxt-link class="text-h5 text--primary mr-2" style="text-decoration: none;" to="/">
        <span class="d-none d-md-flex">SystemyWP</span>
        <span class="d-flex d-md-none">SWP</span>
      </nuxt-link>

      <v-spacer/>
      <div class="mr-5">
        <v-switch prepend-icon="mdi-theme-light-dark" hide-details="hide-details" v-model="lightTheme"/>
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
                <v-list-item @click="logout">
                  <v-list-item-title>
                    <v-icon left>mdi-logout</v-icon>
                    Logout
                  </v-list-item-title>
                </v-list-item>
              </v-list>
            </v-menu>
          </div>
        </template>
        <template v-slot:forbidden="{login}">
          <v-btn class="d-none d-md-flex" outlined @click="login">
            <v-icon left>mdi-login</v-icon>
            Log In
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
    <popup/>
  </v-app>
</template>

<script>
import {mapActions, mapGetters, mapState} from "vuex";
import IfAuth from "@/components/auth/if-auth";

export default {
  name: "default",
  components: {IfAuth},
  data: () => ({
    lightTheme: false
  }),
  watch: {
    lightTheme: function (val) {
      if (val) {
        this.$vuetify.theme.light = true;
        this.$vuetify.theme.dark = false;
      } else {
        this.$vuetify.theme.light = false;
        this.$vuetify.theme.dark = true;
      }
    }
  },
  computed: {
    ...mapState('auth', ['profile']),
    ...mapGetters('auth', ['client', 'clientAdmin', 'portalAdmin']),
  },
  methods: {
    ...mapActions('auth', ['logout']),
    defaultTest() {
      return this.$axios.$get('/api/test/testAll')
        .then(res => console.log(res))
        .catch(err => console.log(err));
    }
  }
};
</script>
