<template>
  <router-link :to="{ 'name': 'About' }">
    <img class="logo" src="img/logos/systemywp-logo-transparent.png" />
   </router-link>
</template>

<script>
export default {
  name: "AboutRouterLink"
}
</script>

<style scoped>

.logo {
  height: 125px;
 
}
</style>