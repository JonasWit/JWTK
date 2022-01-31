<template>
  <div :class="isDark ? 'dark' : ''">
    <div class="min-h-screen md:flex">

      <button @click="toggleDark">
        toggle Dark
      </button>

      <NavContainer class="flex-none w-full md:max-w-xs">
        <MainNav v-if="nav.navType === 'main'"/>
      </NavContainer>
      <main class="flex-1 px-12 py-6 bg-gray-100 dark:bg-gray-700 h-full min-h-screen">
        <router-view v-slot="{ Component }">
          <transition name="fade" mode="out-in">
            <component :is="Component"/>
          </transition>
        </router-view>
      </main>
    </div>
  </div>
</template>

<script>
import NavContainer from "@/components/portal/navigation/NavContainer";
import MainNav from "@/components/portal/navigation/MainNav";
import {useStore} from "vuex";
import {computed} from "vue";
import {NavModel} from "@/models/general/portalDisplayModel";
import SnackPopup from "@/components/generic/SnackPopup";

export default {
  components: {SnackPopup, MainNav, NavContainer},
  setup() {
    const store = useStore()

    function toggleDark() {
      store.dispatch('setDark')
    }

    return {
      nav: computed(() => Object.assign(NavModel, store.state.navType)),
      isDark: computed(() => store.getters["isDark"]),
      toggleDark
    }
  }
}
</script>
<style lang="scss">

</style>
