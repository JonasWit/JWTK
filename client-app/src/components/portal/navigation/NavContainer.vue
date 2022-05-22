<template>
  <div class="md:flex md:justify-end bg-white">
    <nav class="text-right">
      <div class="flex justify-between items-center">
        <h1 class="font-bold uppercase p-4 border-b border-gray-100">
          <router-link class="text-customClassicBlue hover:text-blue-400 transition ease-in-out delay-100" to="/">
            systemywp.pl
          </router-link>
        </h1>
        <div class="px-4 cursor-pointer md:hidden" @click="toggleNav">
          <svg class="h-6 w-6" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
            <path d="M4 6h16M4 12h16M4 18h16" stroke-linecap="round" stroke-linejoin="round" stroke-width="2"/>
          </svg>
        </div>
      </div>
      <div id="mainNavBar" class="hidden md:block">
        <slot></slot>
      </div>
    </nav>
  </div>
</template>

<script>

import {useStore} from "vuex";

export default {
  name: "NavContainer",
  setup() {
    const store = useStore()

    function toggleNav() {
      store.commit('toggleNavBar')
    }

    return {
      toggleNav
    }
  }
}
</script>

<style scoped>

</style>