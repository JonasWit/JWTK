<template>
  <v-footer color="tool" padless>
    <v-row justify="center" no-gutters>
      <v-btn v-for="link in links" :key="link" color="white" text rounded class="my-2">
        {{ link }}
      </v-btn>
      <v-col class="tool lighten-2 py-4 text-center white--text" cols="12">
        {{ new Date().getFullYear() }} — <strong>SystemyWp</strong>
      </v-col>
      <quick-links/>
    </v-row>
  </v-footer>
</template>

<script>
import QuickLinks from "@/components/home-page/quick-links";

export default {
  name: "portal-footer",
  components: {QuickLinks},
  data: () => ({
    links: [
      'Home',
      'Kontakt',
    ],
  }),
};
</script>

<style scoped>

</style>
