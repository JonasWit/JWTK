<template>
  <router-link :to="{ 'name': 'ForgotPassword' }" class="flex border-r-4"
               :class="{ 'border-customClassicBlue': $route.path === '/auth/forgot-password',  'border-white': $route.path !== '/auth/forgot-password'}">
    <span class="mt-3  text-xs text-gray-600 hover:underline">Zapomniałeś hasła?</span>
  </router-link>
</template>

<script>
export default {
  name: "ForgotPasswordRouterLink"
}
</script>

<style scoped>

</style>