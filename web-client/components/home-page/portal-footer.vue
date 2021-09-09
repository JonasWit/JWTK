<template>
  <v-footer padless>
    <v-row no-gutters class="d-flex justify-center">

      <div class="btn-link pa-2" v-for="link in links" :key="link.id" text rounded>
        <nuxt-link class="link" text :to="link.route">{{ link.name }}
        </nuxt-link>
      </div>

      <div class="pa-2">
        {{ new Date().getFullYear() }} — <strong>SystemyWP</strong>
      </div>
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
        route: '/portal-web/privacy-policy',
        name: 'Polityka Prywatności'
      },
      {
        id: '3',
        route: '/portal-web/terms-of-service',
        name: 'Regulamin'
      },
      {
        id: '4',
        route: '/portal-web/sources',
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
  display: block;
  opacity: 0.6;
  transition: opacity 0.5s ease;
}
</style>
