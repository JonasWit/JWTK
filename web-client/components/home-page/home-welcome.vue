<template>
  <div>
    <video id="video" :src="video" style="max-height: 700px; height: 100vh; min-width: 100%; object-fit: cover"
           autoplay="true" loop="true"></video>
    <div style="position:absolute; top: 10%; z-index: 1; padding: 25px;">
      <div>
        <h1>Witaj na stronie Systemy Wspomagania Pracy</h1>
        <h2>Tworzymy strony internetowe i aplikacje webowe</h2>
        <div class="d-flex justify-space-between">
          <v-btn class="my-3">Sprawdź ofertę</v-btn>
          <v-btn class="my-3">Kontakt</v-btn>
        </div>
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

}
</script>

<style scoped>


</style>
