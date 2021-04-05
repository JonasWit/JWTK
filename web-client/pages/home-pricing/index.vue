<template>
  <v-container>
    <v-row class="my-4">
      <v-row class="d-flex justify-space-between mb-2">
        <v-content class="my-4 title" width="100vw" color="purple darken-1" dark>Sprawdź przykładowy cennik naszych
          usług.
          Aby
          poznać szczegóły zapraszamy do
          <nuxt-link to="/home-contact">kontaktu.</nuxt-link>
        </v-content>
      </v-row>
      <v-row>
        <v-card v-for="item in items" :key="item.id" width="380px" class="mx-auto my-8" elevation="10">
          <v-img :src="item.img" class="white--text align-center" gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)"
                 height="120px">
            <v-card-text class="text-center font-weight-medium headline">{{ item.title }}</v-card-text>
            <v-card width="250px" height="80px" class="price text-center white--text title mb-2">{{
                item.price
              }}
            </v-card>
          </v-img>
          <v-list disabled>
            <v-list-item-group>
              <v-list-item v-for="(bullet, i) in item.bullet" :key="i">
                <v-list-item-icon>
                  <v-icon small color="green">mdi-check</v-icon>
                </v-list-item-icon>
                <v-list-item-content>
                  {{ bullet[0] }}
                </v-list-item-content>
              </v-list-item>
            </v-list-item-group>
          </v-list>
        </v-card>
      </v-row>
    </v-row>
  </v-container>
</template>

<script>
export default {
  name: "home-pricing",

  data: () => {
    return ({
      img: `${require('~/assets/images/home-prizing.jpg')}`,
      items: [
        {
          id: 1,
          img: `${require('~/assets/images/background.jpg')}`,
          title: 'Strony internetowe',
          price: 'od 800 PLN netto',
          bullet: [
            ['strona tworzona na podstawie originalnego szablonu'],
            ['system zarządzania treścią (CMS)'],
            ['optymalizacja treści, aby była bardziej przyjazna dla wyszukiwarek (SEO)'],
            ['Rejestracja domeny'],
            ['Hosting']
          ],
        },
        {
          id: 2,
          img: `${require('~/assets/images/background.jpg')}`,
          title: 'Twoja Kancelaria',
          price: 'od 800 PLN netto',
          bullet: [['strona tworzona na podstawie originalnego szablonu'],
            ['system zarządzania treścią (CMS)'],
            ['optymalizacja treści, aby była bardziej przyjazna dla wyszukiwarek (SEO)'],
            ['Rejestracja domeny'],
            ['Hosting']],
        },
        {
          id: 3,
          img: `${require('~/assets/images/background.jpg')}`,
          title: 'Projekty indywidualne',
          price: 'od 800 PLN netto',
          bullet: [['strona tworzona na podstawie originalnego szablonu'],
            ['system zarządzania treścią (CMS)'],
            ['optymalizacja treści, aby była bardziej przyjazna dla wyszukiwarek (SEO)'],
            ['Rejestracja domeny'],
            ['Hosting']],
        },
      ],
    });
  },
};
</script>

<style scoped>
.price {
  position: absolute;
  left: 60px;
  background-color: crimson;
}
</style>
