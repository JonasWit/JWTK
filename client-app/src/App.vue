<template>
  <div :class="isDark ? 'dark' : ''">
    <div>
      <SnackPopup />
      <GdprModal v-if="!gdprAccepted"></GdprModal>
     
      <main class="flex-1 bg-gray-100 dark:bg-gray-700 h-full min-h-screen">
        <router-view v-slot="{ Component, route }">
          <transition name="fade">
            <div :key="route.name">
              <component :is="Component"></component>
            </div>
          </transition>
        </router-view>
      </main>
    </div> 
    <MainNav  v-if="nav.navType === 'main'" />

    <!-- <PortalFooter class="bottom-0 sticky z-10" /> -->
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
