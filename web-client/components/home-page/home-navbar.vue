<template>
  <v-row class="ml-2">
    <v-list selectable width="100%" class="nav-list d-flex justify-space-between">
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
        route: '/home-pricing',
        name: 'Cennik'
      },
      {
        id: '5',
        route: '/home-legal-app',
        name: 'Twoja Kancelaria'
      },
      {
        id: '6',
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
  font-weight: bold;
  padding: 2px 10px;
}

.nav-item:hover {
  display: block;
  border-bottom: solid 1px crimson;

}

</style>
