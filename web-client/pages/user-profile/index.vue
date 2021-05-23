<template>
  <div>
    <div v-if="authenticated">
      <main-header class="mb-4"/>
      <div>
        <div>
          <personal-data/>
        </div>
        <if-auth>
          <template v-slot:allowed="{portalAdmin, legalAppAllowed, client, clientAdmin}">
            <div v-if="legalAppAllowed && (portalAdmin || clientAdmin)" class="mt-4">
              <legal-application-details v-if="profile.legalAppAllowed"/>
            </div>
            <div>
              <account-remove/>
            </div>
          </template>
        </if-auth>
      </div>
    </div>
  </div>
</template>

<script>
import {mapGetters, mapState} from "vuex";
import LegalApplicationDetails from "@/components/user-profile/legal-application-details";
import MainHeader from "@/components/user-profile/main-header";
import PersonalData from "@/components/user-profile/personal-data";
import AccountRemove from "@/components/user-profile/account-remove";
import IfAuth from "@/components/auth/if-auth";

export default {
  components: {IfAuth, AccountRemove, PersonalData, MainHeader, LegalApplicationDetails},
  middleware: ["client", "authenticated"],
  data: () => ({}),
  computed: {
    ...mapState("auth", ["profile"]),
    ...mapGetters('auth', ['authenticated'])
  },
};
</script>

<style scoped></style>
