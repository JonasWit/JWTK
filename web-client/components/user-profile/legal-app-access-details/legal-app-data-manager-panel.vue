<template>
  <div>

  </div>
</template>

<script>
import {mapActions} from "vuex";

export default {
  name: "legal-app-data-manager-panel",
  beforeMount() {
    this.getSummary();
  },
  methods: {
    ...mapActions('profile-panel-store', ['getSummary'])
  }
};
</script>

<style scoped>

</style>
