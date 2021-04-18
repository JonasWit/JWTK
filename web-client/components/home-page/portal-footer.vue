<template>
  <v-footer padless>
    <v-row justify="center" no-gutters>

      <v-btn class="btn-link my-2" v-for="link in links" :key="link.id" text rounded>
        <nuxt-link class="link" text :to="link.route">{{ link.name }}
        </nuxt-link>
      </v-btn>

      <v-col class="pa-5 text-center" cols="12">
        {{ new Date().getFullYear() }} — <strong>SystemyWp</strong>
      </v-col>
    </v-row>
  </v-footer>
</template>

<script>

export default {
  name: "portal-footer",
  data: () => ({
    links: [
      {
        id: '1',
        route: '/',
        name: 'Home'
      },
      {
        id: '',
        route: '/home-privacy-policy',
        name: 'Polityka Prywatności'
      },
      {
        id: '3',
        route: '/home-statute',
        name: 'Regulamin'
      },
      {
        id: '4',
        route: '/home-contact',
        name: 'Kontakt'
      },
      {
        id: '5',
        route: '/home-sources',
        name: 'Źródła'
      },

    ],
  }),

};
</script>

<style scoped>
.link {
  text-decoration: none;
}

.btn-link:hover {
  background-color: var(--v-secondary-lighten1);
}
</style>
