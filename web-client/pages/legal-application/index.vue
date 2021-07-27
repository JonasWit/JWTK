<template>
  <v-container>
    <v-row class="mt-10">
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
  </v-container>
</template>

<script>
import {legalappRoute} from "@/data/legal-app/legal-app-navigation";

export default {
  middleware: ['legal-app-permission', 'user', 'authenticated'],
  data: () => ({
    items: []
  }),
  created() {
    this.items = legalappRoute.items;
  }
};
</script>

<style scoped>


</style>
