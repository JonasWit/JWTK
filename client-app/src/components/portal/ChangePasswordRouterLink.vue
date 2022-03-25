<template>
  <router-link :to="{ 'name': 'ChangePassword' }" class="px-4 flex justify-end border-r-4"
               :class="{ 'border-customClassicBlue': $route.path === '/auth/changepassword',  'border-white': $route.path !== '/auth/changepassword'}">
    <span class="inline-flex items-center">Zmień Hasło</span>
    <svg class="h-6 w-6 ml-2" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="M15 7a2 2 0 012 2m4 0a6 6 0 01-7.743 5.743L11 17H9v2H7v2H4a1 1 0 01-1-1v-2.586a1 1 0 01.293-.707l5.964-5.964A6 6 0 1121 9z"></path>
    </svg>
  </router-link>
</template>

<script>
export default {
  name: "ChangePasswordRouterLink"
}
</script>

<style scoped>

</style>