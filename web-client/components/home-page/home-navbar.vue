<template>
  <div class="ml-2">
    <v-list selectable width="100%" class="nav-list d-flex">
      <v-app-bar-nav-icon @click="drawer = !drawer" class="hidden-md-and-up"></v-app-bar-nav-icon>
      <v-list-item v-for="item in items" :key="item.id" class="hidden-sm-and-down">
        <nuxt-link class="nav-item" :to="item.route">{{ item.name }}</nuxt-link>
      </v-list-item>
      <v-menu open-on-hover offset-y>
        <template v-slot:activator="{ on, attrs }">
          <v-btn text color="primary" dark v-bind="attrs" v-on="on" class="nav-item pt-3">
            Oferta
          </v-btn>
        </template>
        <v-list>
          <v-list-item v-for="(item, index) in offerItems" :key="index">
            <nuxt-link class="nav-item" :to="item.route">
              {{ item.name }}
            </nuxt-link>
          </v-list-item>
        </v-list>
      </v-menu>
      <v-list-item>
        <nuxt-link class="nav-item" to="/portal-web/contact">
          Kontakt
        </nuxt-link>
      </v-list-item>
    </v-list>
    <v-navigation-drawer app v-model="drawer" absolute temporary>
      <v-list nav dense>
        <v-list-item-group v-model="group">
          <v-list-item v-for="item in items" :key="item.id">
            <v-list-item-icon>
              <v-icon>{{ item.icon }}</v-icon>
            </v-list-item-icon>
            <nuxt-link class="nav-item" :to="item.route">
              {{ item.name }}
            </nuxt-link>
          </v-list-item>
        </v-list-item-group>
      </v-list>
    </v-navigation-drawer>
  </div>
</template>

<script>
export default {
  name: "home-navbar",
  data: () => ({
    drawer: false,
    group: null,
    items: [
      {
        id: '1',
        route: '/',
        name: 'Home',
        icon: 'mdi-home'
      },
      {
        id: '2',
        route: '/portal-web/about',
        name: 'O Nas',
        icon: 'mdi-account-group'
      },
    ],
    offerItems: [
      {
        id: '1',
        route: '/portal-web/offer',
        name: 'Oferta i cennik',
        icon: 'mdi-tag',
      },
      {
        id: '2',
        route: '/portal-web/moja-kancelaria',
        name: 'Moja Kancelaria',
        icon: 'mdi-tag',
      }

    ]
  }),

};
</script>

<style scoped>

.nav-list {
  background: transparent !important;

}

.nav-item {

  white-space: nowrap;
  text-decoration: none;
}

.nav-item:hover {
  display: block;
  opacity: 0.6;
  transition: opacity 0.5s ease;

}


</style>
