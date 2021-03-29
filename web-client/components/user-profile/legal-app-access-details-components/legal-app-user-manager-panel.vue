<template>
  <div>
    <div class="mb-1">
      <prof-legal-app-user-data/>
    </div>
    <v-divider/>
    <div>
      <prof-legal-app-user-access/>
    </div>
  </div>
</template>

<script>
import {mapActions} from "vuex";

export default {
  name: "legal-app-user-manager-panel",
  beforeMount() {
    this.getRelatedUsers();
  },
  methods: {
    ...mapActions('profile-panel-legal-app-store', ['getRelatedUsers']),
  }
};
</script>

<style scoped>

</style>
