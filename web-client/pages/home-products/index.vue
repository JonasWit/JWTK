<template>
  <v-container>
    <v-row class="my-4">
      <v-row class="header my-5 py-2 px-2"><h1>Nasza oferta</h1>
      </v-row>
      <div class="py-3">
        <p>Tworzymy zarówno <span class="title">statyczne strony internetowe jak również aplikacje webowe.</span></p>
        <p>Zamów u nas stronę lub aplikację a
          my zajmiemy się jej stowrzeniem i utrzymywaniem jej przy życiu. Nie będziesz musiał się niczym przejmować!</p>
      </div>
    </v-row>

    <v-row class="my-3">
      <v-divider color="crimson"></v-divider>
    </v-row>

    <v-row class="d-flex justify-space-between mb-4">
      <v-card id="card" v-for="item in items" :key="item.id" width="350px" class="mx-auto my-2">
        <v-img height="100%" :src="item.img" class="white--text align-center"
               gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)">
          <v-row align="center" justify="space-around">
            <img class="icon" :src="item.icon">
          </v-row>
          <v-card-title class="text-uppercase justify-center">{{ item.title }}</v-card-title>
          <v-card-text>{{ item.text }}</v-card-text>
        </v-img>

      </v-card>
    </v-row>
    <v-row class="my-5">
      <v-divider color="crimson"></v-divider>
    </v-row>
    <v-row>
      <p>Zapraszamy do kontaktu, aby poznać wycenę usług.</p>
    </v-row>
    <pricing/>
  </v-container>
</template>

<script>
import Pricing from "@/components/home-page/pricing";

export default {
  name: "home-products",
  components: {Pricing},
  transition: 'slide-bottom',
  data: () => {
    return ({
      items: [
        {
          id: 1,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: `${require('~/assets/images/icons8-website-100.png')}`,
          title: 'Strony i aplikcaje',
          text: 'Tworzymy zarówno statyczne strony internetowe jak również aplikacje webowe. ' +
            'Zamów u nas stronę lub aplikację a my zajmiemy się jej stowrzeniem i utrzymywaniem przy życiu. Nie będziesz musiał się niczym przejmować!'
        },

        {
          id: 2,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: `${require('~/assets/images/icons8-scales-100.png')}`,
          title: 'Gotowe produkty',
          text: 'Jeśli prowadzisz kancelarię prawną, zapraszamy do zapoznania się z ofertą aplikacji Twoja Kancelaria.'
        },


        {
          id: 3,
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
