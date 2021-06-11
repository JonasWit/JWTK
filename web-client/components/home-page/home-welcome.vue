<template>
  <div>
    <home-video/>
    <div style="position:absolute; top: 10%; z-index: 1; padding: 25px; ">
      <h1>Tworzymy aplikacje przyszłości</h1>
      <h5 class="text--secondary">Naszą misją jest optymalizacja każdego aspektu pracy Twojej firmy.</h5>
      <h5 class="text--secondary">Zawsze używany najnowszej dostępnej wersji danej technologii, z nami nigdy nie
        zostaniesz w tyle.</h5>
      <v-divider class="mt-4 mb-4"/>
      <div class="d-flex justify-space-between">
        <nuxt-link to="/portal-web/offer" class="text-decoration-none">
          <v-btn class="my-3" depressed color="primary">Sprawdź ofertę</v-btn>
        </nuxt-link>
        <nuxt-link to="/portal-web/contact" class="text-decoration-none">
          <v-btn class="my-3" depressed color="secondary">Kontakt</v-btn>
        </nuxt-link>
      </div>
    </div>

  </div>
</template>
<script>

export default {
  name: "home-welcome",
};
</script>

<style scoped>
container {
  padding: 0 !important;
}

</style>
