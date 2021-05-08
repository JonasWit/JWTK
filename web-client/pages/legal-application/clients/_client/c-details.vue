<template>
  <legalapp-layout>
    <template v-slot:content>
      <h1>CLIENT NAME PLACEHOLDER</h1>
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
  </legalapp-layout>


</template>

<script>


export default {
  name: "c-details",
  middleware: ['legal-app-permission', 'client', 'authenticated'],
  data: () => ({
    items: [

      {
        id: '1',
        route: `/legal-application/clients/placeholder/contacts`,
        name: 'Kontakty',
        text: 'Lista kontaktów dla Klienta',
      },
      {
        id: '2',
        route: `/legal-application/clients/placeholder/cases`,
        name: 'Sprawy',
        text: 'Zarządzaj sprawami klienta',

      },
      {
        id: '3',
        route: `/legal-application/clients/placeholder/reminders`,
        name: 'Przypomnienia',
        text: 'Sprawdź przypomnienia dla Klienta'
      },
      {
        id: '4',
        route: `/legal-application/clients/placeholder/notes`,
        name: 'Notatki',
        text: 'Zarządzaj notatkami'
      },
      {
        id: '5',
        route: `/legal-application/clients/placeholder/financials`,
        name: 'Rozliczenia',
        text: 'Zarządzaj rozliczeniami'
      },

    ],
  }),


  fetch() {
    console.warn(this.$route.params.client);
    this.items.forEach(x => x.route = x.route.replace("placeholder", this.$route.params.client));
    console.warn(this.$route.params.client);
    console.warn(this.items);

  }

}
;

</script>

<style scoped>

</style>
