<template>
  <v-card class="justify-space-between mt-2">
    <v-list v-for="item in items" :key="item.id">
      <if-auth v-if="item.adminAccess">
        <template v-slot:allowed="{portalAdmin, clientAdmin}">
          <v-tooltip bottom v-if="(portalAdmin || clientAdmin)">
            <template v-slot:activator="{ on, attrs }">
              <nuxt-link class="nav-item " :to="item.route">
                <v-list-item-icon>
                  <v-btn small fab color="#488fef" v-bind="attrs" v-on="on">
                    <v-icon color="white" link>{{ item.icon }}</v-icon>
                  </v-btn>
                </v-list-item-icon>
              </nuxt-link>
            </template>
            <span>{{ item.name }}</span>
          </v-tooltip>
        </template>
      </if-auth>
      <v-tooltip bottom v-else>
        <template v-slot:activator="{ on, attrs }">
          <nuxt-link class="nav-item " :to="item.route">
            <v-list-item-icon>
              <v-btn small fab color="#488fef" v-bind="attrs" v-on="on">
                <v-icon color="white" link>{{ item.icon }}</v-icon>
              </v-btn>
            </v-list-item-icon>
          </nuxt-link>
        </template>
        <span>{{ item.name }}</span>
      </v-tooltip>
    </v-list>


    <v-tooltip bottom>
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="error" fab small depressed v-bind="attrs" v-on="on" @click="$router.back()">
          <v-icon>mdi-chevron-left</v-icon>
        </v-btn>
      </template>
      <span>Powrót do poprzedniej strony</span>
    </v-tooltip>
  </v-card>

</template>

<script>
import {legalappRoute} from "@/data/legal-app/legal-app-navigation";

export default {
  name: "navigation-drawer",
  middleware: ['legal-app-permission', 'user', 'authenticated'],
  data: () => ({
    items: [],
  }),
  created() {
    this.items = legalappRoute.items;
  }
};
</script>

<style scoped>

.nav-card {
  z-index: 1;
}

.v-list {
  background-color: transparent !important;
}

.nav-item {
  text-decoration: none;
}

.v-card {
  position: fixed;
  background-color: transparent !important;
  border: none !important;
  box-shadow: none !important;
}
</style>
