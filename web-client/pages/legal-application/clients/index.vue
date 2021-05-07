<template>
  <v-container fluid>
    <v-row no-gutters>
      <v-col cols="2">
        <navigation-drawer/>
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
import NavigationDrawer from "@/components/legal-app/navigation-drawer";

export default {
  name: "index",
  components: {NavigationDrawer},

}
</script>

<style scoped>

</style>
