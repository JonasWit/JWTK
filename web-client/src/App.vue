<template>
  <Nav>
    <MainNav />
  </Nav>
  <main class="px-12 py-6 bg-gray-100">
    <Authorization/>
    <router-view/>
  </main>
</template>

<script>
import Nav from "@/components/portal/navigation/Nav";
import Authorization from "@/components/portal/user/Authorization";
import MainNav from "@/components/portal/navigation/MainNav";

export default {
  components: {MainNav, Authorization, Nav},
  // setup() {
  //   const store = useStore()
  //
  //   return {
  //     nav: computed(() => store.state.navType),
  //   }
  //
  //
  // }
}
</script>
<style lang="scss">

</style>
