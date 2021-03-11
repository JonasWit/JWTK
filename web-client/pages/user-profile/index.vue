<template>
  <div>
    <prof-main-header class="mb-4" />
    <div>
      <v-row>
        <v-col md="8" sm="12" sx="12">
          <prof-personal-data />
        </v-col>
        <v-col md="4" sm="12" sx="12">
          <prof-legal-application-details v-if="profile.legalAppAllowed" />
        </v-col>
      </v-row>
    </div>
  </div>
</template>

<script>
import { mapState } from "vuex";

export default {
  middleware: ["client"],
  data: () => ({}),
  computed: {
    ...mapState("auth", ["profile"]),
  },
};
</script>

<style scoped>
</style>
