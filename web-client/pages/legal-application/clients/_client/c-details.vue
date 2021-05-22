<template>
  <layout>
    <template v-slot:content v-if="client">
      <app-breadcrumbs/>
      <h1 class="ma-3">{{ client.name }}</h1>
      <v-row>
        <div class="d-flex justify-space-between pa-3" v-for="item in items" :key="item.id">
          <v-col>
            <v-hover v-slot="{ hover }">
              <nuxt-link class="nav-item" :to="item.route">
                <v-card class="mx-auto" width="340" outlined :elevation="hover ? 12 : 2">
                  <v-row class="d-flex ma-3" align="center">
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


import AppBreadcrumbs from "@/components/legal-app/app-breadcrumbs";
import Layout from "@/components/legal-app/layout";

export default {
  name: "c-details",
  components: {Layout, AppBreadcrumbs},
  middleware: ['legal-app-permission', 'client', 'authenticated'],
  data: () => ({
    client: null,
  }),
  fetch() {
    this.$axios.$get(`/api/legal-app-clients/client/${this.$route.params.client}`)
      .then(resp => {
        this.client = resp
      })

  },

  async asyncData({params}) {

    return {
      items: [
        {
          id: '1',
          route: `/legal-application/clients/${params.client}/contacts`,
          name: 'Kontakty',
          text: 'Lista kontaktów dla Klienta',
        },
        {
          id: '2',
          route: `/legal-application/clients/${params.client}/cases`,
          name: 'Sprawy',
          text: 'Zarządzaj sprawami klienta',

        },
        {
          id: '3',
          route: `/legal-application/clients/${params.client}/reminders`,
          name: 'Przypomnienia',
          text: 'Sprawdź przypomnienia dla Klienta'
        },
        {
          id: '4',
          route: `/legal-application/clients/${params.client}/notes`,
          name: 'Notatki',
          text: 'Zarządzaj notatkami'
        },
        {
          id: '5',
          route: `/legal-application/clients/${params.client}/financials`,
          name: 'Rozliczenia',
          text: 'Zarządzaj rozliczeniami'
        },

      ],
    }
  },

}
;

</script>

<style scoped>

</style>
