<template>
  <div class="md:flex md:justify-end">
    <nav class="text-right">
      <div class="flex justify-between items-center">
        <h1 class="font-bold uppercase p-4 border-b border-gray-100">
          <router-link class="text-customClassicBlue hover:text-blue-400 transition ease-in-out delay-100" to="/">
            systemywp.pl
          </router-link>
        </h1>
        <div class="px-4 cursor-pointer md:hidden" @click="handleSubmit">
          <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M4 6h16M4 12h16M4 18h16"/>
          </svg>
        </div>
      </div>
      <div class="hidden md:block" id="menu">
        <slot></slot>
      </div>
    </nav>
  </div>
</template>

<script>
export default {
  name: "NavContainer",
  setup() {
    const handleSubmit =  () => {
      const menu = document.querySelector('#menu');

        if (menu.classList.contains('hidden')) {
          menu.classList.remove('hidden');
        } else {
          menu.classList.add('hidden');
        }
    }
    return { handleSubmit }
  }
}
</script>

<style scoped>

</style>