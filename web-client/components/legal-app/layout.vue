<template>
  <v-container>
    <v-row class="my-6">
      <v-col cols="12">
        <navigation-drawer/>
      </v-col>
    </v-row>
    <v-row>
      <v-col cols="12">
        <reminders-floating-icon/>
        <options-floating-icon/>
        <slot name="content"></slot>
      </v-col>
    </v-row>
  </v-container>
</template>
<script>


import RemindersFloatingIcon from "@/components/legal-app/reminders-floating-icon";
import {mapActions} from "vuex";
import OptionsFloatingIcon from "@/components/legal-app/options-floating-icon";
import NavigationDrawer from "@/components/legal-app/navigation-drawer";

export default {
  name: "layout",
  components: {NavigationDrawer, OptionsFloatingIcon, RemindersFloatingIcon},
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
