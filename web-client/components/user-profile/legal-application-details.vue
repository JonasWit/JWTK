<template>
  <div>
    <v-card class="mb-2">
      <v-card-title class="d-flex justify-center">Moja Kancelaria</v-card-title>
      <prof-legal-app-user-statistics/>
    </v-card>
  </div>
</template>

<script>
import {mapActions} from "vuex";

export default {
  name: "legal-application-details",
  beforeMount() {
    this.getRelatedUsers();
  },
  methods: {
    ...mapActions('profile-panel-store', ['getRelatedUsers'])
  }
};
</script>

<style scoped></style>
