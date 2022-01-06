<template>
  <ul class="text-sm mt-6">
    <li class="py-1">
      <router-link :to="{ 'name': 'Home' }" class="px-4 flex justify-end border-r-4 "
                   :class="{ 'border-customClassicBlue': $route.path === '/',  'border-white': $route.path !== '/'}">
        <span class="inline-flex items-center">Strona główna</span>
        <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 ml-2" fill="none" viewBox="0 0 24 24"
             stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M3 12l2-2m0 0l7-7 7 7M5 10v10a1 1 0 001 1h3m10-11l2 2m-2-2v10a1 1 0 01-1 1h-3m-6 0a1 1 0 001-1v-4a1 1 0 011-1h2a1 1 0 011 1v4a1 1 0 001 1m-6 0h6"/>
        </svg>
      </router-link>
    </li>
    <li class="py-1">
      <router-link :to="{ 'name': 'About' }" class="px-4 flex justify-end border-r-4"
                   :class="{ 'border-customClassicBlue': $route.path === '/about',  'border-white': $route.path !== '/about'}">
        <span class="inline-flex items-center">O Nas</span>
        <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 ml-2" fill="none" viewBox="0 0 24 24"
             stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5M9 7h1m-1 4h1m4-4h1m-1 4h1m-5 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4"/>
        </svg>
      </router-link>
    </li>
    <li class="py-1">
      <router-link :to="{ 'name': 'Contact' }" class="px-4 flex justify-end border-r-4"
                   :class="{ 'border-customClassicBlue': $route.path === '/contact',  'border-white': $route.path !== '/contact'}">
        <span class="inline-flex items-center">Kontakt</span>
        <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 ml-2" fill="none" viewBox="0 0 24 24"
             stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M3 8l7.89 5.26a2 2 0 002.22 0L21 8M5 19h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z"/>
        </svg>
      </router-link>
    </li>
    <li class="py-1">
      <router-link :to="{ 'name': 'Privacy' }" class="px-4 flex justify-end border-r-4"
                   :class="{ 'border-customClassicBlue': $route.path === '/privacy',  'border-white': $route.path !== '/privacy'}">
        <span class="inline-flex items-center">Polityka Prywatności</span>
        <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 ml-2" fill="none" viewBox="0 0 24 24"
             stroke="currentColor">
          <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
                d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858.908a3 3 0 114.243 4.243M9.878 9.878l4.242 4.242M9.88 9.88l-3.29-3.29m7.532 7.532l3.29 3.29M3 3l3.59 3.59m0 0A9.953 9.953 0 0112 5c4.478 0 8.268 2.943 9.543 7a10.025 10.025 0 01-4.132 5.411m0 0L21 21"/>
        </svg>
      </router-link>
    </li>
  </ul>
</template>

<script>

export default {
  name: "MainNav",
  setup() {

  }
}
</script>

<style scoped>

</style>