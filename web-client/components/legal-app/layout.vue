<template>
  <v-container fluid class="mx-3 my-4">
    <v-container>
      <v-row no-gutters>
        <v-col cols="2">
          <navigation-drawer/>
        </v-col>
        <v-col cols="9">
          <slot name="content"></slot>
          <reminders-floating-icon/>
          <options-floating-icon/>
        </v-col>
      </v-row>
    </v-container>

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
