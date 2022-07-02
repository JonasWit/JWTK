<template>
  <div class="bottom-0 sticky z-10 main-div">
    <ul class="text-sm flex flex-row justify-center p-5 mb=2">
      <li>
        <HomeRouterLink />
      </li>
      <li>
        <HomeRouterLink />
      </li>
      <li class="absolute bottom-6">
        <AboutRouterLink />
      </li>
      <li v-if="!isAuth">
        <LoginRouterLink />
      </li>
      <li v-if="isAuth">
        <ProfileRouterLink />
      </li>
      <li v-if="isAuth">
        <LogoutRouterLink />
      </li>
      <li>

      </li>
    </ul>
  </div>

</template>

<script>
import {useStore} from "vuex";
import {computed} from "vue";
import AboutRouterLink from "@/components/portal/AboutRouterLink";
import HomeRouterLink from "@/components/portal/HomeRouterLink";
import LoginRouterLink from "@/components/portal/LoginRouterLink";
import LogoutRouterLink from "../LogoutRouterLink.vue";
import ProfileRouterLink from "../ProfileRouterLink.vue";


export default {
  name: "MainNav",
  components: {
    AboutRouterLink,
    HomeRouterLink,
    LoginRouterLink,
    LogoutRouterLink,
    ProfileRouterLink
},
  setup() {
    const store = useStore()
   
    return {
      isAuth: computed(() => store.getters["auth/isAuthorized"]),
      
   
         }
  }
}
</script>

<style scoped>

.main-div {
  background-color: #3c2560;
  color: #ffd606;
}



</style>