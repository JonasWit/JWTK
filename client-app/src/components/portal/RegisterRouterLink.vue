<template>
  <router-link :to="{ 'name': 'Register' }" class="px-4 flex flex-col nav-link" >
   <svg class="h-6 w-6 m-auto" fill="none" stroke="currentColor" viewBox="0 0 24 24"
      xmlns="http://www.w3.org/2000/svg">
      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
        d="M18 9v3m0 0v3m0-3h3m-3 0h-3m-2-5a4 4 0 11-8 0 4 4 0 018 0zM3 20a6 6 0 0112 0v1H3v-1z"></path>
    </svg>
    <span class="inline-flex items-center mt-2">Rejestracja</span>
   </router-link>
</template>

<script>
export default {
  name: "RegisterRouterLink"
}
</script>

<style scoped>


</style>