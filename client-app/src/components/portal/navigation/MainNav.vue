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
import LoginRouterLink from "@/components/auth/LoginRouterLink";
import RegisterRouterLink from "@/components/auth/RegisterRouterLink";
import HomeRouterLink from "@/components/auth/HomeRouterLink";
import AboutRouterLink from "@/components/auth/AboutRouterLink";
import PrivacyRouterLink from "@/components/auth/PrivacyRouterLink";
import ContactRouterLink from "@/components/auth/ContactRouterLink";
import ChangePasswordRouterLink from "@/components/auth/ChangePasswordRouterLink";
import LogoutRouterLink from "@/components/auth/LogoutRouterLink";
import ToSRouterLink from "@/components/auth/ToSRouterLink";

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