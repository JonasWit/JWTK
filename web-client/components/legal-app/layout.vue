<template>
  <v-container>
    <v-row no-gutters>
      <v-col cols="12" class="my-5">
        <breadcrumbs/>
        <options-floating-icon/>
        <reminders-floating-icon/>
        <slot name="content">

        </slot>

      </v-col>
    </v-row>
  </v-container>
</template>
<script>


import NavigationDrawer from "./navigation-drawer";
import Breadcrumbs from "@/components/legal-app/breadcrumbs";
import RemindersFloatingIcon from "@/components/legal-app/reminders-floating-icon";
import OptionsFloatingIcon from "@/components/legal-app/options-floating-icon";

export default {
  name: "layout",
  components: {OptionsFloatingIcon, RemindersFloatingIcon, Breadcrumbs, NavigationDrawer},
};
</script>

<style scoped>

</style>
