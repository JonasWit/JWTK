<template>
  <layout>
    <template v-slot:content>
      <v-toolbar color="primary" class="white--text">
        <v-toolbar-title>Archiwum</v-toolbar-title>
        <v-spacer></v-spacer>
        <v-icon color="white">mdi-archive</v-icon>
      </v-toolbar>
      <v-row>
        <div class="d-flex justify-space-between pa-3" v-for="item in items" :key="item.id">
          <v-col>
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

export default {
  name: "index",
  components: {Layout},
  data: () => ({
    items: [

      {
        id: '1',
        route: `/legal-application/archive/clients`,
        name: 'Archiwum Klientów',
        text: 'Lista zarchiwizowanych Klientów',
        icon: `mdi-account-tie`,
      },
      {
        id: '2',
        route: `/legal-application/archive/cases`,
        name: 'Archiwum Spraw',
        text: 'Lista zarchiwizowanych Spraw',
        icon: 'mdi-briefcase-account-outline'

      },]
  })
}


</script>

<style scoped>

</style>
