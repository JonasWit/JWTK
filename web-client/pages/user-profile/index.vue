<template>
  <div>
    <prof-main-header class="mb-4"/>
    <div>
      <div>
        <prof-personal-data/>
      </div>
      <if-auth>
        <template v-slot:allowed="{portalAdmin, legalAppAllowed, client, clientAdmin}">
          <div v-if="legalAppAllowed && (portalAdmin || clientAdmin)" class="mt-4">
            <prof-legal-application-details v-if="profile.legalAppAllowed"/>
          </div>
        </template>
      </if-auth>
    </div>
  </div>
</template>

<script>
import {mapState} from "vuex";

export default {
  middleware: ["client"],
  data: () => ({}),
  computed: {
    ...mapState("auth", ["profile"]),
  },
};
</script>

<style scoped></style>
