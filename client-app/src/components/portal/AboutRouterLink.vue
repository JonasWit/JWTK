<template>
  <router-link :to="{ 'name': 'About' }" class="nav-link">
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
  margin-bottom: 0px;
  margin-top: -75px;
}
</style>