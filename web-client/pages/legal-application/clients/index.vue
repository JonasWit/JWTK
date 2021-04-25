<template>
  <v-container fluid>
    <v-row no-gutters>
      <v-col cols="2">
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

      </v-col>
      <v-col cols="9">


        <v-card
          class="mx-auto"
          max-width="344"
        >
          <v-card-text>
            <div>Word of the Day</div>
            <p class="display-1 text--primary">
              be•nev•o•lent
            </p>
            <p>adjective</p>
            <div class="text--primary">
              well meaning and kindly.<br>
              "a benevolent smile"
            </div>
          </v-card-text>
          <v-card-actions>
            <v-btn
              text
              color="deep-purple accent-4"
            >
              Learn More
            </v-btn>
          </v-card-actions>
        </v-card>

      </v-col>

    </v-row>
  </v-container>
</template>

<script>
import {legalappRoute} from "@/data/legal-app/legal-app-navigation";

export default {
  name: "index",
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
