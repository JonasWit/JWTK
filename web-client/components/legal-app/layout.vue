<template>
  <v-container>
    <v-row no-gutters>
      <v-col cols="12" class="my-5">
        <breadcrumbs/>
        <reminders-floating-icon/>
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

export default {
  name: "layout",
  components: {RemindersFloatingIcon, Breadcrumbs, NavigationDrawer},
  beforeMount() {
    console.warn('reading tooltip cookies');
    return this.readStatusOfLegalAppTooltips();
  },
  methods: {
    ...mapActions('cookies-store', ['readStatusOfLegalAppTooltips'])
  },
};
</script>

<style scoped>

</style>
