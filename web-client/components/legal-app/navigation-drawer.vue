<template>
  <v-card class="mx-5 nav-card">
    <v-row>
      <v-tooltip bottom>
        <template v-slot:activator="{ on, attrs }">
          <v-btn color="error" fab small v-bind="attrs" v-on="on" @click="$router.back()">
            <v-icon>mdi-chevron-left</v-icon>
          </v-btn>
        </template>
        <span>Powrót do poprzedniej strony</span>
      </v-tooltip>

      <div v-for="item in items" :key="item.id">
        <v-tooltip bottom>
          <template v-slot:activator="{ on, attrs }">
            <nuxt-link class="nav-item mx-2" :to="item.route">
              <v-btn small fab color="#488fef" v-bind="attrs" v-on="on">
                <v-icon color="white" link>{{ item.icon }}</v-icon>
              </v-btn>
            </nuxt-link>
          </template>
          <span>{{ item.name }}</span>
        </v-tooltip>
      </div>
    </v-row>
  </v-card>

  <!--    <v-card>-->
  <!--      <v-row class=" d-flex flex-row mb-6">-->
  <!--        <div v-for="item in items" :key="item.id">-->
  <!--          <v-tooltip bottom>-->
  <!--            <template v-slot:activator="{ on, attrs }">-->
  <!--              <nuxt-link class="nav-item " :to="item.route">-->
  <!--                <v-icon link v-bind="attrs" v-on="on">{{ item.icon }}</v-icon>-->
  <!--              </nuxt-link>-->
  <!--            </template>-->
  <!--            <span>{{ item.name }}</span>-->
  <!--          </v-tooltip>-->
  <!--        </div>-->
  <!--        <v-tooltip bottom>-->
  <!--          <template v-slot:activator="{ on, attrs }">-->
  <!--            <v-btn color="error" fab small depressed v-bind="attrs" v-on="on" @click="$router.back()">-->
  <!--              <v-icon>mdi-chevron-left</v-icon>-->
  <!--            </v-btn>-->
  <!--          </template>-->
  <!--          <span>Powrót do poprzedniej strony</span>-->
  <!--        </v-tooltip>-->
  <!--      </v-row>-->

  <!--    </v-card>-->

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
  /*position: fixed;*/
  background-color: transparent !important;
  border: none !important;
  box-shadow: none !important;
}
</style>
