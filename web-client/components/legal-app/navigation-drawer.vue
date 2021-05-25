<template>
  <v-card>
    <v-list dense nav v-for="item in items" :key="item.id">
      <v-tooltip bottom>
        <template v-slot:activator="{ on, attrs }">
          <nuxt-link class="nav-item" :to="item.route">
            <!--            <v-list-item >-->
            <!--              <v-list-item-content>-->
            <!--                <v-list-item-title>{{ item.name }}</v-list-item-title>-->
            <!--              </v-list-item-content>-->
            <v-list-item-icon link v-bind="attrs" v-on="on">
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-icon>

            <!--            </v-list-item>-->
          </nuxt-link>

        </template>
        <span>{{ item.name }}</span>
      </v-tooltip>
    </v-list>
    <v-tooltip bottom>
      <template v-slot:activator="{ on, attrs }">
        <v-btn color="success" icon v-bind="attrs" v-on="on" @click="$router.back()">
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

.v-card {
  position: fixed;
  background-color: transparent !important;
  border: none !important;
  box-shadow: none !important;
}

</style>
