<template>
  <layout>
    <template v-slot:content v-if="client">
      <h1 class="ma-3">{{ client.name }}</h1>
      <v-row>
        <div class="d-flex justify-space-between pa-3" v-for="item in items" :key="item.id">
          <if-auth v-if="item.adminAccess">
            <template v-slot:allowed="{portalAdmin, userAdmin}">
              <v-col v-if="(portalAdmin || userAdmin)">
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
    </template>
  </layout>
</template>
<script>
import Layout from "@/components/legal-app/layout";
import {handleError} from "@/data/functions";
import {GetClient} from "@/data/endpoints/legal-app/legal-app-client-endpoints";

export default {
  name: "c-details",
  components: {Layout},
  middleware: ['legal-app-permission', 'user', 'authenticated'],
  data: () => ({
    client: null,
  }),
  async fetch() {
    try {
      let clientId = this.$route.params.client
      this.client = await this.$axios.$get(GetClient(clientId));
    } catch (error) {
      handleError(error);
    }
  },
  async asyncData({params}) {
    return {
      items: [
        {
          id: '1',
          route: `/legal-application/clients/${params.client}/contacts`,
          name: 'Kontakty',
          text: 'Lista kontaktów dla Klienta',
          icon: 'mdi-card-account-mail-outline',
          adminAccess: false
        },
        {
          id: '2',
          route: `/legal-application/clients/${params.client}/cases`,
          name: 'Sprawy',
          text: 'Zarządzaj sprawami klienta',
          icon: 'mdi-briefcase-account-outline',
          adminAccess: false

        },
        {
          id: '3',
          route: `/legal-application/clients/${params.client}/notes`,
          name: 'Notatki',
          text: 'Zarządzaj notatkami',
          icon: 'mdi-book-open-outline',
          adminAccess: false
        },
        {
          id: '4',
          route: `/legal-application/clients/${params.client}/financials`,
          name: 'Rozliczenia',
          text: 'Zarządzaj rozliczeniami',
          icon: 'mdi-calculator-variant-outline',
          adminAccess: false
        },
        {
          id: '5',
          route: `/legal-application/clients/${params.client}/archived-cases`,
          name: 'Archiwum Spraw',
          text: 'Lista zarchiwizowanych Spraw',
          icon: 'mdi-briefcase-account-outline',
          adminAccess: true
        },
        {
          id: '6',
          route: `/legal-application/clients/${params.client}/accesses`,
          name: 'Dostępy',
          text: 'Dostępy do Klienta',
          icon: 'mdi-briefcase-account-outline',
          adminAccess: true
        },
      ],

    };
  },

}
;

</script>

<style scoped>

</style>
