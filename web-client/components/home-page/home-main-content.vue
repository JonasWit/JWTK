<template>
  <v-container>
    <h1>Oferta</h1>
    <v-row class="d-flex justify-space-between">
      <v-card id="card" v-for="item in items" :key="item.id" width="350px" class="mx-auto my-2">
        <v-card-title class="text-uppercase text-center">{{ item.title }}</v-card-title>
        <v-icon large color="white">{{ item.icon }}</v-icon>
        <v-card-text>{{ item.text }}</v-card-text>
      </v-card>

    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "home-main-content",
  data: () => {
    return ({
      items: [
        {
          id: 1,
          icon: 'mdi-shield',
          title: 'Bezpieczeństwo',
          text: 'Kładziemy nacisk na bezpieczeńswto naszych aplikacji. Korzystamy z najnowszych rozwiązań w zakresie bezpieczeństwa baz danych i bezpieczeństwa w sieci.'
        },
        {
          id: 2,
          icon: 'mdi-lightbulb-on',
          title: 'Innowacyjność',
          text: 'Korzystamy z najnowszych technologii zapewniających bezproblemowe funkcjonowanie naszych rozwiązań.\n' +
            '\n'
        },
        {
          id: 3,
          icon: 'mdi-message-text-outline',
          title: 'Doradztwo',
          text: 'Pomagamy analizować potrzeby firmy celem dostosowania najlepszych rozwiązań usprawniających funkcjonowanie przedsiębiorstwa.'
        },
        {
          id: 4,
          icon: 'mdi-application',
          title: 'Aplikacje webowe',
          text: 'Zajmujemy się tworzeniem aplikacji webowych oraz późniejszym wsparciem technicznym dla klientów.'
        },
        {
          id: 5,
          icon: 'mdi-laptop',
          title: 'Aplikacje desktopowe',
          text: 'Część naszych usług stanowią również aplikacje desktopowe.'
        },
        {
          id: 6,
          icon: 'mdi-head-lightbulb-outline',
          title: 'Indywidualne rozwiązania',
          text: 'Szukasz innych rozwiązań? Sprawdź co jeszcze możemy dla Ciebie zrobić.'
        },

      ],

    });
  },
  created() {
    console.log(this.items);
  },

};
</script>

<style scoped>

</style>
