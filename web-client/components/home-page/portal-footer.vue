<template>
  <v-footer padless>
    <v-row justify="center" no-gutters>

      <v-btn v-for="link in links"
             :key="link" text
             rounded
             class=" my-2"> {{ link }}
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
      'Polityka Prywatności',
      'Regulamin',
      'Kontakt',
      'Źródła',
    ],
  }),

};
</script>

<style scoped>

</style>
