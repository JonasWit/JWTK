<template>
  <v-card height="100vh">
    <v-navigation-drawer permanent width="100%">
      <v-list-item>
        <v-list-item-content>
          <v-list-item-title class="title">
            Twoja Kancelaria
          </v-list-item-title>
          <v-list-item-subtitle>
            subtext
          </v-list-item-subtitle>
        </v-list-item-content>
      </v-list-item>

      <v-divider></v-divider>

      <v-list dense nav v-for="item in items" :key="item.id">
        <nuxt-link class="nav-item" :to="item.route">
          <v-list-item link>
            <v-list-item-icon>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-icon>

            <v-list-item-content>
              <v-list-item-title>{{ item.name }}</v-list-item-title>
            </v-list-item-content>
          </v-list-item>
        </nuxt-link>
      </v-list>
    </v-navigation-drawer>
  </v-card>

</template>

<script>
import {legalappRoute} from "@/data/legal-app/legal-app-navigation";

export default {
  name: "navigation-drawer",
  data: () => ({
    items: []
  }),
  created() {
    this.items = legalappRoute.items;
  }
}
</script>

<style scoped>
.nav-item {
  text-decoration: none;
}
</style>
