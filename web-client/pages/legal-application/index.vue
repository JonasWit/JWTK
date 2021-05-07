<template>
  <v-container>
    <v-row>
      <div class="d-flex justify-space-between pa-3" v-for="item in items" :key="item.id">
        <v-col>
          <v-hover v-slot="{ hover }">
            <nuxt-link class="nav-item" :to="item.route">
              <v-card class="mx-auto" width="340" outlined :elevation="hover ? 12 : 2">
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
  middleware: ['legal-app-permission', 'client', 'authenticated'],
  data: () => ({
    items: []
  }),
  created() {
    this.items = legalappRoute.items;
  }
};
</script>

<style scoped>
.nav-item {
  text-decoration: none;
}

.v-card {
  border-left: 3px solid var(--v-primary-base);

}
</style>
