<template>
<div class="flex justify-center md:justify-end">
  <router-link :to="{ 'name': 'Login' }">Zaloguj</router-link>
  <router-link class="ml-2" :to="{ 'name': 'Register' }">Zarejestruj</router-link>
</div>
</template>

<script>
export default {
  name: "Authorization"
}
</script>

<style scoped>

</style>