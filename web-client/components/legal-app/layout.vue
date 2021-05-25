<template>
  <v-container fluid>
    <v-row no-gutters>
      <v-col cols="2">
        <navigation-drawer/>
      </v-col>
      <v-col cols="9" class="my-5">
        <slot name="content">
        </slot>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>


import NavigationDrawer from "./navigation-drawer";

export default {
  name: "layout",
  components: {NavigationDrawer},
};
</script>

<style scoped>

</style>
