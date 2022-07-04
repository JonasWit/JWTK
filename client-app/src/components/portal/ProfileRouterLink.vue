<template>
    <router-link :to="{ 'name': 'Profile' }" class="px-4 flex flex-col nav-link">
        <svg xmlns="http://www.w3.org/2000/svg" class="h-6 w-6 m-auto" viewBox="0 0 20 20" fill="currentColor">
            <path fill-rule="evenodd" d="M10 9a3 3 0 100-6 3 3 0 000 6zm-7 9a7 7 0 1114 0H3z" clip-rule="evenodd" />
        </svg>       
        <span class="inline-flex items-center mt-2">Profil</span>
    </router-link>
</template>

<script>
   
export default {
    name: "ProfileRouterLink"
       
    }
</script>

<style lang="scss" scoped>

</style>