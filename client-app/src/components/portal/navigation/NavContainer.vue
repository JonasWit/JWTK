<template>
  <div class="md:flex md:justify-end">
    <nav class="text-right">
      <div class="flex justify-between items-center">
        <h1 class="font-bold uppercase p-4 border-b border-gray-100">
          <router-link class="text-customClassicBlue hover:text-blue-400 transition ease-in-out delay-100" to="/">
            systemywp.pl
          </router-link>
        </h1>
        <div class="px-4 cursor-pointer md:hidden" @click="toggleNav">
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16"/>
          </svg>
        </div>
      </div>
      <div class="hidden md:block" id="mainNavBar">
        <slot></slot>
      </div>
    </nav>
  </div>
</template>

<script>
import {toggleNavBar} from "@/appControl/controlFunctions";

export default {
  name: "NavContainer",
  setup() {
    const toggleNav =  toggleNavBar;
    return { toggleNav }
  }
}
</script>

<style scoped>

</style>