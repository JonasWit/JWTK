<template>
  <div>
    <video id="video" :src="video" style="min-height: 100vh; width: 100%; object-fit: cover" autoplay="true" loop="true"
           muted></video>
    <div style="position:absolute; top: 10%; z-index: 1; padding: 25px; color: #FAFAFA">
      <h1>Witaj na stronie Systemy Wspomagania Pracy</h1>
      <h2>Tworzymy strony internetowe i aplikacje webowe</h2>
      <div class="d-flex justify-space-between">
        <nuxt-link to="/home-products" class="text-decoration-none">
          <v-btn class="my-3" depressed color="error">Sprawdź ofertę</v-btn>
        </nuxt-link>
        <nuxt-link to="/home-contact" class="text-decoration-none">
          <v-btn class="my-3" depressed color="error">Kontakt</v-btn>
        </nuxt-link>
      </div>
    </div>

  </div>
</template>
<script>

export default {
  name: "home-welcome",

  data: () => ({
    video: `${require('~/assets/images/home-page-video.mp4')}`
  }),

};
</script>

<style scoped>
container {
  padding: 0 !important;
}

</style>
