<template>
  <router-link :to="{ 'name': 'DeleteAccount' }" class="px-4 flex justify-end border-r-4"
               :class="{ 'border-customClassicBlue': $route.path === '/auth/delete-account', 'border-white': $route.path !== '/auth/delete-account'}">
    <span class="inline-flex items-center">Usuń Konto</span>
    <svg class="h-6 w-6 ml-2" fill="none" stroke="currentColor" viewBox="0 0 24 24" xmlns="http://www.w3.org/2000/svg">
      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13 7a4 4 0 11-8 0 4 4 0 018 0zM9 14a6 6 0 00-6 6v1h12v-1a6 6 0 00-6-6zM21 12h-6" />
    </svg>
  </router-link>
</template>

<script>
export default {
  name: "DeleteAccountRouterLink"
}
</script>

<style scoped>

</style>