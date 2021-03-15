<template>
  <div>
    <video id="video" :src="video" style="max-height: 500px; height: 100vh; min-width: 100%; object-fit: cover"
           autoplay="true" loop="true"></video>

    <transition>
      <div id="card" style="position:absolute; top: 10%; z-index: 999; ">
        <v-card elevation="23" outlined shaped tile style="max-width: 650px">
          <v-card-title>Witaj na stronie Systemy Wspomagania Pracy</v-card-title>
          <v-card-text>Tworzymy strony internetowe i aplikacje webowe.
            Do każdego zlecenia podchodzimy w sposób indywidualny skupiając się na potrzebach Klienta.
          </v-card-text>
          <v-card-subtitle>Zapraszamy do współpracy!</v-card-subtitle>
          <v-card-actions>
            <v-btn class="my-3">Sprawdź ofertę</v-btn>
          </v-card-actions>
        </v-card>
      </div>
    </transition>
  </div>
</template>
<script>

export default {
  name: "home-welcome",

  data: () => ({
    video: `${require('~/assets/images/blubki-video.mp4')}`
  }),

}
</script>

<style scoped>

#card {
  animation: fadeInAnimation ease 10s;
  animation-iteration-count: 1;
  animation-fill-mode: forwards;
}

@keyframes fadeInAnimation {
  0% {

    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}


</style>
