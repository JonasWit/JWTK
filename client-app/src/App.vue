<template>
  <div>
    <SnackPopup/>
    <div class="grid md:grid-cols-8">
      <NavContainer class="md:col-span-2">
        <MainNav v-if="nav.navType === 'main'"/>
      </NavContainer>
      <main class="px-12 py-6 bg-gray-100 md:col-span-6 md:flex md:flex-col md:h-screen">
        <!--      <router-view/>-->
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

    return {
      nav: computed(() => Object.assign(NavModel, store.state.navType)),
    }
  }
}
</script>
<style lang="scss">

</style>
