<template>
  <v-container>
      <v-row class="my-4">
           <v-row class="d-flex justify-space-between mb-2">
          <p>Sprawdź przykładowy cennik naszych usług. Aby poznać szczegóły zapraszamy do <nuxt-link to="/home-contact">kontaktu.</nuxt-link></p>
        </v-row>
         <v-row>
          <v-card id="card" v-for="item in items" :key="item.id" width="300px" class="mx-auto my-8">
            <v-img :src="item.img" class="white--text align-center" gradient="to bottom, rgba(0,0,0,.1), rgba(0,0,0,.5)"
                   height="">
            <v-card-title>{{ item.title }}</v-card-title>
            <v-card-subtitle class="title white--text my-2">{{ item.price }}</v-card-subtitle>
            </v-img>
             <v-list>
              <v-list-item-group>
                <v-list-item v-for="(bullet, i) in item.bullet" :key="i">
                 <v-list-item-content >
                   {{bullet[0]}}
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
          price: 'od 800 PLN NETTO',
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
          price: 'od 800 PLN NETTO',
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
          price: 'od 800 PLN NETTO',
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

</style>
