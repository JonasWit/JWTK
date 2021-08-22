<template>
  <layout>
    <template v-slot:content>
      <v-container>
        <v-row class="mt-10">
          <div class="d-flex justify-space-between pa-3" v-for="item in items" :key="item.id">
            <if-auth v-if="item.adminAccess">
              <template v-slot:allowed="{portalAdmin, clientAdmin}">
                <v-col v-if="(portalAdmin || clientAdmin)">
                  <v-hover v-slot="{ hover }">
                    <nuxt-link class="nav-item" :to="item.route">
                      <v-card class="mx-auto index-card" width="340" outlined :elevation="hover ? 12 : 2">
                        <v-row class="d-flex ma-3" align="center">
                          <v-icon>{{ item.icon }}</v-icon>
                          <v-card-title class="text-uppercase justify-center">{{ item.name }}</v-card-title>
                        </v-row>
                        <v-card-text class="font-weight-light">{{ item.text }}</v-card-text>
                      </v-card>
                    </nuxt-link>
                  </v-hover>
                </v-col>
              </template>
            </if-auth>
            <v-col v-else>
              <v-hover v-slot="{ hover }">
                <nuxt-link class="nav-item" :to="item.route">
                  <v-card class="mx-auto index-card" width="340" outlined :elevation="hover ? 12 : 2">
                    <v-row class="d-flex ma-3" align="center">
                      <v-icon>{{ item.icon }}</v-icon>
                      <v-card-title class="text-uppercase justify-center">{{ item.name }}</v-card-title>
                    </v-row>
                    <v-card-text class="font-weight-light">{{ item.text }}</v-card-text>
                  </v-card>
                </nuxt-link>
              </v-hover>
            </v-col>
          </div>
        </v-row>
      </v-container>
    </template>
  </layout>

</template>

<script>
import Layout from "@/components/legal-app/layout";
import ProgressBar from "@/components/legal-app/progress-bar";

export default {
  components: {ProgressBar, Layout},
  middleware: ['legal-app-permission', 'user', 'authenticated'],
  async asyncData({params}) {
    return {
      items: [
        {
          id: '1',
          route: '/legal-application/clients',
          name: 'Klienci',
          icon: `mdi-account-tie`,
          text: 'Lista Twoich klientów',
          adminAccess: false
        },
        {
          id: '2',
          route: '/legal-application/reminders',
          name: 'Kalendarz',
          icon: 'mdi-calendar',
          text: 'Zaplanuj spotkania lub dodaj zadania',
          adminAccess: false
        },
        {
          id: '3',
          route: '/legal-application/archive',
          name: 'Archiwum Klientów',
          icon: 'mdi-archive',
          text: 'Lista zarchiwzowanych klinetów',
          adminAccess: true
        },
        {
          id: '4',
          route: '/legal-application/help',
          name: 'Pomoc',
          icon: 'mdi-help',
          text: 'Pomoc',
          adminAccess: false
        },
      ],
    };
  },
};
</script>

<style scoped>


</style>
