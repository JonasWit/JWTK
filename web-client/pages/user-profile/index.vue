<template>
  <div>
    <div v-if="authenticated">
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
            <div>
              <prof-account-remove/>
            </div>
          </template>
        </if-auth>
      </div>
    </div>
  </div>
</template>

<script>
import {mapGetters, mapState} from "vuex";

export default {
  middleware: ["client", "authenticated"],
  data: () => ({}),
  computed: {
    ...mapState("auth", ["profile"]),
    ...mapGetters('auth', ['authenticated'])
  },
};
</script>

<style scoped></style>
