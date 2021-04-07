<template>
  <v-row class="ml-2">
    <v-list selectable width="100%" class="nav-list d-flex ">
      <v-list-item v-for="item in items" :key="item.id">
        <nuxt-link class="nav-item" :to="item.route">{{ item.name }}</nuxt-link>
      </v-list-item>
    </v-list>
  </v-row>
</template>

<script>
export default {
  name: "home-navbar",
  data: () => ({
    items: [
      {
        id: '1',
        route: '/',
        name: 'Home'
      },
      {
        id: '2',
        route: '/home-about',
        name: 'O Nas'
      },
      {
        id: '3',
        route: '/home-products',
        name: 'Oferta'
      },

      {
        id: '4',
        route: '/home-contact',
        name: 'Kontakt'
      },
    ],
  }),

};
</script>

<style scoped>

.nav-list {
  background: transparent;
}

.nav-item {
  white-space: nowrap;
  text-decoration: none;
}

.nav-item:hover {
  display: block;
  border-bottom: solid 1px crimson;
  opacity: 0.6;
  transition: opacity 0.5s ease;

}

</style>
