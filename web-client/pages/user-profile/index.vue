<template>
  <div>
    <prof-main-header class="mb-4"/>
    <div>
      <div>
        <prof-personal-data/>
      </div>
      <div class="mt-4">
        <prof-legal-application-details v-if="profile.legalAppAllowed"/>
      </div>
    </div>
  </div>
</template>

<script>
import {mapGetters, mapState} from "vuex";

export default {
  middleware: ["client"],
  data: () => ({}),
  computed: {
    ...mapGetters("auth", ['clientAdmin', 'client']),
    ...mapState("auth", ["profile"]),
  },
};
</script>

<style scoped></style>
