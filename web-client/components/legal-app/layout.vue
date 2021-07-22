<template>
  <v-container>
    <v-row no-gutters>
      <!--      <v-col cols="2">-->
      <!--                <navigation-drawer/>-->
      <!--      </v-col>-->
      <v-col cols="12" class="my-5">
        <breadcrumbs/>
        <slot name="content">
        </slot>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>


import NavigationDrawer from "./navigation-drawer";
import Breadcrumbs from "@/components/legal-app/breadcrumbs";

export default {
  name: "layout",
  components: {Breadcrumbs, NavigationDrawer},
};
</script>

<style scoped>

</style>
