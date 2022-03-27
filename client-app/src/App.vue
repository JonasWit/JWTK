<template>
  <div :class="isDark ? 'dark' : ''" class="relative">
    <div class="min-h-screen md:flex">
      <SnackPopup/>
      <GdprModal v-if="!gdprAccepted"></GdprModal>
      <NavContainer class="flex-none w-full md:max-w-xs">
        <MainNav v-if="nav.navType === 'main'"/>
      </NavContainer>
      <main class="flex-1 bg-gray-100 dark:bg-gray-700 h-full min-h-screen">
        <router-view v-slot="{ Component }">
          <transition name="fade" mode="out-in">
            <component :is="Component"/>
          </transition>
        </router-view>
      </main>
    </div>
    <PortalFooter/>
  </div>
</template>

<script>
import NavContainer from "@/components/portal/navigation/NavContainer";
import MainNav from "@/components/portal/navigation/MainNav";
import {useStore} from "vuex";
import {computed} from "vue";
import {NavModel} from "@/models/general/portalDisplayModel";
import SnackPopup from "@/components/generic/SnackPopup";
import GdprModal from "@/components/portal/GdprModal";
import PortalFooter from "@/components/portal/PortalFooter";

export default {
  components: {PortalFooter, GdprModal, SnackPopup, MainNav, NavContainer},
  setup() {
    const store = useStore()
    return {
      nav: computed(() => Object.assign(NavModel, store.state.navType)),
      isDark: computed(() => store.getters["isDark"]),
      gdprAccepted: computed(() => store.getters["gdprAccepted"]),
    }
  }
}
</script>
<style lang="scss">

</style>
