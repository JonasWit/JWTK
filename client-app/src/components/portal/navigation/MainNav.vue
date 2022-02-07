<template>
  <ul class="text-sm mt-6">
    <li class="py-1">
      <HomeRouterLink/>
    </li>
    <li class="py-1">
      <ContactRouterLink/>
    </li>
    <li class="py-1">
      <AboutRouterLink/>
    </li>
    <li class="py-1">
      <PrivacyRouterLink/>
    </li>
    <li class="py-1">
      <ToSRouterLink/>
    </li>
    <li v-if="!isAuth" class="py-1">
      <LoginRouterLink/>
    </li>
    <li v-if="!isAuth" class="py-1">
      <RegisterRouterLink/>
    </li>
    <li v-if="isAuth" class="py-1">
      <ChangePasswordRouterLink/>
    </li>
    <li v-if="isAuth" class="py-1">
      <LogoutRouterLink/>
    </li>
  </ul>
</template>

<script>
import {useStore} from "vuex";
import {computed} from "vue";
import LogoutRouterLink from "@/components/portal/LogoutRouterLink";
import ToSRouterLink from "@/components/portal/ToSRouterLink";
import ChangePasswordRouterLink from "@/components/portal/ChangePasswordRouterLink";
import ContactRouterLink from "@/components/portal/ContactRouterLink";
import PrivacyRouterLink from "@/components/portal/PrivacyRouterLink";
import AboutRouterLink from "@/components/portal/AboutRouterLink";
import HomeRouterLink from "@/components/portal/HomeRouterLink";
import RegisterRouterLink from "@/components/portal/RegisterRouterLink";
import LoginRouterLink from "@/components/portal/LoginRouterLink";
export default {
  name: "MainNav",
  components: {
    ToSRouterLink,
    LogoutRouterLink,
    ChangePasswordRouterLink,
    ContactRouterLink,
    PrivacyRouterLink,
    AboutRouterLink, HomeRouterLink, RegisterRouterLink, LoginRouterLink},
  setup() {
    const store = useStore()

    return {
      isAuth: computed(() => store.getters["auth/isAuthorized"]),
    }
  }
}
</script>

<style scoped>

</style>