<template>
  <v-container>
    <v-row no-gutters>
      <v-col cols="12" class="my-5">
        <breadcrumbs/>
        <reminders-floating-icon/>
        <options-floating-icon/>
        <slot name="content"></slot>

      </v-col>
    </v-row>
  </v-container>
</template>
<script>


import NavigationDrawer from "./navigation-drawer";
import Breadcrumbs from "@/components/legal-app/breadcrumbs";
import RemindersFloatingIcon from "@/components/legal-app/reminders-floating-icon";
import {mapActions} from "vuex";
import OptionsFloatingIcon from "@/components/legal-app/options-floating-icon";

export default {
  name: "layout",
  components: {OptionsFloatingIcon, RemindersFloatingIcon, Breadcrumbs, NavigationDrawer},
  beforeMount() {
    return this.readStatusOfLegalAppTooltips();
  },
  methods: {
    ...mapActions('cookies-store', ['readStatusOfLegalAppTooltips'])
  },
};
</script>

<style scoped>

</style>
