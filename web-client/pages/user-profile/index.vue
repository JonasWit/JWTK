<template>
  <div>
    <prof-main-header class="mb-4"/>
    <div>
      <prof-personal-data class="mb-4"/>
      <prof-legal-application-details v-if="profile.legalAppAllowed"/>
    </div>
  </div>
</template>

<script>

import {mapState} from "vuex";

export default {
  middleware: ["client"],
  data: () => ({}),
  computed: {
    ...mapState('auth', ['profile'])
  }
};

</script>

<style scoped>


</style>
