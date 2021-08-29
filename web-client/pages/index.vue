<template>
  <v-container fluid>
    <section class="first-section">
      <v-row class="d-flex justify-space-between mx-9">
        <v-col sm="12" xs="12" md="6" class="content">
          <h1>Systemy Wspomagania Pracy</h1>
          <h5 class="text--secondary">Zwiększ efektywność swojego biznesu poprzez automatyzację zarządzania i
            procesów.</h5>
          <h5 class="text--secondary">Utrzymujemy nasze produkty w najnowszej dostępnej wersji danej technologii,
            dlatego możesz mieć
            pewność, że z nami nigdy nie
            zostaniesz w tyle!</h5>
          <v-divider class="mt-4 mb-4"/>
          <div class="d-flex ">
            <nuxt-link to="/portal-web/offer" class="text-decoration-none">
              <v-btn class="my-3 ml-7" outlined color="primary">Oferta</v-btn>
            </nuxt-link>
            <v-spacer></v-spacer>
            <nuxt-link to="/portal-web/contact" class="text-decoration-none">
              <v-btn class="my-3 mr-7" outlined color="#00a38b">Kontakt</v-btn>
            </nuxt-link>
          </div>
        </v-col>
        <v-col sm="12" xs="12" md="6" class="content-video">
          <video v-if="darkThemeStored" :src="videoDark" autoplay="true" loop="true" muted></video>
          <video v-else :src="videoLight" autoplay="true" loop="true" muted></video>
        </v-col>
      </v-row>
    </section>
  </v-container>
</template>

<script>

import {mapState} from "vuex";


export default {
  data: () => ({
    videoDark: `${require('~/assets/images/home-page-video.mp4')}`,
    videoLight: `${require('~/assets/images/home-video-light.mp4')}`,
  }),
  computed: {
    ...mapState('cookies-store', ['darkThemeStored']),
  },
};
</script>
<style scoped>
.first-section {
  position: relative;
  width: 100%;
  height: 78vh;
  display: flex;
  justify-content: center;
  /*align-items: center;*/
  overflow: hidden;
  /*background-color: azure;*/
  /*background: linear-gradient(45deg, midnightblue, #000000)*/
}

.first-section .content {
  position: relative;
  z-index: 1;
  text-align: left;
  margin-top: 25px;


}

.first-section .content h1 {
  font-size: 3.5vw;
  font-weight: 800 !important;
  line-height: 60px;
  padding: 10px 0px;
  letter-spacing: -.03em !important;
  margin-left: 10px;

}

.first-section .content h5 {
  font-size: 1.5vw;
  font-weight: 400 !important;
  padding: 10px 0px;
  margin-left: 15px;
}

.first-section .content-video {
  position: relative;
  z-index: 1;
}

video {
  height: 60vh;
  width: 100%;
  object-fit: cover
}

.first-section:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  /*background: black;*/
  /*background: linear-gradient(30deg, midnightblue, #000000);*/
  /*border-radius: 0 0 50% 50%/0 0 100% 100%;*/
  transform: scaleX(1.5);
}

@media only screen and (max-width: 960px) {
  .first-section .content h1 {
    font-size: 30px;
    line-height: 30px;
  }

  .first-section .content h5 {
    font-size: 15px;
    padding: 2px 0px;
  }

  .first-section {
    position: relative;
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: center;
    /*align-items: center;*/
    overflow: hidden;
    /*background-color: azure;*/
    /*background: linear-gradient(45deg, midnightblue, #000000)*/
  }
}
</style>
