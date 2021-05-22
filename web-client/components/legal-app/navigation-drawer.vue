<template>
  <v-card height="100vh">
    <v-navigation-drawer v-model="drawer" :mini-variant.sync="mini" permanent width="100%">
      <v-list-item>
        <v-list-item-title class="title">
          Twoja Kancelaria
        </v-list-item-title>
        <v-btn
          icon
          @click.stop="mini = !mini"
        >
          <v-icon>mdi-chevron-left</v-icon>
        </v-btn>
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
    drawer: true,
    items: [],
    mini: true
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

.v-card {
  position: fixed;
}
</style>
