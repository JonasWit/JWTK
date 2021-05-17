<template>
  <v-container fluid>
    <v-row no-gutters>
      <v-col cols="2">
        <navigation-drawer/>
      </v-col>
      <v-col cols="9" class="mt-4">
        <slot name="content">
        </slot>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>
import NavigationDrawer from "@/components/legal-app/navigation-drawer";

export default {
  name: "layout",
  components: {NavigationDrawer}
};
</script>

<style scoped>

</style>
