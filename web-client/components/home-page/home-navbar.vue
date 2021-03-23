<template>
  <v-row>
    <ul class="nav-list d-flex justify-space-between">
      <li class="nav-item">
        <nuxt-link to="/home-main-content">O Nas</nuxt-link>
      </li>
      <li class="nav-item">
        <nuxt-link to="/home-main-content">Oferta</nuxt-link>
      </li>
      <li class="nav-item">
        <nuxt-link to="/home-products-kancelaria">Produkty</nuxt-link>
      </li>
      <li class="nav-item">
        <nuxt-link to="/home-contact">Kontakt</nuxt-link>
      </li>
      <li class="nav-item">
        <nuxt-link to="/home-pricing">Cennik</nuxt-link>
      </li>
    </ul>
  </v-row>
</template>

<script>
export default {
  name: "home-navbar",

};
</script>

<style scoped>
.nav-list {
  list-style: none;
  padding: 0;
  margin: 0;
}

.nav-item {
  margin: 0 10px;
}

.nav-item a {
  text-decoration: none;
  color: #fefefe;
}

.nav-item a:hover,
.nav-item a:active {
  color: #b4b4b4;
}
</style>
