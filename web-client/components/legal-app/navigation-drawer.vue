<template>
  <v-card class="my-13">
    <v-list v-for="item in items" :key="item.id">
      <if-auth v-if="item.adminAccess">
        <template v-slot:allowed="{portalAdmin, clientAdmin}">
          <v-tooltip bottom v-if="(portalAdmin || clientAdmin)">
            <template v-slot:activator="{ on, attrs }">
              <nuxt-link class="nav-item " :to="item.route">
                <v-btn class="raise" small fab color="#488fef" v-bind="attrs" v-on="on">
                  <v-icon color="white" link>{{ item.icon }}</v-icon>
                </v-btn>
              </nuxt-link>
            </template>
            <span>{{ item.name }}</span>
          </v-tooltip>
        </template>
      </if-auth>
      <v-tooltip bottom v-else>
        <template v-slot:activator="{ on, attrs }">
          <nuxt-link class="nav-item " :to="item.route">
            <v-btn class="raise" small fab color="#488fef" v-bind="attrs" v-on="on">
              <v-icon color="white" link>{{ item.icon }}</v-icon>
            </v-btn>
          </nuxt-link>
        </template>
        <span>{{ item.name }}</span>
      </v-tooltip>
    </v-list>
    <v-tooltip bottom>
      <template v-slot:activator="{ on, attrs }">
        <v-btn class="raise" color="error" fab small v-bind="attrs" v-on="on" @click="$router.back()">
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

.v-list {
  background-color: transparent !important;
}

.nav-item {
  text-decoration: none;
}

.raise:hover,
.raise:focus {
  transform: translateY(-0.25em);
}

.nav-item:hover {
  text-decoration: none;
  height: 60px;
}

.v-card {
  position: fixed;
  background-color: transparent !important;
  border: none !important;
  box-shadow: none !important;
}
</style>
