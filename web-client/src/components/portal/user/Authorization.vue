<template>
<div>
  <router-link :to="{ 'name': 'Login' }">Zaloguj</router-link>
  <router-link :to="{ 'name': 'Register' }">Zarejestruj</router-link>
</div>
</template>

<script>
export default {
  name: "Authorization"
}
</script>

<style scoped>

</style>