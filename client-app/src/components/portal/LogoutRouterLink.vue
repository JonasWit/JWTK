<template>
  <router-link :to="{ 'name': 'Logout' }" class="px-4 flex justify-end border-r-4"
               :class="{ 'border-customClassicBlue': $route.path === '/auth/logout', 'border-white': $route.path !== '/auth/logout'}">
    <span class="inline-flex items-center">Wyloguj</span>
    <svg class="h-6 w-6 ml-2" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1"></path>
    </svg>
  </router-link>
</template>

<script>
export default {
  name: "LogoutRouterLink",
}
</script>

<style scoped>

</style>