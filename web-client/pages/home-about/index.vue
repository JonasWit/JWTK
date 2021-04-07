<template>
  <v-container>
    <v-row class="my-4">
      <v-row class="header my-5 py-2 px-2"><h1>Kim jesteśmy ?</h1>
      </v-row>
      <p>
        <span class="title">Systemy Wspomagania Pracy</span> to rozwiązania dostosowane do potrzeb Twojej firmy.
        Tworzymy <span class="subtitle-1">strony internetowe</span> i
        rozwiązania, które wspierają <span class="subtitle-1">funkcjonowanie biznesu</span>. </p>
      <p>Inwestujemy w rozwój i podążamy za nowoczesnymi technologiami.</p>
      <p>Programowanie to nasza pasja, dlatego do każdego zlecenia podchodzimy w sposób indywidualny
        skupiając się na potrzebach Klienta.</p>
      <p class="title">Zapraszamy do współpracy!</p>

    </v-row>
    <v-row class="py-4">
      <v-divider color="crimson"></v-divider>
    </v-row>
    <v-row class="d-flex justify-space-between">
      <v-card id="card" v-for="item in items" :key="item.id" width="350px" class="mx-auto my-2">
        <v-img :src="item.img" class="white--text align-center" gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)"
               height="100%">
          <v-row align="center" justify="space-around" class="my-2">
            <img :src="item.icon">
          </v-row>
          <v-card-title class="text-uppercase justify-center">{{ item.title }}</v-card-title>
          <v-card-text>{{ item.text }}</v-card-text>
        </v-img>
      </v-card>
    </v-row>
    <v-row class="py-4">
      <v-divider color="crimson"></v-divider>
    </v-row>
    <v-row>

    </v-row>
  </v-container>
</template>
<script>

export default {
  name: "home-main-content",
  transition: 'bounce',
  data: () => {
    return ({
      items: [
        {
          id: 1,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: `${require('~/assets/images/icons8-security-configuration-100.png')}`,
          title: 'Bezpieczeństwo',
          text: 'Kładziemy nacisk na bezpieczeńswto naszych aplikacji. Korzystamy z najnowszych rozwiązań w zakresie bezpieczeństwa baz danych i bezpieczeństwa w sieci.'
        },
        {
          id: 2,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: `${require('~/assets/images/icons8-innovation-100.png')}`,
          title: 'Innowacyjność',
          text: 'Korzystamy z najnowszych technologii zapewniających bezproblemowe funkcjonowanie naszych rozwiązań.\n' +
            '\n'
        },
        {
          id: 3,
          img: `${require('~/assets/images/background.jpg')}`,
          icon: `${require('~/assets/images/icons8-short-hair-girl-question-mark-100.png')}`,
          title: 'Elastyczność',
          text: 'Naszym celem jest zadowolenie klienta! Dlatego, realizując nasze projekty i nie wiem co tutaj napisać dalej. POMOCY!!!'
        },


      ],

    });
  },
  created() {
    console.log(this.items);
  },

};
</script>

<style>

.header {
  display: block;
  border-bottom: solid 1px crimson;
}

</style>
