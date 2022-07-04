<template>
  <router-link :to="{ 'name': 'Login' }" class="px-4 flex flex-col nav-link">
     <svg fill="none" class="h-6 w-6 m-auto" stroke="currentColor" viewBox="0 0 24 24"
      xmlns="http://www.w3.org/2000/svg">
      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
        d="M11 16l-4-4m0 0l4-4m-4 4h14m-5 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h7a3 3 0 013 3v1"></path>
    </svg>
    <span class="inline-flex items-center mt-2">Logowanie</span>
 
  </router-link>
</template>

<script>
export default {
  name: "LoginRouterLink",
  setup() {

  }
}
</script>

<style scoped>

</style>