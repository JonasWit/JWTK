<template>
  <v-container>
    <v-row class="my-4">
      <v-card class="my-4" width="100vw" color="pink darken-2" dark><h2 class="px-2 py-3">Nasza oferta?</h2></v-card>
      <div>
        Tworzymy zarówno statyczne strony internetowe jak również aplikacje webowe. Zamów u nas stronę lub aplikację a
        my zajmiemy się jej stowrzeniem i utrzymywaniem jej przy życiu. Nie będziesz musiał się niczym przejmować!
      </div>
    </v-row>
    <v-row>
      <v-card class="my-1" width="100vw" color="pink darken-2" dark><h2 class="px-2 py-3">Co nas wyróżnia?</h2></v-card>
    </v-row>
    <v-row class="d-flex justify-space-between">

      <v-card id="card" v-for="item in items" :key="item.id" width="350px" class="mx-auto my-2">
        <v-img :src="item.img" class="white--text align-center" gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)">

          <v-card-title class="text-uppercase justify-center">{{ item.title }}</v-card-title>
          <v-card-text>{{ item.text }}</v-card-text>
        </v-img>
        <v-row align="center" justify="space-around">
          <img class="icon" :src="item.icon">
        </v-row>
      </v-card>
    </v-row>
    <v-row>

      <sticky-icons/>

    </v-row>


  </v-container>
</template>

<script>
export default {
  name: "home-products",

  data: () => {
    return ({
      items: [
        {
          id: 1,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: `${require('~/assets/images/icons8-website-100.png')}`,
          title: 'Strony internetowe',
          text: 'Tworzymy zarówno statyczne strony internetowe jak również aplikacje webowe.'
        },
        {
          id: 2,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: `${require('~/assets/images/icons8-monitor-100.png')}`,
          title: 'Aplikacje',
          text: 'Zamów u nas stronę lub aplikację a my zajmiemy się jej stowrzeniem i utrzymywaniem przy życiu. Nie będziesz musiał się niczym przejmować!'
        },
        {
          id: 3,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: `${require('~/assets/images/icons8-scales-100.png')}`,
          title: 'Gotowe produkty',
          text: 'Jeśli prowadzisz kancelarię prawną, zapraszamy do zapoznania się z ofertą aplikacji Twoja Kancelraia.'
        },


        {
          id: 4,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: `${require('~/assets/images/icons8-admin-settings-male-100.png')}`,
          title: 'Indywidualne rozwiązania',
          text: 'Szukasz innych rozwiązań? Sprawdź co jeszcze możemy dla Ciebie zrobić. Chętnie poznamy Twój biznes i stworzymy propozycję dostosowaną do Twoich potrzeb.'
        },

      ],

    });
  },
};
</script>

<style scoped>
.icon {
  padding: 20px;
}
</style>
