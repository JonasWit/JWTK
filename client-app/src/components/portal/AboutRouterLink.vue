<template>
  <router-link :to="{ 'name': 'About' }" class="px-4 flex flex-col nav-link">
<svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 m-auto" fill="none" viewBox="0 0 24 24"
         stroke="currentColor">
      <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2"
            d="M19 21V5a2 2 0 00-2-2H7a2 2 0 00-2 2v16m14 0h2m-2 0h-5m-9 0H3m2 0h5M9 7h1m-1 4h1m4-4h1m-1 4h1m-5 10v-5a1 1 0 011-1h2a1 1 0 011 1v5m-4 0h4"/>
    </svg>
    <span class="inline-flex items-center mt-2">O Nas</span>
      </router-link>
</template>

<script>
export default {
  name: "AboutRouterLink"
}
</script>

<style>


</style>