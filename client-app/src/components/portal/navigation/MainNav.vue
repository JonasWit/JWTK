<template>
  <div class="bottom-0 sticky z-10 main-div">
       <ul class="text-sm flex flex-row justify-center p-5">
      <li>
        <HomeRouterLink />
      </li>
      <li class="">
        <OfferRouterLink />
      </li>
      <li class="">
        <AboutRouterLink />
      </li>
      <li v-if="!isAuth" class="">
        <LoginRouterLink />
      </li>
      <li v-if="isAuth" class="">
        <ProfileRouterLink />
      </li>
      <li v-if=" isAuth">
        <LogoutRouterLink />
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
import OfferRouterLink from "../OfferRouterLink.vue";


export default {
  name: "MainNav",
  components: {
    AboutRouterLink,
    HomeRouterLink,
    LoginRouterLink,
    LogoutRouterLink,
    ProfileRouterLink,
    OfferRouterLink
},
  setup() {
    const store = useStore()
   
    return {
      isAuth: computed(() => store.getters["auth/isAuthorized"]),
      
   
         }
  }
}
</script>

<style>

.main-div {
  background-color: #3c2560;
  color: #ffd606;
}


</style>