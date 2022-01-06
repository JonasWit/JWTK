<template>
  <div class="flex justify-center md:justify-end">
    <router-link class="portal-button text-customClassicBlue border-customClassicBlue md:border-2 hover:bg-customClassicBlue hover:text-white" :to="{ 'name': 'Login' }">Zaloguj</router-link>
    <router-link class="portal-button text-customClassicBlue border-customClassicBlue md:border-2 ml-1 hover:bg-customClassicBlue hover:text-white" :to="{ 'name': 'Register' }">Zarejestruj</router-link>
  </div>
</template>

<script>
export default {
  name: "Authorization"
}
</script>

<style scoped>

</style>